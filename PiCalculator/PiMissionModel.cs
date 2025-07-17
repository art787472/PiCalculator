using System.ComponentModel;
using System.Threading;
using System.Windows;
using PiCalculator.ViewModel;

namespace PiCalculator
{

    public class PiMissionModel : INotifyPropertyChanged
    {
        public bool IsCancelled => !TokenSource.IsCancellationRequested ;
        public Visibility BtnVisibility => Status == MissionStatus.Completed ? Visibility.Collapsed : Visibility.Visible;
        public CancellationTokenSource TokenSource { get; set; } = new CancellationTokenSource();
        private MissionStatus _status = MissionStatus.Running;
        public MissionStatus Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                    OnPropertyChanged(nameof(IsCancelled));
                    OnPropertyChanged(nameof(BtnVisibility));
                }
            }
        }
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

        private double _value = 0;
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
