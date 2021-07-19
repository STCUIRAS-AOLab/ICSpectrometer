using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using TIS.Imaging;
using TIS.Imaging.VCDHelpers;
using System.Diagnostics;

namespace ICSpec
{
    public partial class Form1
    {
        float AO_MinimumWL = 740, AO_MaximumWL = 1000, AO_CurrentWL = 0;
        float AO_StepWL = 10, AO_StartWL = 0, AO_EndWL = 0;
        float AO_StepFreq = 10, AO_StartFreq = 0, AO_EndFreq = 0;
        bool ACenterAvailible = false;
        bool ScanXOffsetAvailible = false;
        bool ScanYOffsetAvailible = false;
        bool AOFisON = false;
        int MaxMWidth = 0; int MaxMHeight = 0;
        bool ROISeted = false; double alpha = 0, beta = 0, xenta = 0, zF = 1000000;

        int MaximumWL = 0; int MinimumWL = 0;
        //forDrawROI
       // BufferedGraphicsContext currentContext;
      //  BufferedGraphics myBuffer;
        Pen PancilForDraw2 = new Pen(Color.FromArgb(255, 50, 255, 50));
        Pen PencilForDraw = new Pen(Color.FromArgb(255, 50, 255, 50), 4);
        SolidBrush PencilPodsv = new SolidBrush(Color.FromArgb(100, 50, 255, 50));
        public delegate void ShowStringDelegate(string message);
        private void LogMessage(string message)
        {
            if (null == message)
            {
                throw new ArgumentNullException("message");
            }
            string data = string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}: {1}", DateTime.Now, message);
            object index;
            if(LBConsole.InvokeRequired)
            {

                ShowStringDelegate MessageDelegate = new ShowStringDelegate(LogMessage);
                LBConsole.BeginInvoke(MessageDelegate,new object[] { message });
            }
            else
            if (data.Length <= AttachmentFactor)
            {
                index = data;
                LBConsole.Items.Insert(0, index);
            }
            else
            {
                index = data.Substring(0, AttachmentFactor) + "...";
                LBConsole.Items.Insert(0, index);
                LogAttachment(data.Substring(AttachmentFactor), 1);
            }

        }
        private void LogAttachment(string Addmessage, int level)
        {
            if (null == Addmessage)
            {
                throw new ArgumentNullException("message");
            }
            string data = Addmessage;
            object index;
            if (data.Length <= AttachmentFactor)
            {
                index = "..." + data;
                LBConsole.Items.Insert(level, index);
            }
            else
            {
                index = "..." + data.Substring(0, AttachmentFactor) + "...";
                LBConsole.Items.Insert(level, index);
                LogAttachment(data.Substring(AttachmentFactor), level + 1);
            }

        }
        private void CreateAttachmentFactor(ref int pAF, ListBox pLB)
        {
            const float widthofthesymbol = 5.5f;
            pAF = (int)((float)pLB.Width / widthofthesymbol) - 10;
        }

        /// <summary>
        /// Add an error log message and show an error message box
        /// </summary>
        /// <param name="message">The message</param>
        private void LogError(string message)
        {
            LogMessage("Ошибка: " + message);
        }

        private void SwitchOverlay(TIS.Imaging.OverlayBitmap ob, bool flag)
        {
            icImagingControl1.OverlayBitmapPosition = TIS.Imaging.PathPositions.Display;
            icImagingControl1.OverlayBitmapAtPath[PathPositions.Device].ColorMode  = OverlayColorModes.Color;
            icImagingControl1.OverlayBitmapAtPath[PathPositions.Display].ColorMode = OverlayColorModes.Color;
            icImagingControl1.OverlayBitmapAtPath[PathPositions.Sink].ColorMode = OverlayColorModes.Color;

            ob.Enable = flag;
            ob.DropOutColor = Color.Magenta;
            ob.Fill(ob.DropOutColor);
            ob.FontTransparent = true;
        }
        private void DBInvalidate(TIS.Imaging.OverlayBitmap ob)//функция перерисовки всех элементов через двойную буферизацию
        {
            ob.Fill(ob.DropOutColor);
            ob.DrawText(Color.FromArgb(125,0,255,0), 10, 10, DateTime.Now.ToString());
            int NumOfLines = 1+(int)(1.0f/icImagingControl1.LiveDisplayZoomFactor);
            if (!ROISeted)
            {
                for (int i = 0; i < NumOfLines; i++)
                {
                    ob.DrawFrameRect(Color.FromArgb(125, 250, 250, 250), UserROIGraphics.X + i, UserROIGraphics.Y + i,
                   UserROIGraphics.Width - i, UserROIGraphics.Height - i);
                }      
                
            }
            //если потребуется отрисовать изображение
            #region Example
            /*  try
            {
                // Load the sample bitmap from the project file's directory.
                Image bmp = Bitmap.FromFile(Application.StartupPath + @"\hardware.bmp");
                // Calculate the column to display the bitmap in the
                // upper right corner of Imaging Control.
                int col = icImagingControl1.ImageWidth - 5 - bmp.Width;
                // Retrieve the Graphics object of the OverlayBitmap.
                Graphics g = ob.GetGraphics();
                // Draw the image
                g.DrawImage(bmp, col, 5);

                // Release the Graphics after drawing is finished.
               // ob.ReleaseGraphics(g);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("File not found: " + Ex.Message);
            }*/
            #endregion
            //тяжелая артиллерия
            #region Double Buffer
        /*    currentContext = BufferedGraphicsManager.Current;// Gets a reference to the current BufferedGraphicsContext
            myBuffer = currentContext.Allocate(icImagingControl1.CreateGraphics(), icImagingControl1.DisplayRectangle);//Передает буферу объект, для которого осуществляется перерисовка,и зона на этом объекте
            myBuffer.Graphics.DrawRectangle(PencilForDraw, 1, 10, 10, 10);
            Invalidate();
            myBuffer.Render();
            myBuffer.Dispose();*/
            #endregion

        }
        private void LoadFlipFilter()
        {
            FlipUpDownBut.BackgroundImage = Image.FromFile(Application.StartupPath + "\\FUDBut.png", false);
            FlipUpDownBut.BackgroundImageLayout = ImageLayout.Stretch;
            FlipRightLeftBut.BackgroundImage = Image.FromFile(Application.StartupPath + "\\FRLBut.png", false);
            FlipRightLeftBut.BackgroundImageLayout = ImageLayout.Stretch;

            FlipFilter = icImagingControl1.FrameFilterCreate("Rotate Flip", "");
            FlipFilterFHS = icImagingControl1.FrameFilterCreate("Rotate Flip", "");
            
            if (FlipFilter == null)
                LogError("Failed to create RotateFlipFilter");
            else
            {
                //curfhs.FrameFilters.Clear();
               // curfhs.FrameFilters.Add(FlipFilterFHS);
                icImagingControl1.DeviceFrameFilters.Clear();
                icImagingControl1.DeviceFrameFilters.Add(FlipFilter);
            }         
        }
        private void DisableFlipButtons()
        {
            FlipRightLeftBut.Enabled = false;
            FlipRightLeftBut.BackgroundImage = Image.FromFile(Application.StartupPath + "\\FRLButCapt.png", false);
            FlipUpDownBut.Enabled = false;
            FlipUpDownBut.BackgroundImage = Image.FromFile(Application.StartupPath + "\\FUDButCapt.png", false);
        }
        private void EnableFlipButtons()
        {
            FlipRightLeftBut.Enabled = true;
            FlipRightLeftBut.BackgroundImage = Image.FromFile(Application.StartupPath + "\\FRLBut.png", false);
            FlipUpDownBut.Enabled = true;
            FlipUpDownBut.BackgroundImage = Image.FromFile(Application.StartupPath + "\\FUDBut.png", false);
        }
        private void EnableOverlayBitmapAtPath(PathPositions pos, bool enabled)
        {
            bool wasLive = icImagingControl1.LiveVideoRunning;
            if (wasLive)
                icImagingControl1.LiveStop();

            PathPositions oldPos = icImagingControl1.OverlayBitmapPosition;
            if (enabled)
                icImagingControl1.OverlayBitmapPosition = pos;
            else
                icImagingControl1.OverlayBitmapPosition = PathPositions.Device;

            if (wasLive)
                icImagingControl1.LiveStart();
        }
		
