﻿///-------------------------------------------------------------------------------------------------
/// \file src\ui\3D\Matrix3D2.cs.
///
/// \brief Implements the matrix 018`2 class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monet.src.ui._3D
{
    ///-------------------------------------------------------------------------------------------------
    /// \class Matrix3D2
    ///
    /// \brief A matrix 018`2.
    ///-------------------------------------------------------------------------------------------------

    class Matrix3D2
    {
        /// \brief 以下定义一个3*3的单位矩阵,修改其中元素数值对程序运行并无影响. 因为每次进行矩阵运算时,都先对这个矩阵进行初始化.见Rotation()中的代码.
        public double[,] M = { { 1, 0, 0 },
                                { 0, 1, 0 },
                                { 0, 0, 1 } };
        /// \brief The temporary
        private double[,] tmp = new double[3, 3];

        ///-------------------------------------------------------------------------------------------------
        /// \property private int row, col, k
        ///
        /// \brief Gets the k
        ///
        /// \return The k.
        ///-------------------------------------------------------------------------------------------------

        private int row, col, k;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public void Rotation(int i, int j, double angle)
        ///
        /// \brief 以下Rotation将根据传入参数i,j的不同,分别实现绕x轴,y轴,z轴的顺时针或反时针的旋转. 如: i=0,j=1时,绕Z轴顺时针; i=1,j=0时,绕Y轴反时针,
        ///     如: i=1,j=2时,绕X轴顺时针; i=2,j=1时,绕Y轴反时针, 如: i=0,j=2时,绕Y轴顺时针; i=2,j=0时,绕Y轴反时针, 参见'计算机图形学与矩阵'
        ///
        /// \param i     Zero-based index of the.
        /// \param j     An int to process.
        /// \param angle The angle.
        ///-------------------------------------------------------------------------------------------------

        public void Rotation(int i, int j, double angle)
        {
            //以下for循环对矩阵M进行初始化
            for (row = 0; row < 3; row++)
            {
                for (col = 0; col < 3; col++)
                {
                    if (row != col)
                    {
                        M[row, col] = 0.0;
                    }
                    else
                    {
                        M[row, col] = 1.0;
                    }
                }
            }
            M[i, i] = Math.Cos(angle);
            M[j, j] = Math.Cos(angle);
            M[i, j] = Math.Sin(angle);
            M[j, i] = -Math.Sin(angle);
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public double[,] Times(double[,] N)
        ///
        /// \brief Times the given n
        ///
        /// \param N A double[,] to process.
        ///
        /// \return A double[,].
        ///-------------------------------------------------------------------------------------------------

        public double[,] Times(double[,] N)
        {
            for (row = 0; row < 3; row++)
            {
                for (col = 0; col < 3; col++)
                {
                    tmp[row, col] = 0.0;
                    for (k = 0; k < 3; k++)
                    {
                        tmp[row, col] += M[row, k] * N[k, col];
                    }
                }
            }
            return tmp;
        }
    }
}
