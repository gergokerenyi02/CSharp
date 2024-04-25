using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVA_ZH_Console.Model
{
    public class GameField
    {
        #region Fields
        private bool _isBlack;

        // Egy mező lehet radar által érzékelt
        private bool _isVisible;
        private bool _isTarget;

        // Egy mező ha megsemmisül
        private bool _isDestroyed;

        private int _row;
        private int _col;
        #endregion

        #region Getters/Setters
        public int row
        {
            get
            {
                return _row;
            }
            set
            {
                _row = value;
            }
        }
        public int col
        {
            get
            {
                return _col;
            }
            set
            {
                _col = value;
            }
        }
        public bool isBlack
        {
            get
            {
                return _isBlack;
            }
            set
            {
                _isBlack = value;
            }
        }

        public bool isVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;
            }
        }

        public bool isTarget
        {
            get
            {
                return _isTarget;
            }
            set
            {
                _isTarget = value;
            }
        }

        public bool isDestroyed
        {
            get
            {
                return _isDestroyed;
            }
            set
            {
                _isDestroyed = value;
            }
        }
        #endregion

        public GameField(int row, int col)
        {
            this.row = row;
            this.col = col;

            //isVisible = true;
        }
    }
}
