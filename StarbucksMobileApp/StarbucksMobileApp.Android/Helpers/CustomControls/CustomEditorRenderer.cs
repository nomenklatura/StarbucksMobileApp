using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidX.Core.Content;
using StarbucksMobileApp.Droid.Helpers.CustomControls;
using StarbucksMobileApp.Helpers.CustomControls;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]

namespace StarbucksMobileApp.Droid.Helpers.CustomControls
{
    public class CustomEditorRenderer : EditorRenderer
    {
        CustomEditor view;

        public CustomEditorRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                view = (CustomEditor)Element;
                switch (view.BorderType)
                {
                    case EEntryType.None:
                        BorderEditor(e, true);
                        break;
                    case EEntryType.Border:
                        BorderEditor(e, false);
                        break;
                    case EEntryType.Line:
                        LineEditor(e);
                        break;
                    default:
                        BorderEditor(e, false);
                        break;
                }
            }

        }

        void BorderEditor(ElementChangedEventArgs<Editor> e, bool None = false)
        {
            if (e.NewElement != null)
            {
                view = (CustomEditor)Element;

                if (None)
                {
                    view.BackgroundColor = System.Drawing.Color.Transparent;
                }

                if (view.IsCurvedCornersEnabled)
                {
                    double[] rgb = new double[3]
                    {
                        view.BackgroundColor.ToAndroid().R,
                        view.BackgroundColor.ToAndroid().G,
                        view.BackgroundColor.ToAndroid().B
                    };

                    // creating gradient drawable for the curved background
                    var _gradientBackground = new GradientDrawable(
                        GradientDrawable.Orientation.TopBottom,
                        new int[] {
                            new Android.Graphics.Color((byte)rgb[0], (byte)rgb[1], (byte)rgb[2]),
                            new Android.Graphics.Color((byte)rgb[1], (byte)rgb[2], (byte)rgb[0]),
                            new Android.Graphics.Color((byte)rgb[0], (byte)rgb[1], (byte)rgb[2]),
                            new Android.Graphics.Color((byte)rgb[2], (byte)rgb[1], (byte)rgb[0]),
                            new Android.Graphics.Color((byte)rgb[0], (byte)rgb[1], (byte)rgb[2])
                        });

                    _gradientBackground.SetShape(ShapeType.Rectangle);
                    _gradientBackground.SetColor(view.BackgroundColor.ToAndroid());

                    // Thickness of the stroke line
                    _gradientBackground.SetStroke(view.BorderWidth, view.BorderColor.ToAndroid());

                    // Radius for the curves
                    _gradientBackground.SetCornerRadius(
                        DpToPixels(this.Context,
                            Convert.ToSingle(view.CornerRadius)));

                    // set the background of the label
                    Control.SetBackground(_gradientBackground);

                    view.BackgroundColor = System.Drawing.Color.Transparent;
                }

                ((CustomEditor)e.NewElement).PropertyChanging += OnPropertyChanging;

                // Set padding for the internal text from border
                Control.SetPadding(
                    (int)DpToPixels(this.Context, Convert.ToSingle(12)),
                    Control.PaddingTop,
                    (int)DpToPixels(this.Context, Convert.ToSingle(12)),
                    Control.PaddingBottom);

                if (!view.IsKeyboardEnabled)
                {
                    this.Control.ShowSoftInputOnFocus = false;
                }
                else
                {
                    ((CustomEditor)e.NewElement).PropertyChanging -= OnPropertyChanging;
                }
            }

            if (e.OldElement != null)
            {
                ((CustomEditor)e.OldElement).PropertyChanging -= OnPropertyChanging;
            }

            e.NewElement.Focused += NewElement_Focused;
        }
        void LineEditor(ElementChangedEventArgs<Editor> e)
        {
            try
            {
                if (Control == null || e.NewElement == null) return;

                if (e.NewElement is CustomEditor customEditor)
                {
                    Control.SetHighlightColor(color: customEditor.BorderColor.ToAndroid());

                    JNIEnv.SetField(Control.Handle, JNIEnv.GetFieldID(JNIEnv.FindClass(typeof(TextView)), "mCursorDrawableRes", "I"), 0);
                    TextView textViewTemplate = new TextView(Control.Context);

                    var field = textViewTemplate.Class.GetDeclaredField("mEditor");
                    field.Accessible = true;
                    var editor = field.Get(Control);

                    string[]
                    fieldsNames = { "mTextSelectHandleLeftRes", "mTextSelectHandleRightRes", "mTextSelectHandleRes" },
                    drawablesNames = { "mSelectHandleLeft", "mSelectHandleRight", "mSelectHandleCenter" };

                    for (int index = 0; index < fieldsNames.Length && index < drawablesNames.Length; index++)
                    {
                        field = textViewTemplate.Class.GetDeclaredField(fieldsNames[index]);
                        field.Accessible = true;
                        Drawable handleDrawable = ContextCompat.GetDrawable(Context, field.GetInt(Control));

                        handleDrawable.SetColorFilter(new PorterDuffColorFilter(customEditor.BorderColor.ToAndroid(), PorterDuff.Mode.SrcIn));

                        field = editor.Class.GetDeclaredField(drawablesNames[index]);
                        field.Accessible = true;
                        field.Set(editor, handleDrawable);
                    }

                    if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                    {
                        Control.BackgroundTintList = ColorStateList.ValueOf(customEditor.BorderColor.ToAndroid());
                    }
                    else
                    {
                        Control.Background.SetColorFilter(new PorterDuffColorFilter(customEditor.BorderColor.ToAndroid(), PorterDuff.Mode.SrcAtop));
                    }

                    ((CustomEditor)e.NewElement).PropertyChanging += OnPropertyChanging;

                    // Set padding for the internal text from border
                    Control.SetPadding(
                        (int)DpToPixels(this.Context, Convert.ToSingle(12)),
                        Control.PaddingTop,
                        (int)DpToPixels(this.Context, Convert.ToSingle(12)),
                        Control.PaddingBottom);

                    if (!view.IsKeyboardEnabled)
                    {
                        this.Control.ShowSoftInputOnFocus = false;
                    }
                    else
                    {
                        ((CustomEditor)e.NewElement).PropertyChanging -= OnPropertyChanging;
                    }
                }

                if (e.OldElement != null)
                {
                    ((CustomEditor)e.OldElement).PropertyChanging -= OnPropertyChanging;
                }

                e.NewElement.Focused += NewElement_Focused;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        private void NewElement_Focused(object sender, FocusEventArgs e)
        {
            if (!view.IsKeyboardEnabled)
            {
                this.Control.ShowSoftInputOnFocus = false;
            }
            else
            {
                view.PropertyChanging -= OnPropertyChanging;
            }
        }
        public override IOnFocusChangeListener OnFocusChangeListener
        {
            get => base.OnFocusChangeListener;
            set => base.OnFocusChangeListener = value;
        }

        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }

        private void OnPropertyChanging(object sender, PropertyChangingEventArgs propertyChangingEventArgs)
        {
            // Check if the view is about to get Focus
            if (propertyChangingEventArgs.PropertyName == VisualElement.IsFocusedProperty.PropertyName)
            {
                // incase if the focus was moved from another Editor
                // Forcefully dismiss the Keyboard 
                InputMethodManager imm = (InputMethodManager)this.Context.GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(this.Control.WindowToken, 0);
            }
        }


    }
}