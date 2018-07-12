using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            // TODO This does not work for the inherited PropertyChanged.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Set the changed value to bindable property with call back action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bindable"></param>
        /// <param name="action"></param>
        public static void BindablePropertyChanged<T>(BindableObject bindable, Action<T> action) where T : ContentView
        {
            var control = bindable as T;
            if (control == null)
                return;
            action(control);
        } 
    }   

}
