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
using LDZ_Code;
using static LDZ_Code.ServiceFunctions;
using AO_Lib;
using static AO_Lib.AO_Devices;

namespace ICSpec
{

    public partial class Form1 : Form
    {
        string Cur_Version = "v2.75 Beta";
        //Инициализация всех переменных, необходимых для работы
        private VCDSimpleProperty vcdProp = null;
        private VCDAbsoluteValueProperty AbsValExp = null;// специально для времени экспонирования [c]
        bool ChangingActivatedTextBoxExp = true;
        bool ChangingActivatedTextBoxGain = true;
        ImageStyle SnapImageStyle = new ImageStyle();
        TIS.Imaging.BaseSink m_oldSink;
        TIS.Imaging.FrameHandlerSink curfhs; // Инициализация текущего FramehandlerSink экземпляра для быстрого захвата кадров
        FrameSnapSink CurFSS; // 10022021. Запил нового синка для правильного граба фреймов
        FrameQueueSink CurFQS;

        string filedatename = null;
        public string fileName = null;//Application.StartupPath + @"\\SettingsOfWriting.txt";
        public bool WarningofCapt = false;
        public bool WarningofImage = false;
        string WarningofCaptMessage = "";
        string WarningofImgMessage = "";
        int Xx = 0;
        int Yy = 0;
        int PrevSelectedBinning = 1;
        const byte ediam = 12;
        bool ACenterFlag = false;
        bool UserROIVisualFlag = false;
        bool FPSChangeCausedByUser = true;
        bool AutoSetActivated = false;
        bool AOFSimulatorActivated = false;
        Rectangle UserROIGraphics = new Rectangle();
        Rectangle UserROIGraphics2 = new Rectangle();
        int PointXMax = 0;
        int PointYMax = 0;
        bool DependenceTBWL = true, DependenceTBWN = true, DependenceTrBWL = true, DependenceTrBWN = true, LoadingAOFValues = false;
        int AttachmentFactor = 100;
        FrameFilter FlipFilter, FlipFilterFHS;

        //для перестройки времени экспонирования по кривой
        string WayToCurv_exp = "";
        string WayToCurv_wl = "";
        float[] WLs_toTune = null;


        // Все для работы АО
        bool AO_WL_Controlled_byslider = false;
        double AO_WL_precision = 1000.0;
        double AO_HZ_precision = 1000.0;

        //Все для sweep
        double AO_FreqDeviation_Max_byTime = 0;
        double AO_FreqDeviation = 0.5;
        double AO_TimeDeviation = 10;
        bool AO_Sweep_Needed = false;
        float[,] AO_All_CurveSweep_Params = new float[0, 0];

        bool AO_Sweep_CurveTuning_isEnabled = false;
        bool AO_Sweep_CurveTuning_inProgress = false;
        bool AO_Sweep_CurveTuning_StopFlag = false;

        List<object> ParamList_bkp = new List<object>();
        List<object> ParamList_final = new List<object>();

        UI.Log.Logger Log;
        AO_Filter Filter = null;
        System.Diagnostics.Stopwatch timer_for_sweep = new System.Diagnostics.Stopwatch();
        System.Diagnostics.Stopwatch timer_for_FPS = new System.Diagnostics.Stopwatch();

        string AO_ProgramSweepCFG_filename = "AOData.txt";

        string data_of_start = null;
        string data_NOW = null;
        bool isTimeSesNeeded = false;
        List<dynamic> IMG_buffers_mass = new List<dynamic>();
        Queue<dynamic> IMG_buffers_queue= new Queue<dynamic>();
        LDZ_Code.MultiThreadSaver MSaver;

        //03032021 DFT
        bool StopRequeted = false;
        bool isDftTurnedOn = false;
        DFTForm DFTWindow = null;
        Bitmap ImForDft = null;

