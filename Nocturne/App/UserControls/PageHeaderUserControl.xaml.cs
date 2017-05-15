using Nocturne.App.ViewModels;
using Windows.UI.Xaml.Controls;

namespace Nocturne.App.UserControls
{
    public sealed partial class PageHeaderUserControl : UserControl
    {
        public PageViewModelBase CurrentViewModel { get; set; }

        public PageHeaderUserControl()
        {
            this.InitializeComponent();
        }
    }
}
