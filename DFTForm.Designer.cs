namespace ICSpec
{
    partial class DFTForm
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
            this.components = new System.ComponentModel.Container();
            this.TLP_DFTMain = new System.Windows.Forms.TableLayoutPanel();
            this.ImB_DFT = new Emgu.CV.UI.ImageBox();
            this.TLP_DFTMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImB_DFT)).BeginInit();
            this.SuspendLayout();
            // 
            // TLP_DFTMain
            // 
            this.TLP_DFTMain.ColumnCount = 2;
            this.TLP_DFTMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_DFTMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.TLP_DFTMain.Controls.Add(this.ImB_DFT, 0, 0);
            this.TLP_DFTMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_DFTMain.Location = new System.Drawing.Point(0, 0);
            this.TLP_DFTMain.Name = "TLP_DFTMain";
            this.TLP_DFTMain.RowCount = 1;
            this.TLP_DFTMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_DFTMain.Size = new System.Drawing.Size(960, 697);
            this.TLP_DFTMain.TabIndex = 0;
            // 
            // ImB_DFT
            // 
            this.ImB_DFT.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ImB_DFT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImB_DFT.Location = new System.Drawing.Point(3, 3);
            this.ImB_DFT.Name = "ImB_DFT";
            this.ImB_DFT.Size = new System.Drawing.Size(953, 691);
            this.ImB_DFT.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImB_DFT.TabIndex = 2;
            this.ImB_DFT.TabStop = false;
            // 
            // DFTForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 697);
            this.Controls.Add(this.TLP_DFTMain);
            this.Name = "DFTForm";
            this.Text = "DFTForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DFTForm_FormClosing);
            this.Load += new System.EventHandler(this.DFTForm_Load);
            this.TLP_DFTMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImB_DFT)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TLP_DFTMain;
        private Emgu.CV.UI.ImageBox ImB_DFT;
    }
}