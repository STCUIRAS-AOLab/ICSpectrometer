namespace ICSpec
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.icImagingControl1 = new TIS.Imaging.ICImagingControl();
            this.SaveImgParBut = new System.Windows.Forms.Button();
            this.PropBut = new System.Windows.Forms.Button();
            this.ExitBut = new System.Windows.Forms.Button();
            this.SnapshotBut = new System.Windows.Forms.Button();
            this.ChBExposureAuto = new System.Windows.Forms.CheckBox();
            this.TrBZoomOfImage = new System.Windows.Forms.TrackBar();
            this.L_Zoom = new System.Windows.Forms.Label();
            this.ContTransAfterSnapshot = new System.Windows.Forms.Button();
            this.TimerForExpGain_refresh = new System.Windows.Forms.Timer(this.components);
            this.SaveImageBut = new System.Windows.Forms.Button();
            this.LBConsole = new System.Windows.Forms.ListBox();
            this.GPCamFeat = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.TrBExposureVal = new System.Windows.Forms.TrackBar();
            this.ChBROIAutoCent = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.TrBGainVal = new System.Windows.Forms.TrackBar();
            this.label16 = new System.Windows.Forms.Label();
            this.LFPS = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.CBMResolution = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.BSelectUserResolution = new System.Windows.Forms.Button();
            this.CBoxPixelFormat = new System.Windows.Forms.ComboBox();
            this.CBSignalFormats = new System.Windows.Forms.ComboBox();
            this.CBSavingPixelFormat = new System.Windows.Forms.ComboBox();
            this.CBAvFPS = new System.Windows.Forms.ComboBox();
            this.ChBVisualiseROI = new System.Windows.Forms.CheckBox();
            this.ChBGainAuto = new System.Windows.Forms.CheckBox();
            this.CbBinning = new System.Windows.Forms.ComboBox();
            this.LBinning = new System.Windows.Forms.Label();
            this.NUD_ExposureVal = new System.Windows.Forms.NumericUpDown();
            this.NUD_GainVal = new System.Windows.Forms.NumericUpDown();
            this.L_ExpDim = new System.Windows.Forms.Label();
            this.L_GainDim = new System.Windows.Forms.Label();
            this.BSetROI = new System.Windows.Forms.Button();
            this.BROIFull = new System.Windows.Forms.Button();
            this.BAdapt = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.TBROIPointX = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.TBROIPointY = new System.Windows.Forms.TextBox();
            this.TBROIWidth = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.TBROIHeight = new System.Windows.Forms.TextBox();
            this.BTestAll = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.ChB_Power = new System.Windows.Forms.CheckBox();
            this.L_RealDevName = new System.Windows.Forms.Label();
            this.L_ReqDevName = new System.Windows.Forms.Label();
            this.B_DevOpen = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.TBNamePrefix = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.LLiveFpsLab = new System.Windows.Forms.Label();
            this.LFPSCurrent = new System.Windows.Forms.Label();
            this.FPSTimer = new System.Windows.Forms.Timer(this.components);
            this.ICInvalidateTimer = new System.Windows.Forms.Timer(this.components);
            this.TrB_CurrentWL = new System.Windows.Forms.TrackBar();
            this.B_StartS = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.BSetWL = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.GrBAOFWlSet = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.B_SetHZ = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.TrB_CurrentMHz = new System.Windows.Forms.TrackBar();
            this.label29 = new System.Windows.Forms.Label();
            this.ChB_AutoSetWL = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TabP_1_SimpleTuning = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.NUD_StartL = new System.Windows.Forms.NumericUpDown();
            this.NUD_FinishL = new System.Windows.Forms.NumericUpDown();
            this.NUD_StepL = new System.Windows.Forms.NumericUpDown();
            this.label34 = new System.Windows.Forms.Label();
            this.TLP_Sweep_EasyMode = new System.Windows.Forms.TableLayoutPanel();
            this.label28 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.NUD_FreqDeviation = new System.Windows.Forms.NumericUpDown();
            this.NUD_TimeFdev = new System.Windows.Forms.NumericUpDown();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.NUD_FreqStart = new System.Windows.Forms.NumericUpDown();
            this.NUD_FreqFin = new System.Windows.Forms.NumericUpDown();
            this.NUD_FreqStep = new System.Windows.Forms.NumericUpDown();
            this.ChB_SweepEnabled = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.RB_Series_WLMode = new System.Windows.Forms.RadioButton();
            this.RB_Series_FreqMode = new System.Windows.Forms.RadioButton();
            this.TLP_Saving_of_Frames = new System.Windows.Forms.TableLayoutPanel();
            this.L_Aquired = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.PB_Saving = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PB_SeriesProgress = new System.Windows.Forms.ProgressBar();
            this.L_Saved = new System.Windows.Forms.Label();
            this.L_Info_Aquired = new System.Windows.Forms.Label();
            this.L_Info_Saved = new System.Windows.Forms.Label();
            this.TabP_2_SpectralImg = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.L_Num = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.NUD_Multi_WL1 = new System.Windows.Forms.NumericUpDown();
            this.NUD_Multi_WL2 = new System.Windows.Forms.NumericUpDown();
            this.NUD_Multi_WL3 = new System.Windows.Forms.NumericUpDown();
            this.NUD_Multi_ex_time1 = new System.Windows.Forms.NumericUpDown();
            this.NUD_Multi_ex_time2 = new System.Windows.Forms.NumericUpDown();
            this.NUD_Multi_ex_time3 = new System.Windows.Forms.NumericUpDown();
            this.B_Get_HyperSpectral_Image = new System.Windows.Forms.Button();
            this.NUD_Multi_WL4 = new System.Windows.Forms.NumericUpDown();
            this.NUD_Multi_WL5 = new System.Windows.Forms.NumericUpDown();
            this.NUD_Multi_WL6 = new System.Windows.Forms.NumericUpDown();
            this.NUD_Multi_WL7 = new System.Windows.Forms.NumericUpDown();
            this.NUD_Multi_WL8 = new System.Windows.Forms.NumericUpDown();
            this.NUD_Multi_ex_time4 = new System.Windows.Forms.NumericUpDown();
            this.NUD_Multi_ex_time5 = new System.Windows.Forms.NumericUpDown();
            this.NUD_Multi_ex_time6 = new System.Windows.Forms.NumericUpDown();
            this.NUD_Multi_ex_time7 = new System.Windows.Forms.NumericUpDown();
            this.NUD_Multi_ex_time8 = new System.Windows.Forms.NumericUpDown();
            this.L_WL_Num1 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.ChB_SpectralCycle = new System.Windows.Forms.CheckBox();
            this.NUD_CurrentWL = new System.Windows.Forms.NumericUpDown();
            this.NUD_CurrentMHz = new System.Windows.Forms.NumericUpDown();
            this.GrBImageSettings = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label19 = new System.Windows.Forms.Label();
            this.CBFinalPixelFormat = new System.Windows.Forms.ComboBox();
            this.FlipUpDownBut = new System.Windows.Forms.Button();
            this.FlipRightLeftBut = new System.Windows.Forms.Button();
            this.BkGrWorker_forExpCurveBuilding = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Pan_FPS = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_DFTWindowCall = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_TuneSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.specialSeriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_Tuning_Exposure = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_Load_EXWL_C = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_UseFileExpAsRef = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_UseCurrentExpAsRef = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_UseAbsExposure = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_Tuning_Irregular = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_Load_UDWL_Curve = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_Start_UDWL_tune = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_Tuning_Pereodical = new System.Windows.Forms.ToolStripMenuItem();
            this.curvesCreatingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_CurveCreating_ExposureWL = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_MThreadSave = new System.Windows.Forms.ToolStripMenuItem();
            this.SessionTiming = new System.Windows.Forms.Timer(this.components);
            this.Timer_Sweep = new System.Windows.Forms.Timer(this.components);
            this.BGW_SpectralImageGrabbing = new System.ComponentModel.BackgroundWorker();
            this.BGW_SpectralImageTuning = new System.ComponentModel.BackgroundWorker();
            this.BGW_SpectralCycle = new System.ComponentModel.BackgroundWorker();
            this.BGW_Saver = new System.ComponentModel.BackgroundWorker();
            this.label42 = new System.Windows.Forms.Label();
            this.NUD_TimingInCycle = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.icImagingControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrBZoomOfImage)).BeginInit();
            this.GPCamFeat.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrBExposureVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrBGainVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_ExposureVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_GainVal)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrB_CurrentWL)).BeginInit();
            this.GrBAOFWlSet.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrB_CurrentMHz)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.TabP_1_SimpleTuning.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_StartL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_FinishL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_StepL)).BeginInit();
            this.TLP_Sweep_EasyMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_FreqDeviation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_TimeFdev)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_FreqStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_FreqFin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_FreqStep)).BeginInit();
            this.TLP_Saving_of_Frames.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.TabP_2_SpectralImg.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_WL1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_WL2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_WL3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_ex_time1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_ex_time2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_ex_time3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_WL4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_WL5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_WL6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_WL7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_WL8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_ex_time4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_ex_time5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_ex_time6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_ex_time7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_ex_time8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_CurrentWL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_CurrentMHz)).BeginInit();
            this.GrBImageSettings.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.Pan_FPS.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_TimingInCycle)).BeginInit();
            this.SuspendLayout();
            // 
            // icImagingControl1
            // 
            this.icImagingControl1.BackColor = System.Drawing.Color.Maroon;
            this.icImagingControl1.DeviceListChangedExecutionMode = TIS.Imaging.EventExecutionMode.Invoke;
            this.icImagingControl1.DeviceLostExecutionMode = TIS.Imaging.EventExecutionMode.AsyncInvoke;
            this.icImagingControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.icImagingControl1.ImageAvailableExecutionMode = TIS.Imaging.EventExecutionMode.MultiThreaded;
            this.icImagingControl1.LiveDisplayPosition = new System.Drawing.Point(0, 0);
            this.icImagingControl1.Location = new System.Drawing.Point(3, 1);
            this.icImagingControl1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.icImagingControl1.Name = "icImagingControl1";
            this.tableLayoutPanel1.SetRowSpan(this.icImagingControl1, 2);
            this.icImagingControl1.Size = new System.Drawing.Size(617, 629);
            this.icImagingControl1.TabIndex = 0;
            this.icImagingControl1.OverlayUpdate += new System.EventHandler<TIS.Imaging.ICImagingControl.OverlayUpdateEventArgs>(this.icImagingControl1_OverlayUpdate);
            this.icImagingControl1.ImageAvailable += new System.EventHandler<TIS.Imaging.ICImagingControl.ImageAvailableEventArgs>(this.icImagingControl1_ImageAvailable);
            this.icImagingControl1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.icImagingControl1_Scroll);
            this.icImagingControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.icImagingControl1_MouseDown);
            this.icImagingControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.icImagingControl1_MouseMove);
            this.icImagingControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.icImagingControl1_MouseUp);
            // 
            // SaveImgParBut
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.SaveImgParBut, 2);
            this.SaveImgParBut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SaveImgParBut.Location = new System.Drawing.Point(3, 96);
            this.SaveImgParBut.Name = "SaveImgParBut";
            this.SaveImgParBut.Size = new System.Drawing.Size(282, 26);
            this.SaveImgParBut.TabIndex = 1;
            this.SaveImgParBut.Text = "Image saving parameters";
            this.SaveImgParBut.UseVisualStyleBackColor = true;
            this.SaveImgParBut.Click += new System.EventHandler(this.SaveImgParBut_Click);
            // 
            // PropBut
            // 
            this.tableLayoutPanel6.SetColumnSpan(this.PropBut, 7);
            this.PropBut.Location = new System.Drawing.Point(3, 503);
            this.PropBut.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.PropBut.Name = "PropBut";
            this.PropBut.Size = new System.Drawing.Size(282, 20);
            this.PropBut.TabIndex = 3;
            this.PropBut.Text = "Advanced properties...";
            this.PropBut.UseVisualStyleBackColor = true;
            this.PropBut.Click += new System.EventHandler(this.PropBut_Click);
            // 
            // ExitBut
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.ExitBut, 3);
            this.ExitBut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExitBut.Location = new System.Drawing.Point(3, 732);
            this.ExitBut.Name = "ExitBut";
            this.ExitBut.Size = new System.Drawing.Size(977, 24);
            this.ExitBut.TabIndex = 7;
            this.ExitBut.Text = "Quit";
            this.ExitBut.UseVisualStyleBackColor = true;
            this.ExitBut.Click += new System.EventHandler(this.ExitBut_Click);
            // 
            // SnapshotBut
            // 
            this.tableLayoutPanel5.SetColumnSpan(this.SnapshotBut, 2);
            this.SnapshotBut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SnapshotBut.Location = new System.Drawing.Point(3, 141);
            this.SnapshotBut.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.SnapshotBut.Name = "SnapshotBut";
            this.SnapshotBut.Size = new System.Drawing.Size(78, 33);
            this.SnapshotBut.TabIndex = 8;
            this.SnapshotBut.Text = "Grab";
            this.SnapshotBut.UseVisualStyleBackColor = true;
            this.SnapshotBut.Click += new System.EventHandler(this.SnapshotBut_Click);
            // 
            // ChBExposureAuto
            // 
            this.ChBExposureAuto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ChBExposureAuto.AutoSize = true;
            this.ChBExposureAuto.Location = new System.Drawing.Point(238, 9);
            this.ChBExposureAuto.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.ChBExposureAuto.Name = "ChBExposureAuto";
            this.ChBExposureAuto.Size = new System.Drawing.Size(50, 17);
            this.ChBExposureAuto.TabIndex = 26;
            this.ChBExposureAuto.Text = "Auto";
            this.ChBExposureAuto.UseVisualStyleBackColor = true;
            this.ChBExposureAuto.CheckedChanged += new System.EventHandler(this.ChBExposureAuto_CheckedChanged);
            // 
            // TrBZoomOfImage
            // 
            this.TrBZoomOfImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel2.SetColumnSpan(this.TrBZoomOfImage, 2);
            this.TrBZoomOfImage.Location = new System.Drawing.Point(3, 3);
            this.TrBZoomOfImage.Maximum = 1000;
            this.TrBZoomOfImage.Minimum = 5;
            this.TrBZoomOfImage.Name = "TrBZoomOfImage";
            this.TrBZoomOfImage.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.TrBZoomOfImage.Size = new System.Drawing.Size(45, 560);
            this.TrBZoomOfImage.TabIndex = 28;
            this.TrBZoomOfImage.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TrBZoomOfImage.Value = 100;
            this.TrBZoomOfImage.Scroll += new System.EventHandler(this.ZoomOfImage_Scroll);
            // 
            // L_Zoom
            // 
            this.L_Zoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.L_Zoom.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.L_Zoom, 2);
            this.L_Zoom.Location = new System.Drawing.Point(3, 574);
            this.L_Zoom.Name = "L_Zoom";
            this.L_Zoom.Size = new System.Drawing.Size(54, 13);
            this.L_Zoom.TabIndex = 29;
            this.L_Zoom.Text = "label2";
            // 
            // ContTransAfterSnapshot
            // 
            this.tableLayoutPanel5.SetColumnSpan(this.ContTransAfterSnapshot, 3);
            this.ContTransAfterSnapshot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContTransAfterSnapshot.Location = new System.Drawing.Point(165, 141);
            this.ContTransAfterSnapshot.Margin = new System.Windows.Forms.Padding(1, 1, 3, 1);
            this.ContTransAfterSnapshot.Name = "ContTransAfterSnapshot";
            this.ContTransAfterSnapshot.Size = new System.Drawing.Size(120, 33);
            this.ContTransAfterSnapshot.TabIndex = 32;
            this.ContTransAfterSnapshot.Text = "Continue";
            this.ContTransAfterSnapshot.UseVisualStyleBackColor = true;
            this.ContTransAfterSnapshot.Click += new System.EventHandler(this.ContTransAfterSnapshot_Click);
            // 
            // TimerForExpGain_refresh
            // 
            this.TimerForExpGain_refresh.Tick += new System.EventHandler(this.TimerForExpGain_refresh_Tick);
            // 
            // SaveImageBut
            // 
            this.tableLayoutPanel5.SetColumnSpan(this.SaveImageBut, 2);
            this.SaveImageBut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SaveImageBut.Location = new System.Drawing.Point(83, 141);
            this.SaveImageBut.Margin = new System.Windows.Forms.Padding(1);
            this.SaveImageBut.Name = "SaveImageBut";
            this.SaveImageBut.Size = new System.Drawing.Size(80, 33);
            this.SaveImageBut.TabIndex = 48;
            this.SaveImageBut.Text = "Save";
            this.SaveImageBut.UseVisualStyleBackColor = true;
            this.SaveImageBut.Visible = false;
            this.SaveImageBut.Click += new System.EventHandler(this.SaveImageBut_Click);
            // 
            // LBConsole
            // 
            this.LBConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LBConsole.FormattingEnabled = true;
            this.LBConsole.Location = new System.Drawing.Point(3, 634);
            this.LBConsole.Name = "LBConsole";
            this.LBConsole.Size = new System.Drawing.Size(617, 92);
            this.LBConsole.TabIndex = 51;
            // 
            // GPCamFeat
            // 
            this.GPCamFeat.Controls.Add(this.tableLayoutPanel6);
            this.GPCamFeat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GPCamFeat.Location = new System.Drawing.Point(686, 153);
            this.GPCamFeat.Name = "GPCamFeat";
            this.tableLayoutPanel1.SetRowSpan(this.GPCamFeat, 2);
            this.GPCamFeat.Size = new System.Drawing.Size(294, 573);
            this.GPCamFeat.TabIndex = 52;
            this.GPCamFeat.TabStop = false;
            this.GPCamFeat.Text = "Camera Features:";
            this.GPCamFeat.Enter += new System.EventHandler(this.GPCamFeat_Enter);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 7;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel6.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.TrBExposureVal, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.ChBROIAutoCent, 1, 9);
            this.tableLayoutPanel6.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.label9, 0, 10);
            this.tableLayoutPanel6.Controls.Add(this.TrBGainVal, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.label16, 0, 4);
            this.tableLayoutPanel6.Controls.Add(this.LFPS, 0, 8);
            this.tableLayoutPanel6.Controls.Add(this.label2, 0, 5);
            this.tableLayoutPanel6.Controls.Add(this.label8, 0, 9);
            this.tableLayoutPanel6.Controls.Add(this.CBMResolution, 3, 5);
            this.tableLayoutPanel6.Controls.Add(this.label27, 0, 6);
            this.tableLayoutPanel6.Controls.Add(this.label15, 0, 7);
            this.tableLayoutPanel6.Controls.Add(this.BSelectUserResolution, 2, 5);
            this.tableLayoutPanel6.Controls.Add(this.CBoxPixelFormat, 3, 6);
            this.tableLayoutPanel6.Controls.Add(this.CBSignalFormats, 3, 4);
            this.tableLayoutPanel6.Controls.Add(this.CBSavingPixelFormat, 3, 7);
            this.tableLayoutPanel6.Controls.Add(this.CBAvFPS, 3, 8);
            this.tableLayoutPanel6.Controls.Add(this.ChBVisualiseROI, 6, 9);
            this.tableLayoutPanel6.Controls.Add(this.ChBGainAuto, 6, 2);
            this.tableLayoutPanel6.Controls.Add(this.ChBExposureAuto, 6, 0);
            this.tableLayoutPanel6.Controls.Add(this.CbBinning, 5, 9);
            this.tableLayoutPanel6.Controls.Add(this.LBinning, 3, 9);
            this.tableLayoutPanel6.Controls.Add(this.NUD_ExposureVal, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.NUD_GainVal, 2, 2);
            this.tableLayoutPanel6.Controls.Add(this.L_ExpDim, 5, 0);
            this.tableLayoutPanel6.Controls.Add(this.L_GainDim, 5, 2);
            this.tableLayoutPanel6.Controls.Add(this.PropBut, 0, 14);
            this.tableLayoutPanel6.Controls.Add(this.BSetROI, 0, 13);
            this.tableLayoutPanel6.Controls.Add(this.BROIFull, 2, 13);
            this.tableLayoutPanel6.Controls.Add(this.BAdapt, 5, 13);
            this.tableLayoutPanel6.Controls.Add(this.label11, 0, 12);
            this.tableLayoutPanel6.Controls.Add(this.TBROIPointX, 1, 12);
            this.tableLayoutPanel6.Controls.Add(this.label12, 4, 12);
            this.tableLayoutPanel6.Controls.Add(this.TBROIPointY, 5, 12);
            this.tableLayoutPanel6.Controls.Add(this.TBROIWidth, 1, 10);
            this.tableLayoutPanel6.Controls.Add(this.label10, 0, 11);
            this.tableLayoutPanel6.Controls.Add(this.TBROIHeight, 1, 11);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 16;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.140868F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.143011F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.143011F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.143011F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.143011F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.143011F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.143011F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.143011F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.143011F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.143011F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.143011F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.143011F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.143011F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.143011F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(288, 554);
            this.tableLayoutPanel6.TabIndex = 75;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.tableLayoutPanel6.SetColumnSpan(this.label1, 2);
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Exposure:";
            // 
            // TrBExposureVal
            // 
            this.TrBExposureVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.SetColumnSpan(this.TrBExposureVal, 7);
            this.TrBExposureVal.Location = new System.Drawing.Point(3, 38);
            this.TrBExposureVal.Name = "TrBExposureVal";
            this.TrBExposureVal.Size = new System.Drawing.Size(282, 30);
            this.TrBExposureVal.TabIndex = 19;
            this.TrBExposureVal.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TrBExposureVal.Scroll += new System.EventHandler(this.TrBExposureVal_Scroll);
            // 
            // ChBROIAutoCent
            // 
            this.ChBROIAutoCent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ChBROIAutoCent.AutoSize = true;
            this.tableLayoutPanel6.SetColumnSpan(this.ChBROIAutoCent, 2);
            this.ChBROIAutoCent.Location = new System.Drawing.Point(51, 332);
            this.ChBROIAutoCent.Name = "ChBROIAutoCent";
            this.ChBROIAutoCent.Size = new System.Drawing.Size(65, 17);
            this.ChBROIAutoCent.TabIndex = 72;
            this.ChBROIAutoCent.Text = "Auto Center";
            this.ChBROIAutoCent.UseVisualStyleBackColor = true;
            this.ChBROIAutoCent.CheckedChanged += new System.EventHandler(this.ChBROIAutoCent_CheckedChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Gain:";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 370);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "Width:";
            // 
            // TrBGainVal
            // 
            this.TrBGainVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.SetColumnSpan(this.TrBGainVal, 7);
            this.TrBGainVal.Location = new System.Drawing.Point(3, 110);
            this.TrBGainVal.Name = "TrBGainVal";
            this.TrBGainVal.Size = new System.Drawing.Size(282, 30);
            this.TrBGainVal.TabIndex = 22;
            this.TrBGainVal.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TrBGainVal.Scroll += new System.EventHandler(this.TrBGainVal_Scroll);
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.tableLayoutPanel6.SetColumnSpan(this.label16, 2);
            this.label16.Location = new System.Drawing.Point(3, 154);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(90, 13);
            this.label16.TabIndex = 71;
            this.label16.Text = "Signal format:";
            // 
            // LFPS
            // 
            this.LFPS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.LFPS.AutoSize = true;
            this.tableLayoutPanel6.SetColumnSpan(this.LFPS, 2);
            this.LFPS.Location = new System.Drawing.Point(3, 298);
            this.LFPS.Name = "LFPS";
            this.LFPS.Size = new System.Drawing.Size(90, 13);
            this.LFPS.TabIndex = 74;
            this.LFPS.Text = "Available FPS:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.tableLayoutPanel6.SetColumnSpan(this.label2, 2);
            this.label2.Location = new System.Drawing.Point(3, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 65;
            this.label2.Text = "Resolution:";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 334);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "ROI:";
            // 
            // CBMResolution
            // 
            this.CBMResolution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.SetColumnSpan(this.CBMResolution, 4);
            this.CBMResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBMResolution.FormattingEnabled = true;
            this.CBMResolution.Items.AddRange(new object[] {
            "User"});
            this.CBMResolution.Location = new System.Drawing.Point(122, 186);
            this.CBMResolution.Name = "CBMResolution";
            this.CBMResolution.Size = new System.Drawing.Size(163, 21);
            this.CBMResolution.TabIndex = 64;
            this.CBMResolution.SelectedIndexChanged += new System.EventHandler(this.CBMResolution_SelectedIndexChanged);
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.AutoSize = true;
            this.tableLayoutPanel6.SetColumnSpan(this.label27, 3);
            this.label27.Location = new System.Drawing.Point(3, 226);
            this.label27.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(116, 13);
            this.label27.TabIndex = 23;
            this.label27.Text = "PX Format(camera):";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.tableLayoutPanel6.SetColumnSpan(this.label15, 2);
            this.label15.Location = new System.Drawing.Point(3, 262);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(90, 13);
            this.label15.TabIndex = 68;
            this.label15.Text = "PX Format:";
            // 
            // BSelectUserResolution
            // 
            this.BSelectUserResolution.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BSelectUserResolution.Location = new System.Drawing.Point(97, 186);
            this.BSelectUserResolution.Margin = new System.Windows.Forms.Padding(1);
            this.BSelectUserResolution.Name = "BSelectUserResolution";
            this.BSelectUserResolution.Size = new System.Drawing.Size(21, 21);
            this.BSelectUserResolution.TabIndex = 73;
            this.BSelectUserResolution.Text = "U";
            this.BSelectUserResolution.UseVisualStyleBackColor = true;
            this.BSelectUserResolution.Click += new System.EventHandler(this.BSelectUserResolution_Click);
            // 
            // CBoxPixelFormat
            // 
            this.CBoxPixelFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.SetColumnSpan(this.CBoxPixelFormat, 4);
            this.CBoxPixelFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBoxPixelFormat.FormattingEnabled = true;
            this.CBoxPixelFormat.Location = new System.Drawing.Point(122, 222);
            this.CBoxPixelFormat.Name = "CBoxPixelFormat";
            this.CBoxPixelFormat.Size = new System.Drawing.Size(163, 21);
            this.CBoxPixelFormat.TabIndex = 19;
            this.CBoxPixelFormat.SelectedIndexChanged += new System.EventHandler(this.CBoxPixelFormat_SelectedIndexChanged);
            // 
            // CBSignalFormats
            // 
            this.CBSignalFormats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.SetColumnSpan(this.CBSignalFormats, 4);
            this.CBSignalFormats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBSignalFormats.FormattingEnabled = true;
            this.CBSignalFormats.Location = new System.Drawing.Point(122, 150);
            this.CBSignalFormats.Name = "CBSignalFormats";
            this.CBSignalFormats.Size = new System.Drawing.Size(163, 21);
            this.CBSignalFormats.TabIndex = 70;
            this.CBSignalFormats.SelectedIndexChanged += new System.EventHandler(this.CBSignalFormats_SelectedIndexChanged);
            // 
            // CBSavingPixelFormat
            // 
            this.CBSavingPixelFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.SetColumnSpan(this.CBSavingPixelFormat, 4);
            this.CBSavingPixelFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBSavingPixelFormat.FormattingEnabled = true;
            this.CBSavingPixelFormat.Location = new System.Drawing.Point(122, 258);
            this.CBSavingPixelFormat.Name = "CBSavingPixelFormat";
            this.CBSavingPixelFormat.Size = new System.Drawing.Size(163, 21);
            this.CBSavingPixelFormat.TabIndex = 67;
            // 
            // CBAvFPS
            // 
            this.CBAvFPS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.SetColumnSpan(this.CBAvFPS, 4);
            this.CBAvFPS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBAvFPS.FormattingEnabled = true;
            this.CBAvFPS.Location = new System.Drawing.Point(122, 294);
            this.CBAvFPS.Name = "CBAvFPS";
            this.CBAvFPS.Size = new System.Drawing.Size(163, 21);
            this.CBAvFPS.TabIndex = 75;
            this.CBAvFPS.SelectedIndexChanged += new System.EventHandler(this.CBAvFPS_SelectedIndexChanged);
            // 
            // ChBVisualiseROI
            // 
            this.ChBVisualiseROI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ChBVisualiseROI.AutoSize = true;
            this.ChBVisualiseROI.BackColor = System.Drawing.Color.Transparent;
            this.ChBVisualiseROI.Location = new System.Drawing.Point(241, 332);
            this.ChBVisualiseROI.Name = "ChBVisualiseROI";
            this.ChBVisualiseROI.Size = new System.Drawing.Size(44, 17);
            this.ChBVisualiseROI.TabIndex = 76;
            this.ChBVisualiseROI.Text = "Visual";
            this.ChBVisualiseROI.UseVisualStyleBackColor = false;
            this.ChBVisualiseROI.CheckedChanged += new System.EventHandler(this.ChBVisualiseROI_CheckedChanged);
            // 
            // ChBGainAuto
            // 
            this.ChBGainAuto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ChBGainAuto.AutoSize = true;
            this.ChBGainAuto.Location = new System.Drawing.Point(238, 80);
            this.ChBGainAuto.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.ChBGainAuto.Name = "ChBGainAuto";
            this.ChBGainAuto.Size = new System.Drawing.Size(50, 17);
            this.ChBGainAuto.TabIndex = 63;
            this.ChBGainAuto.Text = "Auto";
            this.ChBGainAuto.UseVisualStyleBackColor = true;
            this.ChBGainAuto.CheckedChanged += new System.EventHandler(this.ChBGainAuto_CheckedChanged);
            // 
            // CbBinning
            // 
            this.CbBinning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.CbBinning.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbBinning.FormattingEnabled = true;
            this.CbBinning.Items.AddRange(new object[] {
            "1",
            "2",
            "4"});
            this.CbBinning.Location = new System.Drawing.Point(193, 330);
            this.CbBinning.Name = "CbBinning";
            this.CbBinning.Size = new System.Drawing.Size(42, 21);
            this.CbBinning.TabIndex = 69;
            this.CbBinning.SelectedIndexChanged += new System.EventHandler(this.CbBinning_SelectedIndexChanged);
            // 
            // LBinning
            // 
            this.LBinning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.LBinning.AutoSize = true;
            this.tableLayoutPanel6.SetColumnSpan(this.LBinning, 2);
            this.LBinning.Location = new System.Drawing.Point(122, 334);
            this.LBinning.Name = "LBinning";
            this.LBinning.Size = new System.Drawing.Size(65, 13);
            this.LBinning.TabIndex = 57;
            this.LBinning.Text = "Binning:";
            // 
            // NUD_ExposureVal
            // 
            this.NUD_ExposureVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.SetColumnSpan(this.NUD_ExposureVal, 3);
            this.NUD_ExposureVal.DecimalPlaces = 8;
            this.NUD_ExposureVal.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.NUD_ExposureVal.Location = new System.Drawing.Point(98, 7);
            this.NUD_ExposureVal.Margin = new System.Windows.Forms.Padding(2);
            this.NUD_ExposureVal.Name = "NUD_ExposureVal";
            this.NUD_ExposureVal.Size = new System.Drawing.Size(90, 20);
            this.NUD_ExposureVal.TabIndex = 77;
            this.NUD_ExposureVal.Value = new decimal(new int[] {
            33,
            0,
            0,
            196608});
            this.NUD_ExposureVal.ValueChanged += new System.EventHandler(this.NUD_ExposureVal_ValueChanged);
            // 
            // NUD_GainVal
            // 
            this.NUD_GainVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.SetColumnSpan(this.NUD_GainVal, 3);
            this.NUD_GainVal.Location = new System.Drawing.Point(98, 79);
            this.NUD_GainVal.Margin = new System.Windows.Forms.Padding(2);
            this.NUD_GainVal.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NUD_GainVal.Name = "NUD_GainVal";
            this.NUD_GainVal.Size = new System.Drawing.Size(90, 20);
            this.NUD_GainVal.TabIndex = 78;
            this.NUD_GainVal.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.NUD_GainVal.ValueChanged += new System.EventHandler(this.NUD_GainVal_ValueChanged);
            // 
            // L_ExpDim
            // 
            this.L_ExpDim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.L_ExpDim.AutoSize = true;
            this.L_ExpDim.Location = new System.Drawing.Point(192, 11);
            this.L_ExpDim.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.L_ExpDim.Name = "L_ExpDim";
            this.L_ExpDim.Size = new System.Drawing.Size(44, 13);
            this.L_ExpDim.TabIndex = 79;
            this.L_ExpDim.Text = "sec.";
            // 
            // L_GainDim
            // 
            this.L_GainDim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.L_GainDim.AutoSize = true;
            this.L_GainDim.Location = new System.Drawing.Point(192, 82);
            this.L_GainDim.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.L_GainDim.Name = "L_GainDim";
            this.L_GainDim.Size = new System.Drawing.Size(44, 13);
            this.L_GainDim.TabIndex = 80;
            this.L_GainDim.Text = "0.1*dB";
            // 
            // BSetROI
            // 
            this.BSetROI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.SetColumnSpan(this.BSetROI, 2);
            this.BSetROI.Location = new System.Drawing.Point(3, 473);
            this.BSetROI.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BSetROI.Name = "BSetROI";
            this.BSetROI.Size = new System.Drawing.Size(93, 24);
            this.BSetROI.TabIndex = 34;
            this.BSetROI.Text = "Set";
            this.BSetROI.UseVisualStyleBackColor = true;
            this.BSetROI.Click += new System.EventHandler(this.BSetROI_Click);
            // 
            // BROIFull
            // 
            this.BROIFull.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.SetColumnSpan(this.BROIFull, 3);
            this.BROIFull.Location = new System.Drawing.Point(96, 473);
            this.BROIFull.Margin = new System.Windows.Forms.Padding(0);
            this.BROIFull.Name = "BROIFull";
            this.BROIFull.Size = new System.Drawing.Size(94, 24);
            this.BROIFull.TabIndex = 46;
            this.BROIFull.Text = "Full";
            this.BROIFull.UseVisualStyleBackColor = true;
            this.BROIFull.Click += new System.EventHandler(this.BROIFull_Click);
            // 
            // BAdapt
            // 
            this.BAdapt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.SetColumnSpan(this.BAdapt, 2);
            this.BAdapt.Location = new System.Drawing.Point(190, 473);
            this.BAdapt.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.BAdapt.Name = "BAdapt";
            this.BAdapt.Size = new System.Drawing.Size(95, 24);
            this.BAdapt.TabIndex = 66;
            this.BAdapt.Text = "Adapt";
            this.BAdapt.UseVisualStyleBackColor = true;
            this.BAdapt.Click += new System.EventHandler(this.BAdapt_Click);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 442);
            this.label11.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 13);
            this.label11.TabIndex = 30;
            this.label11.Text = "Point X:";
            // 
            // TBROIPointX
            // 
            this.TBROIPointX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.SetColumnSpan(this.TBROIPointX, 3);
            this.TBROIPointX.Location = new System.Drawing.Point(51, 439);
            this.TBROIPointX.Name = "TBROIPointX";
            this.TBROIPointX.Size = new System.Drawing.Size(88, 20);
            this.TBROIPointX.TabIndex = 31;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(145, 442);
            this.label12.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 13);
            this.label12.TabIndex = 32;
            this.label12.Text = "Point Y:";
            // 
            // TBROIPointY
            // 
            this.TBROIPointY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.SetColumnSpan(this.TBROIPointY, 2);
            this.TBROIPointY.Location = new System.Drawing.Point(193, 439);
            this.TBROIPointY.Name = "TBROIPointY";
            this.TBROIPointY.Size = new System.Drawing.Size(92, 20);
            this.TBROIPointY.TabIndex = 33;
            // 
            // TBROIWidth
            // 
            this.TBROIWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.SetColumnSpan(this.TBROIWidth, 2);
            this.TBROIWidth.Location = new System.Drawing.Point(51, 367);
            this.TBROIWidth.Name = "TBROIWidth";
            this.TBROIWidth.Size = new System.Drawing.Size(65, 20);
            this.TBROIWidth.TabIndex = 27;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 406);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 28;
            this.label10.Text = "Height:";
            // 
            // TBROIHeight
            // 
            this.TBROIHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.SetColumnSpan(this.TBROIHeight, 2);
            this.TBROIHeight.Location = new System.Drawing.Point(51, 403);
            this.TBROIHeight.Name = "TBROIHeight";
            this.TBROIHeight.Size = new System.Drawing.Size(65, 20);
            this.TBROIHeight.TabIndex = 29;
            // 
            // BTestAll
            // 
            this.BTestAll.Location = new System.Drawing.Point(3, 62);
            this.BTestAll.Name = "BTestAll";
            this.BTestAll.Size = new System.Drawing.Size(54, 30);
            this.BTestAll.TabIndex = 77;
            this.BTestAll.Text = "Tests";
            this.BTestAll.UseVisualStyleBackColor = true;
            this.BTestAll.Click += new System.EventHandler(this.BTestAll_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(986, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(294, 144);
            this.groupBox2.TabIndex = 63;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "AOF Controls:";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.Controls.Add(this.ChB_Power, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.L_RealDevName, 2, 2);
            this.tableLayoutPanel4.Controls.Add(this.L_ReqDevName, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.B_DevOpen, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label18, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.label17, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(288, 125);
            this.tableLayoutPanel4.TabIndex = 74;
            // 
            // ChB_Power
            // 
            this.ChB_Power.Appearance = System.Windows.Forms.Appearance.Button;
            this.tableLayoutPanel4.SetColumnSpan(this.ChB_Power, 4);
            this.ChB_Power.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChB_Power.Location = new System.Drawing.Point(2, 95);
            this.ChB_Power.Margin = new System.Windows.Forms.Padding(2);
            this.ChB_Power.Name = "ChB_Power";
            this.ChB_Power.Size = new System.Drawing.Size(284, 28);
            this.ChB_Power.TabIndex = 84;
            this.ChB_Power.Text = "Power";
            this.ChB_Power.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChB_Power.UseVisualStyleBackColor = true;
            this.ChB_Power.CheckedChanged += new System.EventHandler(this.ChB_Power_CheckedChanged);
            // 
            // L_RealDevName
            // 
            this.L_RealDevName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.L_RealDevName.AutoSize = true;
            this.tableLayoutPanel4.SetColumnSpan(this.L_RealDevName, 2);
            this.L_RealDevName.Location = new System.Drawing.Point(147, 71);
            this.L_RealDevName.Name = "L_RealDevName";
            this.L_RealDevName.Size = new System.Drawing.Size(138, 13);
            this.L_RealDevName.TabIndex = 80;
            this.L_RealDevName.Text = "filename.dev";
            // 
            // L_ReqDevName
            // 
            this.L_ReqDevName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.L_ReqDevName.AutoSize = true;
            this.tableLayoutPanel4.SetColumnSpan(this.L_ReqDevName, 2);
            this.L_ReqDevName.Location = new System.Drawing.Point(147, 40);
            this.L_ReqDevName.Name = "L_ReqDevName";
            this.L_ReqDevName.Size = new System.Drawing.Size(138, 13);
            this.L_ReqDevName.TabIndex = 13;
            this.L_ReqDevName.Text = "filename.dev";
            // 
            // B_DevOpen
            // 
            this.tableLayoutPanel4.SetColumnSpan(this.B_DevOpen, 4);
            this.B_DevOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.B_DevOpen.Location = new System.Drawing.Point(3, 3);
            this.B_DevOpen.Name = "B_DevOpen";
            this.B_DevOpen.Size = new System.Drawing.Size(282, 25);
            this.B_DevOpen.TabIndex = 13;
            this.B_DevOpen.Text = "Open .dev file";
            this.B_DevOpen.UseVisualStyleBackColor = true;
            this.B_DevOpen.Click += new System.EventHandler(this.BDevOpen_Click);
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.tableLayoutPanel4.SetColumnSpan(this.label18, 2);
            this.label18.Location = new System.Drawing.Point(3, 71);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(138, 13);
            this.label18.TabIndex = 79;
            this.label18.Text = "Loaded .dev file :";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.tableLayoutPanel4.SetColumnSpan(this.label17, 2);
            this.label17.Location = new System.Drawing.Point(3, 40);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(138, 13);
            this.label17.TabIndex = 78;
            this.label17.Text = "Required .dev file:";
            this.label17.Click += new System.EventHandler(this.label17_Click);
            // 
            // TBNamePrefix
            // 
            this.TBNamePrefix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.SetColumnSpan(this.TBNamePrefix, 2);
            this.TBNamePrefix.Location = new System.Drawing.Point(3, 36);
            this.TBNamePrefix.Name = "TBNamePrefix";
            this.TBNamePrefix.Size = new System.Drawing.Size(282, 20);
            this.TBNamePrefix.TabIndex = 22;
            this.TBNamePrefix.Text = "ScreenShot";
            this.TBNamePrefix.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBNamePrefix_KeyPress);
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(3, 9);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(138, 13);
            this.label26.TabIndex = 21;
            this.label26.Text = "NamePrefix:";
            // 
            // LLiveFpsLab
            // 
            this.LLiveFpsLab.AutoSize = true;
            this.LLiveFpsLab.Location = new System.Drawing.Point(3, 3);
            this.LLiveFpsLab.Name = "LLiveFpsLab";
            this.LLiveFpsLab.Size = new System.Drawing.Size(30, 13);
            this.LLiveFpsLab.TabIndex = 64;
            this.LLiveFpsLab.Text = "FPS:";
            // 
            // LFPSCurrent
            // 
            this.LFPSCurrent.AutoSize = true;
            this.LFPSCurrent.Location = new System.Drawing.Point(6, 20);
            this.LFPSCurrent.Name = "LFPSCurrent";
            this.LFPSCurrent.Size = new System.Drawing.Size(41, 13);
            this.LFPSCurrent.TabIndex = 65;
            this.LFPSCurrent.Text = "label17";
            // 
            // FPSTimer
            // 
            this.FPSTimer.Enabled = true;
            this.FPSTimer.Interval = 30;
            this.FPSTimer.Tick += new System.EventHandler(this.FPSTimer_Tick);
            // 
            // ICInvalidateTimer
            // 
            this.ICInvalidateTimer.Enabled = true;
            this.ICInvalidateTimer.Interval = 5;
            this.ICInvalidateTimer.Tick += new System.EventHandler(this.ICInvalidateTimer_Tick);
            // 
            // TrB_CurrentWL
            // 
            this.TrB_CurrentWL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.SetColumnSpan(this.TrB_CurrentWL, 6);
            this.TrB_CurrentWL.Location = new System.Drawing.Point(3, 73);
            this.TrB_CurrentWL.Maximum = 1500000;
            this.TrB_CurrentWL.Minimum = 1;
            this.TrB_CurrentWL.Name = "TrB_CurrentWL";
            this.TrB_CurrentWL.Size = new System.Drawing.Size(240, 29);
            this.TrB_CurrentWL.TabIndex = 16;
            this.TrB_CurrentWL.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TrB_CurrentWL.Value = 1;
            this.TrB_CurrentWL.Scroll += new System.EventHandler(this.TBCurrentWL_Scroll);
            // 
            // B_StartS
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.B_StartS, 8);
            this.B_StartS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.B_StartS.Location = new System.Drawing.Point(0, 166);
            this.B_StartS.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.B_StartS.Name = "B_StartS";
            this.B_StartS.Size = new System.Drawing.Size(280, 30);
            this.B_StartS.TabIndex = 1;
            this.B_StartS.Text = "Tuning (simple)";
            this.B_StartS.UseVisualStyleBackColor = true;
            this.B_StartS.Click += new System.EventHandler(this.BStartS_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.tableLayoutPanel7.SetColumnSpan(this.label4, 2);
            this.label4.Location = new System.Drawing.Point(174, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Step:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.tableLayoutPanel7.SetColumnSpan(this.label5, 2);
            this.label5.Location = new System.Drawing.Point(102, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "End WL:";
            // 
            // BSetWL
            // 
            this.BSetWL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BSetWL.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BSetWL.Location = new System.Drawing.Point(246, 3);
            this.BSetWL.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.BSetWL.Name = "BSetWL";
            this.BSetWL.Size = new System.Drawing.Size(42, 29);
            this.BSetWL.TabIndex = 5;
            this.BSetWL.Text = "Set WL";
            this.BSetWL.UseVisualStyleBackColor = true;
            this.BSetWL.Click += new System.EventHandler(this.BSetWL_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.tableLayoutPanel5.SetColumnSpan(this.label6, 3);
            this.label6.Location = new System.Drawing.Point(3, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Current WaveLenght:";
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(208, 11);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(35, 13);
            this.label21.TabIndex = 17;
            this.label21.Text = "nm";
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label23.AutoSize = true;
            this.tableLayoutPanel7.SetColumnSpan(this.label23, 2);
            this.label23.Location = new System.Drawing.Point(30, 53);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(56, 13);
            this.label23.TabIndex = 24;
            this.label23.Text = "Start Freq:";
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label24.AutoSize = true;
            this.tableLayoutPanel7.SetColumnSpan(this.label24, 2);
            this.label24.Location = new System.Drawing.Point(102, 53);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(53, 13);
            this.label24.TabIndex = 25;
            this.label24.Text = "End Freq:";
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label25.AutoSize = true;
            this.tableLayoutPanel7.SetColumnSpan(this.label25, 2);
            this.label25.Location = new System.Drawing.Point(174, 53);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(32, 13);
            this.label25.TabIndex = 26;
            this.label25.Text = "Step:";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(249, 81);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 13);
            this.label13.TabIndex = 33;
            this.label13.Text = "nm";
            // 
            // GrBAOFWlSet
            // 
            this.GrBAOFWlSet.Controls.Add(this.tableLayoutPanel5);
            this.GrBAOFWlSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrBAOFWlSet.Location = new System.Drawing.Point(986, 153);
            this.GrBAOFWlSet.Name = "GrBAOFWlSet";
            this.tableLayoutPanel1.SetRowSpan(this.GrBAOFWlSet, 2);
            this.GrBAOFWlSet.Size = new System.Drawing.Size(294, 573);
            this.GrBAOFWlSet.TabIndex = 53;
            this.GrBAOFWlSet.TabStop = false;
            this.GrBAOFWlSet.Text = "WaveLength settings";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 7;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28531F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28531F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28531F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28531F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28531F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28531F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28816F));
            this.tableLayoutPanel5.Controls.Add(this.B_SetHZ, 6, 1);
            this.tableLayoutPanel5.Controls.Add(this.label20, 6, 3);
            this.tableLayoutPanel5.Controls.Add(this.TrB_CurrentMHz, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label29, 5, 1);
            this.tableLayoutPanel5.Controls.Add(this.label21, 5, 0);
            this.tableLayoutPanel5.Controls.Add(this.ChB_AutoSetWL, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.SnapshotBut, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.SaveImageBut, 2, 4);
            this.tableLayoutPanel5.Controls.Add(this.ContTransAfterSnapshot, 4, 4);
            this.tableLayoutPanel5.Controls.Add(this.tabControl1, 0, 6);
            this.tableLayoutPanel5.Controls.Add(this.label13, 6, 2);
            this.tableLayoutPanel5.Controls.Add(this.TrB_CurrentWL, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.NUD_CurrentWL, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.BSetWL, 6, 0);
            this.tableLayoutPanel5.Controls.Add(this.NUD_CurrentMHz, 3, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 17;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.142741F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.146203F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.143455F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.143455F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.143455F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.143455F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.143455F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.143455F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.143455F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.143455F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.140333F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.142801F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.140333F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.139964F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(288, 554);
            this.tableLayoutPanel5.TabIndex = 75;
            // 
            // B_SetHZ
            // 
            this.B_SetHZ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.B_SetHZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.B_SetHZ.Location = new System.Drawing.Point(246, 38);
            this.B_SetHZ.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.B_SetHZ.Name = "B_SetHZ";
            this.B_SetHZ.Size = new System.Drawing.Size(42, 29);
            this.B_SetHZ.TabIndex = 108;
            this.B_SetHZ.Text = "Set HZ";
            this.B_SetHZ.UseVisualStyleBackColor = true;
            this.B_SetHZ.Click += new System.EventHandler(this.B_SetHZ_Click);
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(249, 116);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(36, 13);
            this.label20.TabIndex = 108;
            this.label20.Text = "MHz";
            // 
            // TrB_CurrentMHz
            // 
            this.TrB_CurrentMHz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.SetColumnSpan(this.TrB_CurrentMHz, 6);
            this.TrB_CurrentMHz.Location = new System.Drawing.Point(3, 108);
            this.TrB_CurrentMHz.Maximum = 150000;
            this.TrB_CurrentMHz.Minimum = 1;
            this.TrB_CurrentMHz.Name = "TrB_CurrentMHz";
            this.TrB_CurrentMHz.Size = new System.Drawing.Size(240, 29);
            this.TrB_CurrentMHz.TabIndex = 108;
            this.TrB_CurrentMHz.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TrB_CurrentMHz.Value = 1;
            this.TrB_CurrentMHz.Scroll += new System.EventHandler(this.TrB_CurrentMHz_Scroll);
            // 
            // label29
            // 
            this.label29.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(208, 46);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(29, 13);
            this.label29.TabIndex = 53;
            this.label29.Text = "MHz";
            // 
            // ChB_AutoSetWL
            // 
            this.ChB_AutoSetWL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.ChB_AutoSetWL.AutoSize = true;
            this.tableLayoutPanel5.SetColumnSpan(this.ChB_AutoSetWL, 3);
            this.ChB_AutoSetWL.Location = new System.Drawing.Point(16, 38);
            this.ChB_AutoSetWL.Name = "ChB_AutoSetWL";
            this.ChB_AutoSetWL.Size = new System.Drawing.Size(90, 29);
            this.ChB_AutoSetWL.TabIndex = 35;
            this.ChB_AutoSetWL.Text = "Slider autoset";
            this.ChB_AutoSetWL.UseVisualStyleBackColor = true;
            this.ChB_AutoSetWL.CheckedChanged += new System.EventHandler(this.ChBAutoSetWL_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tableLayoutPanel5.SetColumnSpan(this.tabControl1, 7);
            this.tabControl1.Controls.Add(this.TabP_1_SimpleTuning);
            this.tabControl1.Controls.Add(this.TabP_2_SpectralImg);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 190);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tableLayoutPanel5.SetRowSpan(this.tabControl1, 11);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(288, 364);
            this.tabControl1.TabIndex = 76;
            // 
            // TabP_1_SimpleTuning
            // 
            this.TabP_1_SimpleTuning.BackColor = System.Drawing.SystemColors.Control;
            this.TabP_1_SimpleTuning.Controls.Add(this.tableLayoutPanel7);
            this.TabP_1_SimpleTuning.Location = new System.Drawing.Point(4, 22);
            this.TabP_1_SimpleTuning.Margin = new System.Windows.Forms.Padding(0);
            this.TabP_1_SimpleTuning.Name = "TabP_1_SimpleTuning";
            this.TabP_1_SimpleTuning.Size = new System.Drawing.Size(280, 338);
            this.TabP_1_SimpleTuning.TabIndex = 0;
            this.TabP_1_SimpleTuning.Text = "Simple tuning";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 8;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel7.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.label5, 3, 0);
            this.tableLayoutPanel7.Controls.Add(this.label4, 5, 0);
            this.tableLayoutPanel7.Controls.Add(this.NUD_StartL, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.NUD_FinishL, 3, 1);
            this.tableLayoutPanel7.Controls.Add(this.NUD_StepL, 5, 1);
            this.tableLayoutPanel7.Controls.Add(this.label34, 7, 1);
            this.tableLayoutPanel7.Controls.Add(this.label23, 1, 2);
            this.tableLayoutPanel7.Controls.Add(this.label24, 3, 2);
            this.tableLayoutPanel7.Controls.Add(this.label25, 5, 2);
            this.tableLayoutPanel7.Controls.Add(this.B_StartS, 0, 6);
            this.tableLayoutPanel7.Controls.Add(this.TLP_Sweep_EasyMode, 0, 5);
            this.tableLayoutPanel7.Controls.Add(this.NUD_FreqStart, 1, 3);
            this.tableLayoutPanel7.Controls.Add(this.NUD_FreqFin, 3, 3);
            this.tableLayoutPanel7.Controls.Add(this.NUD_FreqStep, 5, 3);
            this.tableLayoutPanel7.Controls.Add(this.ChB_SweepEnabled, 0, 4);
            this.tableLayoutPanel7.Controls.Add(this.label14, 7, 3);
            this.tableLayoutPanel7.Controls.Add(this.RB_Series_WLMode, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.RB_Series_FreqMode, 0, 3);
            this.tableLayoutPanel7.Controls.Add(this.TLP_Saving_of_Frames, 0, 7);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 9;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(280, 338);
            this.tableLayoutPanel7.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.tableLayoutPanel7.SetColumnSpan(this.label3, 2);
            this.label3.Location = new System.Drawing.Point(30, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Start WL:";
            // 
            // NUD_StartL
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.NUD_StartL, 2);
            this.NUD_StartL.DecimalPlaces = 3;
            this.NUD_StartL.Location = new System.Drawing.Point(30, 18);
            this.NUD_StartL.Maximum = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            this.NUD_StartL.Name = "NUD_StartL";
            this.NUD_StartL.Size = new System.Drawing.Size(66, 20);
            this.NUD_StartL.TabIndex = 89;
            this.NUD_StartL.ValueChanged += new System.EventHandler(this.NUD_StartL_ValueChanged);
            // 
            // NUD_FinishL
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.NUD_FinishL, 2);
            this.NUD_FinishL.DecimalPlaces = 3;
            this.NUD_FinishL.Location = new System.Drawing.Point(102, 18);
            this.NUD_FinishL.Maximum = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            this.NUD_FinishL.Name = "NUD_FinishL";
            this.NUD_FinishL.Size = new System.Drawing.Size(66, 20);
            this.NUD_FinishL.TabIndex = 92;
            this.NUD_FinishL.ValueChanged += new System.EventHandler(this.NUD_FinishL_ValueChanged);
            // 
            // NUD_StepL
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.NUD_StepL, 2);
            this.NUD_StepL.DecimalPlaces = 3;
            this.NUD_StepL.Location = new System.Drawing.Point(174, 18);
            this.NUD_StepL.Maximum = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            this.NUD_StepL.Name = "NUD_StepL";
            this.NUD_StepL.Size = new System.Drawing.Size(66, 20);
            this.NUD_StepL.TabIndex = 90;
            this.NUD_StepL.ValueChanged += new System.EventHandler(this.NUD_StepL_ValueChanged);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(246, 20);
            this.label34.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(21, 13);
            this.label34.TabIndex = 101;
            this.label34.Text = "nm";
            // 
            // TLP_Sweep_EasyMode
            // 
            this.TLP_Sweep_EasyMode.ColumnCount = 4;
            this.tableLayoutPanel7.SetColumnSpan(this.TLP_Sweep_EasyMode, 8);
            this.TLP_Sweep_EasyMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_Sweep_EasyMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.17F));
            this.TLP_Sweep_EasyMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.54F));
            this.TLP_Sweep_EasyMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.29F));
            this.TLP_Sweep_EasyMode.Controls.Add(this.label28, 3, 0);
            this.TLP_Sweep_EasyMode.Controls.Add(this.label30, 3, 1);
            this.TLP_Sweep_EasyMode.Controls.Add(this.NUD_FreqDeviation, 2, 0);
            this.TLP_Sweep_EasyMode.Controls.Add(this.NUD_TimeFdev, 2, 1);
            this.TLP_Sweep_EasyMode.Controls.Add(this.label31, 1, 0);
            this.TLP_Sweep_EasyMode.Controls.Add(this.label32, 0, 1);
            this.TLP_Sweep_EasyMode.Controls.Add(this.label33, 0, 0);
            this.TLP_Sweep_EasyMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_Sweep_EasyMode.Enabled = false;
            this.TLP_Sweep_EasyMode.Location = new System.Drawing.Point(0, 127);
            this.TLP_Sweep_EasyMode.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_Sweep_EasyMode.Name = "TLP_Sweep_EasyMode";
            this.TLP_Sweep_EasyMode.RowCount = 2;
            this.TLP_Sweep_EasyMode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_Sweep_EasyMode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_Sweep_EasyMode.Size = new System.Drawing.Size(280, 36);
            this.TLP_Sweep_EasyMode.TabIndex = 93;
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(242, 2);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(35, 13);
            this.label28.TabIndex = 1;
            this.label28.Text = "МГц";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label30
            // 
            this.label30.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(243, 20);
            this.label30.Margin = new System.Windows.Forms.Padding(4, 0, 3, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(34, 13);
            this.label30.TabIndex = 2;
            this.label30.Text = "мс";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NUD_FreqDeviation
            // 
            this.NUD_FreqDeviation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.NUD_FreqDeviation.DecimalPlaces = 3;
            this.NUD_FreqDeviation.Location = new System.Drawing.Point(163, 3);
            this.NUD_FreqDeviation.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.NUD_FreqDeviation.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.NUD_FreqDeviation.Name = "NUD_FreqDeviation";
            this.NUD_FreqDeviation.Size = new System.Drawing.Size(73, 20);
            this.NUD_FreqDeviation.TabIndex = 1;
            this.NUD_FreqDeviation.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.NUD_FreqDeviation.ValueChanged += new System.EventHandler(this.NUD_FreqDeviation_ValueChanged);
            // 
            // NUD_TimeFdev
            // 
            this.NUD_TimeFdev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.NUD_TimeFdev.DecimalPlaces = 3;
            this.NUD_TimeFdev.Location = new System.Drawing.Point(163, 21);
            this.NUD_TimeFdev.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.NUD_TimeFdev.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.NUD_TimeFdev.Name = "NUD_TimeFdev";
            this.NUD_TimeFdev.Size = new System.Drawing.Size(73, 20);
            this.NUD_TimeFdev.TabIndex = 2;
            this.NUD_TimeFdev.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.NUD_TimeFdev.ValueChanged += new System.EventHandler(this.NUD_TimeFdev_ValueChanged);
            // 
            // label31
            // 
            this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(143, 2);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(14, 13);
            this.label31.TabIndex = 1;
            this.label31.Text = "±";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label32
            // 
            this.label32.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(3, 20);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(122, 13);
            this.label32.TabIndex = 2;
            this.label32.Text = "Single deviation latency:";
            // 
            // label33
            // 
            this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(3, 2);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(134, 13);
            this.label33.TabIndex = 1;
            this.label33.Text = "Frequency deviation:";
            // 
            // NUD_FreqStart
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.NUD_FreqStart, 2);
            this.NUD_FreqStart.DecimalPlaces = 3;
            this.NUD_FreqStart.Enabled = false;
            this.NUD_FreqStart.Location = new System.Drawing.Point(30, 69);
            this.NUD_FreqStart.Maximum = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            this.NUD_FreqStart.Name = "NUD_FreqStart";
            this.NUD_FreqStart.Size = new System.Drawing.Size(66, 20);
            this.NUD_FreqStart.TabIndex = 102;
            this.NUD_FreqStart.ValueChanged += new System.EventHandler(this.NUD_FreqStart_ValueChanged);
            // 
            // NUD_FreqFin
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.NUD_FreqFin, 2);
            this.NUD_FreqFin.DecimalPlaces = 3;
            this.NUD_FreqFin.Enabled = false;
            this.NUD_FreqFin.Location = new System.Drawing.Point(102, 69);
            this.NUD_FreqFin.Maximum = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            this.NUD_FreqFin.Name = "NUD_FreqFin";
            this.NUD_FreqFin.Size = new System.Drawing.Size(66, 20);
            this.NUD_FreqFin.TabIndex = 103;
            this.NUD_FreqFin.ValueChanged += new System.EventHandler(this.NUD_FreqFin_ValueChanged);
            // 
            // NUD_FreqStep
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.NUD_FreqStep, 2);
            this.NUD_FreqStep.DecimalPlaces = 3;
            this.NUD_FreqStep.Enabled = false;
            this.NUD_FreqStep.Location = new System.Drawing.Point(174, 69);
            this.NUD_FreqStep.Maximum = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            this.NUD_FreqStep.Name = "NUD_FreqStep";
            this.NUD_FreqStep.Size = new System.Drawing.Size(66, 20);
            this.NUD_FreqStep.TabIndex = 104;
            this.NUD_FreqStep.ValueChanged += new System.EventHandler(this.NUD_FreqStep_ValueChanged);
            // 
            // ChB_SweepEnabled
            // 
            this.ChB_SweepEnabled.AutoSize = true;
            this.tableLayoutPanel7.SetColumnSpan(this.ChB_SweepEnabled, 4);
            this.ChB_SweepEnabled.Location = new System.Drawing.Point(3, 105);
            this.ChB_SweepEnabled.Name = "ChB_SweepEnabled";
            this.ChB_SweepEnabled.Size = new System.Drawing.Size(88, 17);
            this.ChB_SweepEnabled.TabIndex = 94;
            this.ChB_SweepEnabled.Text = "Sweep mode";
            this.ChB_SweepEnabled.UseVisualStyleBackColor = true;
            this.ChB_SweepEnabled.CheckedChanged += new System.EventHandler(this.ChB_SweepEnabled_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(246, 71);
            this.label14.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 13);
            this.label14.TabIndex = 105;
            this.label14.Text = "MHz";
            // 
            // RB_Series_WLMode
            // 
            this.RB_Series_WLMode.AutoSize = true;
            this.RB_Series_WLMode.Checked = true;
            this.RB_Series_WLMode.Location = new System.Drawing.Point(3, 20);
            this.RB_Series_WLMode.Margin = new System.Windows.Forms.Padding(3, 5, 3, 2);
            this.RB_Series_WLMode.Name = "RB_Series_WLMode";
            this.RB_Series_WLMode.Size = new System.Drawing.Size(14, 13);
            this.RB_Series_WLMode.TabIndex = 106;
            this.RB_Series_WLMode.TabStop = true;
            this.RB_Series_WLMode.UseVisualStyleBackColor = true;
            this.RB_Series_WLMode.CheckedChanged += new System.EventHandler(this.RB_Series_WLMode_CheckedChanged);
            // 
            // RB_Series_FreqMode
            // 
            this.RB_Series_FreqMode.AutoSize = true;
            this.RB_Series_FreqMode.Location = new System.Drawing.Point(3, 71);
            this.RB_Series_FreqMode.Margin = new System.Windows.Forms.Padding(3, 5, 3, 2);
            this.RB_Series_FreqMode.Name = "RB_Series_FreqMode";
            this.RB_Series_FreqMode.Size = new System.Drawing.Size(14, 13);
            this.RB_Series_FreqMode.TabIndex = 107;
            this.RB_Series_FreqMode.UseVisualStyleBackColor = true;
            this.RB_Series_FreqMode.CheckedChanged += new System.EventHandler(this.RB_Series_FreqMode_CheckedChanged);
            // 
            // TLP_Saving_of_Frames
            // 
            this.TLP_Saving_of_Frames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TLP_Saving_of_Frames.ColumnCount = 3;
            this.tableLayoutPanel7.SetColumnSpan(this.TLP_Saving_of_Frames, 8);
            this.TLP_Saving_of_Frames.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_Saving_of_Frames.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_Saving_of_Frames.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLP_Saving_of_Frames.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLP_Saving_of_Frames.Controls.Add(this.L_Aquired, 1, 1);
            this.TLP_Saving_of_Frames.Controls.Add(this.panel2, 0, 2);
            this.TLP_Saving_of_Frames.Controls.Add(this.panel1, 0, 0);
            this.TLP_Saving_of_Frames.Controls.Add(this.L_Saved, 1, 3);
            this.TLP_Saving_of_Frames.Controls.Add(this.L_Info_Aquired, 0, 1);
            this.TLP_Saving_of_Frames.Controls.Add(this.L_Info_Saved, 0, 3);
            this.TLP_Saving_of_Frames.Location = new System.Drawing.Point(3, 202);
            this.TLP_Saving_of_Frames.Name = "TLP_Saving_of_Frames";
            this.TLP_Saving_of_Frames.RowCount = 4;
            this.tableLayoutPanel7.SetRowSpan(this.TLP_Saving_of_Frames, 2);
            this.TLP_Saving_of_Frames.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_Saving_of_Frames.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLP_Saving_of_Frames.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_Saving_of_Frames.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLP_Saving_of_Frames.Size = new System.Drawing.Size(274, 95);
            this.TLP_Saving_of_Frames.TabIndex = 110;
            // 
            // L_Aquired
            // 
            this.L_Aquired.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.L_Aquired.AutoSize = true;
            this.L_Aquired.Location = new System.Drawing.Point(178, 30);
            this.L_Aquired.Name = "L_Aquired";
            this.L_Aquired.Size = new System.Drawing.Size(24, 13);
            this.L_Aquired.TabIndex = 109;
            this.L_Aquired.Text = "0/0";
            // 
            // panel2
            // 
            this.TLP_Saving_of_Frames.SetColumnSpan(this.panel2, 3);
            this.panel2.Controls.Add(this.PB_Saving);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 47);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(274, 27);
            this.panel2.TabIndex = 1;
            // 
            // PB_Saving
            // 
            this.PB_Saving.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PB_Saving.Location = new System.Drawing.Point(0, 0);
            this.PB_Saving.Margin = new System.Windows.Forms.Padding(2, 5, 2, 2);
            this.PB_Saving.Name = "PB_Saving";
            this.PB_Saving.Size = new System.Drawing.Size(274, 27);
            this.PB_Saving.TabIndex = 109;
            // 
            // panel1
            // 
            this.TLP_Saving_of_Frames.SetColumnSpan(this.panel1, 3);
            this.panel1.Controls.Add(this.PB_SeriesProgress);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(274, 27);
            this.panel1.TabIndex = 0;
            // 
            // PB_SeriesProgress
            // 
            this.PB_SeriesProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PB_SeriesProgress.Location = new System.Drawing.Point(0, 0);
            this.PB_SeriesProgress.Margin = new System.Windows.Forms.Padding(2, 5, 2, 2);
            this.PB_SeriesProgress.Name = "PB_SeriesProgress";
            this.PB_SeriesProgress.Size = new System.Drawing.Size(274, 27);
            this.PB_SeriesProgress.TabIndex = 108;
            this.PB_SeriesProgress.Click += new System.EventHandler(this.PB_SeriesProgress_Click);
            // 
            // L_Saved
            // 
            this.L_Saved.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.L_Saved.AutoSize = true;
            this.L_Saved.Location = new System.Drawing.Point(178, 78);
            this.L_Saved.Name = "L_Saved";
            this.L_Saved.Size = new System.Drawing.Size(24, 13);
            this.L_Saved.TabIndex = 110;
            this.L_Saved.Text = "0/0";
            // 
            // L_Info_Aquired
            // 
            this.L_Info_Aquired.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.L_Info_Aquired.AutoSize = true;
            this.L_Info_Aquired.Location = new System.Drawing.Point(61, 30);
            this.L_Info_Aquired.Name = "L_Info_Aquired";
            this.L_Info_Aquired.Size = new System.Drawing.Size(63, 13);
            this.L_Info_Aquired.TabIndex = 111;
            this.L_Info_Aquired.Text = "Захвачено:";
            // 
            // L_Info_Saved
            // 
            this.L_Info_Saved.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.L_Info_Saved.AutoSize = true;
            this.L_Info_Saved.Location = new System.Drawing.Point(60, 78);
            this.L_Info_Saved.Name = "L_Info_Saved";
            this.L_Info_Saved.Size = new System.Drawing.Size(64, 13);
            this.L_Info_Saved.TabIndex = 112;
            this.L_Info_Saved.Text = "Сохранено:";
            // 
            // TabP_2_SpectralImg
            // 
            this.TabP_2_SpectralImg.BackColor = System.Drawing.SystemColors.Control;
            this.TabP_2_SpectralImg.Controls.Add(this.tableLayoutPanel8);
            this.TabP_2_SpectralImg.Location = new System.Drawing.Point(4, 22);
            this.TabP_2_SpectralImg.Name = "TabP_2_SpectralImg";
            this.TabP_2_SpectralImg.Size = new System.Drawing.Size(280, 338);
            this.TabP_2_SpectralImg.TabIndex = 1;
            this.TabP_2_SpectralImg.Text = "Multispectral image";
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 4;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel8.Controls.Add(this.L_Num, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.label36, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.label37, 2, 0);
            this.tableLayoutPanel8.Controls.Add(this.NUD_Multi_WL1, 1, 1);
            this.tableLayoutPanel8.Controls.Add(this.NUD_Multi_WL2, 1, 2);
            this.tableLayoutPanel8.Controls.Add(this.NUD_Multi_WL3, 1, 3);
            this.tableLayoutPanel8.Controls.Add(this.NUD_Multi_ex_time1, 2, 1);
            this.tableLayoutPanel8.Controls.Add(this.NUD_Multi_ex_time2, 2, 2);
            this.tableLayoutPanel8.Controls.Add(this.NUD_Multi_ex_time3, 2, 3);
            this.tableLayoutPanel8.Controls.Add(this.B_Get_HyperSpectral_Image, 0, 9);
            this.tableLayoutPanel8.Controls.Add(this.NUD_Multi_WL4, 1, 4);
            this.tableLayoutPanel8.Controls.Add(this.NUD_Multi_WL5, 1, 5);
            this.tableLayoutPanel8.Controls.Add(this.NUD_Multi_WL6, 1, 6);
            this.tableLayoutPanel8.Controls.Add(this.NUD_Multi_WL7, 1, 7);
            this.tableLayoutPanel8.Controls.Add(this.NUD_Multi_WL8, 1, 8);
            this.tableLayoutPanel8.Controls.Add(this.NUD_Multi_ex_time4, 2, 4);
            this.tableLayoutPanel8.Controls.Add(this.NUD_Multi_ex_time5, 2, 5);
            this.tableLayoutPanel8.Controls.Add(this.NUD_Multi_ex_time6, 2, 6);
            this.tableLayoutPanel8.Controls.Add(this.NUD_Multi_ex_time7, 2, 7);
            this.tableLayoutPanel8.Controls.Add(this.NUD_Multi_ex_time8, 2, 8);
            this.tableLayoutPanel8.Controls.Add(this.L_WL_Num1, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.label22, 0, 2);
            this.tableLayoutPanel8.Controls.Add(this.label35, 0, 3);
            this.tableLayoutPanel8.Controls.Add(this.label38, 0, 4);
            this.tableLayoutPanel8.Controls.Add(this.label39, 0, 5);
            this.tableLayoutPanel8.Controls.Add(this.label40, 0, 6);
            this.tableLayoutPanel8.Controls.Add(this.label41, 0, 7);
            this.tableLayoutPanel8.Controls.Add(this.label43, 0, 8);
            this.tableLayoutPanel8.Controls.Add(this.ChB_SpectralCycle, 0, 10);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 12;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(280, 338);
            this.tableLayoutPanel8.TabIndex = 0;
            // 
            // L_Num
            // 
            this.L_Num.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.L_Num.AutoSize = true;
            this.L_Num.Location = new System.Drawing.Point(11, 3);
            this.L_Num.Name = "L_Num";
            this.L_Num.Size = new System.Drawing.Size(18, 13);
            this.L_Num.TabIndex = 0;
            this.L_Num.Text = "№";
            // 
            // label36
            // 
            this.label36.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(69, 3);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(47, 13);
            this.label36.TabIndex = 115;
            this.label36.Text = "WL (nm)";
            // 
            // label37
            // 
            this.label37.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(177, 3);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(44, 13);
            this.label37.TabIndex = 116;
            this.label37.Text = "Time (s)";
            // 
            // NUD_Multi_WL1
            // 
            this.NUD_Multi_WL1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NUD_Multi_WL1.Location = new System.Drawing.Point(55, 23);
            this.NUD_Multi_WL1.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.NUD_Multi_WL1.Name = "NUD_Multi_WL1";
            this.NUD_Multi_WL1.Size = new System.Drawing.Size(76, 20);
            this.NUD_Multi_WL1.TabIndex = 95;
            // 
            // NUD_Multi_WL2
            // 
            this.NUD_Multi_WL2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NUD_Multi_WL2.Location = new System.Drawing.Point(55, 48);
            this.NUD_Multi_WL2.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.NUD_Multi_WL2.Name = "NUD_Multi_WL2";
            this.NUD_Multi_WL2.Size = new System.Drawing.Size(76, 20);
            this.NUD_Multi_WL2.TabIndex = 96;
            // 
            // NUD_Multi_WL3
            // 
            this.NUD_Multi_WL3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NUD_Multi_WL3.Location = new System.Drawing.Point(55, 73);
            this.NUD_Multi_WL3.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.NUD_Multi_WL3.Name = "NUD_Multi_WL3";
            this.NUD_Multi_WL3.Size = new System.Drawing.Size(76, 20);
            this.NUD_Multi_WL3.TabIndex = 97;
            // 
            // NUD_Multi_ex_time1
            // 
            this.NUD_Multi_ex_time1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NUD_Multi_ex_time1.DecimalPlaces = 5;
            this.NUD_Multi_ex_time1.Location = new System.Drawing.Point(161, 23);
            this.NUD_Multi_ex_time1.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NUD_Multi_ex_time1.Name = "NUD_Multi_ex_time1";
            this.NUD_Multi_ex_time1.Size = new System.Drawing.Size(76, 20);
            this.NUD_Multi_ex_time1.TabIndex = 103;
            // 
            // NUD_Multi_ex_time2
            // 
            this.NUD_Multi_ex_time2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NUD_Multi_ex_time2.DecimalPlaces = 5;
            this.NUD_Multi_ex_time2.Location = new System.Drawing.Point(161, 48);
            this.NUD_Multi_ex_time2.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NUD_Multi_ex_time2.Name = "NUD_Multi_ex_time2";
            this.NUD_Multi_ex_time2.Size = new System.Drawing.Size(76, 20);
            this.NUD_Multi_ex_time2.TabIndex = 104;
            // 
            // NUD_Multi_ex_time3
            // 
            this.NUD_Multi_ex_time3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NUD_Multi_ex_time3.DecimalPlaces = 5;
            this.NUD_Multi_ex_time3.Location = new System.Drawing.Point(161, 73);
            this.NUD_Multi_ex_time3.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NUD_Multi_ex_time3.Name = "NUD_Multi_ex_time3";
            this.NUD_Multi_ex_time3.Size = new System.Drawing.Size(76, 20);
            this.NUD_Multi_ex_time3.TabIndex = 105;
            this.NUD_Multi_ex_time3.ValueChanged += new System.EventHandler(this.NUD_Multi_ex_time3_ValueChanged);
            // 
            // B_Get_HyperSpectral_Image
            // 
            this.tableLayoutPanel8.SetColumnSpan(this.B_Get_HyperSpectral_Image, 4);
            this.B_Get_HyperSpectral_Image.Dock = System.Windows.Forms.DockStyle.Fill;
            this.B_Get_HyperSpectral_Image.Location = new System.Drawing.Point(0, 220);
            this.B_Get_HyperSpectral_Image.Margin = new System.Windows.Forms.Padding(0);
            this.B_Get_HyperSpectral_Image.Name = "B_Get_HyperSpectral_Image";
            this.B_Get_HyperSpectral_Image.Size = new System.Drawing.Size(280, 30);
            this.B_Get_HyperSpectral_Image.TabIndex = 111;
            this.B_Get_HyperSpectral_Image.Text = "Aquire Spectral Image";
            this.B_Get_HyperSpectral_Image.UseVisualStyleBackColor = true;
            this.B_Get_HyperSpectral_Image.Click += new System.EventHandler(this.B_Get_HyperSpectral_Image_Click);
            // 
            // NUD_Multi_WL4
            // 
            this.NUD_Multi_WL4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NUD_Multi_WL4.Location = new System.Drawing.Point(55, 98);
            this.NUD_Multi_WL4.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.NUD_Multi_WL4.Name = "NUD_Multi_WL4";
            this.NUD_Multi_WL4.Size = new System.Drawing.Size(76, 20);
            this.NUD_Multi_WL4.TabIndex = 98;
            // 
            // NUD_Multi_WL5
            // 
            this.NUD_Multi_WL5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NUD_Multi_WL5.Location = new System.Drawing.Point(55, 123);
            this.NUD_Multi_WL5.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.NUD_Multi_WL5.Name = "NUD_Multi_WL5";
            this.NUD_Multi_WL5.Size = new System.Drawing.Size(76, 20);
            this.NUD_Multi_WL5.TabIndex = 99;
            // 
            // NUD_Multi_WL6
            // 
            this.NUD_Multi_WL6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NUD_Multi_WL6.Location = new System.Drawing.Point(55, 148);
            this.NUD_Multi_WL6.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.NUD_Multi_WL6.Name = "NUD_Multi_WL6";
            this.NUD_Multi_WL6.Size = new System.Drawing.Size(76, 20);
            this.NUD_Multi_WL6.TabIndex = 100;
            // 
            // NUD_Multi_WL7
            // 
            this.NUD_Multi_WL7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NUD_Multi_WL7.Location = new System.Drawing.Point(55, 173);
            this.NUD_Multi_WL7.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.NUD_Multi_WL7.Name = "NUD_Multi_WL7";
            this.NUD_Multi_WL7.Size = new System.Drawing.Size(76, 20);
            this.NUD_Multi_WL7.TabIndex = 101;
            // 
            // NUD_Multi_WL8
            // 
            this.NUD_Multi_WL8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NUD_Multi_WL8.Location = new System.Drawing.Point(55, 198);
            this.NUD_Multi_WL8.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.NUD_Multi_WL8.Name = "NUD_Multi_WL8";
            this.NUD_Multi_WL8.Size = new System.Drawing.Size(76, 20);
            this.NUD_Multi_WL8.TabIndex = 102;
            // 
            // NUD_Multi_ex_time4
            // 
            this.NUD_Multi_ex_time4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NUD_Multi_ex_time4.DecimalPlaces = 5;
            this.NUD_Multi_ex_time4.Location = new System.Drawing.Point(161, 98);
            this.NUD_Multi_ex_time4.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NUD_Multi_ex_time4.Name = "NUD_Multi_ex_time4";
            this.NUD_Multi_ex_time4.Size = new System.Drawing.Size(76, 20);
            this.NUD_Multi_ex_time4.TabIndex = 106;
            // 
            // NUD_Multi_ex_time5
            // 
            this.NUD_Multi_ex_time5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NUD_Multi_ex_time5.DecimalPlaces = 5;
            this.NUD_Multi_ex_time5.Location = new System.Drawing.Point(161, 123);
            this.NUD_Multi_ex_time5.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NUD_Multi_ex_time5.Name = "NUD_Multi_ex_time5";
            this.NUD_Multi_ex_time5.Size = new System.Drawing.Size(76, 20);
            this.NUD_Multi_ex_time5.TabIndex = 107;
            // 
            // NUD_Multi_ex_time6
            // 
            this.NUD_Multi_ex_time6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NUD_Multi_ex_time6.DecimalPlaces = 5;
            this.NUD_Multi_ex_time6.Location = new System.Drawing.Point(161, 148);
            this.NUD_Multi_ex_time6.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NUD_Multi_ex_time6.Name = "NUD_Multi_ex_time6";
            this.NUD_Multi_ex_time6.Size = new System.Drawing.Size(76, 20);
            this.NUD_Multi_ex_time6.TabIndex = 108;
            // 
            // NUD_Multi_ex_time7
            // 
            this.NUD_Multi_ex_time7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NUD_Multi_ex_time7.DecimalPlaces = 5;
            this.NUD_Multi_ex_time7.Location = new System.Drawing.Point(161, 173);
            this.NUD_Multi_ex_time7.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NUD_Multi_ex_time7.Name = "NUD_Multi_ex_time7";
            this.NUD_Multi_ex_time7.Size = new System.Drawing.Size(76, 20);
            this.NUD_Multi_ex_time7.TabIndex = 109;
            // 
            // NUD_Multi_ex_time8
            // 
            this.NUD_Multi_ex_time8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NUD_Multi_ex_time8.DecimalPlaces = 5;
            this.NUD_Multi_ex_time8.Location = new System.Drawing.Point(161, 198);
            this.NUD_Multi_ex_time8.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NUD_Multi_ex_time8.Name = "NUD_Multi_ex_time8";
            this.NUD_Multi_ex_time8.Size = new System.Drawing.Size(76, 20);
            this.NUD_Multi_ex_time8.TabIndex = 110;
            // 
            // L_WL_Num1
            // 
            this.L_WL_Num1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.L_WL_Num1.AutoSize = true;
            this.L_WL_Num1.Location = new System.Drawing.Point(13, 26);
            this.L_WL_Num1.Name = "L_WL_Num1";
            this.L_WL_Num1.Size = new System.Drawing.Size(13, 13);
            this.L_WL_Num1.TabIndex = 117;
            this.L_WL_Num1.Text = "1";
            // 
            // label22
            // 
            this.label22.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(13, 51);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(13, 13);
            this.label22.TabIndex = 118;
            this.label22.Text = "2";
            // 
            // label35
            // 
            this.label35.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(13, 76);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(13, 13);
            this.label35.TabIndex = 119;
            this.label35.Text = "3";
            // 
            // label38
            // 
            this.label38.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(13, 101);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(13, 13);
            this.label38.TabIndex = 120;
            this.label38.Text = "4";
            // 
            // label39
            // 
            this.label39.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(13, 126);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(13, 13);
            this.label39.TabIndex = 121;
            this.label39.Text = "5";
            // 
            // label40
            // 
            this.label40.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(13, 151);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(13, 13);
            this.label40.TabIndex = 122;
            this.label40.Text = "6";
            // 
            // label41
            // 
            this.label41.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(13, 176);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(13, 13);
            this.label41.TabIndex = 123;
            this.label41.Text = "7";
            // 
            // label43
            // 
            this.label43.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(13, 201);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(13, 13);
            this.label43.TabIndex = 125;
            this.label43.Text = "8";
            // 
            // ChB_SpectralCycle
            // 
            this.ChB_SpectralCycle.Appearance = System.Windows.Forms.Appearance.Button;
            this.ChB_SpectralCycle.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tableLayoutPanel8.SetColumnSpan(this.ChB_SpectralCycle, 4);
            this.ChB_SpectralCycle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChB_SpectralCycle.Location = new System.Drawing.Point(0, 250);
            this.ChB_SpectralCycle.Margin = new System.Windows.Forms.Padding(0);
            this.ChB_SpectralCycle.Name = "ChB_SpectralCycle";
            this.ChB_SpectralCycle.Size = new System.Drawing.Size(280, 30);
            this.ChB_SpectralCycle.TabIndex = 112;
            this.ChB_SpectralCycle.Text = "Spectral Cycle";
            this.ChB_SpectralCycle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChB_SpectralCycle.UseVisualStyleBackColor = true;
            this.ChB_SpectralCycle.CheckedChanged += new System.EventHandler(this.ChB_SpectralCycle_CheckedChanged);
            // 
            // NUD_CurrentWL
            // 
            this.NUD_CurrentWL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.SetColumnSpan(this.NUD_CurrentWL, 2);
            this.NUD_CurrentWL.DecimalPlaces = 3;
            this.NUD_CurrentWL.Location = new System.Drawing.Point(123, 7);
            this.NUD_CurrentWL.Margin = new System.Windows.Forms.Padding(0);
            this.NUD_CurrentWL.Maximum = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            this.NUD_CurrentWL.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.NUD_CurrentWL.Name = "NUD_CurrentWL";
            this.NUD_CurrentWL.Size = new System.Drawing.Size(82, 20);
            this.NUD_CurrentWL.TabIndex = 85;
            this.NUD_CurrentWL.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUD_CurrentWL.ValueChanged += new System.EventHandler(this.NUD_CurWL_ValueChanged);
            // 
            // NUD_CurrentMHz
            // 
            this.NUD_CurrentMHz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.SetColumnSpan(this.NUD_CurrentMHz, 2);
            this.NUD_CurrentMHz.DecimalPlaces = 3;
            this.NUD_CurrentMHz.Location = new System.Drawing.Point(123, 42);
            this.NUD_CurrentMHz.Margin = new System.Windows.Forms.Padding(0);
            this.NUD_CurrentMHz.Maximum = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            this.NUD_CurrentMHz.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.NUD_CurrentMHz.Name = "NUD_CurrentMHz";
            this.NUD_CurrentMHz.Size = new System.Drawing.Size(82, 20);
            this.NUD_CurrentMHz.TabIndex = 108;
            this.NUD_CurrentMHz.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // GrBImageSettings
            // 
            this.GrBImageSettings.Controls.Add(this.tableLayoutPanel3);
            this.GrBImageSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrBImageSettings.Location = new System.Drawing.Point(686, 3);
            this.GrBImageSettings.Name = "GrBImageSettings";
            this.GrBImageSettings.Size = new System.Drawing.Size(294, 144);
            this.GrBImageSettings.TabIndex = 66;
            this.GrBImageSettings.TabStop = false;
            this.GrBImageSettings.Text = "Basic settings";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.SaveImgParBut, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.label19, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label26, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.TBNamePrefix, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.CBFinalPixelFormat, 1, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(288, 125);
            this.tableLayoutPanel3.TabIndex = 74;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(3, 71);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(138, 13);
            this.label19.TabIndex = 79;
            this.label19.Text = "PixelFormat(saving):";
            // 
            // CBFinalPixelFormat
            // 
            this.CBFinalPixelFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.CBFinalPixelFormat.FormattingEnabled = true;
            this.CBFinalPixelFormat.Location = new System.Drawing.Point(147, 67);
            this.CBFinalPixelFormat.Name = "CBFinalPixelFormat";
            this.CBFinalPixelFormat.Size = new System.Drawing.Size(138, 21);
            this.CBFinalPixelFormat.TabIndex = 78;
            this.CBFinalPixelFormat.SelectedIndexChanged += new System.EventHandler(this.CBFinalPixelFormat_SelectedIndexChanged);
            // 
            // FlipUpDownBut
            // 
            this.FlipUpDownBut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlipUpDownBut.Location = new System.Drawing.Point(1, 597);
            this.FlipUpDownBut.Margin = new System.Windows.Forms.Padding(1);
            this.FlipUpDownBut.Name = "FlipUpDownBut";
            this.FlipUpDownBut.Size = new System.Drawing.Size(28, 28);
            this.FlipUpDownBut.TabIndex = 72;
            this.FlipUpDownBut.UseVisualStyleBackColor = true;
            this.FlipUpDownBut.Click += new System.EventHandler(this.FlipUpDownBut_Click);
            // 
            // FlipRightLeftBut
            // 
            this.FlipRightLeftBut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlipRightLeftBut.Location = new System.Drawing.Point(31, 597);
            this.FlipRightLeftBut.Margin = new System.Windows.Forms.Padding(1);
            this.FlipRightLeftBut.Name = "FlipRightLeftBut";
            this.FlipRightLeftBut.Size = new System.Drawing.Size(28, 28);
            this.FlipRightLeftBut.TabIndex = 71;
            this.FlipRightLeftBut.UseVisualStyleBackColor = true;
            this.FlipRightLeftBut.Click += new System.EventHandler(this.FlipRightLeftBut_Click);
            // 
            // BkGrWorker_forExpCurveBuilding
            // 
            this.BkGrWorker_forExpCurveBuilding.WorkerReportsProgress = true;
            this.BkGrWorker_forExpCurveBuilding.WorkerSupportsCancellation = true;
            this.BkGrWorker_forExpCurveBuilding.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BkGrWorker_forExpCurveBuilding_DoWork);
            this.BkGrWorker_forExpCurveBuilding.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BkGrWorker_forExpCurveBuilding_ProgressChanged);
            this.BkGrWorker_forExpCurveBuilding.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BkGrWorker_forExpCurveBuilding_RunWorkerCompleted);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.GrBAOFWlSet, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.GPCamFeat, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.GrBImageSettings, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.icImagingControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.LBConsole, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.ExitBut, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.Pan_FPS, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1283, 759);
            this.tableLayoutPanel1.TabIndex = 73;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Controls.Add(this.L_Zoom, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.TrBZoomOfImage, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.FlipRightLeftBut, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.FlipUpDownBut, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(623, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(60, 626);
            this.tableLayoutPanel2.TabIndex = 74;
            // 
            // Pan_FPS
            // 
            this.Pan_FPS.Controls.Add(this.LLiveFpsLab);
            this.Pan_FPS.Controls.Add(this.LFPSCurrent);
            this.Pan_FPS.Controls.Add(this.BTestAll);
            this.Pan_FPS.Location = new System.Drawing.Point(623, 631);
            this.Pan_FPS.Margin = new System.Windows.Forms.Padding(0);
            this.Pan_FPS.Name = "Pan_FPS";
            this.Pan_FPS.Size = new System.Drawing.Size(60, 98);
            this.Pan_FPS.TabIndex = 75;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.TSMI_TuneSettings});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1283, 24);
            this.menuStrip1.TabIndex = 74;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_DFTWindowCall,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 22);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // TSMI_DFTWindowCall
            // 
            this.TSMI_DFTWindowCall.Name = "TSMI_DFTWindowCall";
            this.TSMI_DFTWindowCall.Size = new System.Drawing.Size(216, 22);
            this.TSMI_DFTWindowCall.Text = "Show DFT Transform (beta)";
            this.TSMI_DFTWindowCall.Click += new System.EventHandler(this.TSMI_DFTWindowCall_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.quitToolStripMenuItem.Text = "Exit";
            // 
            // TSMI_TuneSettings
            // 
            this.TSMI_TuneSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.specialSeriesToolStripMenuItem,
            this.curvesCreatingToolStripMenuItem,
            this.TSMI_MThreadSave});
            this.TSMI_TuneSettings.Name = "TSMI_TuneSettings";
            this.TSMI_TuneSettings.Size = new System.Drawing.Size(89, 22);
            this.TSMI_TuneSettings.Text = "Tune settings";
            // 
            // specialSeriesToolStripMenuItem
            // 
            this.specialSeriesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_Tuning_Exposure,
            this.TSMI_Tuning_Irregular,
            this.TSMI_Tuning_Pereodical});
            this.specialSeriesToolStripMenuItem.Name = "specialSeriesToolStripMenuItem";
            this.specialSeriesToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.specialSeriesToolStripMenuItem.Text = "Special series";
            // 
            // TSMI_Tuning_Exposure
            // 
            this.TSMI_Tuning_Exposure.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_Load_EXWL_C,
            this.TSMI_UseFileExpAsRef,
            this.TSMI_UseCurrentExpAsRef,
            this.TSMI_UseAbsExposure});
            this.TSMI_Tuning_Exposure.Name = "TSMI_Tuning_Exposure";
            this.TSMI_Tuning_Exposure.Size = new System.Drawing.Size(232, 22);
            this.TSMI_Tuning_Exposure.Text = "Via WL - Exposure curve";
            // 
            // TSMI_Load_EXWL_C
            // 
            this.TSMI_Load_EXWL_C.Name = "TSMI_Load_EXWL_C";
            this.TSMI_Load_EXWL_C.Size = new System.Drawing.Size(251, 22);
            this.TSMI_Load_EXWL_C.Text = "Load WL - exposure curve";
            this.TSMI_Load_EXWL_C.Click += new System.EventHandler(this.TSMI_Load_EXWL_C_Click);
            // 
            // TSMI_UseFileExpAsRef
            // 
            this.TSMI_UseFileExpAsRef.Name = "TSMI_UseFileExpAsRef";
            this.TSMI_UseFileExpAsRef.Size = new System.Drawing.Size(251, 22);
            this.TSMI_UseFileExpAsRef.Text = "Use reference exposure from file";
            this.TSMI_UseFileExpAsRef.Click += new System.EventHandler(this.TSMI_UseFileExpAsRef_Click);
            // 
            // TSMI_UseCurrentExpAsRef
            // 
            this.TSMI_UseCurrentExpAsRef.Name = "TSMI_UseCurrentExpAsRef";
            this.TSMI_UseCurrentExpAsRef.Size = new System.Drawing.Size(251, 22);
            this.TSMI_UseCurrentExpAsRef.Text = "Use current exposure as reference";
            this.TSMI_UseCurrentExpAsRef.Click += new System.EventHandler(this.TSMI_UseCurrentExpAsRef_Click);
            // 
            // TSMI_UseAbsExposure
            // 
            this.TSMI_UseAbsExposure.Name = "TSMI_UseAbsExposure";
            this.TSMI_UseAbsExposure.Size = new System.Drawing.Size(251, 22);
            this.TSMI_UseAbsExposure.Text = "Use absolute exposure from file";
            this.TSMI_UseAbsExposure.Click += new System.EventHandler(this.TSMI_UseAbsExposure_Click);
            // 
            // TSMI_Tuning_Irregular
            // 
            this.TSMI_Tuning_Irregular.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_Load_UDWL_Curve,
            this.TSMI_Start_UDWL_tune});
            this.TSMI_Tuning_Irregular.Name = "TSMI_Tuning_Irregular";
            this.TSMI_Tuning_Irregular.Size = new System.Drawing.Size(232, 22);
            this.TSMI_Tuning_Irregular.Text = "Via user-defined irregular WLs";
            // 
            // TSMI_Load_UDWL_Curve
            // 
            this.TSMI_Load_UDWL_Curve.Name = "TSMI_Load_UDWL_Curve";
            this.TSMI_Load_UDWL_Curve.Size = new System.Drawing.Size(202, 22);
            this.TSMI_Load_UDWL_Curve.Text = "Load user-defined curve";
            this.TSMI_Load_UDWL_Curve.Click += new System.EventHandler(this.TSMI_Load_UD_Curve_Click);
            // 
            // TSMI_Start_UDWL_tune
            // 
            this.TSMI_Start_UDWL_tune.Name = "TSMI_Start_UDWL_tune";
            this.TSMI_Start_UDWL_tune.Size = new System.Drawing.Size(202, 22);
            this.TSMI_Start_UDWL_tune.Text = "Start tuning";
            this.TSMI_Start_UDWL_tune.Click += new System.EventHandler(this.TSMI_Start_UDWL_tune_Click);
            // 
            // TSMI_Tuning_Pereodical
            // 
            this.TSMI_Tuning_Pereodical.Name = "TSMI_Tuning_Pereodical";
            this.TSMI_Tuning_Pereodical.Size = new System.Drawing.Size(232, 22);
            this.TSMI_Tuning_Pereodical.Text = "Pereodical";
            this.TSMI_Tuning_Pereodical.Click += new System.EventHandler(this.TSMI_Tuning_Pereodical_Click);
            // 
            // curvesCreatingToolStripMenuItem
            // 
            this.curvesCreatingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_CurveCreating_ExposureWL});
            this.curvesCreatingToolStripMenuItem.Name = "curvesCreatingToolStripMenuItem";
            this.curvesCreatingToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.curvesCreatingToolStripMenuItem.Text = "Curves creating";
            // 
            // TSMI_CurveCreating_ExposureWL
            // 
            this.TSMI_CurveCreating_ExposureWL.Name = "TSMI_CurveCreating_ExposureWL";
            this.TSMI_CurveCreating_ExposureWL.Size = new System.Drawing.Size(182, 22);
            this.TSMI_CurveCreating_ExposureWL.Text = "Exposure - WL curve";
            this.TSMI_CurveCreating_ExposureWL.Click += new System.EventHandler(this.TSMI_CurveCreating_ExposureWL_Click);
            // 
            // TSMI_MThreadSave
            // 
            this.TSMI_MThreadSave.Name = "TSMI_MThreadSave";
            this.TSMI_MThreadSave.Size = new System.Drawing.Size(173, 22);
            this.TSMI_MThreadSave.Text = "Multithread saving";
            this.TSMI_MThreadSave.Click += new System.EventHandler(this.TSMI_MThreadSave_Click);
            // 
            // SessionTiming
            // 
            this.SessionTiming.Enabled = true;
            this.SessionTiming.Interval = 50;
            this.SessionTiming.Tick += new System.EventHandler(this.SessionTiming_Tick);
            // 
            // Timer_Sweep
            // 
            this.Timer_Sweep.Enabled = true;
            this.Timer_Sweep.Interval = 50;
            this.Timer_Sweep.Tick += new System.EventHandler(this.Timer_Sweep_Tick);
            // 
            // BGW_SpectralImageGrabbing
            // 
            this.BGW_SpectralImageGrabbing.WorkerReportsProgress = true;
            this.BGW_SpectralImageGrabbing.WorkerSupportsCancellation = true;
            this.BGW_SpectralImageGrabbing.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BGW_SpectralImageGrabbing_DoWork);
            this.BGW_SpectralImageGrabbing.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BGW_SpectralImageGrabbing_RunWorkerCompleted);
            // 
            // BGW_SpectralImageTuning
            // 
            this.BGW_SpectralImageTuning.WorkerSupportsCancellation = true;
            this.BGW_SpectralImageTuning.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BGW_SpectralImageTuning_DoWork);
            this.BGW_SpectralImageTuning.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BGW_SpectralImageTuning_RunWorkerCompleted);
            // 
            // BGW_SpectralCycle
            // 
            this.BGW_SpectralCycle.WorkerReportsProgress = true;
            this.BGW_SpectralCycle.WorkerSupportsCancellation = true;
            this.BGW_SpectralCycle.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BGW_SpectralCycle_DoWork);
            this.BGW_SpectralCycle.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BGW_SpectralCycle_ProgressChanged);
            this.BGW_SpectralCycle.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BGW_SpectralCycle_RunWorkerCompleted);
            // 
            // BGW_Saver
            // 
            this.BGW_Saver.WorkerReportsProgress = true;
            this.BGW_Saver.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BGW_Saver_DoWork);
            this.BGW_Saver.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BGW_Saver_ProgressChanged);
            this.BGW_Saver.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BGW_Saver_RunWorkerCompleted);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label42.Location = new System.Drawing.Point(133, 3);
            this.label42.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(189, 15);
            this.label42.TabIndex = 76;
            this.label42.Text = "Pereodical tune time interval (sec):";
            // 
            // NUD_TimingInCycle
            // 
            this.NUD_TimingInCycle.Location = new System.Drawing.Point(322, 2);
            this.NUD_TimingInCycle.Margin = new System.Windows.Forms.Padding(2);
            this.NUD_TimingInCycle.Maximum = new decimal(new int[] {
            7200,
            0,
            0,
            0});
            this.NUD_TimingInCycle.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.NUD_TimingInCycle.Name = "NUD_TimingInCycle";
            this.NUD_TimingInCycle.Size = new System.Drawing.Size(80, 20);
            this.NUD_TimingInCycle.TabIndex = 75;
            this.NUD_TimingInCycle.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.NUD_TimingInCycle.ValueChanged += new System.EventHandler(this.NUD_TimingInCycle_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 783);
            this.Controls.Add(this.label42);
            this.Controls.Add(this.NUD_TimingInCycle);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Main_Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Form1_WheelScrolled);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.icImagingControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrBZoomOfImage)).EndInit();
            this.GPCamFeat.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrBExposureVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrBGainVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_ExposureVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_GainVal)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrB_CurrentWL)).EndInit();
            this.GrBAOFWlSet.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrB_CurrentMHz)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.TabP_1_SimpleTuning.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_StartL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_FinishL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_StepL)).EndInit();
            this.TLP_Sweep_EasyMode.ResumeLayout(false);
            this.TLP_Sweep_EasyMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_FreqDeviation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_TimeFdev)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_FreqStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_FreqFin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_FreqStep)).EndInit();
            this.TLP_Saving_of_Frames.ResumeLayout(false);
            this.TLP_Saving_of_Frames.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.TabP_2_SpectralImg.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_WL1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_WL2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_WL3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_ex_time1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_ex_time2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_ex_time3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_WL4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_WL5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_WL6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_WL7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_WL8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_ex_time4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_ex_time5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_ex_time6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_ex_time7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Multi_ex_time8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_CurrentWL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_CurrentMHz)).EndInit();
            this.GrBImageSettings.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.Pan_FPS.ResumeLayout(false);
            this.Pan_FPS.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_TimingInCycle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public TIS.Imaging.ICImagingControl icImagingControl1;
        private System.Windows.Forms.Button SaveImgParBut;
        private System.Windows.Forms.Button PropBut;
        private System.Windows.Forms.Button ExitBut;
        private System.Windows.Forms.Button SnapshotBut;
        private System.Windows.Forms.CheckBox ChBExposureAuto;
        private System.Windows.Forms.TrackBar TrBZoomOfImage;
        private System.Windows.Forms.Label L_Zoom;
        private System.Windows.Forms.Button ContTransAfterSnapshot;
        private System.Windows.Forms.Timer TimerForExpGain_refresh;
        private System.Windows.Forms.Button SaveImageBut;
        private System.Windows.Forms.ListBox LBConsole;
        private System.Windows.Forms.GroupBox GPCamFeat;
        private System.Windows.Forms.Button BROIFull;
        private System.Windows.Forms.Button BSetROI;
        private System.Windows.Forms.TextBox TBROIPointY;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox TBROIPointX;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox TBROIHeight;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TBROIWidth;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar TrBGainVal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar TrBExposureVal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox CBoxPixelFormat;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox TBNamePrefix;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button B_DevOpen;
        private System.Windows.Forms.CheckBox ChBGainAuto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CBMResolution;
        private System.Windows.Forms.Button BAdapt;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox CBSavingPixelFormat;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox CBSignalFormats;
        private System.Windows.Forms.CheckBox ChBROIAutoCent;
        private System.Windows.Forms.ComboBox CbBinning;
        private System.Windows.Forms.Label LBinning;
        private System.Windows.Forms.Button BSelectUserResolution;
        private System.Windows.Forms.Label LFPS;
        private System.Windows.Forms.ComboBox CBAvFPS;
        private System.Windows.Forms.Label LLiveFpsLab;
        private System.Windows.Forms.Label LFPSCurrent;
        private System.Windows.Forms.Timer FPSTimer;
        private System.Windows.Forms.CheckBox ChBVisualiseROI;
        private System.Windows.Forms.Timer ICInvalidateTimer;
        private System.Windows.Forms.Button BTestAll;
        private System.Windows.Forms.TrackBar TrB_CurrentWL;
        private System.Windows.Forms.Button B_StartS;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BSetWL;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox GrBAOFWlSet;
        private System.Windows.Forms.CheckBox ChB_AutoSetWL;
        private System.Windows.Forms.GroupBox GrBImageSettings;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label L_RealDevName;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label L_ReqDevName;
        private System.Windows.Forms.ComboBox CBFinalPixelFormat;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button FlipUpDownBut;
        private System.Windows.Forms.Button FlipRightLeftBut;
        private System.ComponentModel.BackgroundWorker BkGrWorker_forExpCurveBuilding;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel Pan_FPS;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.ToolStripMenuItem TSMI_TuneSettings;
        private System.Windows.Forms.ToolStripMenuItem specialSeriesToolStripMenuItem;
        private System.Windows.Forms.CheckBox ChB_Power;
        private System.Windows.Forms.NumericUpDown NUD_CurrentWL;
        private System.Windows.Forms.NumericUpDown NUD_StartL;
        private System.Windows.Forms.NumericUpDown NUD_FinishL;
        private System.Windows.Forms.NumericUpDown NUD_StepL;
        private System.Windows.Forms.Timer SessionTiming;
        private System.Windows.Forms.ToolStripMenuItem curvesCreatingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TSMI_CurveCreating_ExposureWL;
        private System.Windows.Forms.ToolStripMenuItem TSMI_Tuning_Exposure;
        private System.Windows.Forms.ToolStripMenuItem TSMI_Tuning_Irregular;
        private System.Windows.Forms.ToolStripMenuItem TSMI_Tuning_Pereodical;
        private System.Windows.Forms.TableLayoutPanel TLP_Sweep_EasyMode;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.NumericUpDown NUD_FreqDeviation;
        private System.Windows.Forms.NumericUpDown NUD_TimeFdev;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.CheckBox ChB_SweepEnabled;
        private System.Windows.Forms.Timer Timer_Sweep;
        private System.Windows.Forms.ToolStripMenuItem TSMI_Load_UDWL_Curve;
        private System.Windows.Forms.ToolStripMenuItem TSMI_Start_UDWL_tune;
        private System.Windows.Forms.ToolStripMenuItem TSMI_Load_EXWL_C;
        private System.Windows.Forms.NumericUpDown NUD_Multi_WL1;
        private System.Windows.Forms.NumericUpDown NUD_Multi_WL2;
        private System.Windows.Forms.NumericUpDown NUD_Multi_WL3;
        private System.Windows.Forms.NumericUpDown NUD_Multi_ex_time1;
        private System.Windows.Forms.NumericUpDown NUD_Multi_ex_time2;
        private System.Windows.Forms.NumericUpDown NUD_Multi_ex_time3;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Button B_Get_HyperSpectral_Image;
        private System.ComponentModel.BackgroundWorker BGW_SpectralImageGrabbing;
        private System.ComponentModel.BackgroundWorker BGW_SpectralImageTuning;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TabP_1_SimpleTuning;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage TabP_2_SpectralImg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Label L_Num;
        private System.Windows.Forms.NumericUpDown NUD_Multi_WL4;
        private System.Windows.Forms.NumericUpDown NUD_Multi_WL5;
        private System.Windows.Forms.NumericUpDown NUD_Multi_WL6;
        private System.Windows.Forms.NumericUpDown NUD_Multi_WL7;
        private System.Windows.Forms.NumericUpDown NUD_Multi_WL8;
        private System.Windows.Forms.NumericUpDown NUD_Multi_ex_time4;
        private System.Windows.Forms.NumericUpDown NUD_Multi_ex_time5;
        private System.Windows.Forms.NumericUpDown NUD_Multi_ex_time6;
        private System.Windows.Forms.NumericUpDown NUD_Multi_ex_time7;
        private System.Windows.Forms.NumericUpDown NUD_Multi_ex_time8;
        private System.Windows.Forms.Label L_WL_Num1;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.CheckBox ChB_SpectralCycle;
        private System.ComponentModel.BackgroundWorker BGW_SpectralCycle;
        private System.ComponentModel.BackgroundWorker BGW_Saver;
        private System.Windows.Forms.ToolStripMenuItem TSMI_MThreadSave;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.NumericUpDown NUD_TimingInCycle;
        private System.Windows.Forms.NumericUpDown NUD_ExposureVal;
        private System.Windows.Forms.NumericUpDown NUD_GainVal;
        private System.Windows.Forms.Label L_ExpDim;
        private System.Windows.Forms.Label L_GainDim;
        private System.Windows.Forms.ToolStripMenuItem TSMI_UseFileExpAsRef;
        private System.Windows.Forms.ToolStripMenuItem TSMI_UseCurrentExpAsRef;
        private System.Windows.Forms.ToolStripMenuItem TSMI_UseAbsExposure;
        private System.Windows.Forms.ToolStripMenuItem TSMI_DFTWindowCall;
        private System.Windows.Forms.NumericUpDown NUD_FreqStart;
        private System.Windows.Forms.NumericUpDown NUD_FreqFin;
        private System.Windows.Forms.NumericUpDown NUD_FreqStep;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.RadioButton RB_Series_WLMode;
        private System.Windows.Forms.RadioButton RB_Series_FreqMode;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TrackBar TrB_CurrentMHz;
        private System.Windows.Forms.NumericUpDown NUD_CurrentMHz;
        private System.Windows.Forms.Button B_SetHZ;
        private System.Windows.Forms.ProgressBar PB_SeriesProgress;
        private System.Windows.Forms.ProgressBar PB_Saving;
        private System.Windows.Forms.TableLayoutPanel TLP_Saving_of_Frames;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label L_Aquired;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label L_Saved;
        private System.Windows.Forms.Label L_Info_Aquired;
        private System.Windows.Forms.Label L_Info_Saved;
    }
}

