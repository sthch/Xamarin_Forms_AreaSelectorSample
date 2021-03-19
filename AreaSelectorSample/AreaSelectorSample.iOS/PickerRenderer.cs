using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Color = Xamarin.Forms.Color;
using PickerRenderer = AreaSelectorSample.iOS.PickerRenderer;

[assembly: ExportRenderer(typeof(Picker), typeof(PickerRenderer))]
namespace AreaSelectorSample.iOS
{
    public class PickerRenderer : ViewRenderer<Picker, UIPickerView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                SetNativeControl(new UIPickerView(RectangleF.Empty));
            }
            if (e.NewElement != null)
            {
                UpdateItemsSource();
                UpdateSelectedIndex();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == Picker.ItemsSourceProperty.PropertyName || e.PropertyName == Picker.TextColorProperty.PropertyName)
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
            Control.Model = new MyDataModel(Element.ItemsSource, row => Element.SelectedIndex = row, Element.TextColor.ToUIColor());
        }

        private void UpdateSelectedIndex()
        {
            if (Control.Model == null)
            {
                return;
            }
            if (Element.SelectedIndex >= 0 && Element.SelectedIndex < Control.Model.GetRowsInComponent(Control, 0))
            {
                Control.Select(Element.SelectedIndex, 0, true);
            }
        }
    }

    internal class MyDataModel : UIPickerViewModel
    {
        private readonly IList<string> _list = new List<string>();

        private readonly Action<int> _selectedHandler;

        public UIColor TextColor { get; set; }

        public MyDataModel(IEnumerable items, Action<int> selectedHandler, UIColor textColor)
        {
            _selectedHandler = selectedHandler;

            if (items != null)
            {
                foreach (object item in items)
                {
                    _list.Add(item.ToString());
                }
            }
            if (!textColor.Equals(Color.Default.ToUIColor()))
            {
                TextColor = textColor;
            }
        }

        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return _list.Count;
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return _list[(int)row];
        }

        public override UIView GetView(UIPickerView pickerView, nint row, nint component, UIView view)
        {
            UILabel label = new UILabel(pickerView.Bounds)
            {
                Text = _list[(int)row],
                TextAlignment = UITextAlignment.Center
            };
            if (TextColor != null)
            {
                label.TextColor = TextColor;
            }
            return label;
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            _selectedHandler?.Invoke((int)row);
        }
    }
}