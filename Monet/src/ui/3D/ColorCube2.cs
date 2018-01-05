///-------------------------------------------------------------------------------------------------
/// \file src\ui\3D\ColorCube2.cs.
///
/// \brief Implements the color cube 2 class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monet.src.ui._3D
{
    ///-------------------------------------------------------------------------------------------------
    /// \class ColorCube2
    ///
    /// \brief A color cube 2.
    ///-------------------------------------------------------------------------------------------------

    class ColorCube2
    {
        /// \brief 本例用绘制多边形的方法(fillPolygon()与drawPolygon())来绘制几何体的棱(边)
        private static int[,] polygons =
        // Solid cube 正方体(每面一种颜色)
        {{5, 0, 6, 10, 13, 9, 6},
{5, 1, 7, 11, 12, 8, 7},
{5, 2, 6, 7, 8, 9, 6},
{5, 3, 10, 11, 12, 13, 10},
{5, 4, 6, 7, 11, 10, 6},
{5, 5, 8, 9, 13, 12, 8}};
        /// \brief 以下数据与点坐标有关
        private double[,] points =
        // Points for solid cube & polygonal faces cube
        {{1, 0, 0}, {-1, 0, 0}, {0, 1, 0},
        {0, -1, 0}, {0, 0, 1}, {0, 0, -1},
        {1, 1, 1}, {-1, 1, 1}, {-1, 1, -1},{1, 1, -1},
        {1, -1, 1}, {-1, -1, 1}, {-1, -1, -1},{1, -1, -1}};
        /// \brief 以下数据与面有关
        private static int[,] faces =
        {{1, 0}, {1, 1}, {1, 2}, {1, 3}, {1, 4}, {1, 5}};
        /// \brief The npoly
        private int npoly = 6;
        /// \brief The npoints
        const int npoints = 14;
        /// \brief The nfaces
        const int nfaces = 6;
        /// \brief The rot points
        public double[,] rotPts = new double[npoints, 3];
        /// \brief The screen points
        public double[,] scrPts = new double[npoints, 3];
        // public double scrPts[,];
        /// \brief The xx
        public int[] xx = new int[5];
        /// \brief The yy
        public int[] yy = new int[5];
        /// \brief The length
        double len;
        // rotPts = new double[npoints,3];
        // scrPts = new double[npoints,3];
        // xx = new int[5];
        // yy = newint[5];
        /// \brief The ncolour
        const int ncolour = 10;
        /// \brief The color value
        private int[] ColorValue = new int[ncolour];

        ///-------------------------------------------------------------------------------------------------
        /// \brief private Color[] colours = new Color[ncolour];
        ///     private double[] lightvec = {1,0, 0};//光线从左向右
        ///      private double[] lightvec = {0,0, -1}; //光线从外向里(向屏幕深处),正对用户的面最亮 private double[]
        ///      lightvec = {0,0, 1}; //光线从里向外,正对用户的面最暗 private double[] lightvec = {0,1, 0};//光线从上向下
        ///-------------------------------------------------------------------------------------------------

        private double[] lightvec = { 1, 1, -1 };
                                                 //private double colour;

        ///-------------------------------------------------------------------------------------------------
        /// \property double angleX, angleY, angleZ
        ///
        /// \brief Gets the angle z coordinate
        ///
        /// \return The angle z coordinate.
        ///-------------------------------------------------------------------------------------------------

        double angleX, angleY, angleZ;

        ///-------------------------------------------------------------------------------------------------
        /// \property public Matrix3D2 orient, tmp, tmp2, tmp3
        ///
        /// \brief Gets the temporary 3
        ///
        /// \return The temporary 3.
        ///-------------------------------------------------------------------------------------------------

        public Matrix3D2 orient, tmp, tmp2, tmp3;
        /// \brief The scale
        private double scale;
        /// \brief An int to process
        private int p;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public ColorCube2( int CanvasWidth, int CanvasHeight, float SizeScale)
        ///
        /// \brief Constructor
        ///
        /// \param CanvasWidth  Width of the canvas.
        /// \param CanvasHeight Height of the canvas.
        /// \param SizeScale    The size scale.
        ///-------------------------------------------------------------------------------------------------

        public ColorCube2(
        int CanvasWidth, int CanvasHeight, float SizeScale)
        {
            orient = new Matrix3D2();
            tmp = new Matrix3D2();
            tmp2 = new Matrix3D2();
            tmp3 = new Matrix3D2();
            angleX = 20; angleY = 20; angleZ = 10;
            //立方体在转动过程中,颜色的深浅也将发生变化.
            // for (int i = 0; i < ncolour; i++) { //i=0时,(165,165,165); i=9时,(255,255,255)最亮
            //注意其中的'100',可称为'明暗对比系数',加大该值,可令明暗对比更强烈
            // colours[i]= new Color(0,255 -(ncolour-1-i)*100/ncolour,0);// green
            // ColorValue [i]= 255 - (ncolour - 1 - i) * 100 / ncolour;
            //以下其它颜色,有兴趣可以演示一下效果
            // colours[i]= new Color(255 -(ncolour-1-i)*30/ncolour, 255 -(ncolour-1-i)*30/ncolour,255 -(ncolour-1-i)*30/ncolour); // white(灰白)
            //colours[i]= new Color(255 -(ncolour-1-i)*100/ncolour,0,0);// red
            // colours[i]= new Color(0,0,255 -(ncolour-1-i)*100/ncolour); // blue
            // colours[i]= new Color(255 -(ncolour-1-i)*100/ncolour, 255 -(ncolour-1-i)*100/ncolour,0); // yellow
            // colours[i]= new Color(0, 255 -(ncolour-1-i)*100/ncolour, 255 -(ncolour-1-i)*100/ncolour);// cyan
            // colours[i]= new Color(255 -(ncolour-1-i)*100/ncolour, 0, 255 -(ncolour-1-i)*100/ncolour); // magenta
            // }
            //以下将光线方向向量转化为单位向量
            len = Math.Sqrt(lightvec[0] * lightvec[0] +
            lightvec[1] * lightvec[1] +
            lightvec[2] * lightvec[2]);
            lightvec[0] = lightvec[0] / len;
            lightvec[1] = lightvec[1] / len;
            lightvec[2] = lightvec[2] / len;
            //以下将最开始给出的各面中心点坐标(法向量)转换为单位向量,如:
            //(1,0,0)转换为:(1,0,0).不变;
            //(1,1,0)转换为:(0.707,0.707,0)
            //(1,1,1)转换为:(0.577,0.577,0.577)
            for (int i = 0; i < nfaces; i++)
            {
                len = Math.Sqrt(points[i, 0] * points[i, 0] +
                points[i, 1] * points[i, 1] + points[i, 2] * points[i, 2]);
                points[i, 0] = points[i, 0] / len;
                points[i, 1] = points[i, 1] / len;
                points[i, 2] = points[i, 2] / len;
            }
            double max = 0;
            for (int i = 0; i < npoints; i++)
            {
                len = Math.Sqrt(points[i, 0] * points[i, 0] +
                points[i, 1] * points[i, 1] + points[i, 2] * points[i, 2]);
                if (len > max)
                {
                    max = len;
                }
            }
            CubeRotate(1, 1, 1);
            scale = Math.Min(SizeScale * CanvasWidth / 2 / max, SizeScale * CanvasHeight / 2 / max); //计算放大比例
            CalcScrPts((double)CanvasWidth / 2, (double)CanvasHeight / 2, 0); //此方法将先计算转动后的坐标, 再将其转换为屏幕坐标
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public void CubeRotate(double rotX, double rotY, double rotZ)
        ///
        /// \brief Cube rotate
        ///
        /// \param rotX The rot x coordinate.
        /// \param rotY The rot y coordinate.
        /// \param rotZ The rot z coordinate.
        ///-------------------------------------------------------------------------------------------------

        public void CubeRotate(double rotX, double rotY, double rotZ)
        {
            angleX += rotX;
            angleY += rotY;
            angleZ += rotZ;
            tmp.Rotation(1, 2, Math.PI * angleX / 180);//绕y轴旋转50度(前两个参数决定旋转所围绕的轴以及旋转的方向,详见后面Matrix3D类的Rotation())
            tmp2.Rotation(0, 2, Math.PI * angleY / 180);
            tmp3.Rotation(0, 1, Math.PI * angleZ / 180);
            orient.M = tmp3.Times(tmp2.Times(tmp.M));
        }//end CubeRotate

        ///-------------------------------------------------------------------------------------------------
        /// \fn public void CalcScrPts(double x, double y, double z)
        ///
        /// \brief 以下先将点乘以矩阵,计算出转动后的坐标, 再将其转换为屏幕坐标
        ///
        /// \param x The x coordinate.
        /// \param y The y coordinate.
        /// \param z The z coordinate.
        ///-------------------------------------------------------------------------------------------------

        public void CalcScrPts(double x, double y, double z)
        {
            for (p = 0; p < npoints; p++)
            { //将各点转换为转动后的坐标
                rotPts[p, 2] = points[p, 0] * orient.M[2, 0] +
                points[p, 1] * orient.M[2, 1] +
                points[p, 2] * orient.M[2, 2];
                rotPts[p, 0] = points[p, 0] * orient.M[0, 0] +
                points[p, 1] * orient.M[0, 1] +
                points[p, 2] * orient.M[0, 2];
                rotPts[p, 1] = -points[p, 0] * orient.M[1, 0] -
                points[p, 1] * orient.M[1, 1] -
                points[p, 2] * orient.M[1, 2];
            }
            //以下将转动后的各点转换为屏幕坐标
            for (p = nfaces; p < npoints; p++)
            { //注意P的初值.(只转换后八个点,前6个点为各面的中心点,无须转换
                scrPts[p, 0] = (int)(rotPts[p, 0] * scale + x);
                scrPts[p, 1] = (int)(rotPts[p, 1] * scale + y);
            }
        }//end CalcScrPts

        ///-------------------------------------------------------------------------------------------------
        /// \fn private bool faceUp(int f)
        ///
        /// \brief 以下计算可见面
        ///
        /// \param f An int to process.
        ///
        /// \return True if it succeeds, false if it fails.
        ///-------------------------------------------------------------------------------------------------

        private bool faceUp(int f)
        {
            return (rotPts[f, 2] < 0); //rotPts[f,2]为该面的中心点的Z坐标
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public void MyDraw(Graphics g2, int w, int h)
        ///
        /// \brief My draw
        ///
        /// \param g2 The second Graphics.
        /// \param w  The width.
        /// \param h  The height.
        ///-------------------------------------------------------------------------------------------------

        public void MyDraw(Graphics g2, int w, int h)
        {
            for (int f = 0; f < nfaces; f++)
            {
                if (faceUp(f))
                    DrawPoly(g2, f, getColour(f));
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn void DrawPoly(Graphics g2, int nf, int myColorValue)
        ///
        /// \brief Draw polygon
        ///
        /// \param g2           The second Graphics.
        /// \param nf           The nf.
        /// \param myColorValue my color value.
        ///-------------------------------------------------------------------------------------------------

        void DrawPoly(Graphics g2, int nf, int myColorValue)
        {
            //下面NEW一个局部的Point类的数组
            Point[] myPoint = new Point[polygons[nf, 0]]; //polygons[nf, 0]的数值 = 第nf面的多边形顶点数+1（起点与终点为同一点）
            for (int p = 0; p < polygons[nf, 0]; p++)
            {
                xx[p] = (int)scrPts[polygons[nf, p + 2], 0];
                yy[p] = (int)scrPts[polygons[nf, p + 2], 1];
                myPoint[p].X = xx[p];
                myPoint[p].Y = yy[p];
            }
            SolidBrush myBrush = new SolidBrush(Color.FromArgb(10 + myColorValue * 25, 10 + myColorValue * 25, 255)); // 变化区间10---235
            g2.FillPolygon(myBrush, myPoint);
            g2.DrawPolygon(Pens.Black, myPoint);
            //g2.setColor(colour);
            //g2.FillPolygon(xx,yy,polygons[nf,0]);//也可将polygons[nf,0]改作4;(polygons[nf,0]==5)
            //g2.setColor(Color.black);
            //g2.DrawPolygon(xx,yy,polygons[nf,0]);//也可将polygons[nf,0]改作4;(polygons[nf,0]==5)
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private int getColour(int f)
        ///
        /// \brief Gets a colour
        ///
        /// \param f An int to process.
        ///
        /// \return The colour.
        ///-------------------------------------------------------------------------------------------------

        private int getColour(int f)
        {
            int cc = 0;
            double colour = (rotPts[f, 0] * lightvec[0] + rotPts[f, 1] * lightvec[1]
            + rotPts[f, 2] * lightvec[2]);
            if (colour >= -1 && colour < -0.8)
                cc = 9;
            if (colour >= -0.8 && colour < -0.6)
                cc = 8;
            if (colour >= -0.6 && colour < -0.4)
                cc = 7;
            if (colour >= -0.4 && colour < -0.2)
                cc = 6;
            if (colour >= -0.2 && colour < 0)
                cc = 5;
            if (colour >= 0 && colour < 0.2)
                cc = 4;
            if (colour >= 0.2 && colour < 0.4)
                cc = 3;
            if (colour >= 0.4 && colour < 0.6)
                cc = 2;
            if (colour >= 0.6 && colour <= 0.8)
                cc = 1;
            if (colour >= 0.8 && colour <= 1)
                cc = 1;
            return cc;
        }
    }
}
