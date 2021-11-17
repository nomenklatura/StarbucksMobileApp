using CoreAnimation;
using CoreGraphics;
using StarbucksMobileApp.Helpers.CustomControls;
using StarbucksMobileApp.iOS.Helpers.CustomControls;
using System;
using System.Drawing;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace StarbucksMobileApp.iOS.Helpers.CustomControls
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var view = (CustomEntry)Element;
                switch (view.BorderType)
                {
                    case EEntryType.None:
                        BorderEntry(e, true);
                        break;
                    case EEntryType.Border:
                        BorderEntry(e, false);
                        break;
                    case EEntryType.Line:
                        LineEntry(e);
                        break;
                    default:
                        BorderEntry(e, false);
                        break;
                }
            }
        }

        void BorderEntry(ElementChangedEventArgs<Entry> e, bool None = false)
        {
            if (e.NewElement != null)
            {
                var view = (CustomEntry)Element;

                Control.BorderStyle = UITextBorderStyle.None;

                Control.LeftView = new UIView(new CGRect(0f, 0f, 9f, 20f));
                Control.LeftViewMode = UITextFieldViewMode.Always;

                Control.KeyboardAppearance = UIKeyboardAppearance.Dark;
                Control.ReturnKeyType = UIReturnKeyType.Done;

                Control.Layer.CornerRadius = Convert.ToSingle(view.CornerRadius);
                Control.Layer.BorderColor = view.BorderColor.ToCGColor();
                Control.Layer.BorderWidth = view.BorderWidth;
                Control.ClipsToBounds = true;

                //if (this.Element.Keyboard == Keyboard.Numeric)
                this.AddDoneButton();
            }
        }

        void LineEntry(ElementChangedEventArgs<Entry> e)
        {
            if (Control == null) return;
            var view = (CustomEntry)Element;

            if (view != null)
            {

                UITextField textField = Control;
                textField.BorderStyle = UITextBorderStyle.None;
                textField.TintColor = view.BorderColor.ToUIColor();
                CALayer BottomLine = new CALayer
                {
                    BorderColor = view.BorderColor.ToCGColor(),
                    BackgroundColor = view.BorderColor.ToCGColor(),
                    Frame = new CGRect(0, Frame.Height + 5, 1000f, (float)view.BorderWidth)
                };
                Control.ClipsToBounds = true;
                Control.Layer.AddSublayer(BottomLine);

                this.AddDoneButton();
            }
        }


        // <summary>
        /// <para>Add toolbar with Done button</para>
        /// </summary>
        protected void AddDoneButton()
        {
            var toolbar = new UIToolbar(new RectangleF(0.0f, 0.0f, 50.0f, 44.0f));

            var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
            {
                this.Control.ResignFirstResponder();
                var baseEntry = this.Element.GetType();
                ((IEntryController)Element).SendCompleted();
            });

            toolbar.Items = new UIBarButtonItem[] {
                new UIBarButtonItem (UIBarButtonSystemItem.FlexibleSpace),
                doneButton
            };
            this.Control.InputAccessoryView = toolbar;
        }
    }

}