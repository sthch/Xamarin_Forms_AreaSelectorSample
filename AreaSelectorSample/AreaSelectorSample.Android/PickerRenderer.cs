using Android.Content;
using Android.Widget;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using PickerRenderer = AreaSelectorSample.Droid.PickerRenderer;

[assembly: ExportRenderer(typeof(Picker), typeof(PickerRenderer))]
namespace AreaSelectorSample.Droid
{
    public class PickerRenderer : ViewRenderer<Picker, NumberPicker>
    {
        public PickerRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                SetNativeControl(new NumberPicker(Context));
            }
            else
            {
                Control.ValueChanged -= Control_ValueChanged;
            }

            if (e.NewElement != null)
            {
                Control.ValueChanged += Control_ValueChanged;

                UpdateItemsSource();
                UpdateSelectedIndex();
            }
            Control.WrapSelectorWheel = false;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == Picker.ItemsSourceProperty.PropertyName)
            {
                UpdateItemsSource();
            }
            else if (e.PropertyName == Picker.SelectedIndexProperty.PropertyName)
            {
                UpdateSelectedIndex();
            }
        }

        private void UpdateItemsSource()
        {
            IList<string> arr = new List<string>();
            if (Element.ItemsSource?.Count > 0)
            {
                foreach (object item in Element.ItemsSource)
                {
                    arr.Add(item.ToString());
                }
            }
            if (arr.Count > 0)
            {
                int newMax = arr.Count - 1;
                if (newMax < Control.Value)
                {
                    Element.SelectedIndex = newMax;
                }
                Control.MaxValue = newMax;
                Control.SetDisplayedValues(arr.ToArray());
            }
            else
            {
                Control.MaxValue = 0;
                Control.SetDisplayedValues(null);
            }
        }

        private void UpdateSelectedIndex()
        {
            if (Element.SelectedIndex >= Control.MinValue && Element.SelectedIndex < Control.MaxValue)
            {
                Control.Value = Element.SelectedIndex;
            }
        }

        private void Control_ValueChanged(object sender, NumberPicker.ValueChangeEventArgs e)
        {
            Element.SelectedIndex = e.NewVal;
        }
    }
}