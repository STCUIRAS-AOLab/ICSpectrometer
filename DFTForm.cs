using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;

namespace ICSpec
{
    public partial class DFTForm : Form
    {
        int optRows = 0;
        int optCols = 0;
        Rectangle ROI;
        Matrix<float> finalmatrix;
        public DFTForm(Form1 pOwner, int w, int h)
        {
            this.Owner = pOwner;
            InitializeComponent();
            optRows = CvInvoke.GetOptimalDFTSize(h);
            optCols = CvInvoke.GetOptimalDFTSize(w);
            ROI = new Rectangle(0, 0, w, h);
            finalmatrix = new Matrix<float>(optRows, optCols);
        }

        private void DFTForm_Load(object sender, EventArgs e)
        {

        }
        public void DFTFromMat(Bitmap Image)
        {
            Image<Bgr, Single> imageCV_spl = new Image<Bgr, Single>(Image); //приняли RGB
            Image<Gray, Single>[] imageCV = imageCV_spl.Split();            //разделили на 3 канала
            
            var image = new Mat(imageCV[0].Mat, ROI);
           
            var extended = new Mat();
            CvInvoke.CopyMakeBorder(image, extended, 0, optRows - image.Rows, 0, optCols - image.Cols, BorderType.Constant);

            extended.ConvertTo(extended, DepthType.Cv32F);
            var vec = new VectorOfMat(extended, new Mat(extended.Size, DepthType.Cv32F, 1));
            var complex = new Mat();
            CvInvoke.Merge(vec, complex);

            CvInvoke.Dft(complex, complex, DxtType.Forward, 0); //dft
            CvInvoke.Split(complex, vec); //разделение реальной и комплексной матрицы
            var outReal = vec[0];
            var outIm = vec[1];

            CvInvoke.Pow(outReal, 2.0, outReal);    //Re^2
            CvInvoke.Pow(outIm, 2.0, outIm);        //Im^2
            CvInvoke.Add(outReal, outIm, outReal);  //Re^2+Im^2
            CvInvoke.Sqrt(outReal, outReal);        //sqrt(Re^2+Im^2)
            CvInvoke.Log(outReal, outReal);         //для нормального отображения
            

            outReal.CopyTo(finalmatrix);
            finalmatrix = finalmatrix.Clone();
            SwitchQuadrants(ref finalmatrix);

            CvInvoke.Normalize(finalmatrix, finalmatrix, 0.0, 255.0, Emgu.CV.CvEnum.NormType.MinMax);

            RefreshDFT(finalmatrix.Mat);
            
            
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
        private void RefreshDFT(Mat Mat2Show)
        {
            try { ImB_DFT.Image = Mat2Show.ToImage<Bgr, byte>(); } catch { }
        }

        private void DFTForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
