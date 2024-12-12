using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GraphEditor.ViewModels;

public class BaseVM : INotifyPropertyChanged
{
    public BaseVM()
    {
    }

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
    public event PropertyChangedEventHandler? PropertyChanged;
}