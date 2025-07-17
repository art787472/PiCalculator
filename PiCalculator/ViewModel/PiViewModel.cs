using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Converters;

namespace PiCalculator.ViewModel
{
    public class PiViewModel : INotifyPropertyChanged
    {
        private MissionStatus status = MissionStatus.Running;
        public MissionStatus Status 
        {
            get => status; 
            set
            {
                if (status != value)
                {
                    status = value;
                    OnPropertyChanged(nameof(Status));
                    OnPropertyChanged(nameof(BtnVisibility));

                }

            }
        
        }
        public ObservableCollection<PiMissionModel> collections { get; set; } = new ObservableCollection<PiMissionModel>();
        public bool isStart = true;
        public string BtnVisibility => Status == MissionStatus.Completed ? Visibility.Collapsed.ToString() : Visibility.Visible.ToString();

        public bool IsStart
        {
            get => isStart;
            set
            {
                
                if (isStart != value)
                {
                    isStart = value;
                    OnPropertyChanged(nameof(IsStart));
                    OnPropertyChanged(nameof(buttonText));
                    
                }
            }
        }
        public string buttonText => IsStart ? "暫停" : "開始";
        public void Add(PiMissionModel model)
        {
            //var item = collections.FirstOrDefault(x => x.SampleSize == model.SampleSize);
            //if (item != null)
            //{
            //    item.Time = model.Time;
            //    item.Value = model.Value;
            //}
            //else
            //{
            
                collections.Add(model);
            //}
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
