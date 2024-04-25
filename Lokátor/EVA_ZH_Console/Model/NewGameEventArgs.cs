using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVA_ZH_Console.Model
{
    public class NewGameEventArgs : EventArgs
    {
        #region Fields
        public int size;
        #endregion

        public NewGameEventArgs(int size)
        {
                this.size = size;
        }
    }
}
