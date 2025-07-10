using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiCalculator.Contract;

namespace PiCalculator.Presenter
{
    public class PiCalcPresenter : IPiCalcPresenter
    {
        private IPiCalcView _view;
        public IPiCalcView View { get => _view; set => _view = value; }

        public PiCalcPresenter(IPiCalcView view)
        {
            _view = view;
        }

        public async void Calculate(long size)
        {
            PiMissionModel model = new PiMissionModel { Time = "...", SampleSize = size };
            await Task.Run( async () =>
            {
                
                var mission = new PiMission(size);

                model = await  mission.CalculateAsync();

                this._view.CalculateFinish(model);
            });
        }

        
    }
}
