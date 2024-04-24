using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VadászatWPF.ViewModel
{
    public class VadaszatField : ViewModelBase
    {
        private String _character = String.Empty;


        public int X {  get; set; }

        public int Y { get; set; }

        public Tuple<int, int> XY
        {
            get { return new(X, Y); }
        }

        public String Character
        {
            get { return _character; }

            set
            {
                if(_character != value)
                {
                    _character = value;
                    OnPropertyChanged();
                }
            }
        }

        public DelegateCommand? StepCommand { get; set; }
    }
}