        public Form1()
        {
            InitializeComponent();//функция инициалиции элементов управления
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LogMessage("0");
            //this.Text = "Перестраиваемый источник " + version;
            Log = new UI.Log.Logger(LBConsole);
            Log.Message(" - текущее время");
            Filter = AO_Devices.AO_Filter.Find_and_connect_AnyFilter();
            if (Filter.FilterType == FilterTypes.Emulator) { Log.Message("ПРЕДУПРЕЖДЕНИЕ: Не обнаружены подключенные АО фильтры. Фильтр будет эмулирован."); }
            else { Log.Message("Обнаружен подключенный АО фильтр. Тип фильтра: " + Filter.FilterType.ToString()); }
            ChB_Power.Enabled = false;
            TSMI_Tuning_Pereodical.Enabled = false;
            //GB_AllAOFControls.Enabled = false;
            //ReadData();
            //tests();
            this.KeyPreview = true;

            /* int st = 500; int fn = 610;
             List<int> wls = new List<int>();
             List<double> exps = new List<double>();
             ExpCurve.GetCurveFromDirectory("TestCurv3.expcurv", ref wls, ref exps);
             ExpCurve.GetNiceCurve(450,740, 500, 621,15, ref wls, ref exps);
             LogMessage("Done!");*/
            LogMessage("1");
            try
            {
                if (!icImagingControl1.DeviceValid)
                {
                    TIS.Imaging.LibrarySetup.SetLocalizationLanguage("ru");
                    //var data_sink = new FrameHandlerSink(false, new FrameType("Y800"));
                    //icImagingControl1.Sink = data_sink;
                    try
                    {
                        icImagingControl1.LoadDeviceStateFromFile("CameraSettings.xml", true);
                    }
                    catch
                    {
                        icImagingControl1.ShowDeviceSettingsDialog();
                    }

                    if (!icImagingControl1.DeviceValid)
                    {
                        MessageBox.Show("Не было выбрано ни одного устройства", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.Exit();
                        return;
                    }
                    else
                    {
                        CreateAttachmentFactor(ref AttachmentFactor, LBConsole);
                        this.Text = "IC Spectrometer " + Cur_Version;
                        fileName = Application.StartupPath + @"\SettingsOfWriting.txt";
                        TrBZoomOfImage.Value = (int)(icImagingControl1.LiveDisplayZoomFactor * 100.00f);
                        LogMessage("2.2");
                        vcdProp = new VCDSimpleProperty(icImagingControl1.VCDPropertyItems);
                        LogMessage(vcdProp==null? "vcdProp==null" : "vcdProp!=null");
                        LogMessage("2.3");
                        icImagingControl1.ScrollbarsEnabled = true;
                        TrBZoomOfImage.Value = (int)(icImagingControl1.LiveDisplayZoomFactor * 100);
                        L_Zoom.Text = TrBZoomOfImage.Value.ToString() + "%";
                        icImagingControl1.ImageRingBufferSize = 301;
                        ReadAllSettingsFromFile(false);
                        icImagingControl1.LiveDisplayDefault = false;
                        ContTransAfterSnapshot.Visible = false;
                        GrBAOFWlSet.Enabled = false;
                    }
                    LogMessage("3");
                    CreateAttachmentFactor(ref AttachmentFactor, LBConsole);
                    TestAvailability(false);
                    Init_Gain_Exposure_Sliders();
                    bool liverun = icImagingControl1.LiveVideoRunning;

                    try
                    {
                        AnalyseFormats();
                        m_oldSink = New_SetSelectedCamera_SignalStream_Format();
                    }
                    catch { }
                    // GetAllResolutionsByFormat(); //Вызывается автоматически после AnalyseFormats(), т.к. есть привязка по событию изменения разрешения
                    // FindCurrentFormat(true);     //Вызывается автоматически после AnalyseFormats(), т.к. есть привязка по событию изменения разрешения
                    try { DetectCameraPixelFormats(); } catch { }
                    try { GetAllAvailibleFPS(); } catch { }
                    try { FindCurrentFPS(false); } catch { }

                    try
                    {
                        if (icImagingControl1.LiveVideoRunning) icImagingControl1.LiveStop();
                        GetAllPixelFormats();
                        GetAllPixelFormatsForSaving();
                        SwitchOverlay(icImagingControl1.OverlayBitmapAtPath[PathPositions.Display], true);
                    }
                    catch { }
                    LogMessage("4");
                    try
                    {
                        icImagingControl1.LiveDisplayDefault = false;

                        icImagingControl1.LiveCaptureLastImage = false;

                        icImagingControl1.LiveCaptureContinuous = true;


                        icImagingControl1.LiveStart();
                    }
                    catch { }
                    DisableAlltheShit();
                    FormatAdaptation();
                    m_oldSink = New_SetSelectedCamera_SignalStream_Format();
                    LogMessage("5");
                    //вырубим триггер
                    if (icImagingControl1.DeviceTriggerAvailable)
                        if (icImagingControl1.DeviceTrigger) icImagingControl1.DeviceTrigger = false;

                    //создание контекстного меню
                    this.WindowState = FormWindowState.Maximized;
                    FormatAdaptation();
                    /*  System.Windows.Forms.ContextMenu MyContextMenu=new System.Windows.Forms.ContextMenu();                 
                      System.Windows.Forms.MenuItem menuItem1=new System.Windows.Forms.MenuItem();
                      System.Windows.Forms.MenuItem menuItem2 = new System.Windows.Forms.MenuItem();
                      MyContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { menuItem1, menuItem2 });
                      menuItem1.Index = 0;
                      menuItem1.Text = "Сохранение изображения";
                      menuItem2.Index = 1;
                      menuItem2.Text = "Измерения";
                      icImagingControl1.ContextMenu = MyContextMenu;
                      menuItem1.Click += new System.EventHandler(this.menuItem1_Click);*/
                }
            }
            catch (Exception ext)
            {
                MessageBox.Show(ext.Message);
            }
            finally
            {
                if (icImagingControl1.DeviceValid)
                { icImagingControl1.LiveStart(); timer_for_FPS.Start(); }
                LogMessage("6");
                //saver initilizing
                {
                    MSaver = new LDZ_Code.MultiThreadSaver(LogMessage);
                    TLP_Saving_of_Frames.Visible = false;
                    /*PB_SeriesProgress.Visible = false;
                    PB_Saving.Visible = false;*/

                    MSaver.OnFrameSaved += MSaver_OnFrameSaved;
                    MSaver.OnSerieStarted += MSaver_OnSerieStarted;
                    MSaver.OnSerieSaved += MSaver_OnSerieSaved;
                    MSaver.OnAllFramesSaved += MSaver_OnAllFramesSaved;
                }
                //MSaver.OnSerieStarted += (() => { this.Invoke(new Action(() => { PB_SeriesProgress.Value = 0; PB_SeriesProgress.Visible = true; })); });
                //MSaver.OnSerieStarted += ((x) => { this.Invoke(new Action<string>((meow) => { PB_Saving.Value = 0; PB_Saving.Visible = true; })); });
                //MSaver.OnSerieSaved += ((x) => { this.Invoke(new Action<string>((meow) => { PB_SeriesProgress.Visible = false; })); });

               
            }
        }//функция предзагрузки окна для динамической инициализации некоторых элементов управления 

        private void MSaver_OnAllFramesSaved(int frames_saved, int frames_gotten)
        {
            System.Threading.Thread.Sleep(1000);
            if (TLP_Saving_of_Frames.InvokeRequired)
            {
                TLP_Saving_of_Frames.Invoke(new Action(() => { TLP_Saving_of_Frames.Visible = false; }));
            }
            else
            {
                TLP_Saving_of_Frames.Visible = false;
            }

            if (PB_SeriesProgress.InvokeRequired)
            {
                PB_SeriesProgress.Invoke(new Action(() =>
                {
                    PB_SeriesProgress.Value = 0;
                }));
            }
            else
            {
                PB_SeriesProgress.Value = 0;
            }

            if (PB_Saving.InvokeRequired)
            {
                PB_Saving.Invoke(new Action(() =>
                {
                    PB_Saving.Value = 0;
                }));
            }
            else
            {
                PB_Saving.Value = 0;
            }
        }

        private void MSaver_OnSerieSaved(string NameOfTheSerie,int Frames)
        {
            Log.Message(String.Format("Серия {0} сохранена! Сохранено кадров: {1} ", NameOfTheSerie, Frames));
        }

        private void MSaver_OnSerieStarted(string NameOfTheSerie)
        {
            

            if (PB_SeriesProgress.InvokeRequired)
            {
                PB_SeriesProgress.Invoke(new Action(()=>
                        {
                            PB_SeriesProgress.Value = 0;
                        }));
            }
            else
            {
                PB_SeriesProgress.Value = 0;
            }
            if(PB_Saving.InvokeRequired)
            {
                PB_Saving.Invoke(new Action(() =>
                {
                    PB_Saving.Value = 0;
                }));               
            }
            else
            {
                PB_Saving.Value = 0;
            }

            if (TLP_Saving_of_Frames.InvokeRequired)
            {
                TLP_Saving_of_Frames.Invoke(new Action(() => { TLP_Saving_of_Frames.Visible = true; }));
            }
            else
            {
                TLP_Saving_of_Frames.Visible = true;
            }

        }

        private void MSaver_OnFrameSaved(int frames_saved, int frames_gotten)
        {
            int PercProg = (int)(((float)frames_saved / (float)frames_gotten) * 100);
            string data = String.Format("{0}/{1}", frames_saved, frames_gotten);

            if (PB_Saving.InvokeRequired)
            {
                PB_Saving.Invoke(new Action(() => { PB_Saving.Value = PercProg; }));
                L_Saved.Invoke(new Action(() => { L_Saved.Text = data; }));
            }
            else
            {
                PB_Saving.Value = PercProg;
                L_Saved.Text = data;
            }
        }


        private void ExitBut_Click(object sender, EventArgs e)//функция выхода из приложения
        {
            Close();
        }

        private void SaveImgParBut_Click(object sender, EventArgs e)//функция открытия диалового окна сохраниния изображения
        {
            CreateDialog(false);
        }

        private void PropBut_Click(object sender, EventArgs e)//функция,вызывающая диалоговое окно свойств
        {
            icImagingControl1.ShowPropertyDialog();
           
            Refresh_GainExp_Ctrls();
            RefreshROIControls(false);
        }
        private void Form1_WheelScrolled(object sender, MouseEventArgs e)//фукция,обрабатывающая прокрутку колеса мыши для увеличения в live режиме
        {
            float ZFactCurrent = icImagingControl1.LiveDisplayZoomFactor;
            float abs = (float)((PerfectRounding(ZFactCurrent * 100, 0)) % 5) / 100.0f;
            if (abs != 0)
            {
                icImagingControl1.LiveDisplayZoomFactor = ZFactCurrent - abs;
                TrBZoomOfImage.Value = (int)(icImagingControl1.LiveDisplayZoomFactor * 100.0f);
                L_Zoom.Text = TrBZoomOfImage.Value.ToString() + "%";
            }
            if (!(((TrBZoomOfImage.Value > (TrBZoomOfImage.Maximum - (e.Delta / 24) + 1)) && (e.Delta > 0)) || ((TrBZoomOfImage.Value < TrBZoomOfImage.Minimum - (e.Delta / 24) - 1) && (e.Delta < 0))))
            {
                TrBZoomOfImage.Value += e.Delta / 24;
                ScrollOcc();
            }
        }
        private void ZoomOfImage_Scroll(object sender, EventArgs e)//событие увеличения изображения
        {
            ScrollOcc();
        }

        private void icImagingControl1_Scroll(object sender, ScrollEventArgs e)//функция,запоминающая координаты положения увеличения в live окне
        {
            Point p = icImagingControl1.AutoScrollPosition;
        }

        private void TimerForExpGain_refresh_Tick(object sender, EventArgs e)//Пока включена автоматическая регулировка, эта функция отвечает за отображения значения параметра 
        {
            try
            {
                NUD_ExposureVal.ValueChanged-= NUD_ExposureVal_ValueChanged;
                NUD_ExposureVal.Value = (decimal)(AbsValExp.Value);
                NUD_ExposureVal.ValueChanged += NUD_ExposureVal_ValueChanged;

                TrBExposureVal.Scroll -= TrBExposureVal_Scroll;
                TrBExposureVal.Value = Exposure_real2slide(AbsValExp.Value);
                TrBExposureVal.Scroll += TrBExposureVal_Scroll;
                //ChBExposureAuto.Checked = vcdProp.Automation[VCDIDs.VCDID_Exposure];

                NUD_GainVal.ValueChanged -= NUD_GainVal_ValueChanged;
                NUD_GainVal.Value = (decimal)vcdProp.RangeValue[VCDIDs.VCDID_Gain];
                NUD_GainVal.ValueChanged += NUD_GainVal_ValueChanged;

                TrBGainVal.Scroll -= TrBGainVal_Scroll;
                TrBGainVal.Value = vcdProp.RangeValue[VCDIDs.VCDID_Gain];
                TrBGainVal.Scroll += TrBGainVal_Scroll;
                //ChBGainAuto.Checked = vcdProp.Automation[VCDIDs.VCDID_Gain];
            }
            catch { }
        }


        private void SnapshotBut_Click(object sender, EventArgs e)//событие создания скриншота окна
        {
            //if (FlipFilter.GetBoolParameter("Flip H")) FlipFilter.SetBoolParameter("Flip H", false);
            // else FlipFilter.SetBoolParameter("Flip H", true);
            SnapShot();
            SaveImageBut.Visible = true;
            DisableFlipButtons();

            /*  if (FlipFilterFHS.GetBoolParameter("Flip H")) FlipFilterFHS.SetBoolParameter("Flip H", false);
              else FlipFilterFHS.SetBoolParameter("Flip H", true);*/
        }

        private void ContTransAfterSnapshot_Click(object sender, EventArgs e)//функция, вызывающия появление кновки "продолжить трансляцию" после скриншота
        {
            icImagingControl1.LiveStart();
            SnapshotBut.Enabled = true;
            ContTransAfterSnapshot.Visible = false;
            SaveImageBut.Visible = false;
            EnableFlipButtons();
        }


        private void Form1_MouseMove(object sender, MouseEventArgs e)//функция,запоминающая позицию курсора в окне при движении по нему
        {
            Xx = Cursor.Position.X;
            Yy = Cursor.Position.Y;
        }



        private void SaveImageBut_Click(object sender, EventArgs e)
        {
            try
            {
                ReadAllSettingsFromFile(false);
                if (WarningofImage)
                {
                    MessageBox.Show(WarningofImgMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (icImagingControl1.LiveVideoRunning) icImagingControl1.LiveStop();
                    ContTransAfterSnapshot.Visible = true;
                    string SCRName = CheckScreenShotBasicName();
                    string date = GetFullDateString();
                    var buf = curfhs.LastAcquiredBuffer;
                    string exactname = SnapImageStyle.Directory + SCRName + "_" + date + "_" + AO_CurrentWL.ToString() + SnapImageStyle.Extension;
                    buf.SaveAsTiff(exactname);
                    LogMessage("Сохранено: " + exactname);
                }
            }
            catch (ICException exc)
            {
                LogError("ORIGINAL: " + exc.Message);
                LogError("Code: " + exc.TargetSite);
            }
        }

        private void BStartS_Click(object sender, EventArgs e)
        {
            if (!TSMI_Load_UDWL_Curve.Checked)
            {
                WLs_toTune = null;
            }
            DisableFlipButtons();
            if (RB_Series_WLMode.Checked)
            {
                int stepss = CalculateSteps_viaWLs();
                New_SnapAndSaveMassive_viaWLs_10022021(AO_StartWL, AO_EndWL, stepss, WLs_toTune);
            }
            else
            {
                int stepss = CalculateSteps_viaMHzs();
                New_SnapAndSaveMassive_viaFreqs_10022021(AO_StartFreq, AO_EndFreq, stepss);
            }
                EnableFlipButtons();
        }

        private void BDevOpen_Click(object sender, EventArgs e)
        {
            string AO_DEV_loaded = null;
            string AO_DEV_loaded_fullPath = null;

            var DR = OpenDevSearcher(ref AO_DEV_loaded, ref AO_DEV_loaded_fullPath);

            if (DR == DialogResult.OK)
                try
                {
                    var Status = Filter.Read_dev_file(AO_DEV_loaded_fullPath);
                    if (Status == 0)
                        Log.Message(AO_DEV_loaded_fullPath + " - файл считан успешно!");
                    else
                        throw new Exception(Filter.Implement_Error(Status));
                    if (Filter.FilterType == FilterTypes.STC_Filter)
                    {
                        Log.Message("Бит инверсия (считана из файла): " + (Filter as STC_Filter).Bit_inverse_needed.ToString().ToLower());
                    }
                    else if ((Filter.FilterType == FilterTypes.Emulator))
                    {
                        Log.Message("Бит инверсия (считана из файла): " + (Filter as Emulator).Bit_inverse_needed.ToString().ToLower());
                    }
                }
                catch (Exception exc)
                {
                    Log.Message("Произошла ошибка при прочтении .dev файла");
                    Log.Error("ORIGINAL:" + exc.Message);
                    return;
                }
            else return;

            AO_FreqDeviation_Max_byTime = AO_TimeDeviation / (1000.0f / Filter.AO_ExchangeRate_Min);
            InitializeComponents_byVariables();
            try { Load_properties_for_WL_ctrls((decimal)Filter.WL_Max, (decimal)Filter.WL_Min, (decimal)AbsValExp.Value, (decimal)AbsValExp.RangeMax, 0); }
            catch
            {
                
            }
        }

        private void BFolderOpen_Click(object sender, EventArgs e)
        {
            OpenSomeFolder();
        }


        private void TBCurrentWL_Scroll(object sender, EventArgs e)
        {
            if (DependenceTrBWL && !LoadingAOFValues)
            { SetInactiveDependence(3); TrBWL_OnScroll(); SetInactiveDependence(0); }
        }
        private void TrB_CurrentMHz_Scroll(object sender, EventArgs e)
        {
            if (DependenceTrBWN && !LoadingAOFValues)
            {
                SetInactiveDependence(4); TrB_MHz_OnScroll(); SetInactiveDependence(0);
            }
        }
       
     
        private void SetInactiveDependence(int state)
        {
            switch (state)
            {
                case 1: { DependenceTBWL = true; DependenceTBWN = false; DependenceTrBWL = false; DependenceTrBWN = false; } break;
                case 2: { DependenceTBWL = false; DependenceTBWN = true; DependenceTrBWL = false; DependenceTrBWN = false; } break;
                case 3: { DependenceTBWL = false; DependenceTBWN = false; DependenceTrBWL = true; DependenceTrBWN = false; } break;
                case 4: { DependenceTBWL = false; DependenceTBWN = false; DependenceTrBWL = false; DependenceTrBWN = true; } break;
                default: { DependenceTBWL = true; DependenceTBWN = true; DependenceTrBWL = true; DependenceTrBWN = true; } break;
            }
        }

        private void BSetWL_Click(object sender, EventArgs e)
        {
            float data_CurrentWL = (float)(TrB_CurrentWL.Value / AO_WL_precision);
            CurrentWL_Change();
        }
        private void B_SetHZ_Click(object sender, EventArgs e)
        {
            CurrentHZ_Change();
        }

        private void BSetROI_Click(object sender, EventArgs e)
        {
            //if (!CheckRight()) return;
            var ic = icImagingControl1;
            ic.LiveStop();
            var oldS = New_SetSelectedCamera_SignalStream_Format(); //из-за этой функции после ресета становится доступно сохранение кадров
            bool result = SetSelectedFormatAndBinning();
            result = SetSelectedPartialScan();
            ROISeted = true;
            
            try { ic.LiveStart(); }
            catch (ICException exc)
            {
                LogError("ORIGINAL: " + exc.Message);
                LogMessage("Неудача при выставлении нового формата. Попытка возврата к предыдущему...");
                ic.Sink = oldS;
                try
                {
                    ic.LiveStart();
                    LogMessage("Возврат к предыдущему формату осуществлен успешно");
                }
                catch (ICException exc2)
                {
                    LogError("ORIGINAL: " + exc2.Message);
                }
            }
            FormatAdaptation();
            GetAllAvailibleFPS();
            FindCurrentFPS(false);
            RefreshROIControls(false, result);
            GetValuesAfterSet();
            if (ic.LiveVideoRunning)
            {
                try
                {
                    LogMessage("MemoryCurrentGrabberColorformat is " + ic.MemoryCurrentGrabberColorformat.ToString() + " now.");
                    LogMessage("MemoryPixelFormat is " + ic.MemoryPixelFormat.ToString() + " now.");
                }
                catch (Exception exc) { LogError("ORIGINAL: " + exc.Message); }
            }
        }

        private void TBExposureVal_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void TBGainVal_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void TrBExposureVal_Scroll(object sender, EventArgs e)
        {
            try
            {
                ChangingActivatedTextBoxExp = false;
                double value = Exposure_Slide2real(TrBExposureVal.Value);
                LoadExposure_ToCam(ref AbsValExp, value);
                decimal promval = (decimal)PerfectRounding((Exposure_Slide2real(TrBExposureVal.Value)),7);
                NUD_ExposureVal.Value = promval;
                ChangingActivatedTextBoxExp = true;

                //Установка одинаковой экспозиции во всех боксах
               /* for (int i = 0; i < WLS_at_all; ++i)
                {
                    var EXP_control = this.Controls.Find("NUD_Multi_ex_time" + (i + 1).ToString(),true);
                    (EXP_control[0] as NumericUpDown).Value = (decimal)(AbsValExp.Value / ((double)WLS_at_all));                  
                }*/
               // frames_aquired = 0; timer_for_FPS.Restart();
            }
            catch (Exception ex) { LogError("ORIGINAL: " + ex.Message); }
        }
        private static void LoadExposure_ToCam(ref VCDAbsoluteValueProperty var, double pvalue)
        {
            if (pvalue < var.RangeMin) var.Value = var.RangeMin;
            else if (pvalue > var.RangeMax) var.Value = var.RangeMax;
            else var.Value = pvalue;
        }
        private void LoadGain(ref VCDSimpleProperty SProp, double pvalue)
        {
            string ChangeVCDID2 = VCDIDs.VCDID_Gain;
            if (pvalue < SProp.RangeMin(ChangeVCDID2))
            {
                NUD_GainVal.Value = SProp.RangeMin(ChangeVCDID2);

            }
            else if (pvalue > SProp.RangeMax(ChangeVCDID2))
            {
                NUD_GainVal.Value = SProp.RangeMax(ChangeVCDID2);

            }
            else
            {
                NUD_GainVal.Value = SProp.RangeMin(ChangeVCDID2);

                NUD_GainVal.Value = (decimal)pvalue;
                SProp.RangeValue[ChangeVCDID2] = (int)pvalue;

            }

        }
        private void TrBGainVal_Scroll(object sender, EventArgs e)
        {
            ChangingActivatedTextBoxGain = false;
            vcdProp.RangeValue[VCDIDs.VCDID_Gain] = TrBGainVal.Value;
            NUD_GainVal.Value = TrBGainVal.Value;
            ChangingActivatedTextBoxGain = true;
        }

        private void ChBGainAuto_CheckedChanged(object sender, EventArgs e)
        {
            vcdProp.Automation[VCDIDs.VCDID_Gain] = ChBGainAuto.Checked;
            TrBGainVal.Enabled = !ChBGainAuto.Checked;
            if (ChBGainAuto.Checked)
            {
                TimerForExpGain_refresh.Enabled = true;
                TimerForExpGain_refresh.Start();
            }
            else
            {
                TimerForExpGain_refresh.Stop();
                TimerForExpGain_refresh.Enabled = false;
            }
        }

        private void ChBExposureAuto_CheckedChanged(object sender, EventArgs e)
        {
            vcdProp.Automation[VCDIDs.VCDID_Exposure] = ChBExposureAuto.Checked;
            TrBExposureVal.Enabled = !ChBExposureAuto.Checked;
            if (ChBExposureAuto.Checked)
            {
                TimerForExpGain_refresh.Enabled = true;
                TimerForExpGain_refresh.Start();
            }
            else
            {
                TimerForExpGain_refresh.Stop();
                TimerForExpGain_refresh.Enabled = false;
            }
        }

        private void BAdapt_Click(object sender, EventArgs e)
        {
            FormatAdaptation();
        }

        private void m_AcquireButton_Click(object sender, EventArgs e)
        {

        }

        private void TBNamePrefix_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (TBNamePrefix.Text.Length > 14) e.Handled = true;
            if (e.KeyChar == Convert.ToChar(8)) e.Handled = false;
        }

        private void CBSignalFormats_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllResolutionsByFormat();
            FindCurrentFormat(false);
        }

