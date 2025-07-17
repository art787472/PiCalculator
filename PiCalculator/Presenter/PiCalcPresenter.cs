using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using PiCalculator.Contract;

namespace PiCalculator.Presenter
{
    public class PiCalcPresenter : IPiCalcPresenter
    {
        private IPiCalcView _view;
        public IPiCalcView View { get => _view; set => _view = value; }
        private ConcurrentQueue<PiMissionModel> tasks = new ConcurrentQueue<PiMissionModel>();
        private ConcurrentDictionary<long, PiMissionModel> cdict = new ConcurrentDictionary<long, PiMissionModel>();
        private ConcurrentBag<PiMissionModel> _cache = new ConcurrentBag<PiMissionModel>();
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        public PiCalcPresenter(IPiCalcView view)
        {
            _view = view;
        }

  
        public void StartMission()
        {
            
            //HW: 思考為甚麼會有這個現象? 不是無窮迴圈嗎? 為甚麼進不來?
            Task.Run(async () => {
                while(true)
                {
                    if (cancellationTokenSource.Token.IsCancellationRequested)
                        break;

                    if(tasks.Count() > 0)
                    {
                        tasks.TryDequeue(out PiMissionModel model);
                        
                        // 先擱置，之後回來討論為甚麼要有await & 為甚麼要有執行續?
                        await Task.Run(async () =>
                        {
                            model.Status = ViewModel.MissionStatus.Running;
                            var mission = new PiMission(model);
                            await mission.CalculateAsync();
                        });
                    }

                }
            });
        }

        public PiMissionModel SendMissionRequest(long sampleSize)
        {
            if(cdict.TryGetValue(sampleSize, out PiMissionModel model) && model.IsCancelled)
            {
                return null;
            }
        
            model = new PiMissionModel() { SampleSize = sampleSize };
            cdict[sampleSize] = model;
            tasks.Enqueue(model);
            
            return model;   
            


        }

        public void FetchCompletedMission()
        {
            List<PiMissionModel> completedMissions = _cache.ToList();
            _cache = new ConcurrentBag<PiMissionModel>();
  
            this._view.UpdateDataView(completedMissions);
        }

        public void StopMission()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource = new CancellationTokenSource();
            

        }

    }
}
