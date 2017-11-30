using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet
{
    static class Program
    {
        ///-------------------------------------------------------------------------------------------------
        /// \fn static void Main()
        ///
        /// \brief 应用程序的主入口点。
        ///-------------------------------------------------------------------------------------------------

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(MainWin.GetInstance());
        }
    }
}
