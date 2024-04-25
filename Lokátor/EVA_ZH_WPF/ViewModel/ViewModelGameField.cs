using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EVA_ZH_WPF.ViewModel
{
    public class ViewModelGameField : ViewModelBase
    {
        #region Fields
        private int _row;
        private int _col;

        private int _buttonSize;

        public DelegateCommand? ButtonClickedCommand { get; private set; }

        private SolidColorBrush _color = null!;
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

        public int buttonSize
        {

            get
            {
                return _buttonSize;
            }
            set
            {
                if (value != _buttonSize)
                {
                    _buttonSize = value;
                    OnPropertyChanged(nameof(buttonSize));
                }
            }
        }

        public SolidColorBrush color
        {
            get { return _color; }
            set
            {
                if (value != _color)
                {
                    _color = value;
                    OnPropertyChanged(nameof(color));
                }
            }
        }
        #endregion

        public ViewModelGameField(int row, int col, int buttonSize)
        {
            this.row = row;
            this.col = col;
            this.buttonSize = buttonSize;
            color = new SolidColorBrush(Colors.White);
            ButtonClickedCommand = new DelegateCommand(param => OnButtonClicked());
        }

        public event EventHandler<ButtonClickedEventArgs>? ButtonClicked;

        private void OnButtonClicked()
        {
            ButtonClicked?.Invoke(this, new ButtonClickedEventArgs(row, col));
        }
    }
}
