using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ICSpec
{
    public partial class SaveDialog : Form
    {
        bool typeofwind=false;//if true,then it will be for video       
        public bool returning = false;
        private TIS.Imaging.MediaStreamSink m_Sink;
        Form1 frmFirst = null;
        LDZ_Code.ImageStyle ReturnImageStyle = new LDZ_Code.ImageStyle();
        string LastSaveDirectory=null;
        string fileName = null;

        public SaveDialog()
        {
            InitializeComponent();
        }

        private void SaveDialog_Load(object sender, EventArgs e)
        {
           frmFirst = this.Owner as Form1;
           InitDialog();
           LoadSettingsFromFile();
        }
        private void LoadSettingsFromFile()
        {
            if (File.Exists(fileName))
            {
                string[] readText = File.ReadAllLines(fileName);

                //Video Settings
                if (typeofwind)
                {
                    SaveDirectBox.Text = readText[5].Substring(19, readText[5].Length - 19);

                    string localcontainername = readText[6].Substring(readText[6].LastIndexOf(':') + 1, readText[6].Length - (readText[6].LastIndexOf(':') + 1));
                    bool containerfound = false;
                    for (int i = 0; i < ExtensCBox.Items.Count; i++)
                    {
                        if (ExtensCBox.Items[i].ToString() == localcontainername)
                        {
                            ExtensCBox.SelectedItem = ExtensCBox.Items[i];
                            containerfound = true;
                        }
                    }
                    if (!containerfound) ExtensCBox.SelectedIndex = 0;

                    string localcodecname = readText[7].Substring(readText[7].LastIndexOf(':') + 1, readText[7].Length - (readText[7].LastIndexOf(':') + 1));
                    bool codecfound = false;
                    for (int i = 0; i < CodecCBox.Items.Count; i++)
                    {
                        if (CodecCBox.Items[i].ToString() == localcodecname)
                        {
                            CodecCBox.SelectedItem = CodecCBox.Items[i];
                            codecfound = true;
                        }
                    }
                    if (!codecfound) CodecCBox.SelectedItem = CodecCBox.Items[TryTofindCodec()];
                }
                else
                {
                    //Image Settings
                    SaveDirectBox.Text = readText[1].Substring(19, readText[1].Length - 19);
                    
                    for(int i=0;i<ExtensCBox.Items.Count;i++)
                        if(ExtensCBox.Items[i].ToString()==readText[2].Substring(readText[2].LastIndexOf(':') + 1, readText[2].Length - (readText[2].LastIndexOf(':') + 1)))
                        {
                            ExtensCBox.SelectedIndex=i;
                            i=ExtensCBox.Items.Count;
                        }
                    string temp = readText[3].Substring(readText[3].LastIndexOf(':') + 1, readText[3].Length - (readText[3].LastIndexOf(':') + 1));
                    if (temp != "")
                    {
                        QualityTRBar.Value = Convert.ToInt32(temp);
                        LQuality.Text = QualityTRBar.Value + "%";
                    }
                    else QualityTRBar.Value = 100;
                }
            }
        }
        private void InitDialog()
        {
            fileName = frmFirst.fileName;
            if (typeofwind)
            {
                QualityTRBar.Enabled = false;
                QualityTRBar.Visible = false;
                label2.Text = "Кодеки";
                CodecCBox.Enabled = true;
                CodecCBox.Visible = true;
                CodecPropBut.Enabled = true;
                CodecPropBut.Visible = true;
                LQuality.Enabled = false;
                LQuality.Visible = false;
                label3.Visible = true;
                label3.Enabled = true;
                ExtensCBox.Visible = true;
                ExtensCBox.Enabled = true;

                SaveDirectBox.Text = LastSaveDirectory;
                CodecCBox.DataSource = frmFirst.icImagingControl1.AviCompressors;
                ExtensCBox.DataSource = frmFirst.icImagingControl1.MediaStreamContainers;              
                
            }
            else
            {
                QualityTRBar.Enabled = true;
                QualityTRBar.Visible = true;
                label2.Text = "Качество(jpg)";
                CodecCBox.Enabled = false;
                CodecCBox.Visible = false;
                CodecPropBut.Enabled = false;
                CodecPropBut.Visible = false;
                LQuality.Enabled = true;
                LQuality.Visible = true;               
                label3.Visible = true;
                label3.Enabled = true;
                ExtensCBox.Visible = true;
                ExtensCBox.Enabled = true;

                QualityTRBar.Value = 100;
                LQuality.Text = QualityTRBar.Value.ToString() + "%";
                SaveDirectBox.Text = LastSaveDirectory;
                ExtensCBox.Items.Clear();
               // ExtensCBox.Items.Add(".jpg"); ExtensCBox.Items.Add(".bmp");
                ExtensCBox.Items.Add(".tiff");
                ExtensCBox.SelectedIndex = 0; ExtensCBox.Items.Add(".tif");
            }
        }
        private int TryTofindCodec()
        {
            string realname="";
            int i = 0;
            this.Text = frmFirst.Text;
            realname = frmFirst.icImagingControl1.VideoFormatCurrent;
            realname=realname.Remove(realname.IndexOf(' '),realname.Length-realname.IndexOf(' '));
            for (i = CodecCBox.Items.Count-1; i >0 ; i--)
            {
                if (CodecCBox.GetItemText(CodecCBox.Items[i]).Contains(realname))              
                    break;               
            }
            return i;
        }
        private void ViewBut_Click(object sender, EventArgs e)
        {       
               /* SaveFileDialog dlg = new SaveFileDialog();
                dlg.AddExtension = true;
                if (typeofwind)
                {
                    string ext = CurrentMediaStreamContainer.PreferredFileExtension;
                    dlg.DefaultExt = ext;
                    dlg.Filter = CurrentMediaStreamContainer.Name  + " Video Files (*." + ext + ")|*." + ext + "||";
                    dlg.FilterIndex = 1;
                    dlg.RestoreDirectory = true;          
                }
                else
                {
                    dlg.FileName = "Image.jpg";
                    dlg.DefaultExt = "jpg";
                    dlg.Filter = "JPEG Images (*.jpg)|*.jpg|BMP Images (*.bmp)|*.bmp";
                    dlg.OverwritePrompt = true;
                    dlg.FilterIndex = 1;
                    dlg.RestoreDirectory = true;
                    dlg.AddExtension=true;
                }*/

               
               /* if (dlg.ShowDialog() == DialogResult.OK)
                {
                    SaveDirectBox.Text = dlg.FileName;
                    ReturnImageStyle.Directory = dlg.FileName;
                    ReturnImageStyle.Extension = FindExt(SaveDirectBox.Text);
                    ReturnImageStyle.Quality = -1;
                    if (ReturnImageStyle.Extension == ".jpg")
                    {
                        LQuality.Enabled = true;
                        QualityTRBar.Enabled = true;
                        QualityTRBar.Minimum = 0;
                        QualityTRBar.Maximum = 100;
                        QualityTRBar.Value = 100;
                        ReturnImageStyle.Quality = QualityTRBar.Value;
                        LQuality.Text = QualityTRBar.Value.ToString() + "%";
                    }
                    else QualityTRBar.Enabled = false;                  
                }*/
            FolderBrowserDialog dialog = new FolderBrowserDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                        SaveDirectBox.Text = dialog.SelectedPath;
            
            
        }
        private string FindExt(string s)
        {
            return s.Substring(s.LastIndexOf('.'), s.Length - s.LastIndexOf('.'));
        }
        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }
        public void ShowDialog(bool type)
        {
            typeofwind = type;            
            ShowDialog();
             
        }
        public TIS.Imaging.MediaStreamSink retmethod()
        {    
            return m_Sink;
        }
        public LDZ_Code.ImageStyle ReturnImageStyleMethod()
        {
            return ReturnImageStyle;
        }
        public bool IsReturning()
        {
            return returning;
        }     
        private TIS.Imaging.MediaStreamContainer CurrentMediaStreamContainer
        {
            get
            {
                return (TIS.Imaging.MediaStreamContainer)ExtensCBox.SelectedItem;
            }
        }
        private TIS.Imaging.AviCompressor CurrentVideoCodec
        {
            get
            {
                if (CurrentMediaStreamContainer != null && CurrentMediaStreamContainer.IsCustomCodecSupported)
                {
                    return (TIS.Imaging.AviCompressor)CodecCBox.SelectedItem;
                }
                else
                {
                    return null;
                }
            }
        }

        private void ExtensCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (typeofwind)
            {
                if (CurrentMediaStreamContainer.IsCustomCodecSupported)
                {
                    CodecCBox.Enabled = true;
                    if (CurrentVideoCodec != null) CodecPropBut.Enabled = CurrentVideoCodec.PropertyPageAvailable;
                }
                else
                {
                    CodecCBox.Enabled = false;
                    CodecPropBut.Enabled = false;
                }
            }
            else
            {
                if (ExtensCBox.SelectedIndex == 0)
                {
                    QualityTRBar.Enabled = true;
                    LQuality.Enabled = true;
                }
                else
                {
                    QualityTRBar.Enabled = false;
                    LQuality.Enabled = false;
                }

            }

            
        }
        private void CodecCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CodecPropBut.Enabled = CurrentVideoCodec.PropertyPageAvailable; 
          
        }

        private void CodecPropBut_Click(object sender, EventArgs e)
        {
            CurrentVideoCodec.ShowPropertyPage();
        }

        private void OKBut_Click(object sender, EventArgs e)
        {
            bool alldone = true;
            if (SaveDirectBox.Text == "")
            {
                MessageBox.Show("Введите директорию файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                alldone = false;
            }
            else
            {
                if (!System.IO.Directory.Exists(SaveDirectBox.Text))
                {
                    if (SaveDirectBox.Text.IndexOf(':') != 1)
                    {
                        MessageBox.Show("Введите корректное имя диска", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        alldone = false;
                    }
                    else
                    {                        
                        try
                        {
                            System.IO.Directory.CreateDirectory(SaveDirectBox.Text);
                        }
                        catch (System.IO.IOException except)
                        {                                               
                            MessageBox.Show(except.Message + "Введите расположение, в котором возможно создать папку.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alldone = false;
                        }
                    }
                    
                }
               /* if ((!((SaveDirectBox.Text.Contains(".jpg")) || ((SaveDirectBox.Text.Contains(".bmp")))))&&(!typeofwind))
                {
                    MessageBox.Show("Введите разрешенное расширение(*.bmp или *.jpg)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }*/
                if (alldone)
                {
                    if(typeofwind)
                        frmFirst.WarningofCapt = false;
                    else
                        frmFirst.WarningofImage = false;
                    if (!File.Exists(fileName))
                    {
                        using (StreamWriter sw = new StreamWriter(new FileStream(fileName, FileMode.Create, FileAccess.Write)))
                        {
                            if (typeofwind)
                            {
                                sw.WriteLine("Image Settings:");
                                sw.WriteLine("      1.Directory::");
                                sw.WriteLine("      2.Extension::");
                                sw.WriteLine("      3.Quality::");
                                sw.WriteLine("Video Settings:");
                                sw.WriteLine("      1.Directory::" + SaveDirectBox.Text + "\\");
                                sw.WriteLine("      1.Media Stream Container(extension)::" + ExtensCBox.SelectedItem.ToString());
                                sw.WriteLine("      1.Codec::" + CodecCBox.SelectedItem.ToString());
                            }
                            else
                            {
                                sw.WriteLine("Image Settings:");
                                sw.WriteLine("      1.Directory::" + SaveDirectBox.Text + "\\");
                                sw.WriteLine("      2.Extension::" + ExtensCBox.Text);
                                sw.WriteLine("      3.Quality::" + QualityTRBar.Value.ToString());
                                sw.WriteLine("Video Settings:");
                                sw.WriteLine("      1.Directory::");
                                sw.WriteLine("      1.Media Stream Container(extension)::");
                                sw.WriteLine("      1.Codec::");
                            }
                            
                        }
                    }
                    else
                    {            
                        string temp=null;//если файл есть, то отркываем его и пишем в него     
                            if(SaveDirectBox.Text.LastIndexOf('\\')==SaveDirectBox.Text.Length-1) temp = "      1.Directory::" + SaveDirectBox.Text;
                            else temp="      1.Directory::" + SaveDirectBox.Text + "\\";
                            if (typeofwind)
                            {
                                string[] readText = File.ReadAllLines(fileName);
                                string[] writeText = {readText[0],readText[1],readText[2],readText[3],"Video Settings:",
                                                         temp,
                                                         "      2.Media Stream Container(extension)::" + ExtensCBox.SelectedItem.ToString(),
                                                         "      3.Codec::" + CodecCBox.SelectedItem.ToString()};
                                File.WriteAllLines(fileName, writeText);
                                
                            }
                            else
                            {
                                string[] readText = File.ReadAllLines(fileName);                              
                                string[] writeText = {"Image Settings:",
                                                         temp,
                                                         "      2.Extension::" + ExtensCBox.SelectedItem.ToString(),
                                                         "      3.Quality::" + QualityTRBar.Value.ToString(),
                                                         readText[4],readText[5],readText[6],readText[7]};
                                File.WriteAllLines(fileName, writeText);
                            }                        
                    }
                }
                returning = true;
                    Close();
                }
            }
        

        private void SaveDialog_FormClosing(object sender, FormClosingEventArgs e)
        {         
            if (returning)
            {
                if (typeofwind)
                {
                   /* m_Sink = new TIS.Imaging.MediaStreamSink();
                    m_Sink.StreamContainer = CurrentMediaStreamContainer;
                    m_Sink.Codec = CurrentVideoCodec;
                    m_Sink.Filename = SaveDirectBox.Text + "\\"; // познал истину тут. \" и \\ - явления одного и того же порядка*/
                  
                }
                else
                {
                   /* ReturnImageStyle.Quality = QualityTRBar.Value;
                    ReturnImageStyle.Extension = ExtensCBox.Text;
                    ReturnImageStyle.Directory = SaveDirectBox.Text + "\\" ;   */               
                }
            }           
        }

        private void CancelBut_Click(object sender, EventArgs e)
        {
            returning = false;
            Close();
        }

        private void QualityTRBar_Scroll(object sender, EventArgs e)
        {
            LQuality.Text = QualityTRBar.Value.ToString() + "%";
        }

        private void DateInNameChkbox_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
