using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVA_ZH_WPF.ViewModel
{
    public class ButtonClickedEventArgs : EventArgs
    {
        public int row;
        public int col;
        public ButtonClickedEventArgs(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
    }
}
