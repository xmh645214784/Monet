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
        static Setting mInstance;

        public Pen Pen { get => pen; set => pen = value; }

        private Setting()
        {
            Pen=new Pen(Color.FromName("black"));
            // set the default implementing method.
            LineImplementMethod = LineImplementMethod.LINE_SYSTEM;
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
                LineTool lt = (LineTool)(ToolKit.GetInstance().LineTool);
                lt.ChangeImplementMethod(value);
            }

        }
    }
}
