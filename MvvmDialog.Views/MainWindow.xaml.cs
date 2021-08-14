using Microsoft.Win32;
using MvvmDialog.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Threading.Tasks;

namespace MvvmDialog.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ReactiveWindow<MainViewModel>
    {

        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new MainViewModel();

            this.WhenActivated(d =>
            {
                HandleViewModelBound(d);
            });
        }

        protected void HandleViewModelBound(CompositeDisposable d)
        {
            this.BindInteraction(
                ViewModel,
                vm => vm.SaveFileDialog,
                async interaction =>
                {
                    var result = await Task.Run(() =>
                    {
                        var dialog = new SaveFileDialog()
                        {
                            FileName = interaction.Input,
                            AddExtension = true,
                            DefaultExt = "txt"
                        };

                        if(dialog.ShowDialog()?? false)
                        {
                            return dialog.FileName;
                        }
                        else
                        {
                            return null;
                        }
                    });

                    interaction.SetOutput(result);
                })
                .DisposeWith(d);

            this.OneWayBind(ViewModel, vm => vm.ResultMessage, v => v.ResultTextBlock.Text).DisposeWith(d);

            this.Bind(ViewModel, vm => vm.FileName, v => v.FileNameTextBox.Text).DisposeWith(d);

            this.Bind(ViewModel, vm => vm.InputText, v => v.InputTextBox.Text).DisposeWith(d);

            this.BindCommand(ViewModel, vm => vm.SaveFileCommand, v => v.SaveFileButton).DisposeWith(d);
        }
    }
}
