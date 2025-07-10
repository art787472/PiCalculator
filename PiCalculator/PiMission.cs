using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiCalculator
{
    public class PiMission
    {
        public readonly long sampleSize;

        public PiMissionModel Calculate()
        {
            var stopwatch = Stopwatch.StartNew();
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            long sum = 0;
            for (long i = 0; i < sampleSize; i++)
            {
                double x = rnd.NextDouble();
                double y = rnd.NextDouble();
                if ((x * x + y * y) < 1)
                {
                    sum++;
                }
                
            }
            var res = 4 * sum / (double)sampleSize;
            stopwatch.Stop();
            var ms = stopwatch.Elapsed.TotalMilliseconds;
            Debug.WriteLine($"{sampleSize}: {ms}ms");

            PiMissionModel model = new PiMissionModel();
            //model.Time = ms.ToString();
            model.Time = DateTime.Now.ToString("HH:mm:ss");
            model.Value = res;
            model.SampleSize = sampleSize;
            return model;
        }
        public async Task<PiMissionModel> CalculateAsync()
        {
            
            var stopwatch = Stopwatch.StartNew();
            long sum = 0;
            object key = new object();
            int batchAmount = 5;
            long batchSize = sampleSize / batchAmount;
            

            List<Task> tasks = new List<Task>();

            for (int i = 0; i < batchAmount; i++)
            {
                var idx = i;
                Task task = new Task(() =>
                {
                    Debug.WriteLine($"任務{idx + 1}開始");
                    Random rnd = new Random(Guid.NewGuid().GetHashCode());
                    long s = 0;
                    for (long j = idx * batchSize; j < idx * batchSize + batchSize; j++)
                    {
                        double x = rnd.NextDouble();
                        double y = rnd.NextDouble();
                        if ((x * x + y * y) < 1)
                        {
                            s++;
                        }

                    }
                    lock(key)
                    {
                        sum+=s;

                    }

                    Debug.WriteLine($"任務{idx + 1}結束");
                });
                tasks.Add(task);
                task.Start();
            }
            await Task.WhenAll(tasks);


            var res = 4 * sum / (double)sampleSize;
            stopwatch.Stop();
            var ms = stopwatch.Elapsed.TotalMilliseconds;
            Debug.WriteLine($"{sampleSize}: {ms}ms");

            PiMissionModel model = new PiMissionModel();
            //model.Time = ms.ToString();
            model.Time = DateTime.Now.ToString("HH:mm:ss");
            model.Value = res;
            model.SampleSize = sampleSize;
            return model;
        }

        public PiMission(long sampleSize)
        {
            this.sampleSize = sampleSize;
        }
    }
}
