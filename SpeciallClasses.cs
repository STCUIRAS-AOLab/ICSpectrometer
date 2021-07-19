using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using AO_Lib;
using TIS.Imaging;
using DShowLib;

namespace LDZ_Code
{
    public class ImageStyle//инициализация класса сохранения параметров изображения
    {
        public string Extension { set; get; }
        public int Quality { set; get; }
        public string Directory { set; get; }
    }

    public class Queue_with_change<T>
    {
        private readonly Queue<T> queue = new Queue<T>();
        public event EventHandler Changed;
        public event EventHandler Queid;

        protected virtual void OnChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnQuied()
        {
            if (Queid != null) Queid(this, EventArgs.Empty);
        }
        public virtual void Enqueue(T item)
        {
            queue.Enqueue(item);
            OnChanged();
            OnQuied();
        }
        public int Count { get { return queue.Count; } }

        public virtual T Dequeue()
        {
            T item = queue.Dequeue();
            OnChanged();
            return item;
        }
    }

    public class ExpCurve
    {
        int StartWL = 0;
        int FinishWL = 0;
        public static void GetNiceCurve(int pRealMin, int pRealMax, int pStWL, int pFinWL, int pStep, List<int> pWls, List<double> pExps)
        {
            /*    int pMinWL = pRealMin;
                int pMaxhWL = pRealMax;
                if (pWls.Count != pExps.Count)
                    throw new Exception("Некорректное содержимое файла для перестройки.");
                ResortValues(ref pWls, ref pExps);
                InterpolateValues(pMinWL, pMaxhWL, ref pWls, ref pExps);
                CutValues(pStWL, pFinWL, pStep, ref pWls, ref pExps);*/

        }
        public static void Get_Interpolated_WlExpCurveFromDirectory(string path,
            int pWlRealMin, int pWlRealMax,
            int pStWL, int pFinWL, int pStep,
            ref List<int> pWls, ref List<double> pExps, ref List<double> pRealExps_byRef,
            ref double pGain, ref double pFPS, ref double pExpRef)
        {
            int pMinWL = pWlRealMin;
            int pMaxhWL = pWlRealMax;
            bool UseReference_Exposure = (pExpRef == -1); //если передана экспозиция -1, то юзаем экспозичию из файла
            try
            {
                Get_WlExpCurveFromDirectory(path, ref pWls, ref pExps, ref pRealExps_byRef, ref pGain, ref pFPS, ref pExpRef);
            }
            catch
            {
                throw new Exception("Некорректное содержимое файла для перестройки.");
            }
            //  if (UseReference_Exposure) pExps = RealExps_byRef;
            ResortValues(ref pWls, ref pExps, ref pRealExps_byRef); //отсортируем
            InterpolateValues(pMinWL, pMaxhWL, ref pWls, ref pExps, ref pRealExps_byRef); //проинтерполируем с шагом 1
                                                                                          // ICSpec.Form1.ShowStringDelegate obj = null;
                                                                                          //WriteCurveToFile(path, pWls, pExps, pGain, pFPS, ref obj, true);
            CutValues(pStWL, pFinWL, pStep, ref pWls, ref pExps, ref pRealExps_byRef);
        }
        public static void Get_WlExpCurveFromDirectory(string path,
            ref List<int> pWls, ref List<double> pExps_fromfile, ref List<double> pExps_ref,
            ref double pGain, ref double pFPS, ref double pExposure_ref)
        {
            string[] readText = File.ReadAllLines(path);
            int StartPos = 0, EndPos = 0;
            string Gain_str = "";
            string Fps_str = "";
            string Exp_reference_str = "";

            for (int i = 0; i < readText.Count(); i++)
            {
                if (readText[i].IndexOf('.') != -1)
                {
                    readText[i] = readText[i].Replace(".", ",");
                }
                if (readText[i] == "<Data>") StartPos = i + 1;
                else if (readText[i] == "</Data>") EndPos = i;
                else if (readText[i].Contains("<FPS>")) Fps_str = ServiceFunctions.Files.XML_CutFromEdges(readText[i]);
                else if (readText[i].Contains("<Gain>")) Gain_str = ServiceFunctions.Files.XML_CutFromEdges(readText[i]);
                else if (readText[i].Contains("<Reference Exposure>")) Exp_reference_str = ServiceFunctions.Files.XML_CutFromEdges(readText[i]);
            }
            int num = EndPos - StartPos;

            pGain = Convert.ToDouble(Gain_str.Replace('.', ','));
            pFPS = Convert.ToDouble(Fps_str.Replace('.', ','));
            if (pExposure_ref == -1) pExposure_ref = Convert.ToDouble(Exp_reference_str.Replace('.', ','));

            for (int i = StartPos; i < EndPos; i++)
            {
                pWls.Add(Convert.ToInt32(readText[i].Substring(0, readText[i].IndexOf('\t'))));
                //2nd num - real exp
                string Real_exp_currrent = readText[i].Substring(readText[i].IndexOf('\t') + 1, readText[i].LastIndexOf('\t') - readText[i].IndexOf('\t') - 1);
                pExps_fromfile.Add(Convert.ToDouble(Real_exp_currrent.Replace('.', ',')));
                //3d num - Multiplier
                string Ref_exp_currrent = readText[i].Substring(readText[i].LastIndexOf('\t') + 1);
                pExps_ref.Add(pExposure_ref * Convert.ToDouble(Ref_exp_currrent.Replace('.', ',')));
            }
        }
        private static void ResortValues(ref List<int> ppWls, ref List<double> ppExps, ref List<double> ppExpsRef)//сортивка методом вставок
        {
            for (int i = 1; i < ppWls.Count; i++)
            {
                int cur = ppWls[i];
                double cur2 = ppExps[i];
                double cur_refexp = ppExpsRef[i];
                int j = i;
                while (j > 0 && cur < ppWls[j - 1])
                {
                    ppWls[j] = ppWls[j - 1];
                    ppExps[j] = ppExps[j - 1];
                    ppExpsRef[j] = ppExpsRef[j - 1];
                    j--;
                }
                ppWls[j] = cur;
                ppExps[j] = cur2;
                ppExpsRef[j] = cur_refexp;
            }
        }
        private static void InterpolateValues(int ppMinWL, int ppMaxWL, ref List<int> ppWls, ref List<double> ppExps, ref List<double> ppExps_ref)
        {
            int ValuesEdded = 0;
            bool StartWL_exists = false;
            bool FinishWL_exists = false;
            if (ppWls[0] != ppMinWL)
            {
                if ((ppWls[0] < ppMinWL) && (ppWls.IndexOf(ppMinWL) == -1))
                {

                    double expcur = ppExps[1] - ((ppExps[1] - ppExps[0]) / (double)(ppWls[1] - ppWls[0])) * ((double)(ppWls[1] - ppMinWL));
                    double expcur2 = ppExps_ref[1] - ((ppExps_ref[1] - ppExps_ref[0]) / (double)(ppWls[1] - ppWls[0])) * ((double)(ppWls[1] - ppMinWL));
                    ppWls[0] = ppMinWL;
                    ppExps[0] = expcur;
                    ppExps_ref[0] = expcur2;
                }
                else if (ppWls[0] > ppMinWL)
                {
                    double expcur = ppExps[1] - ((ppExps[1] - ppExps[0]) / (double)(ppWls[1] - ppWls[0])) * ((double)(ppWls[1] - ppMinWL));
                    double expcur2 = ppExps_ref[1] - ((ppExps_ref[1] - ppExps_ref[0]) / (double)(ppWls[1] - ppWls[0])) * ((double)(ppWls[1] - ppMinWL));
                    ppWls.Insert(0, ppMinWL);
                    ppExps.Insert(0, expcur);
                    ppExps_ref.Insert(0, expcur2);
                }
            }
            else
            {
                StartWL_exists = true;
            }
            if (ppWls[ppWls.Count - 1] != ppMaxWL)
            {
                if (ppWls[ppWls.Count - 1] < ppMaxWL)
                {
                    int a = ppWls.Count - 1;
                    int b = ppWls.Count - 2;
                    double expcur = ppExps[a] - ((ppExps[a] - ppExps[b]) / (double)(ppWls[a] - ppWls[b])) * ((double)(ppWls[a] - ppMaxWL));
                    double expcur2 = ppExps_ref[a] - ((ppExps_ref[a] - ppExps_ref[b]) / (double)(ppWls[a] - ppWls[b])) * ((double)(ppWls[a] - ppMaxWL));
                    ppWls.Add(ppMaxWL);
                    ppExps.Add(expcur);
                    ppExps_ref.Add(expcur2);
                }
                else if ((ppWls[ppWls.Count - 1] > ppMaxWL) && ((ppWls.IndexOf(ppMaxWL) == -1)))
                {
                    int a = ppWls.Count - 1;
                    int b = ppWls.Count - 2;
                    double expcur = ppExps[a] - ((ppExps[a] - ppExps[b]) / (double)(ppWls[a] - ppWls[b])) * ((double)(ppWls[a] - ppMaxWL));
                    double expcur2 = ppExps_ref[a] - ((ppExps_ref[a] - ppExps_ref[b]) / (double)(ppWls[a] - ppWls[b])) * ((double)(ppWls[a] - ppMaxWL));
                    ppWls[a] = ppMaxWL;
                    ppExps[a] = expcur;
                    ppExps_ref[a] = expcur2;
                }
            }
            else
            {
                FinishWL_exists = true;
            }
            int[] IndWithWLs = new int[ppWls.Count];
            IndWithWLs[0] = ppWls[0];
            int NextWL = 0;
            IndWithWLs[IndWithWLs.Count() - 1] = ppWls[ppWls.Count - 1];

            for (int i = 0; i < ppWls.Count - 1; i++)
            {
                NextWL++;
                int NeedToAdd = ppWls[i + 1] - ppWls[i] - 1;
                for (int j = NeedToAdd; j >= 1; j--)
                {
                    ppWls.Insert(i + 1, ppWls[i] + j);
                    ppExps.Insert(i + 1, ppExps[i + 1 + NeedToAdd - j] + (ppExps[i] - ppExps[i + 1 + NeedToAdd - j]) * ((double)(NeedToAdd + 1 - j) / (double)(NeedToAdd + 1)));
                    ppExps_ref.Insert(i + 1, ppExps_ref[i + 1 + NeedToAdd - j] + (ppExps_ref[i] - ppExps_ref[i + 1 + NeedToAdd - j]) * ((double)(NeedToAdd + 1 - j) / (double)(NeedToAdd + 1)));
                }
                IndWithWLs[NextWL] = i + 1 + NeedToAdd;
                i += NeedToAdd;
            }
        }
        private static void CutValues(int ppStartWL, int ppFinishWL, int ppStep, ref List<int> ppWls, ref List<double> ppExps, ref List<double> ppExpsRef)
        {

            int ValuesAdded = 0;
            List<int> NewWls = new List<int>();
            List<double> NewExps = new List<double>();
            List<double> NewExpsRef = new List<double>();
            List<int> IndToAdd = new List<int>();
            bool EndWlNeed = (((ppFinishWL - ppStartWL) % ppStep) == 0) ? false : true;

            for (int i = 0; i < ppWls.Count; i++)
            {
                if ((ppWls[i] == ppStartWL + ppStep * ValuesAdded) || (ppWls[i] == ppFinishWL))
                {
                    IndToAdd.Add(i);
                    ValuesAdded++;
                    if (ppWls[i] == ppFinishWL) break;
                }
            }
            for (int i = 0; i < IndToAdd.Count; i++)
            {
                NewWls.Add(ppWls[IndToAdd[i]]);
                NewExps.Add(ICSpec.Form1.PerfectRounding(ppExps[IndToAdd[i]], 5));
                NewExpsRef.Add(ICSpec.Form1.PerfectRounding(ppExpsRef[IndToAdd[i]], 5));
            }
            ppWls = NewWls;
            ppExps = NewExps;
            ppExpsRef = NewExpsRef;
        }

