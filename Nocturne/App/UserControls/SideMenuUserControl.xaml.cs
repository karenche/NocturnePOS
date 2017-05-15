using Nocturne.App.ViewModels;
using Windows.System;
using Windows.UI.Xaml.Controls;

namespace Nocturne.App.UserControls
{
    public sealed partial class SideMenuUserControl : UserControl
    {
        public PageViewModelBase CurrentViewModel { get; set; }

        public SideMenuUserControl()
        {
            this.InitializeComponent();
        }

        private void btnInstallDirectory_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
#pragma warning disable 4014
            Launcher.LaunchFolderAsync(Windows.Storage.ApplicationData.Current.LocalFolder, new FolderLauncherOptions
            {
                DesiredRemainingView = Windows.UI.ViewManagement.ViewSizePreference.UseMinimum
            });
#pragma warning restore 4014
        }
    }
}
