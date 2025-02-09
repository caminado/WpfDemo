using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace WpfApp1
{
    public class AddSectionViewModel
    {
        public string SectionName { get; set; }
        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }

        public AddSectionViewModel(Action<bool?> closeAction)
        {
            OkCommand = new RelayCommand(() => closeAction(true)); // Pass true for OK
            CancelCommand = new RelayCommand(() => closeAction(false)); // Pass false for Cancel
        }
    }
}
