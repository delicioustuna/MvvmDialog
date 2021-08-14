using MvvmDialog.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace MvvmDialog.ViewModels
{
    public class MainViewModel : ReactiveObject, IActivatableViewModel
    {
        [Reactive]
        public string InputText { get; set; }

        [Reactive]
        public string FileName { get; set; }

        [Reactive]
        public string ResultMessage { get; set; }

        public ReactiveCommand<Unit, SaveTextRequest> SaveFileCommand { get; private set; }

        public Interaction<string, string> SaveFileDialog { get; set; }

        public ViewModelActivator Activator { get; }


        public MainViewModel()
        {
            Activator = new ViewModelActivator();

            this.WhenActivated(d =>
            {
                HandleViewModelBound(d);
            });
        }


        void HandleViewModelBound(CompositeDisposable d)
        {
            SaveFileDialog = new Interaction<string, string>();

            SaveFileCommand = ReactiveCommand.CreateFromObservable(() => SaveFileDialog.Handle(FileName).Select(x => new SaveTextRequest() { FilePath = x, InputText = this.InputText }));

            SaveFileCommand.Select(x => new SaveText().Save(x)).Subscribe(x => ResultMessage = x.Message).DisposeWith(d);
        }
    }
}
