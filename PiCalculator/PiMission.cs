using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PiCalculator
{
    public class PiMission
    {
        public readonly PiMissionModel model = null;


        public PiMission(PiMissionModel model)
        {
            this.model = model;
        }

        public PiMissionModel Calculate()
        {
            var stopwatch = Stopwatch.StartNew();
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            long sum = 0;
            for (long i = 0; i < model.SampleSize; i++)
            {
                double x = rnd.NextDouble();
                double y = rnd.NextDouble();
                if ((x * x + y * y) < 1)
                {
                    sum++;
                }

            }
            var res = 4 * sum / (double)model.SampleSize;
            stopwatch.Stop();
            var ms = stopwatch.Elapsed.TotalMilliseconds;
            Debug.WriteLine($"{model.SampleSize}: {ms}ms");


            return model;
        }
        public async Task<PiMissionModel> CalculateAsync()
        {
            model.Status = ViewModel.MissionStatus.Running;
            var stopwatch = Stopwatch.StartNew();
            long sum = 0;
            object key = new object();
            int batchAmount = 5;
            long batchSize = model.SampleSize / batchAmount;


            List<Task> tasks = new List<Task>();

            for (int i = 0; i < batchAmount; i++)
            {
                var idx = i;
                Task task = new Task(() =>
                {
                    
                    Random rnd = new Random(Guid.NewGuid().GetHashCode());
                    long s = 0;
                    for (long j = idx * batchSize; j < idx * batchSize + batchSize; j++)
                    {
                        if (model.TokenSource.IsCancellationRequested)
                        {
                            break;
                        }
                        double x = rnd.NextDouble();
                        double y = rnd.NextDouble();
                        if ((x * x + y * y) < 1)
                        {
                            s++;
                        }

                    }
                    lock (key)
                    {
                        sum += s;

                    }
                });
                tasks.Add(task);
                task.Start();
            }
            await Task.WhenAll(tasks);

            
            var res = 4 * sum / (double)model.SampleSize;
            stopwatch.Stop();
            var ms = stopwatch.Elapsed.TotalMilliseconds;
            Debug.WriteLine($"{model.SampleSize}: {ms}ms");

            //model.Time = ms.ToString();
            model.Time = DateTime.Now.ToString("HH:mm:ss");
            model.Value = res;
            model.Status = ViewModel.MissionStatus.Completed;

            if(model.TokenSource.IsCancellationRequested)
            {
                model.Value = 0f;
                model.Status = ViewModel.MissionStatus.Cancled;
            }
            return model;
        }
    }

}
