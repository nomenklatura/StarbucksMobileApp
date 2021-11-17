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

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace StarbucksMobileApp.Droid.Helpers.CustomControls
{
    public class CustomEntryRenderer : EntryRenderer
    {
        CustomEntry view;

        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                view = (CustomEntry)Element;
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

                if (view.Keyboard==Keyboard.Numeric)
                {
                    this.Control.KeyListener = Android.Text.Method.DigitsKeyListener.GetInstance(string.Format("1234567890{0}", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                    this.Control.InputType = Android.Text.InputTypes.ClassNumber | Android.Text.InputTypes.NumberFlagDecimal;
                }
            }

        }

        void BorderEntry(ElementChangedEventArgs<Entry> e, bool None = false)
        {
            if (e.NewElement != null)
            {
                view = (CustomEntry)Element;

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

                ((CustomEntry)e.NewElement).PropertyChanging += OnPropertyChanging;

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
                    ((CustomEntry)e.NewElement).PropertyChanging -= OnPropertyChanging;
                }
            }

            if (e.OldElement != null)
            {
                ((CustomEntry)e.OldElement).PropertyChanging -= OnPropertyChanging;
            }

            e.NewElement.Focused += NewElement_Focused;
        }
        void LineEntry(ElementChangedEventArgs<Entry> e)
        {
            try
            {
                if (Control == null || e.NewElement == null) return;

                if (e.NewElement is CustomEntry customEntry)
                {
                    Control.SetHighlightColor(color: customEntry.BorderColor.ToAndroid());

                    if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
                    {
                        Control.SetTextCursorDrawable(0); //This API Intrduced in android 10
                    }
                    else
                    {
                        IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
                        IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "mCursorDrawableRes", "I");
                        JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, 0);
                    }

                    //JNIEnv.SetField(Control.Handle, JNIEnv.GetFieldID(JNIEnv.FindClass(typeof(TextView)), "mCursorDrawableRes", "I"), 0);
                    TextView textViewTemplate = new TextView(Control.Context);

                    var field = textViewTemplate.Class.GetDeclaredField("mEditor");
                    field.Accessible = true;
                    var editor = field.Get(Control);

                    try
                    {
                        string[]
                                    fieldsNames = { "mTextSelectHandleLeftRes", "mTextSelectHandleRightRes", "mTextSelectHandleRes" },
                                    drawablesNames = { "mSelectHandleLeft", "mSelectHandleRight", "mSelectHandleCenter" };

                        for (int index = 0; index < fieldsNames.Length && index < drawablesNames.Length; index++)
                        {
                            field = textViewTemplate.Class.GetDeclaredField(fieldsNames[index]);
                            field.Accessible = true;
                            Drawable handleDrawable = ContextCompat.GetDrawable(Context, field.GetInt(Control));

                            handleDrawable.SetColorFilter(new PorterDuffColorFilter(customEntry.BorderColor.ToAndroid(), PorterDuff.Mode.SrcIn));

                            field = editor.Class.GetDeclaredField(drawablesNames[index]);
                            field.Accessible = true;
                            field.Set(editor, handleDrawable);
                        }
                    }
                    catch (Exception err)
                    {
                        Console.WriteLine(err.Message);
                    }

                    if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                    {
                        Control.BackgroundTintList = ColorStateList.ValueOf(customEntry.BorderColor.ToAndroid());
                    }
                    else
                    {
                        Control.Background.SetColorFilter(new PorterDuffColorFilter(customEntry.BorderColor.ToAndroid(), PorterDuff.Mode.SrcAtop));
                    }

                    ((CustomEntry)e.NewElement).PropertyChanging += OnPropertyChanging;

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
                        ((CustomEntry)e.NewElement).PropertyChanging -= OnPropertyChanging;
                    }
                }

                if (e.OldElement != null)
                {
                    ((CustomEntry)e.OldElement).PropertyChanging -= OnPropertyChanging;
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
                // incase if the focus was moved from another Entry
                // Forcefully dismiss the Keyboard 
                InputMethodManager imm = (InputMethodManager)this.Context.GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(this.Control.WindowToken, 0);
            }
        }
    }
}