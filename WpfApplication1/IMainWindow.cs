using System.ComponentModel;

namespace WpfApplication1
{
    public interface IMainWindow
    {
        string PlayButtonName { get; }

        event PropertyChangedEventHandler PropertyChanged;

        void InitializeComponent();
    }
}