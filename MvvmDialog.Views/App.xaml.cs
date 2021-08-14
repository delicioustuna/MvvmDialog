using ReactiveUI;
using Splat;
using System.Windows;

namespace MvvmDialog.Views
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Regist ViewModels
            Locator.CurrentMutable.RegisterViewsForViewModels(typeof(MainWindow).Assembly);

            base.OnStartup(e);
        }
    }
}
