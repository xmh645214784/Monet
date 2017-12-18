using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet.src.ui
{
    public class ResizeButton : Button
    {
        PictureBox mainView;
        public ResizeButton(PictureBox mainView,
                            Point point,
                            Cursor cursor)
        {
            this.mainView = mainView;
            this.Location = point;
            this.Size = new Size(20, 20);
            mainView.Controls.Add(this);
            this.Show();
        }

        public void Disappear()
        {
            this.Enabled = false;
            mainView.Controls.Remove(this);
        }
    }
}