        public static void CreateCurve(ref System.ComponentModel.BackgroundWorker pBackWorker, ref System.ComponentModel.DoWorkEventArgs pE,
            ref TIS.Imaging.ICImagingControl IC, ref TIS.Imaging.VCDHelpers.VCDSimpleProperty VSExp, ref TIS.Imaging.VCDAbsoluteValueProperty pAbsVal,
            int pWlRealMin, int pWlRealMax, int pStWL, int pFinWL, int pStep, int pCurGain, double pFPS,
            ICSpec.Form1.ShowStringDelegate pMesShowDel, bool pwasAutomation, AO_Devices.AO_Filter pFilter)
        {
            try
            {
                if (pBackWorker.CancellationPending) { pE.Cancel = true; return; }
                int minSecondsToWait = 1;
                double FPSminSecondsToWait = 10.0 / pFPS;
                double SecondsToWait = (minSecondsToWait < FPSminSecondsToWait) ? FPSminSecondsToWait : minSecondsToWait;

                List<int> Wls = new List<int>();
                List<double> Exps = new List<double>();
                // bool EndWlNeed = (((pFinWL - pStWL) % pStep) == 0) ? false : true;
                int TotalWls = (pFinWL - pStWL) / pStep;
                for (int i = 0; i < TotalWls; i++)
                {
                    Wls.Add(pStWL + i * pStep);
                }
                Wls.Add(pFinWL);
                if (pBackWorker.CancellationPending) { pE.Cancel = true; return; }
                string ChangeVCDID = TIS.Imaging.VCDIDs.VCDID_Exposure;
                if (!VSExp.AutoAvailable(ChangeVCDID))
                {
                    throw new Exception("Автоподстройка невозможна из-за отсутствия функции автоподстройки на данной камере");
                }
                else
                {
                    VSExp.Automation[ChangeVCDID] = true;
                }
                if (pBackWorker.CancellationPending) { pE.Cancel = true; return; }
                pFilter.Set_Wl(Wls[0]);
                System.Threading.Thread.Sleep(1000);
                System.Diagnostics.Stopwatch stw = new System.Diagnostics.Stopwatch();
                double CurrentExp = pAbsVal.Value;
                for (int i = 0; i < Wls.Count(); i++)
                {
                    if (pBackWorker.CancellationPending) { pE.Cancel = true; return; }
                    string message = "Начальная экспозиция для длины волны " + Wls[i].ToString() + ": " + CurrentExp.ToString();
                    pMesShowDel.Invoke(message);
                    //LB.BeginInvoke(pMesShowDel, new object[] { message });
                    pFilter.Set_Wl(Wls[i]);
                    stw.Restart();
                    //  System.Threading.Thread.Sleep()
                    bool TimeNotElapsed = true;
                    while (TimeNotElapsed)
                    {
                        if (pBackWorker.CancellationPending) { pE.Cancel = true; return; }
                        if (pAbsVal.Value != CurrentExp)
                        {
                            CurrentExp = pAbsVal.Value;
                            stw.Restart();
                        }
                        else if (stw.Elapsed.TotalSeconds > SecondsToWait)
                        {
                            TimeNotElapsed = false;
                            Exps.Add(ICSpec.Form1.PerfectRounding(CurrentExp, 7));
                            pBackWorker.ReportProgress(Wls[i]);
                            string message2 = "Подобрана экспозиция для длины волны " + Wls[i].ToString() + ": " + Exps[i].ToString();
                            pMesShowDel.Invoke(message2);
                            // LB.BeginInvoke(pMesShowDel, new object[] { message2 });
                        }
                    }
                }
                string filename = IC.DeviceCurrent.Name.ToString() + "_" + ICSpec.Form1.GetFullDateString() + ".expcurv";
                string message3 = "Подбор экспозиций завершен! Запись в файл " + filename;
                pMesShowDel.Invoke(message3);
                if (pBackWorker.CancellationPending) { pE.Cancel = true; return; }
                //  LB.BeginInvoke(pMesShowDel, new object[] { message3 });
                //WriteCurveToFile(filename, Wls, Exps, pCurGain, pFPS, ref pMesShowDel, false);

            }
            catch (Exception exc)
            {
                string ChangeVCDID = TIS.Imaging.VCDIDs.VCDID_Exposure;
                if (pwasAutomation) VSExp.Automation[ChangeVCDID] = pwasAutomation;
                throw exc;
            }
        }
        public static void WriteCurveToFile(string filename, List<int> pWls, List<double> pExps, double ppGain, double ppFPS, ref ICSpec.Form1.ShowStringDelegate ppMesShowDel, bool NeedEnotherExt)
        {
            try
            {
                if (NeedEnotherExt) filename = filename.Insert(filename.LastIndexOf('.') + 1, "f");
                List<string> ListToWrite = new List<string>();
                ListToWrite.Add("<Gain>");
                string sGain = (ppGain == -1) ? "user defined" : ppGain.ToString().Replace(',', '.');
                ListToWrite.Add(sGain);
                ListToWrite.Add("</Gain>");
                ListToWrite.Add("<FPS>");
                string sFPS = (ppFPS == -1) ? "user defined" : ppFPS.ToString().Replace(',', '.');
                ListToWrite.Add(sFPS);
                ListToWrite.Add("</FPS>");
                ListToWrite.Add("<Data>");
                for (int i = 0; i < pWls.Count(); i++)
                {
                    ListToWrite.Add(pWls[i].ToString() + "\t" + pExps[i].ToString().Replace(',', '.'));
                    string message3 = "Длина волны " + pWls[i].ToString() + " со значением экспозиции " + pExps[i].ToString().Replace(',', '.') + " сохранена";
                    if (ppMesShowDel != null) ppMesShowDel.Invoke(message3);
                }
                ListToWrite.Add("</Data>");
                File.WriteAllLines(filename, ListToWrite, Encoding.UTF8);
                string message = "Запись в файл " + filename + " завершена успешно!";
                if (ppMesShowDel != null) ppMesShowDel.Invoke(message);
            }
            catch (Exception exc)
            {
                string message = "Запись в файл " + filename + " завершена с ошибкой.";
                if (ppMesShowDel != null) ppMesShowDel.Invoke(message);
                throw exc;
            }
        }
    }

