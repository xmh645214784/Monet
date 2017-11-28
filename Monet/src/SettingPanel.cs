using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monet
{
    class Setting
    {
        private LineImplementMethod lineImplementMethod;

        Pen pen;
        Color frontColor;
        Color backgroundColor;
        static Setting mInstance;

        public Pen Pen { get => pen; set => pen = value; }

        private Setting()
        {
            Pen=new Pen(Color.FromName("black"));
            FrontColor = Color.FromName("black");
            BackgroundColor = Color.FromName("white");
            // set the default implementing method.
            LineImplementMethod = LineImplementMethod.LINE_DDA;
        }
        public static Setting GetInstance()
        {
            if (mInstance == null)
            {
                mInstance = new Setting();
            }
            return mInstance;
        }
        public void SettingPenColor(Color color)
        {
            pen.Color = color;
        }

        public void SettingPenWidth(int width)
        {
            pen.Width = width;
        }

        public LineImplementMethod LineImplementMethod
        {
            get => lineImplementMethod;
            set
            {
                lineImplementMethod = value;
                LineTool lt = (LineTool)(ToolKit.GetInstance().lineTool);
                lt.ChangeImplementMethod(value);
            }

        }

        public Color FrontColor { get => frontColor; set => frontColor = value; }
        public Color BackgroundColor { get => backgroundColor; set => backgroundColor = value; }
    }
}
