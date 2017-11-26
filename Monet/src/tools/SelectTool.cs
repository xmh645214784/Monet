﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet
{
    sealed class SelectTool : Tool
    {
        Point startPoint;
        Point endPoint;
        Rectangle selectRect;

        bool isNewARect;

        Pen solidPen;
        bool isExistOneRect;
        bool isMove;

        public SelectTool(PictureBox mainView, Button button) : base(mainView, button)
        {
            isNewARect = false;
            isExistOneRect = false;
            isMove = false;

            solidPen = new Pen(Color.Gray, 2);
            solidPen.DashStyle = DashStyle.Custom;
            solidPen.DashPattern = new float[] { 1f, 1f };
        }
        public override void RegisterTool()
        {
            base.RegisterTool();
            mainView.Cursor = Cursors.Cross;
            mainView.MouseDown += MainView_MouseDown;
            mainView.MouseMove += MainView_MouseMove;
            mainView.MouseUp += MainView_MouseUp;
            mainView.PreviewKeyDown += MainView_PreviewKeyDown;
        }

        public override void UnRegisterTool()
        {
            base.UnRegisterTool();
            mainView.Cursor = Cursors.Default;
            mainView.MouseDown -= MainView_MouseDown;
            mainView.MouseMove -= MainView_MouseMove;
            mainView.MouseUp -= MainView_MouseUp;
            mainView.PreviewKeyDown -= MainView_PreviewKeyDown;
        }

        private void MainView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MainView_MouseMove(object sender, MouseEventArgs e)
        {
            //When there is a rect in pictureBox. 
            // We should set the cursor when mouse move over it.
            // Otherwise, cross.
            if(isExistOneRect&& selectRect.Contains(e.Location))
            {
                mainView.Cursor = Cursors.SizeAll;
            }
            else
            {
                mainView.Cursor = Cursors.Cross;
            }
            if (isNewARect||isMove)
            {
                //take out the last but two valid image, take clone of it ,push it into stack.
                History.GetInstance().UndoAction();
                Image lastValidClone = (Image)History.GetInstance().TopAction().Clone();
                History.GetInstance().PushBackAction(lastValidClone);
                mainView.Image = lastValidClone;

                //either new a select rect or move an exist rect
                System.Diagnostics.Debug.Assert((isNewARect && isMove) == false);
                if(isNewARect)
                {
                    //draw a new one
                    using (Graphics g = Graphics.FromImage(mainView.Image))
                    {
                        endPoint = e.Location;
                        g.DrawRectangle(solidPen, Common.Rectangle(startPoint, endPoint));
                        mainView.Invalidate();
                    }
                }
                else if(isMove)
                {
                    throw new NotImplementedException();
                }
            }
        }
        private void MainView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startPoint = e.Location;
                
                if (isExistOneRect)
                {
                    if(selectRect.Contains(e.Location))//move select part
                    {
                        isMove = true;
                        Image newClone = (Image)mainView.Image.Clone();
                        History.GetInstance().PushBackAction(newClone);
                        mainView.Image = newClone;
                        using (Graphics g = Graphics.FromImage(mainView.Image))
                        {
                            ;
                        }
                    }
                    else //cancel the previous rect which user has selected before.And select a new one
                    {
                        //take out the last but two valid image, take clone of it ,push it into stack.
                        History.GetInstance().UndoAction();
                        Image lastValidClone = (Image)History.GetInstance().TopAction().Clone();
                        History.GetInstance().PushBackAction(lastValidClone);
                        mainView.Image = lastValidClone;
                        isNewARect = true;
                    }
                }
                else//new a select rect
                {
                    isNewARect = true;
                    Image newClone = (Image)mainView.Image.Clone();
                    History.GetInstance().PushBackAction(newClone);
                    mainView.Image = newClone;
                }
            }
        }

        private void MainView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if(isMove)//move an exist rect
                {
                    System.Diagnostics.Debug.Assert(isExistOneRect);
                    selectRect.Offset(endPoint.X-startPoint.X,endPoint.Y-startPoint.Y);
                    isMove = false;
                }
                else// draw a new rect or cancel an exist rect
                { 
                    isNewARect = false;
                    if (startPoint == e.Location)//cancel an exist rect
                        isExistOneRect = false;
                    else//draw a new rect
                    {
                        isExistOneRect = true;
                        selectRect = Common.Rectangle(startPoint, endPoint);
                    }
                }
            }
        }
    }
}