using Microsoft.Practices.ServiceLocation;
using Nocturne.App.ViewModels;
using Nocturne.BL.Interfaces;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Nocturne.App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly DispatcherTimer _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMinutes(1)
        };
        private readonly ISessionService _sessionService;

        public MainViewModel CurrentViewModel { get; private set; }

        public MainPage()
        {
            InitializeComponent();
            _timer.Tick += timer_Tick;
            _timer.Start();
            _sessionService = ServiceLocator.Current.GetInstance<ISessionService>();
            UpdateTime();
            UpdateClientInsideCount();
        }

        private void UpdateTime()
        {
            tbTime.Text = DateTime.Now.ToString("HH:mm");
        }

        private void UpdateClientInsideCount()
        {
            var clientCount =  _sessionService.Find(s => !s.To.HasValue).Length;
            tbClientsInside.Text = string.Format("{0} client{1}", clientCount, clientCount != 1 ? "s" : "");
        }

        private void timer_Tick(object sender, object e)
        {
            UpdateTime();
            UpdateClientInsideCount();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = CurrentViewModel = ServiceLocator.Current.GetInstance<MainViewModel>();
            base.OnNavigatedTo(e);
        }
    }
}
