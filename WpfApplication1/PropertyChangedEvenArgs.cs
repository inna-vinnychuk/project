using System.ComponentModel;

namespace WpfApplication1
{
    internal class PropertyChangedEvenArgs : PropertyChangedEventArgs
    {
        public PropertyChangedEvenArgs(string propertyName) : base(propertyName)
        {
        }
    }
}