        private void ReadAllSettingsFromFile(bool typeofwind)//чтение настроек записи из файла
        {
            if (!File.Exists(fileName))
            {
                using (StreamWriter sw = new StreamWriter(new FileStream(fileName, FileMode.Create, FileAccess.Write)))
                {
                    sw.WriteLine("Image Settings:");
                    sw.WriteLine("      1.Directory::");
                    sw.WriteLine("      2.Extension::");
                    sw.WriteLine("      3.Quality::");
                    sw.WriteLine("Video Settings:");
                    sw.WriteLine("      1.Directory::");
                    sw.WriteLine("      1.Media Stream Container(extension)::");
                    sw.WriteLine("      1.Codec::");
                }
            }

            string[] readText = File.ReadAllLines(fileName);
            if (typeofwind)  //Video Settings
            {
                filedatename = DateTime.Now.ToString();
                filedatename = filedatename.Replace(' ', '_');
                filedatename = filedatename.Replace(':', '_');
                filedatename = filedatename.Replace('.', '_');
                m_oldSink = icImagingControl1.Sink;
                TIS.Imaging.MediaStreamSink m_Sink = new TIS.Imaging.MediaStreamSink();


                string localcontainername = readText[6].Substring(readText[6].LastIndexOf(':') + 1, readText[6].Length - (readText[6].LastIndexOf(':') + 1));
                for (int i = 0; i < icImagingControl1.MediaStreamContainers.Count(); i++)
                {
                    if (icImagingControl1.MediaStreamContainers[i].Name == localcontainername)
                    {
                        m_Sink.StreamContainer = icImagingControl1.MediaStreamContainers[i];
                    }
                }
                string temp = Application.StartupPath + "\\Видео\\" + filedatename + "." + m_Sink.StreamContainer.PreferredFileExtension;
                if (readText[5].Substring(19, readText[5].Length - 19) == "")
                {
                    if (!Directory.Exists(Application.StartupPath + "\\Видео\\"))
                    {
                        try
                        {
                            System.IO.Directory.CreateDirectory(Application.StartupPath + "\\Видео");
                        }
                        catch (System.IO.IOException except)
                        {
                            WarningofCapt = true;
                            WarningofCaptMessage = except.Message + "Невозможно создать директорию  " + Application.StartupPath + "\\Видео";
                        }
                    }
                    else
                        temp = Application.StartupPath + "\\Видео\\" + filedatename + "." + m_Sink.StreamContainer.PreferredFileExtension;
                    //WarningofCapt = true;
                    // WarningofCaptMessage="Введите директорию файла для записи в разделе " + SaveImgParBut.Name + "!";
                }
                else
                {
                    if (!Directory.Exists(readText[5].Substring(19, readText[5].Length - 19)))
                    {
                        //WarningofCapt = true;
                        // WarningofCaptMessage = "Введите существующую директорию файла для записи в разделе " + SaveImgParBut.Name + "!";
                        WarningofCapt = false;
                        temp = Application.StartupPath + "\\Видео\\" + filedatename + "." + m_Sink.StreamContainer.PreferredFileExtension;
                    }
                    else
                    {
                        temp = readText[5].Substring(19, readText[5].Length - 19) + filedatename + "." + m_Sink.StreamContainer.PreferredFileExtension;
                        WarningofCapt = false;
                    }
                }
                m_Sink.Filename = temp;

                string localcodecname = readText[7].Substring(readText[7].LastIndexOf(':') + 1, readText[7].Length - (readText[7].LastIndexOf(':') + 1));
                for (int i = 0; i < icImagingControl1.AviCompressors.Count(); i++)
                {
                    if (icImagingControl1.AviCompressors[i].Name == localcodecname)
                    {
                        m_Sink.Codec = icImagingControl1.AviCompressors[i];
                    }
                }
                icImagingControl1.LiveStop();
                icImagingControl1.Sink = m_Sink;
            }
            else//Image Settings
            {
                string temp = null;
                SnapImageStyle.Directory = Application.StartupPath + "\\Фото\\";
                SnapImageStyle.Extension = ".bmp";
                if (readText[1].Substring(19, readText[1].Length - 19) == "")
                {
                    if (!Directory.Exists(Application.StartupPath + "\\Фото\\"))
                    {
                        try
                        {
                            System.IO.Directory.CreateDirectory(Application.StartupPath + "\\Фото\\");
                        }
                        catch (System.IO.IOException except)
                        {
                            WarningofCapt = true;
                            WarningofCaptMessage = except.Message + "Невозможно создать директорию  " + Application.StartupPath + "\\Фото";
                        }
                    }
                    else
                        SnapImageStyle.Directory = Application.StartupPath + "\\Фото\\";
                    // WarningofImage = true;
                    //WarningofImgMessage = "Введите директорию файла для записи в разделе " + SaveImgParBut.Name + "!";
                }
                else WarningofCapt = false;
                if (!Directory.Exists(readText[1].Substring(19, readText[1].Length - 19)))
                {
                    SnapImageStyle.Directory = Application.StartupPath + "\\Фото\\";
                    //WarningofImage = true;
                    // WarningofImgMessage = "Введите существующую директорию файла для записи в разделе " + SaveImgParBut.Name + "!";                  
                }
                else SnapImageStyle.Directory = readText[1].Substring(19, readText[1].Length - 19);

                string alph = readText[2].Substring(19, readText[2].Length - 19) ;
                if (alph != "")
                {
                    SnapImageStyle.Extension = readText[2].Substring(readText[2].LastIndexOf(':') + 1, readText[2].Length - (readText[2].LastIndexOf(':') + 1));
                }
                temp = readText[3].Substring(readText[3].LastIndexOf(':') + 1, readText[3].Length - (readText[3].LastIndexOf(':') + 1));
                if (temp != "") SnapImageStyle.Quality = Convert.ToInt32(temp);
                else SnapImageStyle.Quality = 100;
            }
        }
        private void SnapShot()//функция создания скриншота окна
        {
            ReadAllSettingsFromFile(false);
            if (WarningofImage)
            {
                MessageBox.Show(WarningofImgMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    curfhs.SnapImage();
                    icImagingControl1.LiveStop();
                    LogMessage("Изображение захвачено. ");
                    ContTransAfterSnapshot.Visible = true;
                    SnapshotBut.Enabled = false;
                }
                catch
                { LogError("Ошибка при захвате кадра"); }
            }
        }
        private void CreateDialog(bool type)//функция создания диалового окна сохраниния видео
        {
            SaveDialog f2 = new SaveDialog();
            f2.Owner = this;
            f2.ShowDialog(type);
        }
        private void Init_Gain_Exposure_Sliders() //функция инициализации ползунка для регулировки отдельных свойст камеры
        {
            string ChangeVCDID = VCDIDs.VCDID_Exposure;
            string ChangeVCDID2 = VCDIDs.VCDID_Gain;

            if (!vcdProp.AutoAvailable(ChangeVCDID))//если невозможна автоматическая регулировка,отключить возможность ее включения
            {
                ChBExposureAuto.Enabled = false;
            }
            else
            {
                ChBExposureAuto.Enabled = true;
                vcdProp.Automation[ChangeVCDID] = false;
            }

            if (!vcdProp.AutoAvailable(ChangeVCDID2))//если невозможна автоматическая регулировка,отключить возможность ее включения
            {
                ChBGainAuto.Enabled = false;
            }
            else
            {
                ChBGainAuto.Enabled = true;
                vcdProp.Automation[ChangeVCDID2] = false;
            }

            if (!vcdProp.Available(ChangeVCDID))//если невозможна  регулировка,отключить
            {
                TrBExposureVal.Enabled = false;
                NUD_ExposureVal.Enabled = false;
            }
            else
            {
                
                AbsValExp = (VCDAbsoluteValueProperty)icImagingControl1.VCDPropertyItems.FindInterface(ChangeVCDID +
                    ":" + VCDIDs.VCDElement_Value + ":" + VCDIDs.VCDInterface_AbsoluteValue);

                

                NUD_ExposureVal.DecimalPlaces = Detect_Right_DecimalPositions_Num((decimal)AbsValExp.RangeMin);
                NUD_ExposureVal.Minimum = (decimal)AbsValExp.RangeMin;
                NUD_ExposureVal.Maximum = (decimal)AbsValExp.RangeMax;

                double CentralValue_onSlider = 0.25;
                //22072020 было решено определять середину слайдера, как 1/8 от всего диапазона
                if((decimal)CentralValue_onSlider <= NUD_ExposureVal.Minimum || (decimal)CentralValue_onSlider >= NUD_ExposureVal.Maximum)
                    CentralValue_onSlider = (double)(NUD_ExposureVal.Minimum + (NUD_ExposureVal.Maximum - NUD_ExposureVal.Minimum) / (decimal)8.0);

                TrBExposureVal.Enabled = true;
                NUD_ExposureVal.Enabled = true;
                int Az = TrBExposureVal.Minimum = (int)PerfectRounding((double)(AbsValExp.RangeMin * zF),0);
                int Bz = TrBExposureVal.Maximum = (int)PerfectRounding((double)(AbsValExp.RangeMax * zF),0);
                alpha = (AbsValExp.RangeMin*AbsValExp.RangeMax - CentralValue_onSlider * CentralValue_onSlider) / (AbsValExp.RangeMin + AbsValExp.RangeMax - 2* CentralValue_onSlider);
                xenta = Math.Pow((AbsValExp.RangeMax - alpha) / (AbsValExp.RangeMin - alpha), (zF / ((Bz - Az))));               
                beta = (CentralValue_onSlider - alpha) / Math.Pow(xenta, (Bz + Az) / (2 * zF));
                int val1slide = Az;              
                double val1real = Exposure_Slide2real(val1slide);
                int val2slide = Exposure_real2slide(val1real);
                if (val1slide == val2slide) LogMessage("Exponential Exposure test passed successfully!");
                try { TrBExposureVal.Value = Exposure_real2slide(AbsValExp.Value); }
                catch
                {
                    if (TrBExposureVal.Value < TrBExposureVal.Minimum) TrBExposureVal.Value = TrBExposureVal.Minimum;
                    else if (TrBExposureVal.Value > TrBExposureVal.Maximum) TrBExposureVal.Value = TrBExposureVal.Maximum;
                }
                TrBExposureVal.TickFrequency = (TrBExposureVal.Maximum - TrBExposureVal.Minimum) / 10;
                ChangingActivatedTextBoxExp = false;
                NUD_ExposureVal.Value = (decimal)(PerfectRounding(AbsValExp.Value, NUD_ExposureVal.DecimalPlaces));
                ChangingActivatedTextBoxExp = true;
            }
            if (!vcdProp.Available(ChangeVCDID2))//если невозможна  регулировка,отключить
            {
                TrBGainVal.Enabled = false;
                NUD_GainVal.Enabled = false;
            }
            else
            {
                TrBGainVal.Enabled = true;
                NUD_GainVal.Enabled = true;
                NUD_GainVal.Minimum = TrBGainVal.Minimum = vcdProp.RangeMin(ChangeVCDID2);
                NUD_GainVal.Maximum = TrBGainVal.Maximum = vcdProp.RangeMax(ChangeVCDID2);
                TrBGainVal.Value = vcdProp.RangeValue[ChangeVCDID2];
                TrBGainVal.TickFrequency = (TrBGainVal.Maximum - TrBGainVal.Minimum) / 10;
                ChangingActivatedTextBoxGain = false;
                NUD_GainVal.Value = TrBGainVal.Value;
                ChangingActivatedTextBoxGain = true;
            }
        }
        private double Exposure_Slide2real(int arg)
        {

            double a = Math.Pow(xenta, ((double)arg)/zF);
            double b = beta * a;
            return (alpha + b);

        }
        private int Exposure_real2slide(double arg)
        {
            return (int)PerfectRounding((float)(zF*(Math.Log(((arg - alpha) / beta),xenta))),0);

        }
        private int Detect_Right_DecimalPositions_Num(decimal val)
        {
            try
            {
                int positions = 0;
                while((val-(int)val!=0)&&((int)val < 10))
                {
                    val *= 10;
                    positions++;
                }
                return positions;
            }
            catch
            {
                return 5;
            }

        }
        private bool dots(string s)//функция,проверяющая, дробное или целое число введено в окно регулировки параметров
        {
            if ((s.IndexOf('.') + s.IndexOf('/')) != -2) return true;
            else return false;
        }
        private float NormalConvertToFloat(string s)//полное преобразования текста числа, введенного в любом дробном формате в окно регулировки параметров,в дробное число  
        {

            float returning = 0.00f;

            int i = 0;
            int zappos = s.Length;
            string prom = null;

            if (s.IndexOf('/') != -1)
            {
                zappos = s.IndexOf('/');
                double chislit = 0;
                double znam = 1;
                prom = s.Substring(0, zappos);
                if (prom != "") chislit = Convert.ToDouble(prom);
                if (zappos != s.Length)
                {
                    prom = s.Substring(zappos + 1, s.Length - (zappos + 1));
                    if (prom != "") znam = Convert.ToDouble(prom);
                }
                returning = (float)((chislit / znam));
            }
            else
            {
                s = s.Replace(",", ".");
                double celaya = 0;
                double drobnaya = 0;
                for (i = 0; i < s.Length; i++)
                {
                    if (Convert.ToInt32(s[i]) == 46) zappos = i; //если это точка                 
                }
                prom = s.Substring(0, zappos);
                if (prom != "") celaya = Convert.ToDouble((prom));
                if (zappos != s.Length)
                {
                    prom = s.Substring(zappos + 1, s.Length - (zappos + 1));
                    if (prom != "") drobnaya = (Convert.ToDouble(prom) * Math.Pow(10.00, Convert.ToDouble((-1) * (s.Length - (zappos + 1)))));
                }
                returning = (float)celaya + (float)drobnaya;
            }
            return returning;
        }
        private void ScrollOcc()//функция,инициализирующая ползунок увеличения в live режиме
        {
            TrBZoomOfImage.Visible = true;
            L_Zoom.Visible = true;
            if (icImagingControl1.LiveDisplayDefault == false)
            {

                icImagingControl1.LiveDisplayZoomFactor = (float)TrBZoomOfImage.Value / 100.0f;
                L_Zoom.Text = TrBZoomOfImage.Value.ToString() + "%";
                if (icImagingControl1.LiveDisplayZoomFactor >= 1.0) icImagingControl1.ScrollbarsEnabled = true;
                else icImagingControl1.ScrollbarsEnabled = false;
            }
            else
            {
                MessageBox.Show("The zoom factor can only be set" + "\n" + "if LiveDisplayDefault returns False!");
            }
        }
        private void CheckEmptyness(TextBox TB)
        {
            string name = null;
            if (TB.Name == TBROIHeight.Name) { name = "Height"; }
            else if (TB.Name == TBROIWidth.Name) { name = "Width"; }
            else if (TB.Name == TBROIPointX.Name) { name = "Point X"; }
            else if (TB.Name == TBROIPointY.Name) { name = "Point Y"; }
            else { name = TB.Name; }
            if ((TB.Text == "") || (TB.Text == null))
                throw new Exception("Данные в элементе " + name + " некорректны");
        }
        private Bitmap[] StartSession(int Number)
        {
            Bitmap[] Resultive = new Bitmap[Number];
            ReadAllSettingsFromFile(false);
            int codeerr = 0;
            if (WarningofImage)
            {
                LogError(WarningofImgMessage);
            }
            else
            {
                try
                {
                    codeerr = Filter.Set_Wl(AO_MinimumWL);//,AOFSimulatorActivated);
                    if (codeerr != 0) { throw new Exception(Filter.Implement_Error(codeerr)); };
                }
                catch (Exception ex)
                {
                    LogError(ex.Message);
                }
                System.Threading.Thread.Sleep(200);
                for (int i = 0; i < Number; i += 1)
                {
                    AO_CurrentWL = AO_MinimumWL + i * AO_StepWL;
                    try
                    {
                        codeerr = Filter.Set_Wl(AO_CurrentWL);//, AOFSimulatorActivated);
                        if (codeerr != 0) { throw new Exception(Filter.Implement_Error(codeerr)); };
                    }
                    catch (Exception ex)
                    {
                        LogError(ex.Message);
                    }

                    icImagingControl1.MemorySnapImage();
                    //Resultive[i] = new Bitmap(icImagingControl1.ImageBuffers[0].Bitmap);
                    Resultive[i] = new Bitmap(icImagingControl1.ImageActiveBuffer.Bitmap);
                    LogMessage(String.Format("Изображение №{0:D2} захвачено. ", i.ToString()));
                }

            }
            return Resultive;
        }
        private void New_SnapAndSaveMassive2(int pStartWL,int pFinishWL,int pSteps)
        {
            TIS.Imaging.ImageBuffer[] rval = new TIS.Imaging.ImageBuffer[pSteps+1];
            ReadAllSettingsFromFile(false);
            int codeerr = 0;
            if (WarningofImage) { LogError(WarningofImgMessage); }
            else
            {
                try
                {
                    codeerr = Filter.Set_Wl(AO_MinimumWL);//, AOFSimulatorActivated);
                    if (codeerr != 0) { throw new Exception(Filter.Implement_Error(codeerr)); };
                }
                catch (Exception ex)
                {
                    LogError(ex.Message);
                }
               // System.Threading.Thread.Sleep(20);

                Stopwatch SessionDone = new Stopwatch(); SessionDone.Start();
                for (int i = 0; i < pSteps; i += 1)
                {
                    AO_CurrentWL = pStartWL + i * AO_StepWL;
                    try
                    {
                        codeerr = Filter.Set_Wl(AO_CurrentWL);//, AOFSimulatorActivated);
                        if (codeerr != 0) { throw new Exception(Filter.Implement_Error(codeerr)); };
                    }
                    catch (Exception ex)
                    {
                        LogError(ex.Message);
                    }
                    curfhs.SnapImage();
                    rval[i] = curfhs.LastAcquiredBuffer;
                    //icImagingControl1.MemorySnapImage();
                    //Resultive[i] = new Bitmap(icImagingControl1.ImageActiveBuffer.Bitmap);
                  //  LogMessage(String.Format("Изображение №{0:D2} захвачено. ", i.ToString()));
                }
                try
                {
                    codeerr = Filter.Set_Wl(pFinishWL);//, AOFSimulatorActivated);
                    if (codeerr != 0) { throw new Exception(Filter.Implement_Error(codeerr)); };
                    curfhs.SnapImage();
                    rval[pSteps] = curfhs.LastAcquiredBuffer;
                   // LogMessage(String.Format("Изображение №{0:D2} захвачено. ", pSteps.ToString()));
                }
                catch (Exception ex)
                {
                    LogError(ex.Message);
                }
                SessionDone.Stop();
                LogMessage("Захват кадров завершен. Прошедшее время: " + SessionDone.Elapsed.ToString());
                LogMessage("Заявленное FPS: " + icImagingControl1.DeviceFrameRate);
                LogMessage("Реальное   FPS: " + (((double)(pSteps + 1)) / SessionDone.Elapsed.TotalSeconds).ToString());
            }
            string SCRName = CheckScreenShotBasicName();
            string date = GetDateString();
            string NameDirectory = DateTime.Now.ToString().Replace('.','_').Replace(' ','_').Replace(':','_')+"\\";
            Directory.CreateDirectory(SnapImageStyle.Directory + NameDirectory);
            for (int i = 0; i < pSteps; i++)
            {
                try
                {
                    AO_CurrentWL = pStartWL + i * AO_StepWL;
                    string local = SnapImageStyle.Directory + NameDirectory + SCRName + "_" + date + "_" + AO_CurrentWL.ToString() + SnapImageStyle.Extension;
                    rval[i].SaveAsTiff(local);
                   // LogMessage("Формат пикселей: " + Massive2Save[i].PixelFormat.ToString());
                }
                catch (Exception e3)
                {
                    LogError("Сохранение " + i.ToString() + " не произошло.");
                    LogError("ORIGINAL: " + e3.Message);
                }
            }
            try
            {
                rval[pSteps].SaveAsTiff(SnapImageStyle.Directory + NameDirectory +SCRName + "_" + date + "_" + pFinishWL.ToString() + SnapImageStyle.Extension);
                // LogMessage("Формат пикселей: " + Massive2Save[i].PixelFormat.ToString());
            }
            catch (Exception e3)
            {
                LogError("Сохранение " + pSteps.ToString() + " не произошло.");
                LogError("ORIGINAL: " + e3.Message);
            }
            SetInactiveDependence(1);
            NUD_CurrentWL.Text = AO_CurrentWL.ToString();
            SetInactiveDependence(0);
        }
        private void New_SnapAndSaveMassive_viaFrequencies(float pStart_MHz, float pFinish_MHz, int MHz_Steps, float[] MHz_Vals = null)
        {

            string date = GetDateString();

            string NameDirectory = GetFullDateString() + "\\";
            int level = 0;
            try
            {
                TIS.Imaging.ImageBuffer[] rval = new TIS.Imaging.ImageBuffer[MHz_Steps + 1];
                ReadAllSettingsFromFile(false);
                int codeerr = 0;
                bool IsNeeded_ExpCurve = TSMI_Load_EXWL_C.Checked;
                double Gain = 0, FPS = 0;
                List<int> wls = new List<int>();
                List<double> exps = new List<double>();
                List<double> exps_ref = new List<double>();
                double Exposure_ref_use_file = (TSMI_UseFileExpAsRef.Checked) ? -1 : AbsValExp.Value;

                List<float> allvalues = new List<float>();
                List<double> Times2SetWL = new List<double>();
                List<double> Times2SnapImage = new List<double>();
                List<double> Times2CopyImage = new List<double>();
                //List<double> Times2CopyImage = new List<double>();
                level = 1;
                if (MHz_Vals == null)
                {
                    for (int i = 0; i < MHz_Steps; i++)
                    {
                        float AO_CurrentHZ = pStart_MHz + i * AO_StepFreq;
                        allvalues.Add(AO_CurrentHZ);
                    }
                    allvalues.Add(pFinish_MHz);
                    if (allvalues[MHz_Steps] == allvalues[MHz_Steps - 1]) allvalues.RemoveAt(MHz_Steps);
                }
                else
                {
                    allvalues = new List<float>(MHz_Vals);
                }
                int psteps2 = allvalues.Count;
                if (icImagingControl1.ImageRingBufferSize < allvalues.Count)
                {
                    icImagingControl1.LiveStop();
                    icImagingControl1.ImageRingBufferSize = allvalues.Count;
                    icImagingControl1.LiveStart();
                }
                level = 2;
                if (WarningofImage) { level = 3; LogError(WarningofImgMessage); }
                else
                {
                    try
                    {
                        codeerr = Filter.Set_Hz(pStart_MHz);//, AOFSimulatorActivated);
                        if (IsNeeded_ExpCurve)
                        {
                            double exposure2use = (TSMI_UseAbsExposure.Checked) ? exps[0] : exps_ref[0];
                            LoadExposure_ToCam(ref AbsValExp, exposure2use);
                            LoadGain(ref vcdProp, Gain);
                        }
                        Thread.Sleep(500);
                        if (codeerr != 0) { throw new Exception(Filter.Implement_Error(codeerr)); };
                    }
                    catch (Exception ex)
                    {
                        LogError(ex.Message);
                    }
                    // System.Threading.Thread.Sleep(20);             
                    Stopwatch SessionDone = new Stopwatch(); SessionDone.Start();
                    level = 4;
                    for (int i = 0; i < psteps2; i++)
                    {

                        if (IsNeeded_ExpCurve)
                        {
                            double exposure2use = (TSMI_UseAbsExposure.Checked) ? exps[i] : exps_ref[i];
                            LoadExposure_ToCam(ref AbsValExp, exposure2use);
                        }

                        Stopwatch swl = new Stopwatch(); swl.Start();
                        //AOF.AOM_SetWL(allvalues[i], AOFSimulatorActivated);
                        Filter.Set_Hz(allvalues[i]);//, AOFSimulatorActivated);


                        swl.Stop();
                        Times2SetWL.Add(swl.Elapsed.TotalMilliseconds);

                        Stopwatch swl2 = new Stopwatch();
                        swl2.Start();
                        curfhs.SnapImage();
                        swl2.Stop();
                        Times2SnapImage.Add(swl2.Elapsed.TotalMilliseconds);

                        Stopwatch swl3 = new Stopwatch(); swl3.Start();
                        rval[i] = curfhs.LastAcquiredBuffer;
                        swl3.Stop();
                        Times2CopyImage.Add(swl3.Elapsed.TotalMilliseconds);

                    }
                    SessionDone.Stop();

                    level = 5;
                    double MediumTime2SWL = 0;
                    double MediumTime2SI = 0;
                    double MediumTime2CI = 0;
                    for (int i = 0; i < Times2SetWL.Count; i++) { MediumTime2SWL += Times2SetWL[i]; }
                    for (int i = 0; i < Times2SnapImage.Count; i++) { MediumTime2SI += Times2SnapImage[i]; }
                    for (int i = 0; i < Times2CopyImage.Count; i++) { MediumTime2CI += Times2CopyImage[i]; }
                    MediumTime2SWL /= Times2SetWL.Count;
                    MediumTime2SI /= Times2SnapImage.Count;
                    MediumTime2CI /= Times2CopyImage.Count;
                    LogMessage("Среднее время на перестройку: " + MediumTime2SWL.ToString());
                    LogMessage("Среднее время на захват: " + MediumTime2SI.ToString());
                    LogMessage("Среднее время на копирование:  " + MediumTime2CI.ToString());
                    LogMessage("Захват кадров завершен. Прошедшее время: " + SessionDone.Elapsed.ToString());
                    LogMessage("Заявленное FPS: " + icImagingControl1.DeviceFrameRate);
                    LogMessage("Реальное   FPS: " + (((double)(psteps2)) / SessionDone.Elapsed.TotalSeconds).ToString());
                    level = 6;
                }
                level = 7;
                string SCRName = CheckScreenShotBasicName();
                Directory.CreateDirectory(SnapImageStyle.Directory + NameDirectory);
                if (!TSMI_MThreadSave.Checked)
                {
                    for (int i = 0; i < psteps2; i++)
                    {
                        try
                        {
                            string value_local = String.Format("{0:0.000}", allvalues[i]).Replace(',', '.');
                            string local = SnapImageStyle.Directory + NameDirectory + SCRName + "_" + date + "_" +
                                value_local + SnapImageStyle.Extension;
                            if (File.Exists(local))
                            {
                                int num = 1;
                                while (File.Exists(local))
                                {
                                    num++;
                                    local = SnapImageStyle.Directory + NameDirectory + SCRName + "_" + date + "_" +
                                value_local + "_" + num.ToString() + SnapImageStyle.Extension;
                                }
                            }
                            rval[i].SaveAsTiff(local);
                            // LogMessage("Формат пикселей: " + Massive2Save[i].PixelFormat.ToString());
                        }
                        catch (Exception e3)
                        {
                            LogError("Сохранение " + i.ToString() + " не произошло.");
                            LogError("ORIGINAL: " + e3.Message);
                        }
                    }
                }
                else
                {
                    List<dynamic> Frames_and_Names_cur = new List<dynamic>();
                    for (int i = 0; i < psteps2; i++)
                    {
                        try
                        {
                            string value_local = String.Format("{0:0.000}", allvalues[i]).Replace(',', '.');
                            string local = SnapImageStyle.Directory + NameDirectory + SCRName + "_" + date + "_" +
                                value_local + SnapImageStyle.Extension;
                            if (File.Exists(local))
                            {
                                int num = 1;
                                while (File.Exists(local))
                                {
                                    num++;
                                    local = SnapImageStyle.Directory + NameDirectory + SCRName + "_" + date + "_" +
                                value_local + "_" + num.ToString() + SnapImageStyle.Extension;
                                }
                            }
                            Frames_and_Names_cur.Add(new { Buffer = rval[i], Name = local });
                            // Frames_and_Names_cur[0].Buffer.Dispose();

                            // LogMessage("Формат пикселей: " + Massive2Save[i].PixelFormat.ToString());
                        }
                        catch (Exception e3)
                        {
                            LogError("Сохранение " + i.ToString() + " не произошло.");
                            LogError("ORIGINAL: " + e3.Message);
                        }
                    }

                    IMG_buffers_mass.Add(new { Frames_and_Names = new List<dynamic>(Frames_and_Names_cur), Dir_name = NameDirectory.Substring(0, NameDirectory.Count() - 1) });
                    if (!BGW_Saver.IsBusy) BGW_Saver.RunWorkerAsync();
                }
                //rval = null;
                level = 8;
                SetInactiveDependence(1);
                NUD_CurrentWL.Text = AO_CurrentWL.ToString();
                SetInactiveDependence(0);
                level = 9;
            }
            catch (Exception e)
            {
                Log.Error("Произошла ошибка во время захвата или сохранения. Этап выполнения функции:" + level.ToString());
                Log.Error("ORIGINAL: " + e.Message);
            }
        }
        private void New_SnapAndSaveMassive_viaWLs(float pStartWL, float pFinishWL, int pSteps,float[] WLVals = null)
        {

            string date = GetDateString();

            string NameDirectory = GetFullDateString() + "\\";
            int level = 0;
            try
            {
                TIS.Imaging.ImageBuffer[] rval = new TIS.Imaging.ImageBuffer[pSteps + 1];
                ReadAllSettingsFromFile(false);
                int codeerr = 0;
                bool IsNeeded_ExpCurve = TSMI_Load_EXWL_C.Checked;
                double Gain = 0, FPS = 0;
                List<int> wls = new List<int>();
                List<double> exps = new List<double>();
                List<double> exps_ref = new List<double>();
                double Exposure_ref_use_file = (TSMI_UseFileExpAsRef.Checked) ? -1 : AbsValExp.Value; 
                if (IsNeeded_ExpCurve)
                {
                    LDZ_Code.ExpCurve.Get_Interpolated_WlExpCurveFromDirectory(WayToCurv_exp, (int)Filter.WL_Min, (int)Filter.WL_Max,
                        (int)pStartWL, (int)pFinishWL, (int)AO_StepWL,
                        ref wls, ref exps, ref exps_ref,
                        ref Gain, ref FPS,ref Exposure_ref_use_file);
                }

                List<float> allvalues = new List<float>();
                List<double> Times2SetWL = new List<double>();
                List<double> Times2SnapImage = new List<double>();
                List<double> Times2CopyImage = new List<double>();
                //List<double> Times2CopyImage = new List<double>();
                level = 1;
                if (WLVals == null)
                {
                    for (int i = 0; i < pSteps; i++)
                    {
                        AO_CurrentWL = pStartWL + i * AO_StepWL;
                        allvalues.Add(AO_CurrentWL);
                    }
                    allvalues.Add(pFinishWL);
                    if (allvalues[pSteps] == allvalues[pSteps - 1]) allvalues.RemoveAt(pSteps);
                }
                else
                {
                    allvalues = new List<float>(WLVals);
                }
                int psteps2 = allvalues.Count;
                if (icImagingControl1.ImageRingBufferSize < allvalues.Count)
                {
                    icImagingControl1.LiveStop();
                    icImagingControl1.ImageRingBufferSize = allvalues.Count;
                    icImagingControl1.LiveStart();
                }
                level = 2;
                if (WarningofImage) { level = 3; LogError(WarningofImgMessage); }
                else
                {
                    try
                    {
                        codeerr = Filter.Set_Wl(pStartWL);//, AOFSimulatorActivated);
                        if (IsNeeded_ExpCurve)
                        {
                            double exposure2use = (TSMI_UseAbsExposure.Checked) ?  exps[0] : exps_ref[0];
                            LoadExposure_ToCam(ref AbsValExp, exposure2use);
                            LoadGain(ref vcdProp, Gain);
                        }
                        Thread.Sleep(500);
                        if (codeerr != 0) { throw new Exception(Filter.Implement_Error(codeerr)); };
                    }
                    catch (Exception ex)
                    {
                        LogError(ex.Message);
                    }
                    // System.Threading.Thread.Sleep(20);             
                    Stopwatch SessionDone = new Stopwatch(); SessionDone.Start();
                    level = 4;
                    for (int i = 0; i < psteps2; i++)
                    {
                       
                        if (IsNeeded_ExpCurve)
                        {
                            double exposure2use = (TSMI_UseAbsExposure.Checked) ? exps[i] : exps_ref[i];
                            LoadExposure_ToCam(ref AbsValExp, exposure2use);
                        }

                        Stopwatch swl = new Stopwatch(); swl.Start();
                        //AOF.AOM_SetWL(allvalues[i], AOFSimulatorActivated);
                        Filter.Set_Wl(allvalues[i]);//, AOFSimulatorActivated);


                        swl.Stop();
                        Times2SetWL.Add(swl.Elapsed.TotalMilliseconds);

                        Stopwatch swl2 = new Stopwatch();
                        swl2.Start();
                        curfhs.SnapImage();
                        swl2.Stop();
                        Times2SnapImage.Add(swl2.Elapsed.TotalMilliseconds);

                        Stopwatch swl3 = new Stopwatch(); swl3.Start();
                        rval[i] = curfhs.LastAcquiredBuffer;
                        swl3.Stop();
                        Times2CopyImage.Add(swl3.Elapsed.TotalMilliseconds);

                    }
                    SessionDone.Stop();

                    level = 5;
                    double MediumTime2SWL = 0;
                    double MediumTime2SI = 0;
                    double MediumTime2CI = 0;
                    for (int i = 0; i < Times2SetWL.Count; i++) { MediumTime2SWL += Times2SetWL[i]; }
                    for (int i = 0; i < Times2SnapImage.Count; i++) { MediumTime2SI += Times2SnapImage[i]; }
                    for (int i = 0; i < Times2CopyImage.Count; i++) { MediumTime2CI += Times2CopyImage[i]; }
                    MediumTime2SWL /= Times2SetWL.Count;
                    MediumTime2SI /= Times2SnapImage.Count;
                    MediumTime2CI /= Times2CopyImage.Count;
                    LogMessage("Среднее время на перестройку: " + MediumTime2SWL.ToString());
                    LogMessage("Среднее время на захват: " + MediumTime2SI.ToString());
                    LogMessage("Среднее время на копирование:  " + MediumTime2CI.ToString());
                    LogMessage("Захват кадров завершен. Прошедшее время: " + SessionDone.Elapsed.ToString());
                    LogMessage("Заявленное FPS: " + icImagingControl1.DeviceFrameRate);
                    LogMessage("Реальное   FPS: " + (((double)(psteps2)) / SessionDone.Elapsed.TotalSeconds).ToString());
                    level = 6;
                }
                level = 7;
                string SCRName = CheckScreenShotBasicName();
                Directory.CreateDirectory(SnapImageStyle.Directory + NameDirectory);
                if (!TSMI_MThreadSave.Checked)
                {
                    for (int i = 0; i < psteps2; i++)
                    {
                        try
                        {
                            string local = SnapImageStyle.Directory + NameDirectory + SCRName + "_" + date + "_" +
                                ((int)allvalues[i]).ToString() + SnapImageStyle.Extension;
                            if (File.Exists(local))
                            {
                                int num = 1;
                                while (File.Exists(local))
                                {
                                    num++;
                                    local = SnapImageStyle.Directory + NameDirectory + SCRName + "_" + date + "_" +
                                ((int)allvalues[i]).ToString() + "_" + num.ToString() + SnapImageStyle.Extension;
                                }
                            }
                            rval[i].SaveAsTiff(local);
                            // LogMessage("Формат пикселей: " + Massive2Save[i].PixelFormat.ToString());
                        }
                        catch (Exception e3)
                        {
                            LogError("Сохранение " + i.ToString() + " не произошло.");
                            LogError("ORIGINAL: " + e3.Message);
                        }
                    }
                }
                else
                {
                    List<dynamic> Frames_and_Names_cur = new List<dynamic>();
                    for (int i = 0; i < psteps2; i++)
                    {
                        try
                        {
                            string local = SnapImageStyle.Directory + NameDirectory + SCRName + "_" + date + "_" +
                                ((int)allvalues[i]).ToString() + SnapImageStyle.Extension;
                            if (File.Exists(local))
                            {
                                int num = 1;
                                while (File.Exists(local))
                                {
                                    num++;
                                    local = SnapImageStyle.Directory + NameDirectory + SCRName + "_" + date + "_" +
                                ((int)allvalues[i]).ToString() + "_" + num.ToString() + SnapImageStyle.Extension;
                                }
                            }
                            Frames_and_Names_cur.Add(new { Buffer = rval[i], Name = local });
                            // Frames_and_Names_cur[0].Buffer.Dispose();

                            // LogMessage("Формат пикселей: " + Massive2Save[i].PixelFormat.ToString());
                        }
                        catch (Exception e3)
                        {
                            LogError("Сохранение " + i.ToString() + " не произошло.");
                            LogError("ORIGINAL: " + e3.Message);
                        }
                    }

                    IMG_buffers_mass.Add(new { Frames_and_Names = new List<dynamic>(Frames_and_Names_cur), Dir_name = NameDirectory.Substring(0, NameDirectory.Count() - 1) });
                    if (!BGW_Saver.IsBusy) BGW_Saver.RunWorkerAsync();
                }
                //rval = null;
                level = 8;
                SetInactiveDependence(1);
                NUD_CurrentWL.Text = AO_CurrentWL.ToString();
                SetInactiveDependence(0);
                level = 9;
            }
            catch(Exception e)
            {
                Log.Error("Произошла ошибка во время захвата или сохранения. Этап выполнения функции:" + level.ToString());
                Log.Error("ORIGINAL: " + e.Message);
            }
        }
        private void New_SnapAndSaveMassive_viaWLs_10022021(float pStartWL, float pFinishWL, int pSteps, float[] WLVals = null)
        {
            ReadAllSettingsFromFile(false);
            string date = GetDateString();
            string NameDirectory = GetFullDateString() + "\\";
            string SCRName = CheckScreenShotBasicName();

            NameDirectory = Path.Combine(SnapImageStyle.Directory, NameDirectory);
            
            string LocalDir = NameDirectory;
            if (Directory.Exists(NameDirectory))
            {
                int num = 1;
                while (Directory.Exists(LocalDir))
                {
                    num++;
                    LocalDir = NameDirectory.Substring(0, NameDirectory.Length - 3) + "(" + num.ToString() + ")" + "\\";
                }
            }
            NameDirectory = LocalDir;

            Directory.CreateDirectory(NameDirectory); //Read directories

            try
            {

                List<float> WLs = new List<float>();
                if (WLVals == null) //Calculate WLs
                {
                    for (int i = 0; i < pSteps; i++)                  
                        WLs.Add(pStartWL + i * AO_StepWL);                   
                    WLs.Add(pFinishWL);
                    if (WLs[pSteps] == WLs[pSteps - 1]) WLs.RemoveAt(pSteps);
                }
                else
                {
                    WLs = new List<float>(WLVals);
                }

                LDZ_Code.HyperSpectralGrabber HSGrabber = new LDZ_Code.HyperSpectralGrabber(

                    new
                    {
                        FFS = CurFSS,
                        AOF = Filter,
                        SAVER = TSMI_MThreadSave.Checked ? MSaver : null,
                        LOG = new Action<string>((message) => LogMessage(message))
                    },
                    new
                    {
                        WLS = WLs,
                        PATH = NameDirectory,
                        PREFIX = SCRName,
                        EXTENSION = SnapImageStyle.Extension,
                        ONWLS = true                       
                    });

                HSGrabber.OnProgressChanged += HSGrabber_OnProgressChanged;

                if (TSMI_MThreadSave.Checked)
                {
                    HSGrabber.OnSerieFinished += (() => Swith_interface_while_grabbing(true));
                    HSGrabber.OnSerieStarted += (() => Swith_interface_while_grabbing(false));
                }
                else
                {
                    HSGrabber.OnSerieFinished += (() => Swith_interface_while_grabbing(true, false, false));
                    HSGrabber.OnSerieStarted += (() => Swith_interface_while_grabbing(false, false, true));
                }

                HSGrabber.StartGrabbing();
                SetInactiveDependence(1);
                NUD_CurrentWL.Text = AO_CurrentWL.ToString();
                SetInactiveDependence(0);
            }
            catch (Exception e)
            {
               // Log.Error("Произошла ошибка во время захвата или сохранения. Этап выполнения функции:" + level.ToString());
                Log.Error("ORIGINAL: " + e.Message);
            }
        }