        private void CBMResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshROIControls(true);
        }

        private void ChBROIAutoCent_CheckedChanged(object sender, EventArgs e)
        {
            if (ChBROIAutoCent.Checked)
            {
                ACenterFlag = true; TBROIPointX.Enabled = TBROIPointY.Enabled = false;
                Set_Partial_scan_Auto_center_Switch(icImagingControl1, true);
            }
            else
            {
                ACenterFlag = false; TBROIPointX.Enabled = TBROIPointY.Enabled = true;
                Set_Partial_scan_Auto_center_Switch(icImagingControl1, false);
            }

        }

        private void CbBinning_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBMResolution.SelectedIndex == 0) CorrectROIValues();
        }

        private void BSelectUserResolution_Click(object sender, EventArgs e)
        {
            CBMResolution.SelectedIndex = 0;
        }

        private void CBAvFPS_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeFPS();
        }

        private void FPSTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (icImagingControl1.DeviceValid)
                {
                    try
                    {
                        double FPS_toString = (icImagingControl1.DeviceFrameRate > (double)(1.0 / AbsValExp.Value) ? (double)(1.0 / AbsValExp.Value) : icImagingControl1.DeviceFrameRate);
                        LFPSCurrent.Text = PerfectRounding(FPS_toString,2).ToString();
                            // (((double)frames_aquired) / (timer_for_FPS.ElapsedMilliseconds / 1000.0)).ToString();
                            //icImagingControl1.DeviceFrameRate.ToString();
                    }

                    catch { }
                }
            }
            catch
            {

            }
        }

        private void icImagingControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (UserROIVisualFlag)
            {
                if (e.Button == MouseButtons.Left)
                {
                    ROISeted = false; ChBROIAutoCent.Checked = false;
                    UserROIGraphics2.X = (int)(((float)e.Location.X) / icImagingControl1.LiveDisplayZoomFactor);
                    UserROIGraphics2.Y = (int)(((float)e.Location.Y) / icImagingControl1.LiveDisplayZoomFactor);
                    /* int devide = icImagingControl1.VideoFormatCurrent.BinningFactor;
                     UserROIGraphics.X /= devide;
                     UserROIGraphics.Y /= devide;*/
                }
            }
        }

        private void icImagingControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (UserROIVisualFlag)
            {
                if (e.Button == MouseButtons.Left)
                {
                    UserROIGraphics2.Width = (int)(((float)e.Location.X) / icImagingControl1.LiveDisplayZoomFactor);
                    UserROIGraphics2.Height = (int)(((float)e.Location.Y) / icImagingControl1.LiveDisplayZoomFactor);
                    NormalizeRect(UserROIGraphics2, false);
                    /* int devide = icImagingControl1.VideoFormatCurrent.BinningFactor;
                     UserROIGraphics.Height /= devide;
                     UserROIGraphics.Height /= devide;*/
                }
            }
        }
        private void icImagingControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (UserROIVisualFlag)
            {
                if (e.Button == MouseButtons.Left)
                {
                    UserROIGraphics2.Width = (int)(((float)e.Location.X) / icImagingControl1.LiveDisplayZoomFactor);
                    UserROIGraphics2.Height = (int)(((float)e.Location.Y) / icImagingControl1.LiveDisplayZoomFactor);
                    /*int devide = icImagingControl1.VideoFormatCurrent.BinningFactor;
                    UserROIGraphics.X /= devide;
                    UserROIGraphics.Y /= devide;
                    UserROIGraphics.Width /= devide;
                    UserROIGraphics.Height /= devide;*/

                    NormalizeRect(UserROIGraphics2, true);
                    if (!ROISeted && ChBVisualiseROI.Checked && (CBMResolution.SelectedIndex == 0))
                    {
                        TBROIHeight.Text = (UserROIGraphics.Height - UserROIGraphics.Y).ToString();
                        TBROIWidth.Text = (UserROIGraphics.Width - UserROIGraphics.X).ToString();
                        TBROIPointX.Text = (UserROIGraphics.X).ToString();
                        TBROIPointY.Text = (UserROIGraphics.Y).ToString();
                    }
                }
            }
        }

        private void ChBVisualiseROI_CheckedChanged(object sender, EventArgs e)
        {
            if (ChBVisualiseROI.Checked)
            {
                UserROIVisualFlag = true; CbBinning.Enabled = false;
                
            }
            else
            {
                UserROIVisualFlag = false; CbBinning.Enabled = true;
            }
        }

        private void ICInvalidateTimer_Tick(object sender, EventArgs e)
        {
            try
            {// if(icImagingControl1.LiveVideoRunning)
             // DBInvalidate(icImagingControl1.OverlayBitmap);
            }
            catch
            {
                
            }
        }

        private void BROIFull_Click(object sender, EventArgs e)
        {
            SetMaxValuesToROIControls();
        }

        private void BTestAll_Click(object sender, EventArgs e)
        {
            TestAvailability(true);
            TestAOF_dll();
        }

        private void ChBAutoSetWL_CheckedChanged(object sender, EventArgs e)
        {
            AO_WL_Controlled_byslider = ChB_AutoSetWL.Checked;
        }

        private void ChBActivateAOFSimulator_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                icImagingControl1.SaveDeviceStateToFile("CameraSettings.xml");
            }
            catch
            {

            }
        }

        private void CBoxPixelFormat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BSnSq_Click(object sender, EventArgs e)
        {
            //SnapSequence2img();
        }

        private void TBEx1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && (e.KeyChar != Convert.ToChar(8)) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
        }


        private void ChkB_LoadedExpCurve_CheckedChanged(object sender, EventArgs e)
        {
             



        }

        /*  delegate void DCreation(ref TIS.Imaging.ICImagingControl IC,
              ref TIS.Imaging.VCDHelpers.VCDSimpleProperty VSExp, ref TIS.Imaging.VCDAbsoluteValueProperty pAbsVal,
              int pWlRealMin, int pWlRealMax, int pStWL, int pFinWL, int pStep);*/

        private void BCreateAutoCurve_Click(object sender, EventArgs e)
        {
         
            // DCreation DelegateForCurv = new DCreation(ExpCurve.CreateCurve);
        }

        private void BkGrWorker_forExpCurveBuilding_DoWork(object sender, DoWorkEventArgs e)
        {


            var worker = sender as BackgroundWorker;
            bool wasAutomation = false;
            string ChangeVCDID = TIS.Imaging.VCDIDs.VCDID_Exposure;
        //    var a = vcdProp.RangeValue[VCDIDs.VCDElement_AutoReference];
            //var b = vcdProp.RangeValue[VCDIDs.VCDElement_AutoMaxValueAuto];
            //var c = vcdProp.RangeValue[VCDIDs.VCDElement_AutoMaxValue];
            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                try
                {
                    ShowStringDelegate MessageDelegate = new ShowStringDelegate(LogMessage);
                    var ptrExp = AbsValExp;
                    double CurFPS = icImagingControl1.DeviceFrameRate;
                    int CurGain = vcdProp.RangeValue[VCDIDs.VCDID_Gain];

                    wasAutomation = vcdProp.Automation[ChangeVCDID];

                    ExpCurve.CreateCurve(ref worker, ref e, ref icImagingControl1, ref vcdProp, ref ptrExp,
                           MinimumWL, MaximumWL, (int)AO_StartWL, (int)AO_EndWL, (int)AO_StepWL, CurGain, CurFPS, MessageDelegate, wasAutomation,Filter);
                    vcdProp.Automation[ChangeVCDID] = wasAutomation;
                }
                catch (Exception exc)
                {
                    LogError(exc.Message);
                    vcdProp.Automation[ChangeVCDID] = wasAutomation;
                }
            }
        }

        private void BkGrWorker_forExpCurveBuilding_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string message2 = "Exposure for " + e.ProgressPercentage + " is fitted";
            LogMessage(message2);
        }

        private void BkGrWorker_forExpCurveBuilding_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ShowStringDelegate MessageDelegate = new ShowStringDelegate(LogMessage);
            if (e.Cancelled == true)
            {
                MessageDelegate.Invoke("Fitting canceled!");
                TSMI_CurveCreating_ExposureWL.Text = "Exposure - WL curve";
                TSMI_CurveCreating_ExposureWL.Checked = false;
            }
            else if (e.Error != null)
            {
                MessageDelegate.Invoke("Error : " + e.Error.Message);
                TSMI_CurveCreating_ExposureWL.Text = "Exposure - WL curve";
                TSMI_CurveCreating_ExposureWL.Checked = false;
            }
            else
            {
                MessageDelegate.Invoke("Fitting finished!");
                TSMI_CurveCreating_ExposureWL.Text = "Exposure - WL curve";
                TSMI_CurveCreating_ExposureWL.Checked = false;
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void GPCamFeat_Enter(object sender, EventArgs e)
        {

        }

        private void viaMSToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ChB_LoadWLCurve_CheckedChanged(object sender, EventArgs e)
        {

       
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            try { FormatAdaptation(); } catch { }
            //  CreateAttachmentFactor(ref AttachmentFactor, LBConsole);
            Log = new UI.Log.Logger(LBConsole);
        }

        private void ChB_Power_CheckedChanged(object sender, EventArgs e)
        {

            bool newAOFPowerStatus = ChB_Power.Checked;
            if (newAOFPowerStatus)
            {
                try
                {
                    var state = Filter.PowerOn();
                    if (state == 0)
                    {
                        Log.Message("Активация АОФ успешна!");
                        GrBAOFWlSet.Enabled = true;
                        TSMI_Tuning_Pereodical.Enabled = true;
                    }
                    else throw new Exception(Filter.Implement_Error(state));
                }
                catch (Exception exc)
                {
                    Log.Message("Возникла проблема при активации АОФ.");
                    Log.Error(exc.Message);
                }
            }
            else
            {
                GrBAOFWlSet.Enabled = false;
                Filter.PowerOff();
            }
        }

        private void CurrentWL_Change()
        {
            float data_CurrentWL = (float)(TrB_CurrentWL.Value / AO_WL_precision);
            if (AO_WL_Controlled_byslider)
            {
                if (AO_Sweep_Needed)
                {
                    if (!timer_for_sweep.IsRunning || timer_for_sweep.ElapsedMilliseconds > 500)
                    {
                        timer_for_sweep.Restart();
                      //  Timer_Sweep.Enabled = true;
                        ReSweep(data_CurrentWL);
                    }
                }
                else
                {
                    try
                    {
                        var state = Filter.Set_Wl(data_CurrentWL);
                        if (state != 0) throw new Exception(Filter.Implement_Error(state));
                        Log.Message("Перестройка на " + data_CurrentWL.ToString() + " нм / " +Filter.HZ_Current.ToString() + " MHz прошла успешно!");
                    }
                    catch (Exception exc)
                    {
                        Log.Error(exc.Message);
                    }
                }
            }
        }

        private void CurrentHZ_Change()
        {
            float data_CurrentHZ = (float)System.Math.Round(((double)((int)(NUD_CurrentMHz.Value * 1000)) / 1000.0f),3);
            if (AO_WL_Controlled_byslider)
            {
                if (AO_Sweep_Needed)
                {
                    /*if (!timer_for_sweep.IsRunning || timer_for_sweep.ElapsedMilliseconds > 500)
                    {
                        timer_for_sweep.Restart();
                        //  Timer_Sweep.Enabled = true;
                        ReSweep(data_CurrentWL);
                    }*/
                }
                else
                {
                    try
                    {
                        var state = Filter.Set_Hz(data_CurrentHZ);
                        if (state != 0) throw new Exception(Filter.Implement_Error(state));
                        Log.Message("Перестройка на " + data_CurrentHZ.ToString() + " MHz / " + Filter.HZ_Current.ToString() + " MHz прошла успешно!");
                    }
                    catch (Exception exc)
                    {
                        Log.Error(exc.Message);
                    }
                }
            }
        }

        private void NUD_CurWL_ValueChanged(object sender, EventArgs e)
        {
            TrB_CurrentWL.Value = (int)(NUD_CurrentWL.Value * (decimal)AO_WL_precision);
            CurrentWL_Change();
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //System.Diagnostics.Stopwatch SW1 = new System.Diagnostics.Stopwatch();
        private void icImagingControl1_OverlayUpdate(object sender, ICImagingControl.OverlayUpdateEventArgs e)
        {
            /*Log.Message("Кадр получен!");
            Log.Message("Время с последнего кадра:" + SW1.ElapsedMilliseconds.ToString() + ". Время экспонирования: " + (AbsValExp.Value*1000).ToString());
             SW1.Restart();*/
            var arg = e.overlay;
            if (icImagingControl1.LiveVideoRunning) DBInvalidate(arg);
        }

        private void CBFinalPixelFormat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FlipUpDownBut_Click(object sender, EventArgs e)//функция отзеркаливания live изображения сверху вниз
        {
            if (FlipFilter.GetBoolParameter("Flip V")) FlipFilter.SetBoolParameter("Flip V", false);
            else FlipFilter.SetBoolParameter("Flip V", true);
            /*   if (FlipFilterFHS.GetBoolParameter("Flip V")) FlipFilterFHS.SetBoolParameter("Flip V", false);
               else FlipFilterFHS.SetBoolParameter("Flip V", true);*/
        }
        private void FlipRightLeftBut_Click(object sender, EventArgs e)//функция отзеркаливания live изображения слева направо
        {
            if (FlipFilter.GetBoolParameter("Flip H")) FlipFilter.SetBoolParameter("Flip H", false);
            else FlipFilter.SetBoolParameter("Flip H", true);
            /* if (FlipFilterFHS.GetBoolParameter("Flip H")) FlipFilterFHS.SetBoolParameter("Flip H", false);
             else FlipFilterFHS.SetBoolParameter("Flip H", true);*/
        }

      

      

        private void NUD_StartL_ValueChanged(object sender, EventArgs e)
        {
            AO_StartWL = (float)NUD_StartL.Value;
        }

        private void NUD_FinishL_ValueChanged(object sender, EventArgs e)
        {
            AO_EndWL = (float)NUD_FinishL.Value;
        }

        private void NUD_StepL_ValueChanged(object sender, EventArgs e)
        {
            AO_StepWL = (float)NUD_StepL.Value;
        }

        private void B_Start_TimedSession_Click(object sender, EventArgs e)
        {
            if (B_StartS.Text != "Break session")
            {
                ReadAllSettingsFromFile(false);
                if (WarningofImage)
                {
                    MessageBox.Show(WarningofImgMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

               string date = GetDateString();
                Directory.CreateDirectory(SnapImageStyle.Directory + date);
               string NameDirectory = GetDateString() + "\\";
               string SCRName = CheckScreenShotBasicName();
                Start_NoAOFSession();
            }
            else
            {
                DialogResult a = MessageBox.Show("Вы уверены, что хотите прервать сессию? Данные сохранятся, но отчет начнется заново.",
                    "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes) Stop_NoAOFSession();
                else return;
            }
        }
        private void Start_NoAOFSession()
        {
       /*     SessionTiming.Enabled = true;
            SessionTiming.Start(); AllTheSession.Start();
            Steps_InSession_MAX = BasicDataSession.GetNumOfFragments();
            BStartS.Text = "Break session";

            //
            data_of_Start = string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.Now);
            stw.Start();
            SnapShot_New(0, Steps_InSession); stw.Stop();
            LogMessage("Время сохранения (мс): " + stw.Elapsed.TotalMilliseconds.ToString());*/
        }
        private void Stop_NoAOFSession()
        {
         /*   SessionTiming.Enabled = false;
            SessionTiming.Stop(); AllTheSession.Stop();
            Ticks_InSession = 0; Steps_InSession = 0;
            BStartS.Text = "Start session";*/
        }

        private void SessionTiming_Tick(object sender, EventArgs e)
        {
            if (isTimeSesNeeded)
            {
                try
                {
                    string NOW = string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.Now);
                    // int dd_elapsed = Convert.ToInt32(NOW.Substring(8, 2)) - Convert.ToInt32(data_of_Start.Substring(8, 2));
                    int hh_elapsed = Convert.ToInt32(NOW.Substring(11, 2)) - Convert.ToInt32(data_of_start.Substring(11, 2));
                    int mm_elapsed = Convert.ToInt32(NOW.Substring(14, 2)) - Convert.ToInt32(data_of_start.Substring(14, 2));
                    int ss_elapsed = Convert.ToInt32(NOW.Substring(17, 2)) - Convert.ToInt32(data_of_start.Substring(17, 2));

                    int Secs_total_elapsed =/* dd_elapsed * 86400 +*/hh_elapsed * 3600 + mm_elapsed * 60 + ss_elapsed; 
                    if (Secs_total_elapsed >= TimeOfTuning_inSeconds)
                    {
                        data_of_start = string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.Now); //обнуление, по факту
                        int stepss = CalculateSteps_viaWLs();
                        DisableFlipButtons();
                        if (!TSMI_Load_EXWL_C.Checked) { WLs_toTune = null; }
                        New_SnapAndSaveMassive_viaWLs(AO_StartWL, AO_EndWL, stepss, WLs_toTune);
                        EnableFlipButtons();
                    }
                }
                catch (Exception exc)
                {
                    Log.Error("Непредвиденная ошибка во время съемки.");
                    Log.Error("ORIGINAL:" + exc.Message);
                }
            }
        }

        private void TSMI_Tuning_Pereodical_Click(object sender, EventArgs e)
        {
            if (!TSMI_Tuning_Pereodical.Checked)
            {
                isTimeSesNeeded = true;
                int stepss = CalculateSteps_viaWLs();
                DisableFlipButtons();
                if (!TSMI_Load_UDWL_Curve.Checked) { WLs_toTune = null; }
                data_of_start = string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.Now);
                New_SnapAndSaveMassive_viaWLs(AO_StartWL, AO_EndWL, stepss, WLs_toTune);
                EnableFlipButtons();
            }
            else
            {
                isTimeSesNeeded = false;
            }
            TSMI_Tuning_Pereodical.Checked = !TSMI_Tuning_Pereodical.Checked;
        }

        private void exposureWLCurveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void TSMI_CurveCreating_ExposureWL_Click(object sender, EventArgs e)
        {
            if (!TSMI_CurveCreating_ExposureWL.Checked)
            {
                TSMI_CurveCreating_ExposureWL.Checked = true;
                TSMI_CurveCreating_ExposureWL.Text = "Exposure - WL curve (creating...)";
                AO_StartWL = Convert.ToInt32(NUD_StartL.Text);
                AO_EndWL = Convert.ToInt32(NUD_FinishL.Text);
                AO_StepWL = Convert.ToInt16(NUD_StepL.Text);
                BkGrWorker_forExpCurveBuilding.RunWorkerAsync();
            }
            else
            {
                TSMI_CurveCreating_ExposureWL.Checked = false;
                TSMI_CurveCreating_ExposureWL.Text = "Exposure - WL curve";
                BkGrWorker_forExpCurveBuilding.CancelAsync();
            }
        }

        private void ChB_SweepEnabled_CheckedChanged(object sender, EventArgs e)
        {
            TLP_Sweep_EasyMode.Enabled = ChB_SweepEnabled.Checked;
            AO_Sweep_Needed = ChB_SweepEnabled.Checked;
        }

        private void NUD_FreqDeviation_ValueChanged(object sender, EventArgs e)
        {
            AO_FreqDeviation = (double)NUD_FreqDeviation.Value;
        }

        private void NUD_TimeFdev_ValueChanged(object sender, EventArgs e)
        {
            AO_TimeDeviation = (double)NUD_TimeFdev.Value;
            AO_FreqDeviation_Max_byTime = AO_TimeDeviation / (1000.0f / Filter.AO_ExchangeRate_Min);
            /*NUD_FreqDeviation.Maximum = (decimal)
                (AO_FreqDeviation_Max_byTime < Filter.AO_FreqDeviation_Max ? AO_FreqDeviation_Max_byTime : Filter.AO_FreqDeviation_Max);*/
            NUD_FreqDeviation.Maximum = (decimal)(Filter.AO_FreqDeviation_Max);
            NUD_FreqDeviation.Minimum = (decimal)(Filter.AO_FreqDeviation_Min);
        }

        private void Timer_Sweep_Tick(object sender, EventArgs e)
        {
            if (AO_WL_Controlled_byslider)
            {
                if (AO_Sweep_Needed)
                {
                    if (timer_for_sweep.ElapsedMilliseconds > 600)
                    {
                        float data_CurrentWL = (float)(TrB_CurrentWL.Value / AO_WL_precision);
                        timer_for_sweep.Reset();
                        ReSweep(data_CurrentWL);
                    }
                }
            }
        }

        private void TSMI_Tuning_Exposure_Click(object sender, EventArgs e)
        {
           
        }

        private void TSMI_Tuning_Irregular_Click(object sender, EventArgs e)
        {
           
        }

        private void TSMI_Load_UD_Curve_Click(object sender, EventArgs e)
        {
            if (TSMI_Load_UDWL_Curve.Checked)
            {
                WayToCurv_wl = LDZ_Code.Files.OpenFiles("Choose your tune file", true, false, ".txt")[0];
                if (WayToCurv_wl == "")
                {
                    TSMI_Load_UDWL_Curve.Text = "Load user-defined curve";
                    TSMI_Load_UDWL_Curve.Checked = !TSMI_Load_UDWL_Curve.Checked;
                }
                else
                {
                    TSMI_Load_UDWL_Curve.Text = "Curve: " + Path.GetFileName(WayToCurv_wl);
                    var allstrings = LDZ_Code.Files.Read_txt(WayToCurv_wl);
                    float[] mass = null, mass2 = null;
                    LDZ_Code.Files.Get_WLData_byKnownCountofNumbers(1, allstrings.ToArray(), out mass, out WLs_toTune, out mass2);
                    List<float> data = new List<float>(WLs_toTune);
                    data.Reverse();
                    for (int i = 0; i < data.Count(); i++)
                    {


                        if ((data[i] < MinimumWL) || (data[i] > MaximumWL))
                        {
                            LogMessage(String.Format("Обнаруженная в списке длина волны {0} не принадлежит диапазону {1}-{2}. Съемка на этой длине волны производиться не будет.",
                              data[i], MinimumWL, MaximumWL));
                            data.RemoveAt(i);
                            i--;
                        }
                    }
                    WLs_toTune = data.ToArray();
                    LogMessage("Перестройка по кривой длин волн включена.");
                }
            }
            else
            {
                TSMI_Load_UDWL_Curve.Text = "Load user-defined curve";
                WayToCurv_wl = "";
                LogMessage("Перестройка по кривой длин волн отключена.");
            }
            TSMI_Load_UDWL_Curve.Checked = !TSMI_Load_UDWL_Curve.Checked;
        }

        private void startTuningToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void TSMI_Start_UDWL_tune_Click(object sender, EventArgs e)
        {
            data_of_start = string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.Now);
            int stepss = CalculateSteps_viaWLs();
            DisableFlipButtons();
            if (!TSMI_Load_UDWL_Curve.Checked) { WLs_toTune = null; }
            New_SnapAndSaveMassive_viaWLs(AO_StartWL, AO_EndWL, stepss, WLs_toTune);
            EnableFlipButtons();
        }

        private void TSMI_Load_EXWL_C_Click(object sender, EventArgs e)
        {
            if (!TSMI_Load_EXWL_C.Checked)
            {
                WayToCurv_exp = OpenExpcfgSearcher();
                if (WayToCurv_exp == "")
                {
                    TSMI_Load_EXWL_C.Text = "Load Exposure - WL curve";
                    TSMI_Tuning_Exposure.Checked = !TSMI_Tuning_Exposure.Checked;
                }
                else
                {
                    TSMI_Load_EXWL_C.Text = "Curve: " + WayToCurv_exp;

                    List<int> wls = new List<int>();
                    List<double> exps = new List<double>();
                    List<double> exps_ref = new List<double>();
                    double reference_exposure = (true == true) ? -1 : AbsValExp.Value;
                    AO_MinimumWL = Convert.ToInt32(NUD_StartL.Text);
                    AO_MaximumWL = Convert.ToInt32(NUD_FinishL.Text);
                    double Gain = 0, FPS = 0;
                    ExpCurve.Get_Interpolated_WlExpCurveFromDirectory(WayToCurv_exp, MinimumWL, MaximumWL, (int)AO_MinimumWL, (int)AO_MaximumWL, (int)AO_StepWL, 
                        ref wls, ref exps,ref exps_ref, ref Gain, ref FPS, ref reference_exposure);
                    LogMessage("Перестройка по кривой включена.");
                }
            }
            else
            {
                TSMI_Load_EXWL_C.Text = "Load Exposure - WL curve";
                WayToCurv_exp = "";
                LogMessage("Перестройка по кривой отключена.");
            }
            TSMI_Load_EXWL_C.Checked = !TSMI_Load_EXWL_C.Checked;
        }

        private void startTuningToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            data_of_start = string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.Now);
            int stepss = CalculateSteps_viaWLs();
            DisableFlipButtons();
            if (!TSMI_Load_UDWL_Curve.Checked) { WLs_toTune = null; }
            New_SnapAndSaveMassive_viaWLs((int)AO_StartWL, (int)AO_EndWL, stepss, null);
            EnableFlipButtons();
        }

        System.Diagnostics.Stopwatch SW = new System.Diagnostics.Stopwatch();
        decimal finalExposure = 0;
        const int WLS_at_all = 8;
        int[] times_to_sleep = new int[WLS_at_all];
        float[] WLs_2set = new float[WLS_at_all];
        double Exposure_was = 0;
        float WL_was = 500;

        //Новая концепция: мы включаем автокопирование фреймов в буфер. 
        //При первом копировании мы включаем перестройку. Поскольку второй фрейм начал регистрироваться раньше, чем началась перестройка,
        // то информативным является третий кадр.
        string Last_and_new_Image2save = "1.tiff";
        bool GettingHyperSpectralImage = false;
        private void B_Get_HyperSpectral_Image_Click(object sender, EventArgs e)
        {
            GettingHyperSpectralImage = true;
            if (ChB_SpectralCycle.Checked)
            {
                BGW_SpectralCycle.CancelAsync();
                ByteMass_precalculated_list.Clear();
                ChB_SpectralCycle.Checked = false;
            }

            finalExposure = 0;
            if(vcdProp.Available(VCDIDs.VCDID_Exposure)) Exposure_was = AbsValExp.Value;
            
            SW.Reset();
            //извлечение данных об экспозиции для каждой ДВ
            for(int i =0;i<WLS_at_all;++i)
            {
                //поиск элемента управления с заданным номером для ДВ и копирование значения
                var EXP_control = this.Controls.Find("NUD_Multi_ex_time" + (i+1).ToString(),true);
                decimal data_var = (decimal)(EXP_control[0] as NumericUpDown).Value;
                times_to_sleep[i] = (int)(data_var * 1000);
                finalExposure += data_var;
                //поиск элемента управления с заданным номером для вр.эксп. и копирование значения
                var WL_control = this.Controls.Find("NUD_Multi_WL" + (i+1).ToString(),true);
                WLs_2set[i] = (float)(WL_control[0] as NumericUpDown).Value;
            }

            WL_was = Filter.WL_Current;
            //Проверка времени экспонирования
            if (vcdProp.Available(VCDIDs.VCDID_Exposure))
            {
                if (((double)finalExposure - AbsValExp.RangeMax) > 0.000001)
                {
                    Log.Error("Суммарное заданое время экспонирования " + finalExposure.ToString() + " сек. больше максимально возможного для данной камеры ("
                        + AbsValExp.RangeMax.ToString() + " сек.)");
                    return;
                }
                else if (((double)finalExposure - AbsValExp.RangeMin) < 0.000001)
                {
                    Log.Error("Суммарное заданое время экспонирования " + finalExposure.ToString() + " сек. меньше минимально возможного для данной камеры ("
                        + AbsValExp.RangeMin.ToString() + " сек.)");
                    return;
                }
            }
            

            if ((precalculating_mode) && (Filter.FilterType == FilterTypes.STC_Filter))
            {
                for (int i = 0; i < WLS_at_all; i++)
                {
                    if (times_to_sleep[i] >= 0.001f)
                    {
                        float curHZ = Filter.Get_HZ_via_WL(WLs_2set[i]);
                        ByteMass_precalculated_list.Add((Filter as STC_Filter).Create_byteMass_forHzTune(curHZ));
                    }
                }
            }

            //ImgName calculating
            string SCRName = CheckScreenShotBasicName();
            string date = GetTimeString();

            string WLS_name = "";
            decimal deltaa = (decimal)(1.0/(System.Math.Pow(10, NUD_Multi_ex_time1.DecimalPlaces)));
            if (NUD_Multi_ex_time1.Value >= deltaa) WLS_name += ("_" + NUD_Multi_WL1.Value.ToString());
            if (NUD_Multi_ex_time2.Value >= deltaa) WLS_name += ("_" + NUD_Multi_WL2.Value.ToString());
            if (NUD_Multi_ex_time3.Value >= deltaa) WLS_name += ("_" + NUD_Multi_WL3.Value.ToString());
            if (NUD_Multi_ex_time4.Value >= deltaa) WLS_name += ("_" + NUD_Multi_WL4.Value.ToString());
            if (NUD_Multi_ex_time5.Value >= deltaa) WLS_name += ("_" + NUD_Multi_WL5.Value.ToString());
            if (NUD_Multi_ex_time6.Value >= deltaa) WLS_name += ("_" + NUD_Multi_WL6.Value.ToString());
            if (NUD_Multi_ex_time7.Value >= deltaa) WLS_name += ("_" + NUD_Multi_WL7.Value.ToString());
            if (NUD_Multi_ex_time8.Value >= deltaa) WLS_name += ("_" + NUD_Multi_WL8.Value.ToString());

            string local_name = SnapImageStyle.Directory + date + "_" + WLS_name + SnapImageStyle.Extension;
           
            Last_and_new_Image2save = local_name;

            for (int i = 0; i < times_of_WLset.Count(); i++)
                times_of_WLset[i] = 0;

            Log.Message("---------------------------------");

            icImagingControl1.LiveStop();
            TIS.Imaging.ImageBuffer[] rval = new TIS.Imaging.ImageBuffer[1];
            icImagingControl1.ImageRingBufferSize = 1;
            icImagingControl1.LiveStart();
            curfhs.SnapMode = false; //Включает регистрацию фреймов

           // SW.Restart();
            // BGW_SpectralImageTuning.RunWorkerAsync();

            //  curfhs.SnapImage(); // именно из-за этого curfhs.SnapMode = true;
            //  rval[0] = curfhs.LastAcquiredBuffer;
            //   rval[0].SaveAsTiff(local_name);

            if (!icImagingControl1.LiveVideoRunning) icImagingControl1.LiveStart();
               // Filter.Set_Wl(WL_was);
               
           
        }
        private void B_Get_HyperSpectral_Image_Click2(object sender, EventArgs e)
        {
            if (ChB_SpectralCycle.Checked)
            {
                BGW_SpectralCycle.CancelAsync();
                ByteMass_precalculated_list.Clear();
                ChB_SpectralCycle.Checked = false;
            }

            finalExposure = 0;
            if (vcdProp.Available(VCDIDs.VCDID_Exposure)) Exposure_was = AbsValExp.Value;

            // //  icImagingControl1.LiveCaptureContinuous = true;
            // // curfhs.SnapMode = true; //взаимоисключающие вещи с предыдущим

            const bool use_inCamera_invoke = false;
            const bool use_one_image = true;


            //  Filter.Set_Wl((float)NUD_Multi_WL1.Value); Log.Message("Перестройка на ДВ1: " + NUD_Multi_WL1.Value.ToString());
            SW.Reset();
            for (int i = 0; i < WLS_at_all; ++i)
            {
                //поиск элемента управления с заданным номером для ДВ и копирование значения
                var EXP_control = this.Controls.Find("NUD_Multi_ex_time" + (i + 1).ToString(), true);
                decimal data_var = (decimal)(EXP_control[0] as NumericUpDown).Value;
                times_to_sleep[i] = (int)(data_var * 1000);
                finalExposure += data_var;
                //поиск элемента управления с заданным номером для вр.эксп. и копирование значения
                var WL_control = this.Controls.Find("NUD_Multi_WL" + (i + 1).ToString(), true);
                WLs_2set[i] = (float)(WL_control[0] as NumericUpDown).Value;
            }
            WL_was = Filter.WL_Current;
            if (vcdProp.Available(VCDIDs.VCDID_Exposure))
            {
                if (finalExposure > (decimal)AbsValExp.RangeMax)
                {
                    Log.Error("Суммарное заданое время экспонирования " + finalExposure.ToString() + " сек. больше максимально возможного для данной камеры ("
                        + AbsValExp.RangeMax.ToString() + " сек.)");
                    return;
                }
                else if (finalExposure < (decimal)AbsValExp.RangeMin)
                {
                    Log.Error("Суммарное заданое время экспонирования " + finalExposure.ToString() + " сек. меньше минимально возможного для данной камеры ("
                        + AbsValExp.RangeMin.ToString() + " сек.)");
                    return;
                }
            }
            Log.Message("---------------------------------");

            if (use_one_image)
            {
                if (vcdProp.Available(VCDIDs.VCDID_Exposure)) LoadExposure_ToCam(ref AbsValExp, (double)finalExposure);
                Thread.Sleep((int)((Exposure_was/*+ finalExposure*/) * 1000));

                icImagingControl1.LiveStop();
                TIS.Imaging.ImageBuffer[] rval = new TIS.Imaging.ImageBuffer[1];
                icImagingControl1.ImageRingBufferSize = 1;
                icImagingControl1.LiveStart();
                curfhs.SnapMode = true; //Резрешает использование SnapImage

                if ((precalculating_mode) && (Filter.FilterType == FilterTypes.STC_Filter))
                {
                    for (int i = 0; i < WLS_at_all; i++)
                    {
                        if (times_to_sleep[i] >= 0.001f)
                        {
                            float curHZ = Filter.Get_HZ_via_WL(WLs_2set[i]);
                            ByteMass_precalculated_list.Add((Filter as STC_Filter).Create_byteMass_forHzTune(curHZ));
                        }
                    }
                }

                SW.Restart();
                BGW_SpectralImageTuning.RunWorkerAsync();
                if (use_inCamera_invoke)
                {
                    BGW_SpectralImageGrabbing.RunWorkerAsync();
                    Log.Message("Image grabbing is started...");
                }
                else
                {
                    //  curfhs.SnapImage(); // именно из-за этого curfhs.SnapMode = true;
                    //  rval[0] = curfhs.LastAcquiredBuffer;
                    string SCRName = CheckScreenShotBasicName();
                    string date = GetFullDateString();
                    string local_name = SnapImageStyle.Directory + SCRName + "_" + date + "_"
                        + NUD_Multi_WL1.Value.ToString() + "_" + NUD_Multi_WL8.Value.ToString() + SnapImageStyle.Extension;
                    //   rval[0].SaveAsTiff(local_name);

                    if (!icImagingControl1.LiveVideoRunning) icImagingControl1.LiveStart();
                    Filter.Set_Wl(WL_was);
                }
            }
            else
            {

            }
        }


        private void BGW_SpectralImageGrabbing_DoWork(object sender, DoWorkEventArgs e)
        {

            long time1 = SW.ElapsedMilliseconds;
            if (icImagingControl1.InvokeRequired) icImagingControl1.Invoke((Action)(() => {icImagingControl1.MemorySnapImage(); } ));
            else icImagingControl1.MemorySnapImage();
            Log.Message("Время на захват изображения: "+(SW.ElapsedMilliseconds - time1).ToString()+" мс");
            Log.Message("---------------------------------");
        }

        long[] times_of_WLset = new long[WLS_at_all];
        
        private void BGW_SpectralImageTuning_DoWork2(object sender, DoWorkEventArgs e)
        {
            B_Get_HyperSpectral_Image.Invoke(new MethodInvoker(delegate
            {
                B_Get_HyperSpectral_Image.Enabled = false;
            }));
           // B_Get_HyperSpectral_Image.Enabled = false;

            var FP = Filter as STC_Filter;
            if ((precalculating_mode) && (Filter.FilterType == FilterTypes.STC_Filter))
            {

                for (int i = 0; i < WLS_at_all; i++)
                {
                    if (times_to_sleep[i] >= 0.0001f)
                    {
                        FP.Set_Hz_via_bytemass(ByteMass_precalculated_list[i]);
                        /*    Log.Message("Перестройка на ДВ" + (i + 1).ToString() + ": "
                                + WLs_2set[i].ToString() + ". Прошло времени: " + SW.ElapsedMilliseconds);
                            times_of_WLset[i] = SW.ElapsedMilliseconds;*/
                        // worker.ReportProgress((int)WLs_2set[i]);
                        Thread.Sleep(times_to_sleep[i]);
                    }
                }

                int j_data = 0;
                for (int i = 0; (i < WLS_at_all) && (j_data < 2); i++)
                {
                    if (times_to_sleep[i] >= 0.0001f)
                    {
                        j_data++;
                        FP.Set_Hz_via_bytemass(ByteMass_precalculated_list[i]);
                        //Log.Message("Перестройка на ДВ1: " + NUD_Multi_WL1.Value.ToString() +". Прошло времени: "+SW.ElapsedMilliseconds);
                        Thread.Sleep(times_to_sleep[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < WLS_at_all; i++)
                {
                    if (times_to_sleep[i] != 0)
                    {
                        Filter.Set_Wl(WLs_2set[i]);
                     //   Log.Message("Перестройка на ДВ" + (i + 1).ToString() + ": "
                    //        + WLs_2set[i].ToString() + ". Прошло времени: " + SW.ElapsedMilliseconds);
                    //    times_of_WLset[i] = SW.ElapsedMilliseconds;
                        Thread.Sleep(times_to_sleep[i]);
                    }
                }
                //second cycle for compensation of asynchrous starting of grab and tuning
                for (int i = 0; i < WLS_at_all; i++)
                {
                    if (times_to_sleep[i] != 0)
                    {
                        Filter.Set_Wl(WLs_2set[i]);
                  //      Log.Message("Перестройка на ДВ" + (i + 1).ToString() + ": "
                   //         + WLs_2set[i].ToString() + ". Прошло времени: " + SW.ElapsedMilliseconds);
                  //      times_of_WLset[i] = SW.ElapsedMilliseconds;
                        Thread.Sleep(times_to_sleep[i]);
                    }
                }

             /*   int j_data = 0;
                for (int i = 0; (i < WLS_at_all) && (j_data < 2); i++)
                {
                    if (times_to_sleep[i] != 0)
                    {
                        j_data++;
                        Filter.Set_Wl(WLs_2set[i]);
                        //Log.Message("Перестройка на ДВ1: " + NUD_Multi_WL1.Value.ToString() +". Прошло времени: "+SW.ElapsedMilliseconds);
                        Thread.Sleep(times_to_sleep[i]);
                    }
                }*/
            }
        }
        private void BGW_SpectralImageTuning_DoWork(object sender, DoWorkEventArgs e)
        {
            B_Get_HyperSpectral_Image.Invoke(new MethodInvoker(delegate
            {
                B_Get_HyperSpectral_Image.Enabled = false;
            }));
            
            while(!(sender as BackgroundWorker).CancellationPending)
            {
                
                    for (int i = 0; i < WLS_at_all; i++)
                    {
                        if (times_to_sleep[i] != 0)
                        {
                            Filter.Set_Wl(WLs_2set[i]);
                              /* Log.Message("Перестройка на ДВ" + (i + 1).ToString() + ": "
                                    + WLs_2set[i].ToString() + ". Прошло времени: " + SW.ElapsedMilliseconds);*/
                                times_of_WLset[i] = SW.ElapsedMilliseconds;
                            Thread.Sleep(times_to_sleep[i]);
                        }
                    }
                
            }
            e.Cancel = true;
        }
        private void BGW_SpectralImageTuning_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                Log.Message("Захват кадра осуществлен!");

                string data_times = "";
                for (int i = 0; i < WLS_at_all - 1; i++)
                {
                    data_times += times_of_WLset[i].ToString() + " :::";
                }
                data_times += times_of_WLset[7].ToString();
                //B_Tun
                Log.Message("Времена перестроек: " + data_times);
            }
            else if (e.Error != null)
            {
                Log.Message("Error: " + e.Error.Message);
            }
            else
            {
                string data_times = "";
                for(int i =0;i<WLS_at_all-1;i++)
                {
                    data_times += times_of_WLset[i].ToString() + " :::";
                }
                data_times += times_of_WLset[7].ToString();
                //B_Tun
                Log.Message("Времена перестроек: " + data_times);
            }

            GettingHyperSpectralImage = false;
            B_Get_HyperSpectral_Image.Invoke(new MethodInvoker(delegate
            {
                B_Get_HyperSpectral_Image.Enabled = true;
            }));
        }


        private void BGW_SpectralImageGrabbing_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            /* if (e.Cancelled == true)
             {
                 Log.Message("Canceled!");
             }
             else */
            if (e.Error == null)
            {
                SW.Stop();
                Log.Message("Image Grabbed! Time elapsed: " + SW.ElapsedMilliseconds.ToString());
                LoadExposure_ToCam(ref AbsValExp, Exposure_was);
                //IMG Save here

                string SCRName = CheckScreenShotBasicName();
                string date = GetFullDateString();
                icImagingControl1.MemorySaveImage(SnapImageStyle.Directory + SCRName + "_" + date + "_"
                    + NUD_Multi_WL1.Value.ToString() + "_" + NUD_Multi_WL2.Value.ToString() + "_" + NUD_Multi_WL3.Value.ToString() + SnapImageStyle.Extension);
                icImagingControl1.LiveStart();
                Filter.Set_Wl(WL_was);
            }
            else
            {
                Log.Message("Error: " + e.Error.Message);
            }
        }
        private void NUD_Multi_ex_time3_ValueChanged(object sender, EventArgs e)
        {

        }
        int frames_gotten_spec = 0;
        private void icImagingControl1_ImageAvailable(object sender, ICImagingControl.ImageAvailableEventArgs e)
        {
            if (GettingHyperSpectralImage)
            {
                frames_gotten_spec++;
                switch (frames_gotten_spec)
                {
                    case 1:
                        {
                            BGW_SpectralImageTuning.RunWorkerAsync(); SW.Restart();
                            break;
                        }
                    case 2:
                        {

                            break;
                        }
                    case 3:
                        {
                            curfhs.LastAcquiredBuffer.SaveAsTiff(Last_and_new_Image2save);
                            Log.Message("Время экспонирования кадра(с):" + (curfhs.LastAcquiredBuffer.SampleEndTime - curfhs.LastAcquiredBuffer.SampleStartTime).ToString());
                            curfhs.SnapMode = true;
                            BGW_SpectralImageTuning.CancelAsync();
                            frames_gotten_spec = 0;
                            Filter.Set_Wl(WL_was);

                            // curfhs.LastAcquiredBuffer.
                            break;
                        }
                    default:
                        {
                            curfhs.SnapMode = true;
                            BGW_SpectralImageTuning.CancelAsync();
                            frames_gotten_spec = 0;
                            Filter.Set_Wl(WL_was);
                            break;
                        }
                }
            }
            else if(isDftTurnedOn)
            {
                if (DFTWindow != null)
                {
                    ImForDft = new Bitmap(icImagingControl1.ImageBuffers[0].Bitmap);
                  //  ImForDft = new Bitmap("tests.jpg");
                    DFTWindow.DFTFromMat(ImForDft);
                   // isDftTurnedOn = false;
                }
            }
        }
        bool precalculating_mode = true;
        List<byte[]> ByteMass_precalculated_list = new List<byte[]>();
        private void ChB_SpectralCycle_CheckedChanged(object sender, EventArgs e)
        {
            if(ChB_SpectralCycle.Checked)
            {
                finalExposure = 0;
                SW.Reset();
                for (int i = 0; i < WLS_at_all; ++i)
                {
                    //поиск элемента управления с заданным номером для ДВ и копирование значения
                    var EXP_control = this.Controls.Find("NUD_Multi_ex_time" + (i + 1).ToString(), true);
                    decimal data_var = (decimal)(EXP_control[0] as NumericUpDown).Value;
                    times_to_sleep[i] = (int)(data_var * 1000);
                    finalExposure += data_var;
                    //поиск элемента управления с заданным номером для вр.эксп. и копирование значения
                    var WL_control = this.Controls.Find("NUD_Multi_WL" + (i + 1).ToString(), true);
                    WLs_2set[i] = (float)(WL_control[0] as NumericUpDown).Value;
                }
                WL_was = Filter.WL_Current;

                Log.Message("---------------------------------");

                if((precalculating_mode)&&(Filter.FilterType == FilterTypes.STC_Filter))
                {
                    for (int i = 0; i < WLS_at_all; i++)
                    {
                        if (times_to_sleep[i] >= 0.001f)
                        {
                            float curHZ = Filter.Get_HZ_via_WL(WLs_2set[i]);
                            ByteMass_precalculated_list.Add((Filter as STC_Filter).Create_byteMass_forHzTune(curHZ));
                        }
                    }
                }

                BGW_SpectralCycle.RunWorkerAsync();
            }
            else
            {
                BGW_SpectralCycle.CancelAsync();
                ByteMass_precalculated_list.Clear();
            }
        }

        private void BGW_SpectralCycle_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            var FP = Filter as STC_Filter;
            if ((precalculating_mode) && (Filter.FilterType == FilterTypes.STC_Filter))
            {
                while (worker.CancellationPending == false)
                {
                    for (int i = 0; i < WLS_at_all; i++)
                    {
                        if (times_to_sleep[i] >= 0.001f)
                        {
                            FP.Set_Hz_via_bytemass(ByteMass_precalculated_list[i]);
                            worker.ReportProgress((int)WLs_2set[i]);
                            Thread.Sleep(times_to_sleep[i]);
                        }
                        if (worker.CancellationPending) break;
                    }
                }
            }
            else
            {
                while (worker.CancellationPending == false)
                {
                    for (int i = 0; i < WLS_at_all; i++)
                    {
                        if (times_to_sleep[i] != 0)
                        {
                            Filter.Set_Wl(WLs_2set[i]);
                            worker.ReportProgress((int)WLs_2set[i]);
                            Thread.Sleep(times_to_sleep[i]);
                        }
                        if (worker.CancellationPending) break;
                    }
                }
            }
        }

        private void BGW_SpectralCycle_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            Log.Message("Установленная длина волны " + e.ProgressPercentage + " нм");
        }

        private void BGW_SpectralCycle_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Log.Message("Циклическая перестройка остановлена!");
            Filter.Set_Wl(WL_was);
        }

        string lastSavedBuf_name = "";
        bool AllSeriesIsSaved = true;
        private void BGW_Saver_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var Timer = new System.Diagnostics.Stopwatch();
                AllSeriesIsSaved = IMG_buffers_mass.Count == 0 ? true : false;
                //IMG_buf_toSave.Add(new { Frames_and_Names = new List<dynamic>(Frames_and_Names_cur), Dir_name = NameDirectory.Substring(0, NameDirectory.Count() - 2) });
                while (!AllSeriesIsSaved)
                {
                    if (IMG_buffers_mass.Count == 1)
                    { int ahg = 0; }

                    var current_frame_buf = IMG_buffers_mass[0].Frames_and_Names;
                    for (int i = current_frame_buf.Count - 1; i != -1; i--)
                    {
                        /*  Timer.Start();
                          while (Timer.ElapsedMilliseconds < 100) { }
                          Timer.Reset();*/
                        current_frame_buf[i].Buffer.SaveAsTiff(current_frame_buf[i].Name);
                        // current_frame_buf[i].Buffer.Dispose();
                        //Log.Error("кадр 0," + i.ToString()+" очищен!");
                        current_frame_buf.RemoveAt(i);
                        // Log.Error("кадр 0," + i.ToString() + " удален!");
                    }


                    IMG_buffers_mass[0].Frames_and_Names.Clear();
                    lastSavedBuf_name = IMG_buffers_mass[0].Dir_name;
                    IMG_buffers_mass.RemoveAt(0);
                    for(int j=0;j<10;j++) //подождем 10*0.5с, вдруг придет еще одна серия
                    {
                        if (IMG_buffers_mass.Count == 0)
                        {
                            Thread.Sleep(500); AllSeriesIsSaved = true;
                        }
                        else { AllSeriesIsSaved = false; break; }
                    }
                    BGW_Saver.ReportProgress(0);
                }
            }
            catch (Exception exc)
            {
                Log.Error("Ошибка во время многопоточного сохранения: " + exc.Message);

            }
        }

        private void BGW_Saver_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AllSeriesIsSaved = IMG_buffers_mass.Count == 0 ? true : false; //на всякий случай проверим, нет ли еще серий в памяти
            if (!AllSeriesIsSaved) BGW_Saver.RunWorkerAsync();
        }

        private void BGW_Saver_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Log.Message("Данные в директории " + lastSavedBuf_name + " сохранены! Осталось серий в памяти: " + IMG_buffers_mass.Count.ToString());
            AllSeriesIsSaved = IMG_buffers_mass.Count == 0 ? true : false;
        }

      

        private void TSMI_MThreadSave_Click(object sender, EventArgs e)
        {
            TSMI_MThreadSave.Checked = TSMI_MThreadSave.Checked ? false : true;
        }


        int TimeOfTuning_inSeconds = 30;
        private void NUD_TimingInCycle_ValueChanged(object sender, EventArgs e)
        {
            TimeOfTuning_inSeconds = (int)NUD_TimingInCycle.Value;
        }

        private void NUD_ExposureVal_ValueChanged(object sender, EventArgs e)
        {
            if ((!vcdProp.Automation[VCDIDs.VCDID_Exposure]))
            {
                int toslide = 0;
                toslide = Exposure_real2slide((double)(NUD_ExposureVal.Value));
                if ((toslide < (TrBExposureVal.Maximum + 1)) && (toslide > (TrBExposureVal.Minimum - 1)))
                {
                    TrBExposureVal.Value = toslide;
                    TrBExposureVal_Scroll(null, null);
                }
                else
                {
                    TrBExposureVal.Value = Exposure_real2slide(AbsValExp.Default);
                    TrBExposureVal_Scroll(null, null);
                }
            }
        }

        private void NUD_GainVal_ValueChanged(object sender, EventArgs e)
        {
            if ((!vcdProp.Automation[VCDIDs.VCDID_Gain]))
            {
                decimal toslide = 0;
                toslide = NUD_GainVal.Value;
                if ((toslide < (TrBGainVal.Maximum + 1)) && (toslide > (TrBGainVal.Minimum - 1)))
                {
                    TrBGainVal.Value = (int)toslide;
                    TrBGainVal_Scroll(null, null);
                }
                else
                {
                    TrBGainVal.Value = vcdProp.DefaultValue(VCDIDs.VCDID_Gain);
                    TrBGainVal_Scroll(null, null);
                }
            }
        }

        private void TSMI_UseFileExpAsRef_Click(object sender, EventArgs e)
        {
            if(!TSMI_UseFileExpAsRef.Checked)
            {
                TSMI_UseFileExpAsRef.Checked = true;
                TSMI_UseCurrentExpAsRef.Checked = false;
                TSMI_UseAbsExposure.Checked = false;
            }
        }

        private void TSMI_UseCurrentExpAsRef_Click(object sender, EventArgs e)
        {
            if (!TSMI_UseCurrentExpAsRef.Checked)
            {
                TSMI_UseFileExpAsRef.Checked = false;
                TSMI_UseCurrentExpAsRef.Checked = true;
                TSMI_UseAbsExposure.Checked = false;
            }
        }

        private void TSMI_UseAbsExposure_Click(object sender, EventArgs e)
        {
            if (!TSMI_UseAbsExposure.Checked)
            {
                TSMI_UseFileExpAsRef.Checked = false;
                TSMI_UseCurrentExpAsRef.Checked = false;
                TSMI_UseAbsExposure.Checked = true;
            }
        }

        private void TSMI_DFTWindowCall_Click(object sender, EventArgs e)
        {
            //icImagingControl1.LiveCaptureContinuous = true;
            icImagingControl1.LiveStop();
            icImagingControl1.Sink = CurFQS;
            icImagingControl1.LiveStart();

            
            isDftTurnedOn = true;
            StopRequeted = false;
            DFTWindow = new DFTForm(this, test_frame.FrameType.Width, test_frame.FrameType.Height);
            //ImForDft = new Bitmap(test_frame.FrameType.Width, test_frame.FrameType.Height);//03032021 Больше нет необходимости инициализировать первый кадр
            DFTWindow.FormClosed += DFTWindow_FormClosed;
            DFTWindow.Show();

        }

        private void DFTWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopRequeted = true;

         /*   var InputCount = CurFQS.InputQueueSize;
            var OutCount = CurFQS.OutputQueueSize;
            var DroppedCount = CurFQS.CountOfFramesDropped;

            CurFQS.PopAllInputQueueBuffers();
            CurFQS.PopAllOutputQueueBuffers();

            InputCount = CurFQS.InputQueueSize;
            OutCount = CurFQS.OutputQueueSize;
            DroppedCount = CurFQS.CountOfFramesDropped;*/

            while(!frameProcessed)
            {
                Application.DoEvents(); //ждем обработки всех других кадров, не затормаживая основной поток
            }

            icImagingControl1.LiveStop();
            PreviousFrame = null;
            icImagingControl1.Sink = CurFSS;
            icImagingControl1.LiveStart();
        }

        IFrameQueueBuffer PreviousFrame = null;


        bool frameProcessed = false;
        FrameQueuedResult NewBufferCallback(IFrameQueueBuffer frame)
        {          
            CurFQS.QueueBuffer(frame);
            if (PreviousFrame != null && !StopRequeted)
            {
                frameProcessed = false;
                ProcessFrame(PreviousFrame);
                frameProcessed = true;
            }
            PreviousFrame = frame;
            return FrameQueuedResult.SkipReQueue;
        }
        private void ProcessFrame(IFrameQueueBuffer frame)
        {
            Log.Message("Кадр обработан!");
            var frame2 = frame.CreateBitmapWrap();
            if (DFTWindow != null)
            {
                DFTWindow.DFTFromMat(frame2);
            }
        }
        private void RB_Series_FreqMode_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void NUD_FreqStep_ValueChanged(object sender, EventArgs e)
        {
            AO_StepFreq = (float)NUD_FreqStep.Value;
        }

        private void NUD_FreqFin_ValueChanged(object sender, EventArgs e)
        {
            AO_EndFreq = (float)NUD_FreqFin.Value;
        }

        private void NUD_FreqStart_ValueChanged(object sender, EventArgs e)
        {
            AO_StartFreq = (float)NUD_FreqStart.Value;
        }

        private void PB_SeriesProgress_Click(object sender, EventArgs e)
        {

        }

        private void RB_Series_WLMode_CheckedChanged(object sender, EventArgs e)
        {
          
                NUD_StepL.Enabled = RB_Series_WLMode.Checked;
                NUD_StartL.Enabled = RB_Series_WLMode.Checked;
                NUD_FinishL.Enabled = RB_Series_WLMode.Checked;

                NUD_FreqStart.Enabled = !RB_Series_WLMode.Checked;
                NUD_FreqFin.Enabled = !RB_Series_WLMode.Checked;
                NUD_FreqStep.Enabled = !RB_Series_WLMode.Checked;

        }

        private void tests()
        {

            Filter.Read_dev_file("ampl_avm1-011-3.dev");
            Filter.PowerOn();
            Filter.Set_Wl(Filter.WL_Max);
            Filter.Set_Wl((Filter.WL_Max + Filter.WL_Min) / 2);
            Filter.Set_Wl(Filter.WL_Min);
            Filter.Set_Hz((Filter.HZ_Min + Filter.HZ_Max) / 2);
            float delta = 5;
            Filter.Set_Sweep_on((Filter.HZ_Max + Filter.HZ_Min) / 2 - delta, 2 * delta, 1, true);
            System.Threading.Thread.Sleep(2000);
            Filter.Set_Sweep_off();
            //  Filter.PowerOff();
        }
        private void InitializeComponents_byVariables()
        {
            try
            {
                ChB_Power.Checked = false;

                ChB_AutoSetWL.Checked = AO_WL_Controlled_byslider;
                L_ReqDevName.Text = Filter.Ask_required_dev_file();
                L_RealDevName.Text = Filter.Ask_loaded_dev_file();
                float data_CurWL = (Filter.WL_Max + Filter.WL_Min) / 2;
                Filter.Set_Wl(data_CurWL);

                NUD_CurrentWL.Maximum = (decimal)Filter.WL_Max;
                NUD_CurrentWL.Minimum = (decimal)Filter.WL_Min;
                NUD_CurrentWL.Value = (decimal)data_CurWL;

                TrB_CurrentWL.Maximum = (int)(Filter.WL_Max * AO_WL_precision);
                TrB_CurrentWL.Minimum = (int)(Filter.WL_Min * AO_WL_precision);       
                TrB_CurrentWL.Value = (int)(data_CurWL * AO_WL_precision);

                NUD_CurrentMHz.Maximum = (decimal)(Filter.HZ_Max);
                NUD_CurrentMHz.Minimum = (decimal)(Filter.HZ_Min);
                NUD_CurrentMHz.Value = (decimal)(Filter.HZ_Current);

                TrB_CurrentMHz.Maximum = (int)((double)NUD_CurrentMHz.Maximum * AO_HZ_precision);
                TrB_CurrentMHz.Minimum = (int)((double)NUD_CurrentMHz.Minimum * AO_HZ_precision);
                TrB_CurrentMHz.Value = (int)((double)NUD_CurrentMHz.Value * AO_HZ_precision);

                NUD_StartL.Minimum = NUD_FinishL.Minimum = NUD_StartL.Value = (decimal)Filter.WL_Min;
                NUD_StartL.Maximum = NUD_FinishL.Maximum = NUD_FinishL.Value = (decimal)Filter.WL_Max;
                NUD_StepL.Minimum = 1;
                NUD_StepL.Maximum =(decimal)(Filter.WL_Max - Filter.WL_Min);

                NUD_FreqStart.Minimum = NUD_FreqFin.Minimum = NUD_FreqStart.Value = (decimal)Filter.HZ_Min;
                NUD_FreqStart.Maximum = NUD_FreqFin.Maximum = NUD_FreqFin.Value = (decimal)Filter.HZ_Max;
                NUD_FreqStep.Minimum = (decimal)0.05;
                NUD_FreqStep.Maximum = (decimal)(Filter.HZ_Max - Filter.HZ_Min);
                

                ChB_Power.Enabled = true;

                Log.Message("Инициализация элементов управления прошла успешно!");
            }
            catch (Exception exc)
            {
                Log.Error("Инициализация элементов управления завершилась с ошибкой.");
            }
        }

        
        private void ReSweep(float p_data_CurrentWL)
        {
            Filter.Set_Sweep_off();
            float HZ_toset = Filter.Get_HZ_via_WL(p_data_CurrentWL);
            System.Drawing.PointF data_for_sweep = Filter.Sweep_Recalculate_borders(HZ_toset, (float)AO_FreqDeviation);

            Log.Message(String.Format("ЛЧМ Параметры: ДВ:{0} / Частота:{1} / Девиация частоты:{2}", p_data_CurrentWL, HZ_toset, AO_FreqDeviation));
            Log.Message(String.Format("Доступные для установки ЛЧМ параметры:  ДВ: {0} / Частота:{1} / Девиация частоты: {2} ",
                p_data_CurrentWL, HZ_toset, data_for_sweep.Y / 2));
            Log.Message(String.Format("Пересчет:  {0}+{1}", data_for_sweep.X, data_for_sweep.Y));



            var state = Filter.Set_Sweep_on(data_for_sweep.X, data_for_sweep.Y, AO_TimeDeviation, true);
            if (state != 0) Log.Error(Filter.Implement_Error(state));
            else Log.Message("Режим ЛЧМ около длины волны " + p_data_CurrentWL.ToString() + " нм запущен!");
        }
        private DialogResult OpenDevSearcher(ref string CfgToLoad, ref string CfgToLoad_fullPath)
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
                    CfgToLoad_fullPath = OPF.FileName;
                    CfgToLoad = OPF.FileName.Substring(k, OPF.FileName.Length - k);
                }
                catch (Exception ex)
                {
                    Log.Error("Не удалось считать файл с диска. Оригинал ошибки: " + ex.Message);
                }
                return DialogResult.OK;
            }
            else return DialogResult.Cancel;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Q) && e.Alt)
            {
                Application.Exit(); return;// RemovePointsInRect();
            }
            if ((e.KeyCode == Keys.S) && e.Alt)
            {
                if (icImagingControl1.LiveVideoRunning)
                    icImagingControl1.LiveStop();
                else
                    icImagingControl1.LiveStart();
            }
          /*  if ((e.KeyCode == Keys.B) && e.Alt)
            {
                StopRequeted = !StopRequeted;
                Log.Message(StopRequeted.ToString());
            }*/

        }



    }

}
