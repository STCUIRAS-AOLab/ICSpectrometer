namespace ICSpec
{
    partial class SaveDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SaveDirectBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ViewBut = new System.Windows.Forms.Button();
            this.CodecCBox = new System.Windows.Forms.ComboBox();
            this.CodecPropBut = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.CancelBut = new System.Windows.Forms.Button();
            this.OKBut = new System.Windows.Forms.Button();
            this.ExtensCBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.QualityTRBar = new System.Windows.Forms.TrackBar();
            this.LQuality = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.QualityTRBar)).BeginInit();
            this.SuspendLayout();
            // 
            // SaveDirectBox
            // 
            this.SaveDirectBox.Location = new System.Drawing.Point(12, 27);
            this.SaveDirectBox.Name = "SaveDirectBox";
            this.SaveDirectBox.Size = new System.Drawing.Size(702, 20);
            this.SaveDirectBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Директория";
            // 
            // ViewBut
            // 
            this.ViewBut.Location = new System.Drawing.Point(720, 25);
            this.ViewBut.Name = "ViewBut";
            this.ViewBut.Size = new System.Drawing.Size(75, 23);
            this.ViewBut.TabIndex = 2;
            this.ViewBut.Text = "Обзор";
            this.ViewBut.UseVisualStyleBackColor = true;
            this.ViewBut.Click += new System.EventHandler(this.ViewBut_Click);
            // 
            // CodecCBox
            // 
            this.CodecCBox.FormattingEnabled = true;
            this.CodecCBox.Location = new System.Drawing.Point(12, 73);
            this.CodecCBox.Name = "CodecCBox";
            this.CodecCBox.Size = new System.Drawing.Size(699, 21);
            this.CodecCBox.TabIndex = 3;
            this.CodecCBox.SelectedIndexChanged += new System.EventHandler(this.CodecCBox_SelectedIndexChanged);
            // 
            // CodecPropBut
            // 
            this.CodecPropBut.Location = new System.Drawing.Point(720, 73);
            this.CodecPropBut.Name = "CodecPropBut";
            this.CodecPropBut.Size = new System.Drawing.Size(75, 23);
            this.CodecPropBut.TabIndex = 4;
            this.CodecPropBut.Text = "Свойства";
            this.CodecPropBut.UseVisualStyleBackColor = true;
            this.CodecPropBut.Click += new System.EventHandler(this.CodecPropBut_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Кодеки";
            // 
            // CancelBut
            // 
            this.CancelBut.Location = new System.Drawing.Point(720, 175);
            this.CancelBut.Name = "CancelBut";
            this.CancelBut.Size = new System.Drawing.Size(75, 23);
            this.CancelBut.TabIndex = 6;
            this.CancelBut.Text = "Отмена";
            this.CancelBut.UseVisualStyleBackColor = true;
            this.CancelBut.Click += new System.EventHandler(this.CancelBut_Click);
            // 
            // OKBut
            // 
            this.OKBut.Location = new System.Drawing.Point(12, 175);
            this.OKBut.Name = "OKBut";
            this.OKBut.Size = new System.Drawing.Size(75, 23);
            this.OKBut.TabIndex = 7;
            this.OKBut.Text = "ОК";
            this.OKBut.UseVisualStyleBackColor = true;
            this.OKBut.Click += new System.EventHandler(this.OKBut_Click);
            // 
            // ExtensCBox
            // 
            this.ExtensCBox.FormattingEnabled = true;
            this.ExtensCBox.Location = new System.Drawing.Point(12, 135);
            this.ExtensCBox.Name = "ExtensCBox";
            this.ExtensCBox.Size = new System.Drawing.Size(121, 21);
            this.ExtensCBox.TabIndex = 8;
            this.ExtensCBox.SelectedIndexChanged += new System.EventHandler(this.ExtensCBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Расширения";
            // 
            // QualityTRBar
            // 
            this.QualityTRBar.Location = new System.Drawing.Point(9, 73);
            this.QualityTRBar.Maximum = 100;
            this.QualityTRBar.Name = "QualityTRBar";
            this.QualityTRBar.Size = new System.Drawing.Size(702, 45);
            this.QualityTRBar.TabIndex = 10;
            this.QualityTRBar.Scroll += new System.EventHandler(this.QualityTRBar_Scroll);
            // 
            // LQuality
            // 
            this.LQuality.AutoSize = true;
            this.LQuality.Location = new System.Drawing.Point(717, 73);
            this.LQuality.Name = "LQuality";
            this.LQuality.Size = new System.Drawing.Size(35, 13);
            this.LQuality.TabIndex = 11;
            this.LQuality.Text = "label4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(404, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(310, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "В названии будут прописаны дата и время создания файла";
            // 
            // SaveDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 210);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LQuality);
            this.Controls.Add(this.QualityTRBar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ExtensCBox);
            this.Controls.Add(this.OKBut);
            this.Controls.Add(this.CancelBut);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CodecPropBut);
            this.Controls.Add(this.CodecCBox);
            this.Controls.Add(this.ViewBut);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SaveDirectBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SaveDialog";
            this.Text = "SaveDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SaveDialog_FormClosing);
            this.Load += new System.EventHandler(this.SaveDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.QualityTRBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SaveDirectBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ViewBut;
        private System.Windows.Forms.ComboBox CodecCBox;
        private System.Windows.Forms.Button CodecPropBut;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CancelBut;
        private System.Windows.Forms.Button OKBut;
        private System.Windows.Forms.ComboBox ExtensCBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar QualityTRBar;
        private System.Windows.Forms.Label LQuality;
        private System.Windows.Forms.Label label4;
    }
}