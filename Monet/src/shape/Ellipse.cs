﻿using Monet.src.history;
using Monet.src.ui;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet.src.shape
{
    public sealed class Ellipse : Shape,Rotatable,Resizeable
    {

        public AdjustButton adjustButton;
        public MoveButton moveButton;

        public Rectangle rect;

        public double angle = 0.0F;
        public Point midPoint;
        public override object Clone()
        {
            Ellipse ellipse = new Ellipse();
            ellipse.rect = this.rect;
            ellipse.pen = this.pen.Clone() as Pen;
            ellipse.midPoint = this.midPoint;
            ellipse.angle = this.angle;
            ellipse.backColor = this.backColor;
            return ellipse;
        }

        public override bool IsSelectMe(Point point)
        {
            point = Common.RotatingPoint(point, midPoint, -angle);

            bool isVertical = rect.Width < rect.Height;
            double ellipseA = Math.Max(rect.Width / 2.0, rect.Height / 2.0);
            double ellipseB = Math.Min(rect.Width / 2.0, rect.Height / 2.0);
            double ellipseC = Math.Sqrt(ellipseA * ellipseA - ellipseB * ellipseB);
            Point focusA, focusB;//焦点位置
            if (isVertical)
            {
                focusA = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2 + (int)ellipseC);
                focusB = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2 - (int)ellipseC);
            }
            else
            {
                focusA = new Point(rect.X + rect.Width / 2 + (int)ellipseC, rect.Y + rect.Height / 2 );
                focusB = new Point(rect.X + rect.Width / 2 - (int)ellipseC, rect.Y + rect.Height / 2 );
            }
            return Math.Abs(DisOfPoint2Point(point, focusA) + DisOfPoint2Point(point, focusB)-2*ellipseA)<5;
        }

        static double DisOfPoint2Point(Point a,Point b)
        {
            return Math.Sqrt(
                Math.Pow(a.X-b.X,2)+ Math.Pow(a.Y - b.Y, 2)
                );
        }

        public override void ShowAsNotSelected()
        {
            base.ShowAsNotSelected();
            try
            {
                adjustButton.Visible = moveButton.Visible = false;
                adjustButton.Dispose();
                moveButton.Dispose();
            }
            catch (NullReferenceException)
            {
                ;
            }
            finally
            {
                adjustButton = null;
                moveButton = null;
            }
        }

        public override void ShowAsSelected()
        {
            base.ShowAsSelected();
            if (adjustButton == null)
            {
                adjustButton = new AdjustButton(MainWin.GetInstance().MainView(), this, 
                    Common.RotatingPoint(new Point(rect.Right,rect.Top),midPoint,angle), 
                    Cursors.SizeNS);
                adjustButton.MouseDown += AdjustButton_MouseDown;
                adjustButton.MouseUp += AdjustButton_MouseUp;
                adjustButton.MouseMove += AdjustButton_MouseMove;
            }
            if (moveButton == null)
            {
                moveButton = new MoveButton(MainWin.GetInstance().MainView(),
                    this, 
                    Common.RotatingPoint(new Point(rect.Left +rect.Width / 2, rect.Top+rect.Height/2),midPoint,angle),
                    Cursors.SizeAll);
                moveButton.MouseDown += MoveButton_MouseDown;
                moveButton.MouseMove += MoveButton_MouseMove;
                moveButton.MouseUp += MoveButton_MouseUp;
                moveButton.SetBindingPoints(
                    new Ref<Point>(()=>rect.Location,z=> {rect.Location= Common.RotatingPoint(z,midPoint,-angle); }     ),
                    new Ref<Point>(() => adjustButton.Location, z => { adjustButton.Location = z; })
                    );
            }
            Log.LogText(string.Format("Select Ellipse ({0},{1}),({2},{3})", rect.Left, rect.Top, rect.Right, rect.Bottom));
        }

        private void MoveButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (isMoving)
            {
                isMoving = false;
                RetMAction().Action();
                Log.LogText(string.Format("Move Ellipse ({0},{1}),({2},{3})", rect.Left, rect.Top, rect.Right, rect.Bottom));
                ShowAsNotSelected();
            }         
        }

        private void MoveButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMoving)
            {
                moveButton.Location = Common.RotatingPoint(
                    new Point(rect.X+rect.Width/2,rect.Y+rect.Height/2),midPoint,angle
                    );
                RetMAction().Action();
            }
        }

        private void MoveButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMoving = true;
                adjustButton.Visible = false;
                MAction mAction;
                History his = History.GetInstance();
                his.FindShapeInHistory(this, out mAction);
                his.AddBackUpClone(mAction);
            }
        }

        bool isAdjusting=false;

        private void AdjustButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isAdjusting = true;
                MAction mAction;
                History his = History.GetInstance();
                his.FindShapeInHistory(this, out mAction);
                his.AddBackUpClone(mAction);
            }
        }

        private void AdjustButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (isAdjusting)
            {
                isAdjusting = false;
                RetMAction().Action();
                Log.LogText(string.Format("Adjust Ellipse ({0},{1}),({2},{3})",rect.Left,rect.Top,rect.Right,rect.Bottom));
                ShowAsNotSelected();
            }
        }
        private void AdjustButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (isAdjusting)
            {
                rect = Common.Rectangle(new Point(rect.Left, rect.Bottom), Common.RotatingPoint(adjustButton.Location,midPoint,-angle));
                moveButton.Location = Common.RotatingPoint(
                    new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2),
                    midPoint,angle
                    );
                RetMAction().Action();      
            }
        }

        public void Rotate(Point midPoint, double angle)
        {
            this.angle = angle;
            this.midPoint = midPoint;
            this.ShowAsNotSelected();
        }

        public void ShowAsResizing()
        {
            ShowAsSelected();            
        }
        

        public void ShowAsNotResizing()
        {
            ShowAsNotSelected();            
        }
    }
}
