using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Microsoft.Botframework.Xamarin.Behaviors
{
    public class EnabledBehavior : Behavior<Label>
    {
        protected override void OnAttachedTo(Label bindable)
        {
            bindable.PropertyChanged += Bindable_PropertyChanged;
        }

        protected override void OnDetachingFrom(Label bindable)
        {
            bindable.PropertyChanged -= Bindable_PropertyChanged;
        }

        private void Bindable_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == VisualElement.IsEnabledProperty.PropertyName)
            {
                ((Label)sender).TextColor = ((Label)sender).IsEnabled ? Color.FromHex("#0E8387") : Color.Gray;
            }
        }
    }
}