        private void New_SnapAndSaveMassive_viaFreqs_10022021(float pStart_MHz, float pFinish_MHz, int MHz_Steps, float[] MHz_Vals = null)
        {
            ReadAllSettingsFromFile(false);
            string date = GetDateString();
            string NameDirectory = GetFullDateString() + "\\";
            string SCRName = CheckScreenShotBasicName();

            NameDirectory = Path.Combine(SnapImageStyle.Directory, NameDirectory);

            string LocalDir = NameDirectory;
            if (Directory.Exists(NameDirectory))
            {
                int num = 1;
                while (Directory.Exists(LocalDir))
                {
                    num++;
                    LocalDir = NameDirectory.Substring(0, NameDirectory.Length - 3) + "(" + num.ToString() + ")" + "\\";
                }
            }
            NameDirectory = LocalDir;

            Directory.CreateDirectory(NameDirectory); //Read directories

            try
            {

                List<float> Freqs = new List<float>();

                if (MHz_Vals == null) //Calculate WLs
                {
                    for (int i = 0; i < MHz_Steps; i++)
                        Freqs.Add(pStart_MHz + i * AO_StepFreq);
                    Freqs.Add(pFinish_MHz);
                    if (Freqs[MHz_Steps] == Freqs[MHz_Steps - 1]) Freqs.RemoveAt(MHz_Steps);
                }
                else
                {
                    Freqs = new List<float>(MHz_Vals);
                }

                LDZ_Code.HyperSpectralGrabber HSGrabber = new LDZ_Code.HyperSpectralGrabber(

                    new
                    {
                        FFS = CurFSS,
                        AOF = Filter,
                        SAVER = TSMI_MThreadSave.Checked ? MSaver : null,
                        LOG = new Action<string>((message) => LogMessage(message))
                    },
                    new
                    {
                        WLS = Freqs,
                        PATH = NameDirectory,
                        PREFIX = SCRName,
                        EXTENSION = SnapImageStyle.Extension,
                        ONWLS = false
                    });

                HSGrabber.OnProgressChanged += HSGrabber_OnProgressChanged;
                if (TSMI_MThreadSave.Checked)
                {
                    HSGrabber.OnSerieFinished += (() => Swith_interface_while_grabbing(true));
                    HSGrabber.OnSerieStarted += (() => Swith_interface_while_grabbing(false));
                }
                else
                {
                    HSGrabber.OnSerieFinished += (() => Swith_interface_while_grabbing(true, false, false));
                    HSGrabber.OnSerieStarted += (() => Swith_interface_while_grabbing(false, false, true));
                }

                HSGrabber.StartGrabbing();
                SetInactiveDependence(1);
                NUD_CurrentWL.Text = AO_CurrentWL.ToString();
                SetInactiveDependence(0);
            }
            catch (Exception e)
            {
                // Log.Error("Произошла ошибка во время захвата или сохранения. Этап выполнения функции:" + level.ToString());
                Log.Error("ORIGINAL: " + e.Message);
            }
        }

