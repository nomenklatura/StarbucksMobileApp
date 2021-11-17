using CoreGraphics;
using CoreText;
using Foundation;
using StarbucksMobileApp.Helpers.CustomControls;
using StarbucksMobileApp.iOS.Helpers.CustomControls;
using System;
using System.Drawing;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]

namespace StarbucksMobileApp.iOS.Helpers.CustomControls
{
    public class CustomEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var view = (CustomEditor)Element;

                //Control.BorderStyle = UITextBorderStyle.None;

                //Control.LeftView = new UIView(new CGRect(0f, 0f, 9f, 20f));
                //Control.LeftViewMode = UITextFieldViewMode.Always;

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

        private void DrawText(CGContext context, string text, int textHeight, CGPoint point)
        {
            var x = point.X;
            var y = point.Y + textHeight;

            context.TranslateCTM(x, y);

            context.ScaleCTM(1, -1);
            context.SetFillColor(UIColor.Red.CGColor);

            var attributedString = new NSAttributedString(text,
                new CTStringAttributes
                {
                    ForegroundColorFromContext = true,
                    Font = new CTFont("Arial", 16)
                });

            using (var textLine = new CTLine(attributedString))
            {
                textLine.Draw(context);
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
                var baseEditor = this.Element.GetType();
                ((IEditorController)Element).SendCompleted();
            });

            toolbar.Items = new UIBarButtonItem[] {
                new UIBarButtonItem (UIBarButtonSystemItem.FlexibleSpace),
                doneButton
            };
            this.Control.InputAccessoryView = toolbar;
        }

    }
}