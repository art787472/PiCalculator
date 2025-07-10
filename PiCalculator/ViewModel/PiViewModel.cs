using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiCalculator.ViewModel
{
    public class PiViewModel
    {
        public ObservableCollection<PiMissionModel> collections { get; set; } = new ObservableCollection<PiMissionModel>();

        public void Add(PiMissionModel model)
        {
            collections.Add(model);
        }
    }
}