        private void Swith_interface_while_grabbing(bool State, bool SavingVisible = true, bool AqVisible = true)
        {
            tabControl1.Invoke(new Action(() => { tabControl1.Enabled = State; }));

            if (!(SavingVisible && AqVisible) || !State) //24022021. If we use manual control of visibility, it's necessary to show the panel here, not in saver
            {
                TLP_Saving_of_Frames.Invoke(new Action(() => { TLP_Saving_of_Frames.Visible = !State; }));


                PB_Saving.Invoke(new Action(() => { PB_Saving.Visible = SavingVisible; }));
                L_Saved.Invoke(new Action(() => { L_Saved.Visible = SavingVisible; }));
                L_Info_Saved.Invoke(new Action(() => { L_Info_Saved.Visible = SavingVisible; }));

                PB_SeriesProgress.Invoke(new Action(() => { PB_SeriesProgress.Visible = AqVisible; }));
                L_Aquired.Invoke(new Action(() => { L_Aquired.Visible = AqVisible; }));
                L_Info_Aquired.Invoke(new Action(() => { L_Info_Aquired.Visible = AqVisible; }));

                if (!AqVisible)
                { PB_SeriesProgress.Invoke(new Action(() => { PB_SeriesProgress.Value = 0; })); }
                if (!SavingVisible)
                { PB_Saving.Invoke(new Action(() => { PB_Saving.Value = 0; })); }
            }
            
        }

        private void HSGrabber_OnProgressChanged(int frames_gotten,int frames_2_got)
        {
            int Progress = (int)(((float)frames_gotten/(float)(frames_2_got - 1))*100);
            string data = String.Format("{0}/{1}", frames_gotten+1, frames_2_got);

            if(PB_SeriesProgress.InvokeRequired)
            {
                PB_SeriesProgress.Invoke(new Action(() => { PB_SeriesProgress.Value = Progress; }));
                L_Aquired.Invoke(new Action(() => { L_Aquired.Text = data; }));
            }
            else
            {
                PB_SeriesProgress.Value = Progress;
                L_Aquired.Text = data;
            }
        }

