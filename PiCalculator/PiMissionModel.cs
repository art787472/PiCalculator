using System.ComponentModel;

namespace PiCalculator
{

    public class PiMissionModel : INotifyPropertyChanged
    {
        private long _sampleSize;
        public long SampleSize
        {
            get => _sampleSize;
            set
            {
                if (_sampleSize != value)
                {
                    _sampleSize = value;
                    OnPropertyChanged(nameof(SampleSize));
                }
            }
        }

        private string _time;
        public string Time
        {
            get => _time;
            set
            {
                if (_time != value)
                {
                    _time = value;
                    OnPropertyChanged(nameof(Time));
                }
            }
        }

        private double _value;
        public double Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
