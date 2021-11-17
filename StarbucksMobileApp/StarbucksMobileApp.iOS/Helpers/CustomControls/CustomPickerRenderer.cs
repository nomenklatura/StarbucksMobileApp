using CoreAnimation;
using CoreGraphics;
using StarbucksMobileApp.Helpers.CustomControls;
using StarbucksMobileApp.iOS.Helpers.CustomControls;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace StarbucksMobileApp.iOS.Helpers.CustomControls
{
    public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var view = (CustomPicker)Element;
                switch (view.BorderType)
                {
                    case EEntryType.None:
                        BorderPicker(e, true);
                        break;
                    case EEntryType.Border:
                        BorderPicker(e, false);
                        break;
                    case EEntryType.Line:
                        LinePicker(e);
                        break;
                    default:
                        BorderPicker(e, false);
                        break;
                }
            }
        }

        void BorderPicker(ElementChangedEventArgs<Picker> e, bool None = false)
        {
            if (e.NewElement != null)
            {
                var view = (CustomPicker)Element;

                Control.BorderStyle = UITextBorderStyle.None;

                Control.LeftView = new UIView(new CGRect(0f, 0f, 9f, 20f));
                Control.LeftViewMode = UITextFieldViewMode.Always;

                Control.KeyboardAppearance = UIKeyboardAppearance.Dark;
                Control.ReturnKeyType = UIReturnKeyType.Done;

                Control.Layer.CornerRadius = Convert.ToSingle(view.CornerRadius);
                Control.Layer.BorderColor = view.BorderColor.ToCGColor();
                Control.Layer.BorderWidth = view.BorderWidth;
                Control.ClipsToBounds = true;
            }
        }

        void LinePicker(ElementChangedEventArgs<Picker> e)
        {
            if (Control == null) return;
            var view = (CustomPicker)Element;

            if (view != null)
            {
                UITextField textField = Control;
                textField.BorderStyle = UITextBorderStyle.None;
                textField.TintColor = view.BorderColor.ToUIColor();

                CALayer BottomLine = new CALayer
                {
                    BorderColor = view.BorderColor.ToCGColor(),
                    BackgroundColor = view.BorderColor.ToCGColor(),
                    Frame = new CGRect(0, Frame.Height + 5, Frame.Width, 1f)
                };
                Control.Layer.AddSublayer(BottomLine);
            }
        }
    }
}