        private void New_SnapAndSaveMassive_viaWLs_08022021(float pStartWL, float pFinishWL, int pSteps, float[] WLVals = null)
        {

            string date = GetDateString();

            string NameDirectory = GetFullDateString() + "\\";
            int level = 0;
            try
            {
                string SCRName = CheckScreenShotBasicName();
                Directory.CreateDirectory(SnapImageStyle.Directory + NameDirectory);

                Queue<TIS.Imaging.ImageBuffer> rval = new Queue<TIS.Imaging.ImageBuffer>();
                Queue<string> names = new Queue<string>();
                ReadAllSettingsFromFile(false);
                int codeerr = 0;
                bool IsNeeded_ExpCurve = TSMI_Load_EXWL_C.Checked;
                double Gain = 0, FPS = 0;
                List<int> wls = new List<int>();
                List<double> exps = new List<double>();
                List<double> exps_ref = new List<double>();
                double Exposure_ref_use_file = (TSMI_UseFileExpAsRef.Checked) ? -1 : AbsValExp.Value;
                if (IsNeeded_ExpCurve)
                {
                    LDZ_Code.ExpCurve.Get_Interpolated_WlExpCurveFromDirectory(WayToCurv_exp, (int)Filter.WL_Min, (int)Filter.WL_Max,
                        (int)pStartWL, (int)pFinishWL, (int)AO_StepWL,
                        ref wls, ref exps, ref exps_ref,
                        ref Gain, ref FPS, ref Exposure_ref_use_file);
                }

                List<float> allvalues = new List<float>();
                List<double> Times2SetWL = new List<double>();
                List<double> Times2SnapImage = new List<double>();
                List<double> Times2CopyImage = new List<double>();
                level = 1;
                if (WLVals == null)
                {
                    for (int i = 0; i < pSteps; i++)
                    {
                        AO_CurrentWL = pStartWL + i * AO_StepWL;
                        allvalues.Add(AO_CurrentWL);
                    }
                    allvalues.Add(pFinishWL);
                    if (allvalues[pSteps] == allvalues[pSteps - 1]) allvalues.RemoveAt(pSteps);
                }
                else
                {
                    allvalues = new List<float>(WLVals);
                }
                int psteps2 = allvalues.Count;

                level = 2;
                if (WarningofImage) { level = 3; LogError(WarningofImgMessage); }
                else
                {
                    try
                    {
                        codeerr = Filter.Set_Wl(pStartWL);//, AOFSimulatorActivated);
                        if (IsNeeded_ExpCurve)
                        {
                            double exposure2use = (TSMI_UseAbsExposure.Checked) ? exps[0] : exps_ref[0];
                            LoadExposure_ToCam(ref AbsValExp, exposure2use);
                            LoadGain(ref vcdProp, Gain);
                        }
                        Thread.Sleep(500);
                        if (codeerr != 0) { throw new Exception(Filter.Implement_Error(codeerr)); };
                    }
                    catch (Exception ex)
                    {
                        LogError(ex.Message);
                    }
                    // System.Threading.Thread.Sleep(20);             
                    Stopwatch SessionDone = new Stopwatch(); SessionDone.Start();
                    level = 4;

                    string FullPrefix = SnapImageStyle.Directory + NameDirectory + SCRName + "_" + date + "_";
                    MSaver.OpenSerie(allvalues.Count(), NameDirectory);
                    for (int i = 0; i < psteps2; i++)
                    {

                        if (IsNeeded_ExpCurve)
                        {
                            double exposure2use = (TSMI_UseAbsExposure.Checked) ? exps[i] : exps_ref[i];
                            LoadExposure_ToCam(ref AbsValExp, exposure2use);
                        }
                        //Вычисление нового имени
                        string local = FullPrefix + ((int)allvalues[i]).ToString() + SnapImageStyle.Extension;
                        if (File.Exists(local))
                        {
                            int num = 1;
                            while (File.Exists(local))
                            {
                                num++;
                                local = FullPrefix + ((int)allvalues[i]).ToString() + "_" + num.ToString() + SnapImageStyle.Extension;
                            }
                        }

                        Stopwatch swl = new Stopwatch(); swl.Start();
                        Filter.Set_Wl(allvalues[i]);//, AOFSimulatorActivated);                   
                        swl.Stop();
                        Times2SetWL.Add(swl.Elapsed.TotalMilliseconds);
                        //
                        Stopwatch swl2 = new Stopwatch();
                        swl2.Start();
                        //curfhs.SnapImage();
                        swl2.Stop();
                        Times2SnapImage.Add(swl2.Elapsed.TotalMilliseconds);
                        //
                        Stopwatch swl3 = new Stopwatch(); swl3.Start();
                        
                        swl3.Stop();
                        Times2CopyImage.Add(swl3.Elapsed.TotalMilliseconds);
                        

                        MSaver.EnqueFrame(CurFSS.SnapSingle(TimeSpan.FromSeconds(AbsValExp.RangeMax)), local);

                        //Bitmap data_bmp = curfhs.LastAcquiredBuffer.

                    }
                    MSaver.CloseSerie();

                    SessionDone.Stop();

                    level = 5;
                    double MediumTime2SWL = 0;
                    double MediumTime2SI = 0;
                    double MediumTime2CI = 0;
                    for (int i = 0; i < Times2SetWL.Count; i++) { MediumTime2SWL += Times2SetWL[i]; }
                    for (int i = 0; i < Times2SnapImage.Count; i++) { MediumTime2SI += Times2SnapImage[i]; }
                    for (int i = 0; i < Times2CopyImage.Count; i++) { MediumTime2CI += Times2CopyImage[i]; }
                    MediumTime2SWL /= Times2SetWL.Count;
                    MediumTime2SI /= Times2SnapImage.Count;
                    MediumTime2CI /= Times2CopyImage.Count;
                    LogMessage("Среднее время на перестройку: " + MediumTime2SWL.ToString());
                    LogMessage("Среднее время на захват: " + MediumTime2SI.ToString());
                    LogMessage("Среднее время на копирование:  " + MediumTime2CI.ToString());
                    LogMessage("Захват кадров завершен. Прошедшее время: " + SessionDone.Elapsed.ToString());
                    LogMessage("Заявленное FPS: " + icImagingControl1.DeviceFrameRate);
                    LogMessage("Реальное   FPS: " + (((double)(psteps2)) / SessionDone.Elapsed.TotalSeconds).ToString());
                    level = 6;
                }
                level = 7;
                

                //rval = null;
                level = 8;
                SetInactiveDependence(1);
                NUD_CurrentWL.Text = AO_CurrentWL.ToString();
                SetInactiveDependence(0);
                level = 9;
            }
            catch (Exception e)
            {
                Log.Error("Произошла ошибка во время захвата или сохранения. Этап выполнения функции:" + level.ToString());
                Log.Error("ORIGINAL: " + e.Message);
            }
        }
        private void SaveMassive(Bitmap[] Massive2Save)
        {
            string SCRName = CheckScreenShotBasicName();
            Bitmap /*orig,*/ clone; int curWidth, curHeight;
            System.Drawing.Imaging.ImageFormat format = null; Graphics gr;
            if (SnapImageStyle.Extension == ".bmp") { format = System.Drawing.Imaging.ImageFormat.Bmp; }
            else if (SnapImageStyle.Extension == ".jpg") { format = System.Drawing.Imaging.ImageFormat.Jpeg; }
            else { format = System.Drawing.Imaging.ImageFormat.Tiff; }
            int Number = Massive2Save.Count();
            var List2 = new List<Bitmap>();
            // Bitmap[] Mass2 = new Bitmap[Number];
            System.Drawing.Imaging.PixelFormat CurrentPXF = (System.Drawing.Imaging.PixelFormat)CBSavingPixelFormat.SelectedItem;
            try { curWidth = Massive2Save[0].Width; curHeight = Massive2Save[0].Height; }
            catch { LogError("Сохранение не произведено"); return; }
            string date = GetDateString();
            List<int> Mistakes = new List<int>();
            //конвертирование форматов
             for (int i = 0; i < Number; i++)
             {
                // orig = new Bitmap(Massive2Save[i]);
                 try
                 {
                     clone = new Bitmap(Massive2Save[i].Width, Massive2Save[i].Height, (System.Drawing.Imaging.PixelFormat)CBFinalPixelFormat.SelectedItem);
                   //  clone = new Bitmap(orig.Width, orig.Height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                 }
                 catch (Exception e2)
                 {
                     LogError(i.ToString() + " " + e2.Message);
                     Mistakes.Add(i);
                     clone = new Bitmap(Massive2Save[i].Width, Massive2Save[i].Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                 }
                 try
                 {
                     using (gr = Graphics.FromImage(clone))
                     {
                         gr.DrawImage(Massive2Save[i], new Rectangle(0, 0, clone.Width, clone.Height));
                     }
                     LogMessage("Формат пикселей " + clone.PixelFormat.ToString() + " установлен");
                 }
                 catch (Exception e1)
                 {
                     LogError(i.ToString()+" Формат пикселей " + clone.PixelFormat.ToString() + " не установлен");
                     LogMessage("Причина: " + e1.ToString());
                     Mistakes.Add(i);
                 }
                 Massive2Save[i] = clone;
             }
             

            for (int i = 0; i < Number; i++)
            {
                try
                {
                    AO_CurrentWL = AO_MinimumWL + i * AO_StepWL;
                    Massive2Save[i].Save(SnapImageStyle.Directory + SCRName + "_" + date + "_" + AO_CurrentWL.ToString() + SnapImageStyle.Extension, format);
                    LogMessage("Формат пикселей: " + Massive2Save[i].PixelFormat.ToString());
                }
                catch (Exception e3)
                {
                    LogError("Сохранение " + i.ToString() + " " + e3.Message);
                }
            }
            
            SetInactiveDependence(1);
            NUD_CurrentWL.Text = AO_CurrentWL.ToString();
            SetInactiveDependence(0);
        }
        
        private TIS.Imaging.ImageBuffer GrabImage(Guid colorFormat)
        {
            bool wasLive = icImagingControl1.LiveVideoRunning;
            icImagingControl1.LiveStop();
            TIS.Imaging.BaseSink oldSink = icImagingControl1.Sink;
            TIS.Imaging.FrameHandlerSink fhs = new TIS.Imaging.FrameHandlerSink();
            fhs.FrameTypes.Add(new TIS.Imaging.FrameType(colorFormat));
            icImagingControl1.Sink = fhs;

            try
            {
                icImagingControl1.LiveStart();
            }
            catch (TIS.Imaging.ICException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

            TIS.Imaging.ImageBuffer rval = null;

            try
            {
                fhs.SnapImage((int)(2000.0f));
                rval = fhs.LastAcquiredBuffer;
            }
            catch (TIS.Imaging.ICException ex)
            {
                MessageBox.Show(ex.Message);
            }

            icImagingControl1.LiveStop();

            icImagingControl1.Sink = oldSink;

            if (wasLive) icImagingControl1.LiveStart();

            return rval;
        }

        private void Save_3WL_image()
        {

        }
        private void Load_properties_for_WL_ctrls(decimal WL_MAX, decimal WL_MIN,decimal EXPOSURE_CURRENT = 8, decimal TIME_MAX=100, decimal TIME_MIN=0)
        {
            for (int i = 0; i < WLS_at_all; ++i)
            {
                //поиск элемента управления с заданным номером
                var EXP_control = this.Controls.Find("NUD_Multi_ex_time" + (i + 1).ToString(), true)[0] as NumericUpDown;
                EXP_control.Maximum = TIME_MAX;
                EXP_control.Minimum = TIME_MIN;
                EXP_control.Value = (decimal)((double)EXPOSURE_CURRENT / ((double)WLS_at_all));
                //поиск элемента управления с заданным номером
                var WL_control = this.Controls.Find("NUD_Multi_WL" + (i + 1).ToString(), true)[0] as NumericUpDown;
                WL_control.Maximum = WL_MAX;
                WL_control.Minimum = WL_MIN;
                WL_control.Value = (int)((WL_MAX + WL_MIN) / 2); 
            }

        }
        private void NewWrite16Bit(ref ImageBuffer buf,string name)
        {
            UInt32 px1 = ReadY16(buf, 0, 0);
            LogMessage("PX 0,0 is " +px1.ToString());
         /*   WriteY16(ref buf, 0, 0, 0xFFFF);
            WriteY16(ref buf, 1, 1, 0xFFFF);
            WriteY16(ref buf, 2, 2, 0xFFFF);
            WriteY16(ref buf, 3, 3, 0xFFFF);
            WriteY16(ref buf, 4, 4, 0xFFFF);*/
            buf.SaveAsTiff(name + ".tiff");
        }
        private UInt16 ReadY16(TIS.Imaging.ImageBuffer buf, int row, int col)
        {
            // Y16 is top-down, the first line has index 0
            int offset = row * buf.BytesPerLine + col * 2;

            return (UInt16)System.Runtime.InteropServices.Marshal.ReadInt16(buf.GetIntPtr(), offset);
        }
        private void WriteY16(ref TIS.Imaging.ImageBuffer buf, int row, int col, UInt16 value)
        {
            int offset = row * buf.BytesPerLine + col * 2;

            System.Runtime.InteropServices.Marshal.WriteInt16(buf.GetIntPtr(), offset, (short)value);
        }
        private string GetDateString()
        {
            try
            {
                string res = DateTime.Today.ToString();
                return ((res.Substring(0, res.IndexOf(' '))).Remove(res.IndexOf('.'), 1)).Remove(res.LastIndexOf('.') - 1, 1);
            }
            catch { return "date"; }           
        }
        public static string GetFullDateString()
        {
            try
            {
                return DateTime.Now.ToString().Replace('.', '_').Replace(' ', '_').Replace(':', '_');
            }
            catch { return "date_time"; }   
        }
        public static string GetTimeString()
        {
            try { return (DateTime.Now.ToString().Replace('.', '_').Replace(' ', '_').Replace(':', '_')).Substring(11); }
            catch { return "time"; }
        }
        private void OpenDevSearcher()
        {

            OpenFileDialog OPF = new OpenFileDialog();
            OPF.InitialDirectory = Application.StartupPath;
            OPF.Filter = "DEV config files (*.dev)|*.dev|All files (*.*)|*.*";
            OPF.FilterIndex = 0;
            OPF.RestoreDirectory = true;

            if (OPF.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    int k = 1 + OPF.FileName.LastIndexOf('\\');
                    L_RealDevName.Text = OPF.FileName.Substring(k, OPF.FileName.Length - k);
                }
                catch (Exception ex)
                {
                    LogError("Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
        private string OpenExpcfgSearcher()
        {
            string result = "";
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.InitialDirectory = Application.StartupPath;
            OPF.Filter = "Exposure curve files (*.expcurv)|*.expcurv| Exposure FullCurve files (*.fexpcurv)|*.fexpcurv|All files (*.*)|*.*";
            OPF.FilterIndex = 0;
            OPF.RestoreDirectory = true;

            if (OPF.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    int k = 1 + OPF.FileName.LastIndexOf('\\');
                    result = OPF.FileName.Substring(k, OPF.FileName.Length - k);
                }
                catch (Exception ex)
                {
                    LogError("Could not read file from disk. Original error: " + ex.Message);
                }
            }
            return result;
        }
       public static double PerfectRounding(double val, int CharN)
        {
            double mnoj = (float)Math.Pow(10.0, CharN);
            val *= mnoj;
            if (Math.Abs(val - (Int64)val) > 0.5f) return (((Int64)val + 1 * Math.Sign(val - (int)val)) / mnoj);
            else return ((Int64)val / mnoj);
        }
        

        private void TrBWL_OnScroll()
        {
            int codeerr = 0;
            float wl_current = 0;
            wl_current = (float)(TrB_CurrentWL.Value/AO_WL_precision);      
            if(AutoSetActivated)
            try
            {
                
                codeerr = Filter.Set_Wl(wl_current);//, AOFSimulatorActivated);
                if (codeerr != 0) { throw new Exception(Filter.Implement_Error(codeerr)); }
                else LogMessage(NUD_CurrentWL.Text + " wave lenght has been set!");
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
            }

            NUD_CurrentWL.Value = (decimal)wl_current;
            NUD_CurrentMHz.Value = (decimal)Filter.Get_HZ_via_WL(wl_current);
            TrB_CurrentMHz.Value =(int)((double)NUD_CurrentMHz.Value * AO_HZ_precision);
        }
        private void TrB_MHz_OnScroll()
        {
            int codeerr = 0;
            float current_MHz = 0;
            current_MHz = TrB_CurrentMHz.Value / (float)AO_HZ_precision;
            
            if (AutoSetActivated)
                try
                {
                    codeerr = Filter.Set_Hz(current_MHz);//, AOFSimulatorActivated);
                    if (codeerr != 0) { throw new Exception(Filter.Implement_Error(codeerr)); }
                    else LogMessage(NUD_CurrentMHz.Text + " sound frequency has been set!");
                }
                catch (Exception ex)
                {
                    LogError(ex.Message);
                }

            NUD_CurrentMHz.Value = (decimal)(current_MHz);
            NUD_CurrentWL.Value = (decimal)(Filter.Get_WL_via_HZ(current_MHz));
            TrB_CurrentWL.Value = (int)((double)NUD_CurrentWL.Value * AO_WL_precision);
           
        }
       
        private void BSetWLOnClick()
        {
            int codeerr = 0;
            try
            {
                codeerr = Filter.Set_Wl(Convert.ToInt32(NUD_CurrentWL.Value));//, AOFSimulatorActivated);
                if (codeerr != 0) { throw new Exception(Filter.Implement_Error(codeerr)); }
                else LogMessage(NUD_CurrentWL.Text + " wave lenght has been set!");
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
            }
        }

        private string CheckScreenShotBasicName()
        {
            string result = "ScreenShot";
            if ((TBNamePrefix.Text != "") && (TBNamePrefix.Text != null)) result = TBNamePrefix.Text;
            return result;
        }
        private int CalculateSteps_viaWLs()
        {
            AO_StepWL = Convert.ToInt16(NUD_StepL.Value);
            AO_StartWL = Convert.ToInt32(NUD_StartL.Value);
            AO_EndWL = Convert.ToInt32(NUD_FinishL.Value);
            int steps = (int)((AO_EndWL - AO_StartWL) / AO_StepWL) + 1;
            return steps;
        }
        private int CalculateSteps_viaMHzs()
        {
            AO_StepFreq = (float)NUD_FreqStep.Value;
            AO_StartFreq = (float)NUD_FreqStart.Value;
            AO_EndFreq = (float)NUD_FreqFin.Value;
            int steps = (int)((AO_EndFreq - AO_StartFreq) / AO_StepFreq) + 1;
            return steps;
        }
        private void DisableAlltheShit()
        {
            TBROIHeight.Enabled = false;
            TBROIWidth.Enabled = false;
            TBROIPointX.Enabled = false;
            TBROIPointY.Enabled = false;

           /* TBStartN.Enabled = false;
            TBFinishN.Enabled = false;
            textBox3.Enabled = false;*/

            CBFinalPixelFormat.Enabled = false;

            label19.Visible = false;
            CBFinalPixelFormat.Visible = false;
            CBoxPixelFormat.Enabled = false;
            CBSavingPixelFormat.Enabled = false;
        }
        private float ConvertWL2WN(float WL) {  return 10000000.0f / (WL); }
        private float ConvertWN2WL(float WN) { return 10000000.0f / (WN); }

        private void TestAvailability(bool Visual)
        {
            if(Visual) LogMessage(DateTime.Today.ToString());                
            var ic = icImagingControl1;
            try
            {
                ChBROIAutoCent.Enabled = ACenterAvailible = Is_Partial_scan_Auto_center_Available(ic);
                if (Visual) LogMessage("Существование функции центрирования ROI: " + ACenterAvailible.ToString());
                if (ACenterAvailible)
                {
                    if (Get_Partial_scan_Auto_center_Switch(ic))
                        if (Visual) LogMessage("Функция центрирования ROI включена");
                        else if (Visual) LogMessage("Функция центрирования ROI отключена");
                }
            }
            catch (Exception e) { LogError(e.Message); }
            try
            {
                Set_Partial_scan_Auto_center_Switch(icImagingControl1, true);
                if (Visual) LogMessage("Попытка включения функции центрирования ROI программно. Состояние: " +
                    Get_Partial_scan_Auto_center_Switch(ic).ToString());
                ChBROIAutoCent.Checked = Get_Partial_scan_Auto_center_Switch(ic);
            }
            catch (Exception e) { LogError(e.Message); }
            try
            {
                TBROIPointX.Enabled = ScanXOffsetAvailible = IsPartial_scanX_OffsetAvailable(ic);
                TBROIPointY.Enabled = ScanYOffsetAvailible = IsPartial_scanY_OffsetAvailable(ic);
                if (Visual) LogMessage("Доступность функций смещения ROI по осям: X: " +
                    ScanXOffsetAvailible.ToString() + "; Y: " + ScanYOffsetAvailible.ToString());
            }
            catch (Exception e) { LogError(e.Message); }
            try
            {
                int Value = GetPartial_scanX_OffsetValue(ic);
                int Minimum = GetPartial_scanX_OffsetMin(ic);
                int Maximum = PointXMax = GetPartial_scanX_OffsetMax(ic);
                TBROIPointX.Text = Value.ToString();
                if (Visual) LogMessage("Попытка получения граничных значений PointX . Состояние: Min:" +
                    Minimum.ToString() + "; Max: " + Maximum.ToString() + " . Текущее значение: " + Value.ToString());
            }
            catch (Exception e) { LogError(e.Message); }
            try
            {
                int Value = GetPartial_scanY_OffsetValue(ic);
                int Minimum = GetPartial_scanY_OffsetMin(ic);
                int Maximum = PointYMax = GetPartial_scanY_OffsetMax(ic);
                TBROIPointY.Text = Value.ToString();
                if (Visual) LogMessage("Попытка получения граничных значений PointY . Состояние: Min:" +
                    Minimum.ToString() + "; Max: " + Maximum.ToString() + " . Текущее значение: " + Value.ToString());
            }
            catch (Exception e) { LogError(e.Message); }
            try
            {
                if (Visual) LogMessage("Текущий формат захвата изображения: " + ic.MemoryCurrentGrabberColorformat.ToString());
            }
            catch (Exception e) { LogError(e.Message); }
        }
        private void TestAOF_dll()
        {
            LogMessage("Direct search of aom.dll and aom_old.dll");
            if(File.Exists("aom.dll")) LogMessage("aom.dll has been found in application directory.");
            else LogMessage("aom.dll has NOT been found in application directory!");
            if (File.Exists("aom_old.dll")) LogMessage("aom_old.dll has been found in application directory.");
            else LogMessage("aom_old.dll has NOT been found in application directory!");

            string baseapppath = Application.StartupPath+"\\";
            LogMessage("Search of "+baseapppath+" aom.dll and "+baseapppath+" aom_old.dll");
            if (File.Exists(baseapppath+"aom.dll")) LogMessage("aom.dll has been found in application directory.");
            else LogMessage("aom.dll has NOT been found in application directory!");
            if (File.Exists(baseapppath + "aom_old.dll")) LogMessage("aom_old.dll has been found in application directory.");
            else LogMessage("aom_old.dll has NOT been found in application directory!");
        }
        private void GetAllResolutionsByFormat()
        {
            var ic = icImagingControl1;
            CBMResolution.Items.Clear();
            CBMResolution.Items.Add("User");
            int real_count = ic.VideoFormats.Count();
            for (int i = 0; i < real_count; i++)
            {             
                if (ic.VideoFormats[i].ToString().Contains(CBSignalFormats.SelectedItem.ToString()))
                {
                    CBMResolution.Items.Add(ic.VideoFormats[i]);
                }//ic.VideoFormatCurrent.Name) { index = i; CBMResolution.SelectedIndex = i; Found = true; break; }
            }
        }
        private void FindCurrentFormat(bool ShowFormat)
        {
            var ic = icImagingControl1; bool Found = false;
            int index = 0;
            for (int i = 0; i < CBMResolution.Items.Count; i++)
            {
                if (CBMResolution.Items[i].ToString() == ic.VideoFormatCurrent.Name) { index = i; CBMResolution.SelectedIndex = i; Found = true; break; }
            }
            if (!Found) CBMResolution.SelectedIndex = 0;
            if (ShowFormat) LogMessage("Format: " + ic.VideoFormatCurrent.Name);
            RefreshROIControls(false);
        }
        private void AnalyseFormats()
        {
            var ic = icImagingControl1;
            string format = ic.VideoFormats[0].Name.Substring(0, ic.VideoFormats[0].Name.IndexOf(' ')); string newformat = null;
            CBSignalFormats.Items.Add(format);
            for (int i = 0; i < ic.VideoFormats.Count(); i++)
            {
                newformat = ic.VideoFormats[i].Name.Substring(0, ic.VideoFormats[i].Name.IndexOf(' '));
                if (newformat != format)
                {
                    CBSignalFormats.Items.Add(newformat);
                    format = newformat;
                }
            }
            string currentformat = ic.VideoFormatCurrent.Name.Substring(0, ic.VideoFormatCurrent.Name.IndexOf(' '));
            int j = 0;
            for (j = 0; j < CBSignalFormats.Items.Count; j++)
            {
                if (CBSignalFormats.Items[j].ToString() == currentformat) break;
            }
          //  CBSignalFormats.SelectedIndex = j; //перенесено в конец функции для последовательного срабатывания других функций по событиям*
            //getting MAX width and height
            string maxformat = ic.VideoFormats[ic.VideoFormats.Count() - 1];

            int op_pos = maxformat.IndexOf('(') + 1;
            int cl_pos = maxformat.LastIndexOf(')');
            int s_lenght = cl_pos - op_pos;
            int x_pos = maxformat.LastIndexOf('x');
            string str_MWidth = maxformat.Substring(op_pos, x_pos - op_pos);
            string str_MHeight = maxformat.Substring(x_pos+1, maxformat.Length - x_pos-2);            
            try
            {
                 MaxMWidth = Convert.ToInt16(str_MWidth);
                 MaxMHeight = Convert.ToInt16(str_MHeight);
            }
            catch
            {
                LogError("Ошибка при определении максимального размера матрицы");
            }

            CBSignalFormats.SelectedIndex = j; // перенес сюда*

        }
        private void DetectCameraPixelFormats()
        {
            var ic = icImagingControl1; string remstr;
            /*int index = 0;
            CBoxPixelFormat.Items.Add(ICImagingControlColorformats.ICBY8);
            CBoxPixelFormat.Items.Add(ICImagingControlColorformats.ICRGB24);
            CBoxPixelFormat.Items.Add(ICImagingControlColorformats.ICRGB32);
            CBoxPixelFormat.Items.Add(ICImagingControlColorformats.ICRGB555);
            CBoxPixelFormat.Items.Add(ICImagingControlColorformats.ICRGB565);
            CBoxPixelFormat.Items.Add(ICImagingControlColorformats.ICUYVY);
            CBoxPixelFormat.Items.Add(ICImagingControlColorformats.ICY16);
            CBoxPixelFormat.Items.Add(ICImagingControlColorformats.ICY8);
            CBoxPixelFormat.Items.Add(ICImagingControlColorformats.ICY800);
            CBoxPixelFormat.Items.Add(ICImagingControlColorformats.ICYGB0);
            CBoxPixelFormat.Items.Add(ICImagingControlColorformats.ICYGB1);*/
            try
            {
                remstr = ic.MemoryCurrentGrabberColorformat.ToString();
            }
            catch { LogError("Формат пикселей на матрице не установлен. Скорее всего, вы используйте не IC камеру"); return; }
          /*  for (int i = 0; i < CBoxPixelFormat.Items.Count; i++)
            {
                try
                {
                    ic.MemoryCurrentGrabberColorformat = (ICImagingControlColorformats)CBoxPixelFormat.Items[i];
                }
                catch (Exception e)
                {
                    CBoxPixelFormat.Items.RemoveAt(i); i--;
                }
            }
            for (int i = 0; i < CBoxPixelFormat.Items.Count; i++)
            {
                if (remstr == CBoxPixelFormat.Items[i].ToString())
                {
                    ic.MemoryCurrentGrabberColorformat = (ICImagingControlColorformats)CBoxPixelFormat.Items[i];
                    CBoxPixelFormat.SelectedIndex = i; break;
                }
            }*/
        }
        private void GetAllPixelFormats()
        {
            var ic = icImagingControl1;
            int index = -1;
            CBSavingPixelFormat.Items.Clear();
            CBSavingPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Alpha);
            CBSavingPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Canonical);
            CBSavingPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.DontCare);
            CBSavingPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Extended);
            CBSavingPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Format16bppArgb1555);
            CBSavingPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Format16bppGrayScale);
            CBSavingPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
            CBSavingPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Format16bppRgb565);
            CBSavingPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Format1bppIndexed);
            CBSavingPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            CBSavingPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            CBSavingPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            CBSavingPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            CBSavingPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Format48bppRgb);
            CBSavingPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Format4bppIndexed);
            CBSavingPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Format64bppArgb);
            CBSavingPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Format64bppPArgb);
            CBSavingPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            CBSavingPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Gdi);
            CBSavingPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Indexed);
            CBSavingPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Max);
            CBSavingPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.PAlpha);
            if(ic.LiveVideoRunning) ic.LiveStop();
            var previous_px_format = ic.MemoryPixelFormat;
            for (int i = 0; i < CBSavingPixelFormat.Items.Count; i++)
            {
                try
                {
                    ic.MemoryPixelFormat = (System.Drawing.Imaging.PixelFormat)CBSavingPixelFormat.Items[i];
                }
                catch (Exception e)
                {
                    CBSavingPixelFormat.Items.RemoveAt(i); i--;
                }
            }
            ic.MemoryPixelFormat = previous_px_format;
            try
            {
                for (int i = 0; i < CBSavingPixelFormat.Items.Count; i++)
                {
                    if (ic.MemoryPixelFormat.ToString() == CBSavingPixelFormat.Items[i].ToString())
                    { index = i; CBSavingPixelFormat.SelectedIndex = i; break; }
                }
            }
            catch //06.06 не внес в эту ветвь правки,поправить позже
            {
                if (ic.VideoFormatCurrent.BinningFactor != 1)
                {
                    LogMessage("Задержка при загрузке программы при определении формата пикселей. ");
                    LogMessage("Причина: попытка определения формата пикселей при включенном биннинге.");
                    LogMessage("Попытка вторичного определения...");

                    var currentFormat = ic.VideoFormatCurrent;
                    ic.VideoFormat = ic.VideoFormats[ic.VideoFormats.Count() - 1];
                    ic.LiveStart(); ic.LiveStop();
                    for (int i = 0; i < CBSavingPixelFormat.Items.Count; i++)
                    {
                        if (ic.MemoryPixelFormat.ToString() == CBSavingPixelFormat.Items[i].ToString())
                        { index = i; CBSavingPixelFormat.SelectedIndex = i; break; }
                    }
                    ic.VideoFormat = currentFormat;
                    if (index != -1) LogMessage("Формат пикселей определен: " + CBSavingPixelFormat.Items[index].ToString());
                }
            }
            var PXFormatToSet = (System.Drawing.Imaging.PixelFormat)CBSavingPixelFormat.Items[index];           
            ic.MemoryPixelFormat = PXFormatToSet;
            
        }
        private void GetAllPixelFormatsForSaving()
        {
            CBFinalPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
            CBFinalPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Format16bppRgb565); 
            CBFinalPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            CBFinalPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Format32bppArgb);  
            CBFinalPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            CBFinalPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            CBFinalPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Format48bppRgb);
            CBFinalPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Format64bppArgb);
            CBFinalPixelFormat.Items.Add(System.Drawing.Imaging.PixelFormat.Format64bppPArgb);
            CBFinalPixelFormat.SelectedIndex = 0;
        }
        private void GetAllAvailibleFPS()
        {
            var ic = icImagingControl1; CBAvFPS.Items.Clear();
            for (int i = 0; i < ic.DeviceFrameRates.Count(); i++)
            {
                CBAvFPS.Items.Add(ic.DeviceFrameRates[i]);
            }
        }
        private void FindCurrentFPS(bool IsFPSFreshNeeded)
        {
            bool WasFound = false;
            var FPSCur = icImagingControl1.DeviceFrameRate;
            bool a = icImagingControl1.LiveVideoRunning;
            if (!a) a = false;
            for (int i = 0; i < CBAvFPS.Items.Count; i++)
            {
                if (Convert.ToDouble(CBAvFPS.Items[i]) == FPSCur)
                {
                    CBAvFPS.SelectedIndexChanged -= new EventHandler(CBAvFPS_SelectedIndexChanged);
                    CBAvFPS.SelectedIndex = i;
                    CBAvFPS.SelectedIndexChanged += new EventHandler(CBAvFPS_SelectedIndexChanged);

                    WasFound = true; break;
                }
            }
            FPSChangeCausedByUser = IsFPSFreshNeeded;
            if (!WasFound) CBAvFPS.SelectedIndex = 0;
            FPSChangeCausedByUser = true;
        }
        private void ChangeFPS()
        {
            if (FPSChangeCausedByUser)
            {
                float SelectedRate = icImagingControl1.DeviceFrameRates[CBAvFPS.SelectedIndex];
                try { icImagingControl1.LiveStop(); }
                catch { }
                icImagingControl1.DeviceFrameRate = SelectedRate;
                try { icImagingControl1.LiveStart(); }
                catch { }
            }
        }
        private void Refresh_GainExp_Ctrls()
        {
            if (!vcdProp.Available(VCDIDs.VCDID_Exposure))
            {
                TrBExposureVal.Enabled = false;
                NUD_ExposureVal.Enabled = false;
            }
            else
            {
                NUD_ExposureVal.Value = (decimal)AbsValExp.Value;
                ChBExposureAuto.Checked = vcdProp.Automation[VCDIDs.VCDID_Exposure];
                TrBExposureVal.Enabled = !ChBExposureAuto.Checked;
            }

            if (!vcdProp.Available(VCDIDs.VCDID_Gain))
            {
                TrBGainVal.Enabled = false;
                NUD_GainVal.Enabled = false;
            }
            else
            {
                NUD_GainVal.Value = (decimal)vcdProp.RangeValue[VCDIDs.VCDID_Gain];
                ChBGainAuto.Checked = vcdProp.Automation[VCDIDs.VCDID_Gain];
                TrBGainVal.Enabled = !ChBGainAuto.Checked;
            }
        }
       
        private void SetMaxValuesToROIControls()
        {
            TBROIWidth.Text = MaxMWidth.ToString();
            TBROIHeight.Text = MaxMHeight.ToString();
            TBROIPointX.Text = "0";
            TBROIPointY.Text = "0";
        }
        private void RefreshROIControls(bool CausedByChangingFormatByUser, bool WasError = false)
        {
            var ic = icImagingControl1;
            if (CBMResolution.SelectedIndex == 0) CbBinning.Enabled = true; else CbBinning.Enabled = false;
            try
            {
                int Value = GetPartial_scanX_OffsetValue(ic);
                TBROIPointX.Text = Value.ToString();
            }
            catch (Exception e) { LogError(e.Message); }
            try
            {
                int Value = GetPartial_scanY_OffsetValue(ic);
                TBROIPointY.Text = Value.ToString();
            }
            catch (Exception e) { LogError(e.Message); }
            try
            {
                int ImHeight = 0; int ImWidth = 0;
                bool WasFound = false;
                TBROIWidth.Enabled = false; TBROIHeight.Enabled = false;
                for (int i = 0; i < ic.VideoFormats.Count(); i++)
                {
                    if (CBMResolution.SelectedItem.ToString() == ic.VideoFormats[i].ToString())
                    {
                        ImWidth = ic.VideoFormats[i].Width; ImHeight = ic.VideoFormats[i].Height; WasFound = true;
                        int BinningOfTheFormat = ic.VideoFormats[i].BinningFactor; break;
                    }
                }
                if (!WasFound)
                {
                    TBROIWidth.Enabled = true; TBROIHeight.Enabled = true;
                    if (ScanXOffsetAvailible && !ACenterFlag) TBROIPointX.Enabled = true; else TBROIPointX.Enabled = false;
                    if (ScanYOffsetAvailible && !ACenterFlag) TBROIPointY.Enabled = true; else TBROIPointY.Enabled = false;
                    if (ACenterAvailible) ChBROIAutoCent.Enabled = true; else ChBROIAutoCent.Enabled = false;
                    if ((CausedByChangingFormatByUser)&&(TBROIWidth.Text != "") && (TBROIHeight.Text != ""))
                    {
                        CbBinning.SelectedIndex = 0; ImWidth = Convert.ToInt16(TBROIWidth.Text); ImHeight = Convert.ToInt16(TBROIHeight.Text);
                    }
                    else { ImWidth = ic.ImageWidth; ImHeight = ic.ImageHeight; }
                }
                if (!WasError)
                {
                    TBROIWidth.Text = ImWidth.ToString();
                    TBROIHeight.Text = ImHeight.ToString();
                }
            }
            catch (Exception e) { LogError(e.Message); }
            try
            {
                int Binning = 1;
                if (!CausedByChangingFormatByUser) Binning = ic.VideoFormatCurrent.BinningFactor;
                if (Binning == 1) CbBinning.SelectedIndex = 0;
                else if (Binning == 2) CbBinning.SelectedIndex = 1;
                else if (Binning == 4) CbBinning.SelectedIndex = 2;
                else { CbBinning.Text = "?"; CbBinning.Enabled = false; }
                PrevSelectedBinning = Binning;
            }
            catch (Exception e) { LogError(e.Message); }
            if (Get_Partial_scan_Auto_center_Switch(icImagingControl1)) ChBROIAutoCent.Checked = true;
            else ChBROIAutoCent.Checked = false;
        }
        private void GetValuesAfterSet()
        {
            var ic = icImagingControl1;
            try
            {
                int Binning = 1;
                Binning = ic.VideoFormatCurrent.BinningFactor;
                if (Binning == 1) CbBinning.SelectedIndex = 0;
                else if (Binning == 2) CbBinning.SelectedIndex = 1;
                else if (Binning == 4) CbBinning.SelectedIndex = 2;
                else { CbBinning.Text = "?"; CbBinning.Enabled = false; }
                PrevSelectedBinning = Binning;
            }
            catch (Exception e) { LogError(e.Message); }
            
            TBROIHeight.Text = ic.VideoFormatCurrent.Height.ToString();
            TBROIWidth.Text = ic.VideoFormatCurrent.Width.ToString();
            if (Get_Partial_scan_Auto_center_Switch(ic)) ChBROIAutoCent.Checked = true;
            else ChBROIAutoCent.Checked = false;
            TBROIPointX.Text = (GetPartial_scanX_OffsetValue(ic) / ic.VideoFormatCurrent.BinningFactor).ToString();
            TBROIPointY.Text = (GetPartial_scanY_OffsetValue(ic) / ic.VideoFormatCurrent.BinningFactor).ToString();

        }
        private bool CheckRight()
        {
            bool ExcCatched = false;
            try { CheckEmptyness(TBROIHeight); }
            catch (Exception e1) { LogError(e1.Message); ExcCatched = true; }
            try { CheckEmptyness(TBROIWidth); }
            catch (Exception e1) { LogError(e1.Message); ExcCatched = true; }
            try { CheckEmptyness(TBROIPointX); }
            catch (Exception e1) { LogError(e1.Message); ExcCatched = true; }
            try { CheckEmptyness(TBROIPointY); }
            catch (Exception e1) { LogError(e1.Message); ExcCatched = true; }
            return ExcCatched;
        }
        private bool SetSelectedFormatAndBinning()
        {
            var ic = icImagingControl1; bool WasFound = false; int i = 0; bool WasErr = false;
            for (i = 0; i < ic.VideoFormats.Count(); i++)
            {
                if (CBMResolution.SelectedItem.ToString() == ic.VideoFormats[i].ToString())
                { WasFound = true; break; }
            }
            if (WasFound) ic.VideoFormat = ic.VideoFormats[i];
            else
            {
                int NewBinning = 1;
                try { NewBinning = (int)Math.Pow(2, CbBinning.SelectedIndex); }

                catch { WasErr = true; LogError("Ошибка преобразования биннинга"); }
                try
                {
                    string VideoStreamFormat = CBSignalFormats.SelectedItem.ToString();
                    CorrectROIValues();
                    int Width = Convert.ToInt16(TBROIWidth.Text); int Height = Convert.ToInt16(TBROIHeight.Text);
                    if (NewBinning != 1) ic.VideoFormat = String.Format(VideoStreamFormat + " ({0}x{1}) [Binning {2}x]",
                        Width, Height, NewBinning);
                    else ic.VideoFormat = String.Format(VideoStreamFormat + " ({0}x{1})", Width, Height);
                }
                catch
                {
                    LogError("Невозможно задать выбранный формат матрицы.");
                    LogError("Попробуйте задать другое значение биннинга или параметров ROI."); WasErr = true;
                }
            }
           
            if (!WasErr) LogMessage("Format has been changed to " + ic.VideoFormatCurrent.Name);
            return WasErr;
            //  ic.VideoFormatCurrent.BinningFactor = NewBinning;
        }
        private bool SetSelectedPartialScan()
        {
            bool result = true;
            if (ACenterAvailible && !ACenterFlag)
            {
                try
                {
                    int PointXN = Convert.ToInt16(TBROIPointX.Text); int PointYN = Convert.ToInt16(TBROIPointY.Text);
                    var ic = icImagingControl1;
                    int CurWidth = ic.VideoFormatCurrent.Width, CurHeight = ic.VideoFormatCurrent.Height;
                    int MaxROIX = -1, MaxROIY = -1;
                    int NeededBinning = (int)Math.Pow(2, CbBinning.SelectedIndex);

                    GetMaximumResolution(ref MaxROIX, ref MaxROIY, NeededBinning);
                    int MaxPointX = (MaxROIX - CurWidth);
                    int MaxPointY = (MaxROIY - CurHeight);
                    if (PointXN > MaxPointX) PointXN = MaxPointX - 8;
                    if (PointYN > MaxPointY) PointYN = MaxPointY - 4;
                    if (PointXN < 0) PointXN = 0;
                    if (PointYN < 0) PointYN = 0;
                    TBROIPointX.Text = PointXN.ToString();
                    TBROIPointY.Text = PointYN.ToString();

                    SetPartial_scanX_OffsetValue(ic, PointXN*NeededBinning);
                    SetPartial_scanY_OffsetValue(ic, PointYN * NeededBinning);
                }
                catch { LogError("Ошибка при смещении ROI "); result = false; }
            }
            return result;
        }
        private void CorrectROIValues()
        {
            if ((TBROIWidth.Text != "") && (TBROIHeight.Text != ""))
            {
                int Width2Set = Convert.ToInt16(TBROIWidth.Text);
                int Height2Set = Convert.ToInt16(TBROIHeight.Text);
                int NeededBinning = (int)Math.Pow(2, CbBinning.SelectedIndex);
                int DevineFact = 8;//(int)Math.Pow(2, NeededBinning);
                var ic = icImagingControl1; int CurrentBinning = ic.VideoFormatCurrent.BinningFactor;
                int RWidth = Width2Set * PrevSelectedBinning * CurrentBinning; int RHeight = Height2Set * CurrentBinning * PrevSelectedBinning;
                if (PrevSelectedBinning == CurrentBinning) { RWidth /= PrevSelectedBinning; RHeight /= PrevSelectedBinning; }
                if (RWidth % DevineFact != 0) RWidth -= RWidth % DevineFact;
                if (RHeight % DevineFact != 0) RHeight -= RHeight % DevineFact;
                if (RWidth < 96) RWidth = 96;
                if (RHeight < 96) RHeight = 96;
                else if ((RHeight == MaxMHeight) && (CbBinning.SelectedIndex != 0)) RHeight -= 8;
                if (PrevSelectedBinning != CurrentBinning)
                { RWidth = RWidth / CurrentBinning; RHeight = RHeight / CurrentBinning; }
                PrevSelectedBinning = NeededBinning;
                int trueWidth = RWidth / NeededBinning, trueHeight = RHeight / NeededBinning;
                if (trueWidth % 8 != 0) trueWidth -= trueWidth % 8;
                if (trueHeight % 4 != 0) trueHeight -= trueHeight % 4;
                TBROIWidth.Text = (trueWidth).ToString();
                TBROIHeight.Text = (trueHeight).ToString();
            }
        }
        private void GetMaximumResolution(ref int MaxX,ref int MaxY,int NeededBinning)
        {
            int DevineFactX = 8, DevineFactY = 4;
            MaxX = MaxMWidth/ NeededBinning; MaxY = MaxMHeight / NeededBinning;
            if (MaxX % DevineFactX != 0) MaxX -= MaxX % DevineFactX;
            if (MaxY % DevineFactY != 0) MaxY -= MaxY % DevineFactY;
        }

        private void SetSelectedCameraPixelFormat()
        {
            var ic = icImagingControl1;
            int index = CBoxPixelFormat.SelectedIndex;
            try
            {
                ic.MemoryCurrentGrabberColorformat = (ICImagingControlColorformats)CBoxPixelFormat.Items[index];
            }
            catch (Exception e)
            {
                LogError(e.Message);
            }
        }

        private void CreateNew_FQS(Guid FT)
        {
            CurFQS = new FrameQueueSink((FrameArg) => NewBufferCallback(FrameArg), new FrameType(FT),5);
        }

        IFrameQueueBuffer test_frame;
        private TIS.Imaging.BaseSink New_SetSelectedCamera_SignalStream_Format() 
        {
            var ic = icImagingControl1;
            int index = CBSignalFormats.SelectedIndex;
            var name = CBSignalFormats.Items[index].ToString();
            Guid realGuid;
            if (name == "BY8") realGuid = TIS.Imaging.MediaSubtypes.BY8;
            else if (name == "RGB24") realGuid = TIS.Imaging.MediaSubtypes.RGB24;
            else if (name == "RGB32") realGuid = TIS.Imaging.MediaSubtypes.RGB32;
            else if (name == "RGB555") realGuid = TIS.Imaging.MediaSubtypes.RGB555;
            else if (name == "RGB565") realGuid = TIS.Imaging.MediaSubtypes.RGB565;
            else if (name == "RGB8") realGuid = TIS.Imaging.MediaSubtypes.RGB8;
            else if (name == "UYVY") realGuid = TIS.Imaging.MediaSubtypes.UYVY;
            else if (name == "Y16") realGuid = TIS.Imaging.MediaSubtypes.Y16;
            else if (name == "YGB1") realGuid = TIS.Imaging.MediaSubtypes.YGB1;
            else if (name == "YGB0") realGuid = TIS.Imaging.MediaSubtypes.YGB0;
            else if (name == "YUY2") realGuid = TIS.Imaging.MediaSubtypes.YUY2;
            else if (name == "Y800") realGuid = TIS.Imaging.MediaSubtypes.Y800;
            else
            {
                realGuid = TIS.Imaging.MediaSubtypes.RGB24;
            }
            if(ic.LiveVideoRunning) ic.LiveStop();

            TIS.Imaging.BaseSink oldSink = ic.Sink;

            curfhs = new TIS.Imaging.FrameHandlerSink();
           // curfhs.SnapMode = true;
            curfhs.FrameTypes.Add(new TIS.Imaging.FrameType(realGuid));

            LoadFlipFilter();
            try
            {
                LogMessage("PixelFormat of new Sink.FrameType is " + curfhs.FrameType.PixelFormat.ToString());
            }
            catch (Exception exf) { LogError("ORIGINAL: "+exf.Message); }
            try
            {
                LogMessage("Name of new Sink.FrameType is " + curfhs.FrameType.Name.ToString());
            }
            catch (Exception exf) { LogError("ORIGINAL: " + exf.Message); }
            try
            {
                LogMessage("Bits per pixel: " + curfhs.FrameType.BitsPerPixel.ToString());
            }
            catch (Exception exf) { LogError("ORIGINAL: " + exf.Message); }
            try
            {
                LogMessage("FourCC: " + curfhs.FrameType.FourCC.ToString());
            }
            catch (Exception exf) { LogError("ORIGINAL: " + exf.Message); }
            ic.Sink = curfhs;

            ic.Sink = new TIS.Imaging.FrameSnapSink(realGuid);
            CreateNew_FQS(realGuid);
            CurFSS = ic.Sink as FrameSnapSink;
            ic.LiveStart();
            test_frame = CurFSS.SnapSingle(TimeSpan.FromSeconds(10));
            ic.LiveStop();
            return oldSink;
        }
        private void SetSelectedPixelFormat()
        {
            var ic = icImagingControl1;
            int index = CBSavingPixelFormat.SelectedIndex;
            try
            {
                var a = (System.Drawing.Imaging.PixelFormat)CBSavingPixelFormat.Items[index];
                ic.MemoryPixelFormat = a;
            }
            catch (Exception e)
            {
                LogError(e.Message);
            }
        }
        private void FormatAdaptation()
        {
            var ic = icImagingControl1;
            int WidthOfImage = test_frame.FrameType.Width;// ic.ImageWidth;
            int HeightOfImage = test_frame.FrameType.Height;
            int ControlWidth = ic.Width;
            int ControlHeight = ic.Height;
            float ZFactWidth = (float)ControlWidth / (float)WidthOfImage;
            float ZFactHeight = (float)ControlHeight / (float)HeightOfImage;
            float ZFactFinal = 100.0f;
            if (ZFactWidth >= ZFactHeight) ZFactFinal = ZFactHeight;
            else ZFactFinal = ZFactWidth;
            TrBZoomOfImage.Visible = true;
            L_Zoom.Visible = true;

            if (icImagingControl1.LiveDisplayDefault == false)
            {
                icImagingControl1.LiveDisplayZoomFactor = ZFactFinal;
                TrBZoomOfImage.Value = (int)(icImagingControl1.LiveDisplayZoomFactor * 100.00f);
                L_Zoom.Text = TrBZoomOfImage.Value.ToString() + "%";
                if (icImagingControl1.LiveDisplayZoomFactor > 1.0) icImagingControl1.ScrollbarsEnabled = true;
                else icImagingControl1.ScrollbarsEnabled = false;
            }
            else
            {
                LogError("The zoom factor can only be set" + "\n" + "if LiveDisplayDefault returns False!");
            }

        }
        private void NormalizeRect(Rectangle val, bool PTest)
        {
            int x1 = val.X, y1 = val.Y;
            int x2 = val.Width, y2 = val.Height;
            if (PTest) LogMessage("1Points:  x1=" + val.X.ToString() + " y1=" + val.Y.ToString() + " ; "
                 + " x2=" + val.Width.ToString() + " y2=" + val.Height.ToString());
            if (x1 > x2) SwapValues(ref x1, ref x2);
            if (y1 > y2) SwapValues(ref y1, ref y2);
            if (PTest) LogMessage("2Points:  x1=" + x1.ToString() + " y1=" + y1.ToString() + " ; "
                + " x2=" + x2.ToString() + " y2=" + y2.ToString());
            UserROIGraphics = new Rectangle(x1, y1, x2, y2);
        }
        private Rectangle NormalizeRect(int px1, int py1, int px2, int py2, bool ptest)
        {
            Rectangle r = new Rectangle(px1, py1, px2, py2);
            NormalizeRect(r, ptest);

            return r;
        }
        private void SwapValues(ref int pa, ref int pb)
        {
            int a = pa; pa = pb; pb = a;
        }
        private void OpenSomeFolder()
        {
            /*  if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
              {
                  TBSavingFolder.Text = folderBrowserDialog1.SelectedPath;
                  WindowsFormsApplication5.Settings.Default.Path = folderBrowserDialog1.SelectedPath;
              }*/
        }

        // ****************************************************************
        // C# functions for Partial_scan Auto_center Switch interface
        //
        // Usage:
        // bool CurrentSwitch;
        // if( Is_Partial_scan_Auto_center_Available(icImagingControl1) )
        // {
        //	CurrentSwitch =  Get_Partial_scan_Auto_center_Switch(icImagingControl1);
        //	Set_Partial_scan_Auto_center_Switch(icImagingControl1, false);
        // }

        private bool Is_Partial_scan_Auto_center_Available(TIS.Imaging.ICImagingControl IC)
        {
            if (IC.DeviceValid)
            {
                TIS.Imaging.VCDSwitchProperty SwitchItf;
                SwitchItf = (TIS.Imaging.VCDSwitchProperty)IC.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_PartialScanOffset,
                                                            TIS.Imaging.VCDIDs.VCDElement_PartialScanAutoCenter,
                                                            TIS.Imaging.VCDIDs.VCDInterface_Switch);
                if (SwitchItf != null)
                {
                    return true;
                }
            }
            return false;
        }
        private bool Get_Partial_scan_Auto_center_Switch(TIS.Imaging.ICImagingControl IC)
        {
            if (IC.DeviceValid)
            {
                TIS.Imaging.VCDSwitchProperty SwitchItf;
                SwitchItf = (TIS.Imaging.VCDSwitchProperty)IC.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_PartialScanOffset,
                                                            TIS.Imaging.VCDIDs.VCDElement_PartialScanAutoCenter,
                                                            TIS.Imaging.VCDIDs.VCDInterface_Switch);
                if (SwitchItf != null)
                {
                    return SwitchItf.Switch;
                }
            }
            return false;
        }
        private void Set_Partial_scan_Auto_center_Switch(TIS.Imaging.ICImagingControl IC, bool OnOff)
        {
            if (IC.DeviceValid)
            {
                TIS.Imaging.VCDSwitchProperty SwitchItf;
                SwitchItf = (TIS.Imaging.VCDSwitchProperty)IC.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_PartialScanOffset,
                                                            TIS.Imaging.VCDIDs.VCDElement_PartialScanAutoCenter,
                                                            TIS.Imaging.VCDIDs.VCDInterface_Switch);
                if (SwitchItf != null)
                {
                    SwitchItf.Switch = OnOff;
                }
            }
        }

        // ****************************************************************
        // C# functions for Partial_scan X_Offset Range interface
        // Usage :
        // int Value, Minimum,Maximum;
        // if( IsPartial_scanX_OffsetAvailable(icImagingControl1) )
        // {
        //  	Value = GetPartial_scanX_OffsetValue(icImagingControl1);
        //  	Minimum = GetPartial_scanX_OffsetMin(icImagingControl1);
        //  	Maximum = GetPartial_scanX_OffsetMax(icImagingControl1);
        //  	SetPartial_scanX_OffsetValue(icImagingControl1, 10);
        // } 
        bool IsPartial_scanX_OffsetAvailable(TIS.Imaging.ICImagingControl ic)
        {
            bool bResult = false;
            if (ic.DeviceValid == true)
            {
                TIS.Imaging.VCDRangeProperty Property;
                Property = (TIS.Imaging.VCDRangeProperty)ic.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_PartialScanOffset,
                    TIS.Imaging.VCDIDs.VCDElement_PartialScanOffsetX,
                    TIS.Imaging.VCDIDs.VCDInterface_Range);
                if (Property != null)
                {
                    bResult = true;
                }
            }
            return bResult;
        }

        
        int GetPartial_scanX_OffsetValue(TIS.Imaging.ICImagingControl ic)
        {
            if (ic.DeviceValid == true)
            {
                TIS.Imaging.VCDRangeProperty Property;
                Property = (TIS.Imaging.VCDRangeProperty)ic.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_PartialScanOffset, TIS.Imaging.VCDIDs.VCDElement_PartialScanOffsetX, TIS.Imaging.VCDIDs.VCDInterface_Range);
                if (Property != null)
                {
                    return Property.Value;
                }
            }

            return 0;
        }
        void SetPartial_scanX_OffsetValue(TIS.Imaging.ICImagingControl ic, int Value)
        {
            if (ic.DeviceValid == true)
            {
                TIS.Imaging.VCDRangeProperty Property;
                Property = (TIS.Imaging.VCDRangeProperty)ic.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_PartialScanOffset, TIS.Imaging.VCDIDs.VCDElement_PartialScanOffsetX, TIS.Imaging.VCDIDs.VCDInterface_Range);
                if (Property != null)
                {
                    Property.Value = Value;
                }
            }
        }
        int GetPartial_scanX_OffsetMin(TIS.Imaging.ICImagingControl ic)
        {
            if (ic.DeviceValid == true)
            {
                TIS.Imaging.VCDRangeProperty Property;
                Property = (TIS.Imaging.VCDRangeProperty)ic.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_PartialScanOffset, TIS.Imaging.VCDIDs.VCDElement_PartialScanOffsetX, TIS.Imaging.VCDIDs.VCDInterface_Range);
                if (Property != null)
                {
                    return Property.RangeMin;
                }
            }

            return 0;
        }
        int GetPartial_scanX_OffsetMax(TIS.Imaging.ICImagingControl ic)
        {
            if (ic.DeviceValid == true)
            {
                TIS.Imaging.VCDRangeProperty Property;
                Property = (TIS.Imaging.VCDRangeProperty)ic.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_PartialScanOffset, TIS.Imaging.VCDIDs.VCDElement_PartialScanOffsetX, TIS.Imaging.VCDIDs.VCDInterface_Range);
                if (Property != null)
                {
                    return Property.RangeMax;
                }
            }
            return 0;
        }

        // ****************************************************************
        // C# functions for Partial_scan Y_Offset Range interface
        // Usage :
        // int Value, Minimum,Maximum;
        // if( IsPartial_scanY_OffsetAvailable(icImagingControl1) )
        // {
        //  	Value = GetPartial_scanY_OffsetValue(icImagingControl1);
        //  	Minimum = GetPartial_scanY_OffsetMin(icImagingControl1);
        //  	Maximum = GetPartial_scanY_OffsetMax(icImagingControl1);
        //  	SetPartial_scanY_OffsetValue(icImagingControl1, 10);
        // } 
        bool IsPartial_scanY_OffsetAvailable(TIS.Imaging.ICImagingControl ic)
        {
            bool bResult = false;
            if (ic.DeviceValid == true)
            {
                TIS.Imaging.VCDRangeProperty Property;
                Property = (TIS.Imaging.VCDRangeProperty)ic.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_PartialScanOffset, TIS.Imaging.VCDIDs.VCDElement_PartialScanOffsetY, TIS.Imaging.VCDIDs.VCDInterface_Range);
                if (Property != null)
                {
                    bResult = true;
                }
            }
            return bResult;
        }
        int GetPartial_scanY_OffsetValue(TIS.Imaging.ICImagingControl ic)
        {
            if (ic.DeviceValid == true)
            {
                TIS.Imaging.VCDRangeProperty Property;
                Property = (TIS.Imaging.VCDRangeProperty)ic.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_PartialScanOffset, TIS.Imaging.VCDIDs.VCDElement_PartialScanOffsetY, TIS.Imaging.VCDIDs.VCDInterface_Range);
                if (Property != null)
                {
                    return Property.Value;
                }
            }

            return 0;
        }
        void SetPartial_scanY_OffsetValue(TIS.Imaging.ICImagingControl ic, int Value)
        {
            if (ic.DeviceValid == true)
            {
                TIS.Imaging.VCDRangeProperty Property;
                Property = (TIS.Imaging.VCDRangeProperty)ic.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_PartialScanOffset, TIS.Imaging.VCDIDs.VCDElement_PartialScanOffsetY, TIS.Imaging.VCDIDs.VCDInterface_Range);
                if (Property != null)
                {
                    Property.Value = Value;
                }
            }
        }
        int GetPartial_scanY_OffsetMin(TIS.Imaging.ICImagingControl ic)
        {
            if (ic.DeviceValid == true)
            {
                TIS.Imaging.VCDRangeProperty Property;
                Property = (TIS.Imaging.VCDRangeProperty)ic.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_PartialScanOffset, TIS.Imaging.VCDIDs.VCDElement_PartialScanOffsetY, TIS.Imaging.VCDIDs.VCDInterface_Range);
                if (Property != null)
                {
                    return Property.RangeMin;
                }
            }

            return 0;
        }
        int GetPartial_scanY_OffsetMax(TIS.Imaging.ICImagingControl ic)
        {
            if (ic.DeviceValid == true)
            {
                TIS.Imaging.VCDRangeProperty Property;
                Property = (TIS.Imaging.VCDRangeProperty)ic.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_PartialScanOffset, TIS.Imaging.VCDIDs.VCDElement_PartialScanOffsetY, TIS.Imaging.VCDIDs.VCDInterface_Range);
                if (Property != null)
                {
                    return Property.RangeMax;
                }
            }
            return 0;
        }
      /*  private void SnapSequence2img()
        {
            ReadAllSettingsFromFile(false);
            if (WarningofImage)
            {
                MessageBox.Show(WarningofImgMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            float MinEx = (float)(TrBExposureVal.Minimum) / 1000000.0f;
            float MaxEx = (float)(TrBExposureVal.Maximum) / 1000000.0f;
            float Ex1 = (float)Convert.ToDouble(TBEx1.Text);
            float Ex2 = (float)Convert.ToDouble(TBEx2.Text);
            double PrexEx = AbsValExp.Value;
            if ((Ex1 <= MinEx) || (Ex1 >= MaxEx)) { LogError(String.Format("Введите допустимое время экспонирования.Min: {0} ; Max: {1}", MinEx, MaxEx)); return; }
            if ((Ex2 <= MinEx) || (Ex2 >= MaxEx)) { LogError(String.Format("Введите допустимое время экспонирования.Min: {0} ; Max: {1}", MinEx, MaxEx)); return; }

            filedatename = DateTime.Now.ToString();
            filedatename = filedatename.Replace(' ', '_');
            filedatename = filedatename.Replace(':', '_');
            filedatename = filedatename.Replace('.', '_');
            TIS.Imaging.ImageBuffer[] buf = GrabImage(TIS.Imaging.MediaSubtypes.Y16, Ex1, Ex2);
            NewWrite16Bit(buf[0], SnapImageStyle.Directory + "3_1st_" + filedatename);
            NewWrite16Bit(buf[1], SnapImageStyle.Directory + "3_2st_" + filedatename);
            AbsValExp.Value = PrexEx;

        }
        private TIS.Imaging.ImageBuffer[] GrabImage(Guid colorFormat, float exposure1, float exposure2)
        {
            bool wasLive = icImagingControl1.LiveVideoRunning;
            icImagingControl1.LiveStop();
            TIS.Imaging.BaseSink oldSink = icImagingControl1.Sink;
            TIS.Imaging.FrameHandlerSink fhs = new TIS.Imaging.FrameHandlerSink();
            fhs.FrameTypes.Add(new TIS.Imaging.FrameType(colorFormat));
            icImagingControl1.Sink = fhs;

            try
            {
                icImagingControl1.LiveStart();
            }
            catch (TIS.Imaging.ICException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

            TIS.Imaging.ImageBuffer[] rval = new ImageBuffer[2];

            try
            {
                int var1 = 0, var2 = 0;
                if ((var1 = ((int)(exposure1 * 1000.0f) + 200)) > 500) var1 = 500;
                if ((var2 = ((int)(exposure2 * 1000.0f) + 200)) > 1000) var2 = 1000;
                AbsValExp.Value = exposure1;
                fhs.SnapImage((int)(exposure1 * 1000.0f) + 100);//timeout - время в мс, сколько будет изображение держаться в буфере
                rval[0] = fhs.LastAcquiredBuffer;
                AbsValExp.Value = exposure2;
                fhs.SnapImage((int)(exposure2 * 1000.0f) + 100);
                rval[1] = fhs.LastAcquiredBuffer;
            }
            catch (TIS.Imaging.ICException ex)
            {
                MessageBox.Show(ex.Message);
            }

            icImagingControl1.LiveStop();
            icImagingControl1.Sink = oldSink;
            if (wasLive) icImagingControl1.LiveStart();

            return rval;
        }*/

    }
}

