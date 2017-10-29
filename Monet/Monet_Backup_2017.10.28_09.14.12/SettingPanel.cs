using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monet
{
    class SettingPanel
    {
        Pen pen;
        static SettingPanel mInstance;

        public Pen Pen { get => pen; set => pen = value; }

        private SettingPanel()
        {
            Pen=new Pen(Color.FromName("red"));
        }
        public static SettingPanel GetInstance()
        {
            if (mInstance == null)
            {
                mInstance = new SettingPanel();
            }
            return mInstance;
        }
    }
}
