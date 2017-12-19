using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet.src.ui
{
    class Log
    {
        static RichTextBox richTextBox=MainWin.GetInstance().richTextBox;
        static public void LogText(String str)
        {
            richTextBox.AppendText(str);
            richTextBox.AppendText(Environment.NewLine);
            richTextBox.ScrollToCaret();
        }
        
    }
}
