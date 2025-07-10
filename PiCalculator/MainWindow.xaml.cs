using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using IOCDependencyInjection;
using PiCalculator.Contract;
using PiCalculator.Presenter;
using PiCalculator.ViewModel;
using Timer = System.Threading.Timer;

namespace PiCalculator
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window, IPiCalcView
    {
        private IPiCalcPresenter _presenter;
        public IPiCalcPresenter Presenter { get => _presenter; set => _presenter = value; }
        public PiViewModel viewModel { get; set; } = new PiViewModel();
        private List<long> sizes = new List<long>();
        private Timer timer;
        public MainWindow(IMVPFactory factory)
        {
            InitializeComponent();
            sampleSizeTxt.Width = 50;
            DataContext = viewModel;

           _presenter = factory.Create<IPiCalcView, IPiCalcPresenter>(this);

            timer = new Timer(TimerCallback, null, 0, 1000);
        }

        private void TimerCallback(object state)
        {




            Debug.WriteLine("Hello");
        }

        private  void CalculatorPiClick(object sender, RoutedEventArgs e)
        {
            if (!long.TryParse(sampleSizeTxt.Text, out long size))
            {
                sampleSizeTxt.Text = string.Empty;
                return;
            }
            sampleSizeTxt.Text = string.Empty;
            if (sizes.Contains(size))
            {
                return;
            }

            this.Presenter.Calculate(size);



            //sizes.Add(size);
            //var model = new PiMissionModel { Time = ".", SampleSize = size };
            //collections.Add(model);
            //await Task.Run(() =>
            //{


            //    var mission = new PiMission(size);

            //    var value = mission.Calculate();
            //    var time = DateTime.Now.ToString("HH:mm:ss");
            //    Debug.WriteLine("完成");
            //    this.Dispatcher.Invoke(() =>
            //    {

            //        var r = collections.FirstOrDefault(x => x.SampleSize == size);

            //        r.Value = value;
            //        r.Time = time;
            //    });

            //});
        }
        public void CalculateFinish(PiMissionModel model)
        {
            this.Dispatcher.Invoke(() =>
            {
                viewModel.Add(model);
            });
           
        }
    }

}

