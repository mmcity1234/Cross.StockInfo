using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Cross.StockInfo.Views
{
    public class UserControlView : ContentView, INotifyPropertyChanged
    {
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            // Set the inner BindingContext of the controls to this, while the outer BindingContext of this is set externally.
            Content.BindingContext = this;
        }

        public new event PropertyChangedEventHandler PropertyChanged;

        // This is needed  for intermediate value changes.
        // An initial binding usually works without, even without being a BindableProperty.
        // TODO This seems superfluous for a BindableProperty.
        protected void RaisePropertyChanged(string propertyName)
        {
            // TODO This does not work for the inherited PropertyChanged.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }   
}