    public static class Files
    {
        public static string OpenDirectory()
        {
            string result = null;
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                result = FBD.SelectedPath;
            }
            if (System.IO.Directory.Exists(result)) return result;
            else return null;
        }
        public static string CreateFilter_ForFileDialog(bool pAllowAllExtensions, params string[] format)
        {
            string result = "";
            for (int i = 0; i < format.Count(); i++)
            {
                if (format[i].IndexOf('.') != -1)
                    format[i] = format[i].Remove(format[i].IndexOf('.'), 1);
                if (format[i].IndexOf('*') != -1)
                    format[i] = format[i].Remove(format[i].IndexOf('.'), 1);
                format[i] = String.Format(format[i].ToUpper() + " Files(*.{0}) |*.{0}|", format[i].ToLower());
            }
            for (int i = 0; i < format.Count(); i++)
            {
                result += format[i];
            }
            if (pAllowAllExtensions) result += "All files(*.*) |*.*";
            else { result = result.Substring(0, result.Length - 1); }
            return result;
        }
        public static string OpenSaveDialog(bool AllowAllExtensions, params string[] extensions)
        {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.Filter = CreateFilter_ForFileDialog(AllowAllExtensions, extensions);
            SFD.AddExtension = true;
            if (SFD.ShowDialog() == DialogResult.Cancel) return null;
            // получаем выбранный файл
            string filename = SFD.FileName;
            // сохраняем текст в файл
            return filename;
        }
        public static string[] OpenFiles(string Title, bool AllowAllExtensions, bool ismultiselect, params string[] extensions)
        {
            string[] result = null;
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Filter = CreateFilter_ForFileDialog(true, extensions);
            OFD.Title = Title;
            OFD.Multiselect = ismultiselect;

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                result = OFD.FileNames;
            }
            return result;
        }
        public static List<string> FindFiles_byExstension(string path, string ext)
        {
            List<string> result = new List<string>();
            if (ext.Substring(0, 1) != ".") return result;

            var allFilenames = Directory.EnumerateFiles(path).Select(p => Path.GetFileName(p));

            // Get all filenames that have a .txt extension, excluding the extension
            var candidates = allFilenames.Where(fn => Path.GetExtension(fn) == ext)
                                         .Select(fn => Path.Combine(path, fn));
            result = new List<string>(candidates);
            return result;
        }

        public static List<string> Read_txt(string path)
        {
            string[] AllLines = File.ReadAllLines(path);
            List<string> result = new List<string>(AllLines);
            return result;
        }
        public static bool Write_txt(string path, List<string> data)
        {
            bool result = false;
            try
            {
                string[] AllLines = new string[data.Count];
                data.CopyTo(AllLines);
                File.WriteAllLines(path, AllLines);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
        public static List<int> List_Sort_viaLastNumber(ref List<string> FullImageWays, string ext)
        {
            List<int> result = new List<int>();

            List<string> data_res = new List<string>(FullImageWays.Select(fn => Path.GetFileNameWithoutExtension(fn)));
            result = new List<int>(data_res.Select(fn => Get_LastNumber_from_name(fn)));

            result.Sort(delegate (int wl1, int wl2) { return wl1.CompareTo(wl2); });
            FullImageWays.Sort(delegate (string wl1, string wl2)
            {
                return (Get_LastNumber_from_name(Path.GetFileNameWithoutExtension(wl1)))
             .CompareTo(Get_LastNumber_from_name(Path.GetFileNameWithoutExtension(wl2)));
            });
            return result;
        }
        public static int Get_LastNumber_from_name(string a)
        {
            int res = -1;
            res = Convert.ToInt32(a.Substring(a.LastIndexOf('_') + 1));
            return res;
        }
        public static void Get_WLData_byKnownCountofNumbers(int countofnumbers, string[] AllStrings,
            out float[] pWls, out float[] pHzs, out float[] pCoefs)
        {

            float CurWl = 0, CurHz = 0, CurCoef = 0;
            List<float> dWls = new List<float>();
            List<float> dHzs = new List<float>();
            List<float> dCoefs = new List<float>();

            float[] Params = new float[countofnumbers];
            for (int i = 0; i < AllStrings.Length; ++i)
            {
                try
                {
                    Get_WLData_fromDevString(AllStrings[i], countofnumbers, Params);
                    dHzs.Add(Params[0]); dWls.Add(Params[1]); dCoefs.Add(Params[2]);
                }
                catch
                {
                    continue;
                }
            }
            dWls.Reverse();
            dHzs.Reverse();
            dCoefs.Reverse();

            pWls = dWls.ToArray();
            pHzs = dHzs.ToArray();
            pCoefs = dCoefs.ToArray();
        }
        public static void Get_WLData_fromDevString(string datastring, int NumberOfParamsInString, float[] pPars)
        {
            int startindex = 0; bool startfound = false;
            int finishindex = 0; bool finishfound = false;
            List<float> datavalues = new List<float>();
            for (int i = 0; i < datastring.Length; i++)
            {
                if ((datastring[i] != ' ') && ((Char.IsDigit(datastring[i])) || (datastring[i] == '.') || (datastring[i] == ',') || (datastring[i] == '-')))
                {
                    if (startfound)
                    {
                        finishindex++;
                    }
                    else
                    {
                        startindex = i;
                        startfound = true;
                    }
                }
                else
                {
                    if (startfound)
                    {
                        finishindex = i;
                        finishfound = true;
                    }
                }
                if (startfound && finishfound)
                {
                    string data = datastring.Substring(startindex, finishindex - startindex);
                    datavalues.Add((float)Convert.ToDouble(data.Replace('.', ',')));
                    startfound = finishfound = false;
                }
            }
            if (startfound && !finishfound)
            {
                string data = datastring.Substring(startindex);
                datavalues.Add((float)Convert.ToDouble(data.Replace('.', ',')));
            }
            for (int i = 0; i < NumberOfParamsInString; i++) { pPars[i] = datavalues[i]; }
        }
    }

    public class ServiceFunctions
    {
        public static class Files
        {
            public static string OpenDirectory()
            {
                string result = null;
                FolderBrowserDialog FBD = new FolderBrowserDialog();
                if (FBD.ShowDialog() == DialogResult.OK)
                {
                    result = FBD.SelectedPath;
                }
                if (System.IO.Directory.Exists(result)) return result;
                else return null;
            }
            public static string CreateFilter_ForFileDialog(bool pAllowAllExtensions, params string[] format)
            {
                string result = "";
                for (int i = 0; i < format.Count(); i++)
                {
                    if (format[i].IndexOf('.') != -1)
                        format[i] = format[i].Remove(format[i].IndexOf('.'), 1);
                    format[i] = String.Format(format[i].ToUpper() + " Files(*.{0}) |*.{0}|", format[i].ToLower());
                }
                for (int i = 0; i < format.Count(); i++)
                {
                    result += format[i];
                }
                if (pAllowAllExtensions) result += "All files(*.*) |*.*";
                else { result = result.Substring(0, result.Length - 1); }
                return result;
            }
            public static string OpenSaveDialog(bool AllowAllExtensions, params string[] extensions)
            {
                SaveFileDialog SFD = new SaveFileDialog();
                SFD.Filter = CreateFilter_ForFileDialog(AllowAllExtensions, extensions);
                SFD.AddExtension = true;
                if (SFD.ShowDialog() == DialogResult.Cancel) return null;
                // получаем выбранный файл
                string filename = SFD.FileName;
                // сохраняем текст в файл
                return filename;
            }
            public static string[] OpenFiles()
            {
                string[] result = null;
                OpenFileDialog OFD = new OpenFileDialog();
                OFD.Filter = "Spectrum TXT Files(*.txt)|*.txt|All Files|*.*";
                OFD.Title = "Select Spectrum Files";
                OFD.Multiselect = true;

                if (OFD.ShowDialog() == DialogResult.OK)
                {
                    result = OFD.FileNames;
                }
                return result;
            }
            public static List<string> FindFiles_byExstension(string path, string ext)
            {
                List<string> result = new List<string>();
                if (ext.Substring(0, 1) != ".") return result;

                var allFilenames = Directory.EnumerateFiles(path).Select(p => Path.GetFileName(p));

                // Get all filenames that have a .txt extension, excluding the extension
                var candidates = allFilenames.Where(fn => Path.GetExtension(fn) == ext)
                                             .Select(fn => Path.Combine(path, fn));
                result = new List<string>(candidates);
                return result;
            }

            public static List<string> Read_txt(string path)
            {
                string[] AllLines = File.ReadAllLines(path);
                List<string> result = new List<string>(AllLines);
                return result;
            }
            public static bool Write_txt(string path, List<string> data)
            {
                bool result = false;
                try
                {
                    string[] AllLines = new string[data.Count];
                    data.CopyTo(AllLines);
                    File.WriteAllLines(path, AllLines);
                    result = true;
                }
                catch
                {
                    result = false;
                }
                return result;
            }
            public static string XML_CutFromEdges(string target)
            {
                try
                {
                    int startind = target.IndexOf('>');
                    int finishind = target.LastIndexOf('<');
                    string val = target.Substring(startind + 1, finishind - startind - 1);
                    return val;
                }
                catch
                {
                    return "1";
                }
            }
            public static List<int> List_Sort_viaLastNumber(ref List<string> FullImageWays, string ext)
            {
                List<int> result = new List<int>();

                List<string> data_res = new List<string>(FullImageWays.Select(fn => Path.GetFileNameWithoutExtension(fn)));
                result = new List<int>(data_res.Select(fn => Get_LastNumber_from_name(fn)));

                result.Sort(delegate (int wl1, int wl2) { return wl1.CompareTo(wl2); });
                FullImageWays.Sort(delegate (string wl1, string wl2)
                {
                    return (Get_LastNumber_from_name(Path.GetFileNameWithoutExtension(wl1)))
                 .CompareTo(Get_LastNumber_from_name(Path.GetFileNameWithoutExtension(wl2)));
                });
                return result;
            }
            public static int Get_LastNumber_from_name(string a)
            {
                int res = -1;
                res = Convert.ToInt32(a.Substring(a.LastIndexOf('_') + 1));
                return res;
            }
            public static void Get_WLData_byKnownCountofNumbers(int countofnumbers, string[] AllStrings,
                out float[] pWls, out float[] pHzs, out float[] pCoefs)
            {

                float CurWl = 0, CurHz = 0, CurCoef = 0;
                List<float> dWls = new List<float>();
                List<float> dHzs = new List<float>();
                List<float> dCoefs = new List<float>();

                float[] Params = new float[countofnumbers];
                for (int i = 0; i < AllStrings.Length; ++i)
                {
                    try
                    {
                        Get_WLData_fromDevString(AllStrings[i], countofnumbers, Params);
                        dHzs.Add(Params[0]); dWls.Add(Params[1]); dCoefs.Add(Params[2]);
                    }
                    catch
                    {
                        continue;
                    }
                }
                dWls.Reverse();
                dHzs.Reverse();
                dCoefs.Reverse();

                pWls = dWls.ToArray();
                pHzs = dHzs.ToArray();
                pCoefs = dCoefs.ToArray();
            }
            public static void Get_WLData_fromDevString(string datastring, int NumberOfParamsInString, float[] pPars)
            {
                int startindex = 0; bool startfound = false;
                int finishindex = 0; bool finishfound = false;
                List<float> datavalues = new List<float>();

                //Оказывается, не во всех системах все читается корректно. Делаем проверку
                bool isDotNeeded = false;
                double datastr = 0;
                try { datastr = Convert.ToDouble(("0.1155").Replace('.', ',')); isDotNeeded = false; }
                catch { isDotNeeded = true; }
                try { datastr = Convert.ToDouble(("0,1155").Replace(',', '.')); isDotNeeded = true; }
                catch { isDotNeeded = false; }


                for (int i = 0; i < datastring.Length; i++)
                {
                    if ((datastring[i] != ' ') && ((Char.IsDigit(datastring[i])) || (datastring[i] == '.') || (datastring[i] == ',') || (datastring[i] == '-')))
                    {
                        if (startfound)
                        {
                            finishindex++;
                        }
                        else
                        {
                            startindex = i;
                            startfound = true;
                        }
                    }
                    else
                    {
                        if (startfound)
                        {
                            finishindex = i;
                            finishfound = true;
                        }
                    }
                    if (startfound && finishfound)
                    {
                        string data = datastring.Substring(startindex, finishindex - startindex);


                        datavalues.Add((float)Convert.ToDouble(data.Replace('.', ',')));
                        startfound = finishfound = false;
                    }
                }
                if (startfound && !finishfound)
                {
                    string data = datastring.Substring(startindex);
                    if (isDotNeeded)
                        datavalues.Add((float)Convert.ToDouble(data.Replace(',', '.')));
                    else
                        datavalues.Add((float)Convert.ToDouble(data.Replace('.', ',')));
                }
                for (int i = 0; i < NumberOfParamsInString; i++) { pPars[i] = datavalues[i]; }
            }
        }
        public static class Math
        {
            public static class EmguCV
            {
                /*  public static double CalculateSpecSum(Matrix<float> matrix)
                  {
                      int cx = matrix.Cols / 2;
                      int cy = matrix.Rows / 2;
                      double sum = 0;
                      int sqradius = (int)(1f * cy);
                      int startposX = matrix.Cols / 2 - sqradius;
                      int finishposX = matrix.Cols / 2 + sqradius;
                      int startposY = matrix.Rows / 2 - sqradius;
                      int finishposY = matrix.Rows / 2 + sqradius;
                      for (int i = startposY; i < finishposY; i++)
                      {
                          for (int j = startposX; j < finishposX; j++)
                          {
                              sum += Math.Abs(matrix[i, j] * (Math.Sqrt(Math.Pow(i - cy, 2.0) + Math.Pow(j - cx, 2.0))));
                          }
                      }
                      return sum;
                  }
                  public static void SwitchQuadrants(ref Matrix<float> matrix)
                  {
                      int cx = matrix.Cols / 2;
                      int cy = matrix.Rows / 2;

                      Matrix<float> q0 = matrix.GetSubRect(new Rectangle(0, 0, cx, cy));
                      Matrix<float> q1 = matrix.GetSubRect(new Rectangle(cx, 0, cx, cy));
                      Matrix<float> q2 = matrix.GetSubRect(new Rectangle(0, cy, cx, cy));
                      Matrix<float> q3 = matrix.GetSubRect(new Rectangle(cx, cy, cx, cy));
                      Matrix<float> tmp = new Matrix<float>(q0.Size);

                      q0.CopyTo(tmp);
                      q3.CopyTo(q0);
                      tmp.CopyTo(q3);
                      q1.CopyTo(tmp);
                      q2.CopyTo(q1);
                      tmp.CopyTo(q2);
                  }
                  public static Matrix<float> GetDftMagnitude(Matrix<float> fftData)
                  {
                      //MassiveForChanels
                      Matrix<float>[] outMain = new Matrix<float>[2];
                      //The Real part of the Fourier Transform
                      Matrix<float> outReal = new Matrix<float>(fftData.Size);
                      //The imaginary part of the Fourier Transform
                      Matrix<float> outIm = new Matrix<float>(fftData.Size);

                      var outr = fftData.Split();
                      outReal = outr[0];
                      outIm = outr[1];
                      float summ = 0;
                      for (int i = 0; i < outIm.Rows; i++)
                      {
                          for (int j = 0; j < outIm.Cols; j++)
                          {
                              summ += outIm[i, j];
                          }
                      }
                      CvInvoke.Pow(outReal, 2.0, outReal);
                      CvInvoke.Pow(outIm, 2.0, outIm);

                      CvInvoke.Add(outReal, outIm, outReal);
                      CvInvoke.Pow(outReal, 0.5, outReal);

                      int cols = outReal.Cols;
                      int rows = outReal.Rows;
                      Matrix<float> OnesMatrix = new Matrix<float>(rows, cols, 1);
                      OnesMatrix.SetValue(1);
                      // CvInvoke.Add(outReal, OnesMatrix, outReal); // 1 + Mag
                      //  CvInvoke.Log(outReal, outReal); // log(1 + Mag)            

                      return outReal;
                  }
                  public static Matrix<float> GetDftMagnitude(UMat fftData)
                  {
                      //MassiveForChanels
                      Matrix<float> fftData2 = new Matrix<float>(fftData.Rows, fftData.Cols);
                      fftData.CopyTo(fftData2);
                      Matrix<float>[] outMain = new Matrix<float>[2];
                      //The Real part of the Fourier Transform
                      Matrix<float> outReal = new Matrix<float>(fftData.Size);
                      //The imaginary part of the Fourier Transform
                      Matrix<float> outIm = new Matrix<float>(fftData.Size);

                      var outr = fftData2.Split();
                      outReal = outr[0];
                      outIm = outr[1];
                      float summ = 0;
                      for (int i = 0; i < outIm.Rows; i++)
                      {
                          for (int j = 0; j < outIm.Cols; j++)
                          {
                              summ += outIm[i, j];
                          }
                      }
                      CvInvoke.Pow(outReal, 2.0, outReal);
                      CvInvoke.Pow(outIm, 2.0, outIm);

                      CvInvoke.Add(outReal, outIm, outReal);
                      CvInvoke.Pow(outReal, 0.5, outReal);

                      int cols = outReal.Cols;
                      int rows = outReal.Rows;
                      Matrix<float> OnesMatrix = new Matrix<float>(rows, cols, 1);
                      OnesMatrix.SetValue(1);
                      CvInvoke.Add(outReal, OnesMatrix, outReal); // 1 + Mag
                      CvInvoke.Log(outReal, outReal); // log(1 + Mag)            

                      return outReal;
                  }
                  public static Bitmap Matrix2Bitmap(Matrix<float> matrix)
                  {
                      CvInvoke.Normalize(matrix, matrix, 0.0, 255.0, Emgu.CV.CvEnum.NormType.MinMax);

                      Image<Gray, float> image = new Image<Gray, float>(matrix.Size);
                      matrix.CopyTo(image);

                      return image.ToBitmap();
                  }*/
            }
            public static List<PointF> Interpolate_curv(float[] Wls, float[] Hzs)
            {
                List<PointF> result = new List<PointF>();
                int count_of_gaps = Wls.Count() - 1;

                for (int i = 0; i < count_of_gaps; i++)
                {
                    result.Add(new PointF(Wls[i], Hzs[i]));
                    float count_of_wls_to_restruct = (Wls[i + 1] - Wls[i] - 1);
                    for (int j = 1; j <= count_of_wls_to_restruct; j++)
                    {
                        float new_hz_val = Hzs[i] + j * ((float)(Hzs[i + 1] - Hzs[i])) / (Wls[i + 1] - Wls[i]);// = (x- Hzs[i] )/ (float)j;
                        result.Add(new PointF(Wls[i] + j, new_hz_val));
                    }
                }
                result.Add(new PointF(Wls[count_of_gaps], Hzs[count_of_gaps]));
                return result;
            }
            public static void Interpolate_curv(ref float[] Wls, ref float[] Hzs)
            {
                List<PointF> result = new List<PointF>();
                int count_of_gaps = Wls.Count() - 1;

                for (int i = 0; i < count_of_gaps; i++)
                {
                    result.Add(new PointF(Wls[i], Hzs[i]));
                    float count_of_wls_to_restruct = (Wls[i + 1] - Wls[i] - 1);
                    for (int j = 1; j <= count_of_wls_to_restruct; j++)
                    {
                        float new_hz_val = Hzs[i] + j * ((float)(Hzs[i + 1] - Hzs[i])) / (Wls[i + 1] - Wls[i]);// = (x- Hzs[i] )/ (float)j;
                        result.Add(new PointF(Wls[i] + j, new_hz_val));
                    }
                }
                result.Add(new PointF(Wls[count_of_gaps], Hzs[count_of_gaps]));
                int data_count = result.Count();
                Wls = new float[data_count];
                Hzs = new float[data_count];
                for (int i = 0; i < data_count; i++)
                {
                    Wls[i] = result[i].X;
                    Hzs[i] = result[i].Y;
                }
            }
            public static double Interpolate_value(double x1, double y1, double x2, double y2, double x_tofind)
            {
                return ((y2 - y1) / (x2 - x1)) * (x_tofind - x1) + y1;
            }
            public static List<PointF> Curv_normalize(List<PointF> InitCurv, float NormValue)
            {
                List<PointF> result = new List<PointF>();
                float MaxValue = 0;
                for (int i = 0; i < InitCurv.Count; i++)
                {
                    if (MaxValue < InitCurv[i].Y) MaxValue = InitCurv[i].Y;
                }
                for (int i = 0; i < InitCurv.Count; i++)
                {
                    result.Add(new PointF(InitCurv[i].X, (InitCurv[i].Y * NormValue / MaxValue)));
                }
                return result;
            }
        }
        public static class Controls
        {
            public static class ZGraph
            {
                /*  public static void Spectrum_Add(ZedGraph.ZedGraphControl pZGraph, List<PointF> pSpectrum, string name, Color color, SymbolType sType,
                      System.Drawing.Drawing2D.DashStyle LineStyle, bool Initial_Visability)
                  {
                      // Получим панель для рисования
                      GraphPane pane = pZGraph.GraphPane;

                      // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
                      //  pane.CurveList.Clear();

                      // Создадим список точек
                      PointPairList list = new PointPairList();

                      // Заполняем список точек
                      int Count_ofPts = pSpectrum.Count();
                      for (int i = 0; i < Count_ofPts; i++)
                      {
                          // добавим в список точку
                          list.Add(pSpectrum[i].X, pSpectrum[i].Y);
                      }

                      // Создадим кривую с названием "Sinc", 
                      // которая будет рисоваться голубым цветом (Color.Blue),
                      // Опорные точки выделяться не будут (SymbolType.None)
                      LineItem myCurve = pane.AddCurve(name, list, color, sType);
                      myCurve.Line.Style = LineStyle;
                      myCurve.IsVisible = Initial_Visability;
                      // Вызываем метод AxisChange (), чтобы обновить данные об осях. 
                      // В противном случае на рисунке будет показана только часть графика, 
                      // которая умещается в интервалы по осям, установленные по умолчанию
                      pZGraph.AxisChange();

                      // Обновляем график
                      pZGraph.Invalidate();
                  }
                  public static void Spectrum_Replace(ZedGraph.ZedGraphControl pZGraph, List<PointF> pSpectrum, string name, Color color, SymbolType sType,
                      System.Drawing.Drawing2D.DashStyle LineStyle, bool Initial_Visability,int Position)
                  {
                      // Получим панель для рисования
                      GraphPane pane = pZGraph.GraphPane;

                      // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
                      //  pane.CurveList.Clear();

                      // Создадим список точек
                      PointPairList list = new PointPairList();

                      // Заполняем список точек
                      int Count_ofPts = pSpectrum.Count();
                      for (int i = 0; i < Count_ofPts; i++)
                      {
                          // добавим в список точку
                          list.Add(pSpectrum[i].X, pSpectrum[i].Y);
                      }

                      // Создадим кривую с названием "Sinc", 
                      // которая будет рисоваться голубым цветом (Color.Blue),
                      // Опорные точки выделяться не будут (SymbolType.None)
                      LineItem myCurve = pane.AddCurve(name, list, color, sType);
                      myCurve.Line.Style = LineStyle;
                      myCurve.IsVisible = Initial_Visability;
                      pane.CurveList[Position] = pane.CurveList[pane.CurveList.Count - 1];
                      pane.CurveList.RemoveAt(pane.CurveList.Count - 1);
                      // Вызываем метод AxisChange (), чтобы обновить данные об осях. 
                      // В противном случае на рисунке будет показана только часть графика, 
                      // которая умещается в интервалы по осям, установленные по умолчанию
                      pZGraph.AxisChange();

                      // Обновляем график
                      pZGraph.Invalidate();
                  }
                  public static void Change_Labels(ZedGraph.ZedGraphControl pZGraph, string Title, string xLabel, string yLabel)
                  {
                      GraphPane pane = pZGraph.GraphPane;
                      pane.IsFontsScaled = false;

                      pane.Title = Title;
                      pane.XAxis.Title = xLabel;
                      pane.YAxis.Title = yLabel;
                      pZGraph.AxisChange();
                      pZGraph.Invalidate();
                  }
                  public static void Change_X_Limits(ZedGraph.ZedGraphControl pZGraph, double XMin, double XMax)
                  {
                      GraphPane pane = pZGraph.GraphPane;
                      // pane.XAxis.MinAuto = false;
                      pane.XAxis.Min = XMin;
                      //pane.XAxis.MaxAuto = false;
                      pane.XAxis.Max = XMax;
                      pZGraph.AxisChange();
                      pZGraph.Invalidate();

                  }
                  public static void Change_AutoScale(ZedGraph.ZedGraphControl pZGraph, string XY, bool state)
                  {
                      GraphPane pane = pZGraph.GraphPane;
                      if (XY == "X")
                      {
                          pane.XAxis.MinAuto = state;
                          pane.XAxis.MaxAuto = state;
                      }
                      else if (XY == "Y")
                      {
                          pane.YAxis.MinAuto = state;
                          pane.YAxis.MaxAuto = state;
                      }
                      else //"XY" or another control parameter
                      {
                          pane.XAxis.MinAuto = state;
                          pane.XAxis.MaxAuto = state;
                          pane.YAxis.MinAuto = state;
                          pane.YAxis.MaxAuto = state;
                      }
                      pZGraph.AxisChange();
                      pZGraph.Invalidate();
                  }
                  public static void Change_GraphColor(ZedGraph.ZedGraphControl pZGraph, int Graph_ind, Color FinalColor)
                  {
                      GraphPane pane = pZGraph.GraphPane;
                      pane.CurveList[Graph_ind].Color = FinalColor;
                      pZGraph.Invalidate();

                  }
                  public static void Spectrum_Delete(ZedGraph.ZedGraphControl pZGraph, int index)
                  {
                      // Получим панель для рисования
                      GraphPane pane = pZGraph.GraphPane;
                      pane.CurveList.RemoveAt(index);
                      pZGraph.Invalidate();

                  }
                  public static void Spectrum_ChangeVisability(ZedGraph.ZedGraphControl pZGraph, int index, bool IsVisible)
                  {
                      // Получим панель для рисования
                      GraphPane pane = pZGraph.GraphPane;
                      pane.CurveList[index].IsVisible = IsVisible;
                      pZGraph.Invalidate();
                  }
                  public static void Spectrum_AddInvisibleBorders(ZedGraph.ZedGraphControl pZGraph, double b1, double b2)
                  {
                      GraphPane pane = pZGraph.GraphPane;

                      LineItem line1 = new LineItem(String.Empty, new[] { b1, b1 },
                      new[] { pane.YAxis.Min, pane.YAxis.Max },
                      Color.Black, SymbolType.None);
                      line1.Line.Style = System.Drawing.Drawing2D.DashStyle.Dash;
                      line1.Line.Width = 1f;
                      pane.CurveList.Add(line1);
                      pane.CurveList[pane.CurveList.Count - 1].IsVisible = false;

                      LineItem line2 = new LineItem(String.Empty, new[] { b2, b2 },
                      new[] { pane.YAxis.Min, pane.YAxis.Max },
                      Color.Black, SymbolType.None);
                      line2.Line.Style = System.Drawing.Drawing2D.DashStyle.Dash;
                      line2.Line.Width = 1f;
                      pane.CurveList.Add(line2);
                      pane.CurveList[pane.CurveList.Count - 1].IsVisible = false;

                      pZGraph.Invalidate();
                  }
                  public static void Spectrum_reDrawBorders(ZedGraph.ZedGraphControl pZGraph, double b1, double b2, bool IsVisible)
                  {
                      GraphPane pane = pZGraph.GraphPane;

                      LineItem line1 = new LineItem(String.Empty, new[] { b1, b1 },
                      new[] { pane.YAxis.Min, pane.YAxis.Max },
                      Color.Black, SymbolType.None);
                      line1.Line.Style = System.Drawing.Drawing2D.DashStyle.Dash;
                      line1.Line.Width = 1f;
                      pane.CurveList.RemoveAt(pane.CurveList.Count - 2);
                      pane.CurveList.Add(line1);

                      LineItem line2 = new LineItem(String.Empty, new[] { b2, b2 },
                      new[] { pane.YAxis.Min, pane.YAxis.Max },
                      Color.Black, SymbolType.None);
                      line2.Line.Style = System.Drawing.Drawing2D.DashStyle.Dash;
                      line2.Line.Width = 1f;
                      pane.CurveList.RemoveAt(pane.CurveList.Count - 2);
                      pane.CurveList.Add(line2);

                      pane.CurveList[pane.CurveList.Count - 2].IsVisible = IsVisible;
                      pane.CurveList[pane.CurveList.Count - 1].IsVisible = IsVisible;

                      pZGraph.Invalidate();
                  }*/
            }
            public static class XLSx
            {
                /* public static void WriteColomn_ofValues(ref IXLWorksheet xls_sheet,string Description,
                     List<float> Values,int pRow,int pColomn)
                 {
                     xls_sheet.Cell(pRow, pColomn).Value = Description;

                     for(int i =0;i<Values.Count;i++)
                     {
                         xls_sheet.Cell(pRow + i + 1, pColomn).Value = Values[i];
                     }
                 }
                 public static void WriteColomn_ofValues(ref IXLWorksheet xls_sheet, string Description,
                    List<int> Values, int pRow, int pColomn)
                 {
                     xls_sheet.Cell(pRow, pColomn).Value = Description;

                     for (int i = 0; i < Values.Count; i++)
                     {
                         xls_sheet.Cell(pRow + i + 1, pColomn ).Value = Values[i];
                     }
                 }
                 public static void WriteColomn_ofStrings(ref IXLWorksheet xls_sheet,
                     List<string> Strings, int pRow, int pColomn)
                 {
                     for (int i = 0; i < Strings.Count; i++)
                     {
                         xls_sheet.Cell(pRow+i , pColomn).Value = Strings[i];
                     }
                 }
                 public static void WriteColomn_ofFormalas_R1C1(ref IXLWorksheet xls_sheet, string Description,
                  List<string> Strings, int pRow, int pColomn)
                 {
                     xls_sheet.Cell(pRow, pColomn).Value = Description;
                     for (int i = 0; i < Strings.Count; i++)
                     {
                         xls_sheet.Cell(pRow +1 + i, pColomn).FormulaR1C1 = Strings[i];
                     }
                 }*/
            }
            public static void ResizeControl_ForImage(ref Panel Pan_IMG, ref PictureBox PB_CurrentImage, Bitmap WorkingImage, out PointF Transformation)
            {
                var RealImSize = WorkingImage.Size;
                double ImW2H = ((double)RealImSize.Width) / ((double)RealImSize.Height);
                double ContW2H = ((double)Pan_IMG.Width) / ((double)Pan_IMG.Height);
                Size NewPBSize = new Size(0, 0);
                Point NewPosition = new Point(0, 0);
                if (ContW2H > ImW2H) //то вписываем по высоте
                {
                    NewPBSize.Height = Pan_IMG.Height;
                    NewPBSize.Width = (int)((double)Pan_IMG.Height * ImW2H);
                    //   PB_CurrentImage.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
                    NewPosition = new Point((Pan_IMG.Width - NewPBSize.Width) / 2, 0);
                }
                else //вписываем по ширине
                {
                    NewPBSize.Width = Pan_IMG.Width;
                    NewPBSize.Height = (int)((double)Pan_IMG.Width / ImW2H);

                    //  PB_CurrentImage.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom);
                    NewPosition = new Point(0, (Pan_IMG.Height - NewPBSize.Height) / 2);
                }
                PB_CurrentImage.Size = NewPBSize;
                PB_CurrentImage.Location = NewPosition;

                Transformation = new PointF((float)RealImSize.Width / (float)NewPBSize.Width, (float)RealImSize.Height / (float)NewPBSize.Height);
            }
            public static void Init_TrB(TrackBar pTrB, int min, int max, int value)
            {
                pTrB.Minimum = min;
                pTrB.Maximum = max;
                pTrB.Value = value;
            }
            public static void Init_NUD(NumericUpDown NUD, int min, int max, int value)
            {
                NUD.Minimum = min;
                NUD.Maximum = max;
                NUD.Value = value;
            }
            public delegate void InvokeDelegate_forTrackBar(ref TrackBar ppTrackBar, int a);
            public static void SetValue_onTrackBar_Manually(ref TrackBar pTrackBar, int pValue)
            {
                if (pTrackBar.InvokeRequired)
                {
                    InvokeDelegate_forTrackBar del1 = new InvokeDelegate_forTrackBar(SetValue_onTrackBar_Manually);
                    pTrackBar.BeginInvoke(del1, pTrackBar, pValue);
                }
                else
                {
                    pTrackBar.Value = pValue;
                }
            }
        }
        public static class AO
        {
            public static List<int> GetWLS_fromList_plusSort(ref List<string> FullImageWays, string ext)
            {
                List<int> result = new List<int>();
                if (FullImageWays.Find(x => x.Contains("color")) != null)
                    FullImageWays.RemoveAt(FullImageWays.IndexOf(FullImageWays.Find(x => x.Contains("color"))));
                List<string> data_res = new List<string>(FullImageWays.Select(fn => Path.GetFileNameWithoutExtension(fn)));
                result = new List<int>(data_res.Select(fn => Get_WL_from_string(fn)));
                result.Sort(delegate (int wl1, int wl2) { return wl1.CompareTo(wl2); });
                FullImageWays.Sort(delegate (string wl1, string wl2)
                {
                    return (Get_WL_from_string(Path.GetFileNameWithoutExtension(wl1)))
                 .CompareTo(Get_WL_from_string(Path.GetFileNameWithoutExtension(wl2)));
                });
                return result;
            }
            public static int Get_WL_from_string(string a)
            {
                int res = -1;
                res = Convert.ToInt32(a.Substring(a.LastIndexOf('_') + 1));
                return res;
            }
            public static void Pack_AO_Parameters(ref List<object> pParamList,
                                 bool IsEmulator, bool TurnedON, bool handling_byslider,
                                 string DEV_required, string DEV_loaded,
                                 float pStartL, float pEndL, float pStepL, float pCurrentL, float pMaxL, float pMinL,
                                 double SW_FreqDev, double SW_TimeDev, double SW_FreqDevMax, bool SW_on,
                                 float[] pWls_2pack, float[] pHZs_2pack, float[] pCoefs_2pack)
            {
                pParamList.Clear();

                pParamList.Add(IsEmulator);
                pParamList.Add(TurnedON);
                pParamList.Add(handling_byslider);

                pParamList.Add(DEV_required);
                pParamList.Add(DEV_loaded);

                pParamList.Add(pStartL);
                pParamList.Add(pEndL);
                pParamList.Add(pStepL);
                pParamList.Add(pCurrentL);
                pParamList.Add(pMaxL);
                pParamList.Add(pMinL);

                pParamList.Add(SW_FreqDev);
                pParamList.Add(SW_TimeDev);
                pParamList.Add(SW_FreqDevMax);
                pParamList.Add(SW_on);

                pParamList.Add(pWls_2pack);
                pParamList.Add(pHZs_2pack);
                pParamList.Add(pCoefs_2pack);
            }
            public static void UnPack_AO_Parameters(List<object> pParamList,
                                       ref bool IsEmulator, ref bool TurnedON, ref bool handling_byslider,
                                       ref string DEV_required, ref string DEV_loaded,
                                       ref float pStartL, ref float pEndL, ref float pStepL, ref float pCurrentL, ref float pMaxL, ref float pMinL,
                                       ref double Frequency_deviation, ref double Time_of_deviation, ref double Frequency_dev_Max, ref bool IsSweepOn,
                                       ref float[] pWls, ref float[] pHZs, ref float[] pCoefs)
            {

                IsEmulator = (bool)pParamList[0];
                TurnedON = (bool)pParamList[1];
                handling_byslider = (bool)pParamList[2];

                DEV_required = pParamList[3] as string;
                DEV_loaded = pParamList[4] as string;

                pStartL = (float)pParamList[5];
                pEndL = (float)pParamList[6];
                pStepL = (float)pParamList[7];
                pCurrentL = (float)pParamList[8];
                pMaxL = (float)pParamList[9];
                pMinL = (float)pParamList[10];
                Frequency_deviation = (double)pParamList[11];
                Time_of_deviation = (double)pParamList[12];
                Frequency_dev_Max = (double)pParamList[13];
                IsSweepOn = (bool)pParamList[14];

                pWls = pParamList[15] as float[];
                pHZs = pParamList[16] as float[];
                pCoefs = pParamList[17] as float[];
            }
            public delegate void StringDelegate(string parameter);
            public static bool ConnectAOF(string DevName, ref TrackBar pTrB_WlCont, ref NumericUpDown pTB_CurWl, bool isSimulator,
                dynamic D_mess, dynamic D_err,
                ref float pMinL, ref float pMaxL, ref float pStartL, ref float pFinishL, ref float pCurrentL, ref string pAO_reqName)
            {
                bool result = false;
                var LogError_del = D_err;
                var LogMessage_del = D_mess;

                int codeerr = 0; int num = 0;
                try
                {
                    /* int devs_to_open = LDZ_Code.AO.ListUnopenDevices();
                     if (devs_to_open > 0)
                     {
                         LDZ_Code.AO.List_and_open_Devices();
                         result = true;
                     }
                     else
                     {
                         result = false;
                         LogError_del("Подключенные АОФ не найдены");
                     }*/
                }
                catch (Exception exc)
                {
                    result = false;
                    LogError_del("Произошла ошибка при инициализации АОФ");
                    LogMessage_del("Оригинал ошибки: " + exc.Message);
                }

                try
                {
                    pStartL = pMinL;
                    pFinishL = pMaxL;
                    pCurrentL = (pMinL + pMaxL) / 2;
                    Controls.Init_TrB(pTrB_WlCont,
                        (int)(pTB_CurWl.Minimum = (int)(pMinL)),
                        (int)(pTB_CurWl.Maximum = (int)(pMaxL)),
                        (int)(pTB_CurWl.Value = (int)(pCurrentL)));
                }
                catch (Exception ex)
                {
                    LogError_del(ex.Message);
                }
                return result;
            }
            public static void PowerAOF(bool isSimulator, dynamic D_mess, dynamic D_err, ref bool isPowerOn)
            {
                /* var LogError_del = D_err;
                 var LogMessage_del = D_mess;
                 try
                 {
                     int codeerr = 0;
                     codeerr = LDZ_Code.AO.AOM_PowerOn(isSimulator);
                     if (codeerr != 0)
                     {
                         isPowerOn = false;
                         throw new Exception(LDZ_Code.AO.AOM_IntErr(codeerr));
                     }
                     else
                     {
                         LogMessage_del("AOF Power is ON");
                         isPowerOn = true;
                     }
                 }
                 catch (Exception ex)
                 {
                     LogError_del(ex.Message);
                 }*/
            }
        }
        public static class UI
        {
            public static string GetDateString()
            {
                string res = DateTime.Today.ToString();
                return ((res.Substring(0, res.IndexOf(' '))).Remove(res.IndexOf('.'), 1)).Remove(res.LastIndexOf('.') - 1, 1);
            }
            public static string Get_TimeNow_String()
            {
                return DateTime.Now.ToString().Replace('.', '_').Replace(' ', '_').Replace(':', '_');
            }
            public static class Log
            {
                public class Logger
                {

                    public delegate void Log_del(string message);
                    int AttachmentFactor = 1;
                    ListBox ControlledLB;
                    public Logger(ListBox pLBConsole)
                    {
                        ControlledLB = pLBConsole;
                        Log.CreateAttachmentFactor(ref AttachmentFactor, ControlledLB);
                    }
                    public void Message(string Message)
                    {
                        Log.Message(ControlledLB, AttachmentFactor, Message);
                    }
                    public void Error(string Message)
                    {
                        Log.Error(ControlledLB, AttachmentFactor, Message);
                    }

                }
                /// <summary>
                /// Сообщает об ошибке в элемент ListBox, используемый как коноль вывода
                /// </summary>
                /// <param name="message">The message</param>
                private static void Message(ListBox pLBConsole, int pAttachmentFactor, string message)
                {
                    try
                    {
                        if (null == message)
                        {
                            throw new ArgumentNullException("message");
                        }
                        string data = string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}: {1}", DateTime.Now, message);
                        object index;
                        if (data.Length <= pAttachmentFactor)
                        {
                            index = data;
                            if (pLBConsole.InvokeRequired)
                                pLBConsole.BeginInvoke((Action)(() => Message(pLBConsole, pAttachmentFactor, message)));
                            else
                                pLBConsole.Items.Insert(0, index);
                        }
                        else
                        {
                            index = data.Substring(0, (int)pAttachmentFactor) + "...";
                            if (pLBConsole.InvokeRequired)
                            {
                                pLBConsole.BeginInvoke((Action)(() => Message(pLBConsole, pAttachmentFactor, message)));
                                pLBConsole.BeginInvoke((Action)(() => Attachment(pLBConsole, pAttachmentFactor, data.Substring((int)pAttachmentFactor), 1)));
                            }
                            else
                            {
                                pLBConsole.Items.Insert(0, index);
                                Log.Attachment(pLBConsole, pAttachmentFactor, data.Substring((int)pAttachmentFactor), 1);
                            }
                        }
                    }
                    catch { }

                }

                /// <summary>
                /// Add an error log message and show an error message box
                /// </summary>
                /// <param name="message">The message</param>
                private static void Error(ListBox LBConsole, int pAttachmentFactor, string message)
                {
                    Log.Message(LBConsole, pAttachmentFactor, "Ошибка: " + message);
                }
                private static void Attachment(ListBox pLBConsole, int pAttachmentFactor, string Addmessage, int level)
                {
                    if (null == Addmessage)
                    {
                        throw new ArgumentNullException("message");
                    }
                    string data = Addmessage;
                    object index;
                    if (data.Length <= pAttachmentFactor)
                    {
                        index = "..." + data;
                        pLBConsole.Items.Insert(level, index);
                    }
                    else
                    {
                        index = "..." + data.Substring(0, (int)pAttachmentFactor) + "...";
                        pLBConsole.Items.Insert(level, index);
                        Log.Attachment(pLBConsole, pAttachmentFactor, data.Substring((int)pAttachmentFactor), level + 1);
                    }

                }
                private static void CreateAttachmentFactor(ref int pAttachmentFactor, ListBox pLB)
                {
                    const float widthofthesymbol = 5.8f;
                    pAttachmentFactor = (int)(((float)pLB.Width) / widthofthesymbol) - 1;
                }

            }
        }
    }

    class FrameQueueSinkListener : IFrameQueueSinkListener
    {
        protected Action<IFrameQueueBuffer> process;

        public void SetImageProcessing(Action<IFrameQueueBuffer> procedure)
        {
            process = procedure;
        }

        public void SinkConnected(FrameQueueSink sink, FrameType frameType)
        {
            // here we allocate and queue 5 buffers which too already have the right type
            sink.AllocAndQueueBuffers(5);
        }
        public void SinkDisconnected(FrameQueueSink sink, IFrameQueueBuffer[] dequeuedInputBuffers)
        {
            foreach (IFrameQueueBuffer buf in dequeuedInputBuffers)
            {
                // this is practically sink.PopInputQueueBuffers
            }
            // these are the already copied buffers for which we were not called for/didn't already pop
            IFrameQueueBuffer[] outputBuffers = sink.PopAllOutputQueueBuffers();
        }
        public void FramesQueued(FrameQueueSink sink)
        {
            IFrameQueueBuffer[] buffers = sink.PopAllOutputQueueBuffers();
            foreach (IFrameQueueBuffer buf in buffers)
            {
                process?.Invoke(buf);
                sink.QueueBuffer(buf);
            }
            // we can exit here, because when new buffers arrived after we called PopAllOutputQueueBuffers, we are immediatly called again
        }
    }

    public class HyperSpectralGrabber
    {
        bool OnWls = true;
        List<float> WLs = new List<float>();
        List<double> Times2SetWL = new List<double>();
        List<double> Times2SnapImage = new List<double>();
        System.ComponentModel.BackgroundWorker BGW;
        string TargetPath = null;
        string TargetPrefix = null;
        string TargetExtension = null;
        FrameSnapSink FFS = null;
        AO_Lib.AO_Devices.AO_Filter AOF = null;
        MultiThreadSaver Saver;
        Action<string> Log;
        bool UseMultithreadSaving = true;

        public delegate void ProgressChanged(int frames_gotten,int frames_to_got);
        public delegate void Serie_state();

        public event Serie_state OnSerieStarted;
        public event Serie_state OnSerieFinished;
        public event ProgressChanged OnProgressChanged;

        public HyperSpectralGrabber(FrameSnapSink pFFS, AO_Lib.AO_Devices.AO_Filter pAOF, List<float> pWls,string Path,string Prefix)
        {
            WLs = pWls;
            TargetPath = Path;
            TargetPrefix = Prefix;
            FFS = pFFS;
            AOF = pAOF;

            BGW.WorkerReportsProgress = true;
            BGW.WorkerSupportsCancellation = true;
            BGW.DoWork += BGW_DoWork;
           // new { Buffer = rval[i], Name = local }
        }
        public HyperSpectralGrabber(dynamic ControllableObjects, dynamic SerieParams)
        {
            WLs = SerieParams.WLS;
            TargetPath = SerieParams.PATH;
            TargetPrefix = SerieParams.PREFIX;
            TargetExtension = SerieParams.EXTENSION;
            OnWls = SerieParams.ONWLS;

            FFS = ControllableObjects.FFS;
            AOF = ControllableObjects.AOF;
            Log = ControllableObjects.LOG;
            Saver = ControllableObjects.SAVER;

            BGW = new System.ComponentModel.BackgroundWorker();
            BGW.WorkerReportsProgress = true;
            BGW.WorkerSupportsCancellation = true;
            BGW.DoWork += BGW_DoWork;

            UseMultithreadSaving = (Saver != null);
            // new { Buffer = rval[i], Name = local }
        }

    
        private string GetTodayDate()
        {
            try
            {
                string res = DateTime.Today.ToString();
                return ((res.Substring(0, res.IndexOf(' '))).Remove(res.IndexOf('.'), 1)).Remove(res.LastIndexOf('.') - 1, 1);
            }
            catch { return  "date"; }
        }
        public void StartGrabbing()
        {
            BGW.RunWorkerAsync();
        }
        public void StopGrabbing()
        {

        }

        private void BGW_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            int Steps = WLs.Count;
            int Error = 0;
            string date = GetTodayDate();
            try
            {
                if (OnWls) Error = AOF.Set_Wl(WLs[0]);
                else Error = AOF.Set_Hz(WLs[0]);//, AOFSimulatorActivated);
                FFS.SnapSingle(TimeSpan.FromSeconds(30));
                System.Threading.Thread.Sleep(500);
                if (Error != 0) { throw new Exception(AOF.Implement_Error(Error)); };
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
            // System.Threading.Thread.Sleep(20);             
            System.Diagnostics.Stopwatch SessionDone = new System.Diagnostics.Stopwatch(); SessionDone.Start();

            string FullPrefix = Path.Combine(TargetPath, TargetPrefix + "_" + date + "_");
            string dirName_data = (TargetPath.Substring(0, TargetPath.Length - 1));
            dirName_data = dirName_data.Substring(dirName_data.LastIndexOf('\\') + 1);
            if (UseMultithreadSaving) Saver.OpenSerie(WLs.Count(), dirName_data);
            OnSerieStarted?.Invoke();

            for (int i = 0; i < Steps; i++)
            {
                //Вычисление нового имени
                string WL_cur = (String.Format("{0:0.000}", WLs[i])).Replace(',','.');                
                string local = FullPrefix + WL_cur + TargetExtension;
                if (File.Exists(local))
                {
                    int num = 1;
                    while (File.Exists(local))
                    {
                        num++;
                        local = FullPrefix + WL_cur + "_" + num.ToString() + TargetExtension;
                    }
                }

                System.Diagnostics.Stopwatch swl = new System.Diagnostics.Stopwatch(); swl.Start();
                if (OnWls) AOF.Set_Wl(WLs[i]);
                else AOF.Set_Hz(WLs[i]);
                swl.Stop();
                Times2SetWL.Add(swl.Elapsed.TotalMilliseconds);
                //Check for some lags //02.06.2021
                System.Threading.Thread.Sleep(100);

                System.Diagnostics.Stopwatch swl2 = new System.Diagnostics.Stopwatch();
                swl2.Start();
                if (UseMultithreadSaving) Saver.EnqueFrame(FFS.SnapSingle(TimeSpan.FromSeconds(30)), local);
                else { (FFS.SnapSingle(TimeSpan.FromSeconds(30))).SaveAsTiff(local); }
                swl2.Stop();
                Times2SnapImage.Add(swl2.Elapsed.TotalMilliseconds);
                //

                //Bitmap data_bmp = curfhs.LastAcquiredBuffer.
                OnProgressChanged?.Invoke(i, Steps);
            }
            if (UseMultithreadSaving) Saver.CloseSerie();
            else { System.Threading.Thread.Sleep(1000); }
            

            SessionDone.Stop();


            double MediumTime2SWL = 0;
            double MediumTime2SI = 0;
            double MediumTime2CI = 0;

            for (int i = 0; i < Times2SetWL.Count; i++) { MediumTime2SWL += Times2SetWL[i]; }
            for (int i = 0; i < Times2SnapImage.Count; i++) { MediumTime2SI += Times2SnapImage[i]; }
            MediumTime2SWL /= Times2SetWL.Count;
            MediumTime2SI /= Times2SnapImage.Count;
            Log("Среднее время на перестройку: " + MediumTime2SWL.ToString() + " мс");
            Log("Среднее время на захват и копирование: " + MediumTime2SI.ToString() + " мс");
            Log("Захват кадров завершен. Прошедшее время: " + SessionDone.Elapsed.ToString());
            Log("Реальное   FPS: " + (((double)(Steps)) / SessionDone.Elapsed.TotalSeconds).ToString());

            OnSerieFinished?.Invoke();
        }


    }

    public class MultiThreadSaver
    {
        Queue<IFrameQueueBuffer> buffer = new Queue<IFrameQueueBuffer>();
        Queue<string> names = new Queue<string>();
        
        bool AquisitionStarted = false;
        bool AquisitionFinished = false;

        int counter_gotten_frames = 0;
        int counter_saved_frames = 0;
        int counter_saved_frames_totally = 0;
        int counter_series = 0;

        string LastName = null;
        Queue<int> SeriePlans = new Queue<int>();
        Queue<int> SerieStarts = new Queue<int>();
        Queue<int> SerieFinishes = new Queue<int>();

        System.ComponentModel.BackgroundWorker Saver = new System.ComponentModel.BackgroundWorker();
        Action<string> Log;

        public delegate void SerieSavedReporter(string NameOfTheSerie,int Frames);
        public delegate void SerieInfoReporter(string NameOfTheSerie);
        public event SerieSavedReporter OnSerieSaved;
        public event SerieInfoReporter OnSerieStarted;

        public delegate void ProgressReported(int frames_saved, int frames_gotten);
        public event ProgressReported OnFrameSaved;
        public event ProgressReported OnAllFramesSaved;

        public MultiThreadSaver(Action<string> logger = null)
        {
            Saver.DoWork += Saver_DoWork;
            Saver.RunWorkerAsync();
            Log = logger;
        }

        private void Saver_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while(true)
            {
                try
                {
                    if (buffer.Count != 0)
                    {
                        using (var frame = buffer.Dequeue())
                        {
                            LastName = names.Dequeue();
                         //   frame.SaveAsTiff(LastName);
                            counter_saved_frames++;
                            OnFrameSaved?.Invoke(counter_saved_frames, counter_gotten_frames);
                        }                    
                    }
                }
                catch
                {

                }
                if (SeriePlans.Count != 0 || (AquisitionStarted && !AquisitionFinished))
                {
                    try
                    {
                        if (counter_saved_frames >= counter_saved_frames_totally+SeriePlans.Peek())
                        {
                            int FramesSaved = SeriePlans.Dequeue();
                            string lastSeriename = Path.GetFileName(Path.GetDirectoryName(LastName));
                            // Log?.Invoke(String.Format("Серия {0} сохранена! Сохранено кадров: {1} ", lastSeriename, FramesSaved));
                            counter_saved_frames_totally += FramesSaved;
                            OnSerieSaved?.Invoke(lastSeriename, FramesSaved);
                        }
                    }
                    catch
                    {

                    }
                }
                else
                {
                    if (!AquisitionStarted && AquisitionFinished && SeriePlans.Count==0 && (counter_saved_frames == counter_gotten_frames ) && counter_gotten_frames!=0)
                    {
                         OnAllFramesSaved?.Invoke(counter_saved_frames, counter_gotten_frames);
                        counter_saved_frames_totally = 0;
                        counter_gotten_frames = 0;
                        counter_saved_frames = 0;
                    }
                }
            }
        }

        public void SerieStarted()
        {
        }

        public void EnqueFrame(IFrameQueueBuffer frame, string name)
        {
            AquisitionStarted = true;
            AquisitionFinished = false;
            names.Enqueue(name);
            buffer.Enqueue(frame);
            counter_gotten_frames++;
        }
        public void OpenSerie(int Num_of_Frames2Save,string NameOfDir)
        {
            AquisitionStarted = true;
            AquisitionFinished = false;
            SerieStarts.Enqueue(counter_gotten_frames);
            OnSerieStarted?.Invoke(NameOfDir);
        }
        public void CloseSerie()
        {
            SerieFinishes.Enqueue(counter_gotten_frames);
            SeriePlans.Enqueue((SerieFinishes.Dequeue() - SerieStarts.Dequeue()));
            System.Threading.Thread.Sleep(500);
            AquisitionFinished = true;
            AquisitionStarted = false;

        }

        public void CloseAfterSaving()
        {
            AquisitionStarted = false;
        }

        public int GetNumberOfFrames()
        {
            return buffer.Count;
        }

    }
}
        
