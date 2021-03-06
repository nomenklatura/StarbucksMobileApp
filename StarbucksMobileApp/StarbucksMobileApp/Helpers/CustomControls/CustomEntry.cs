using Xamarin.Forms;

namespace StarbucksMobileApp.Helpers.CustomControls
{
    public class CustomEntry : Entry
    {
        public static readonly BindableProperty BorderColorProperty =
        BindableProperty.Create(
            nameof(BorderColor),
            typeof(Color),
            typeof(CustomEntry),
            Color.Gray);

        // Gets or sets BorderColor value
        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public static readonly BindableProperty WidthRightPaddingProperty =
        BindableProperty.Create(
            nameof(WidthRightPadding),
            typeof(int),
            typeof(CustomEntry),
            0);

        public int WidthRightPadding
        {
            get { return (int)GetValue(WidthRightPaddingProperty); }
            set { SetValue(WidthRightPaddingProperty, value); }
        }

        public static readonly BindableProperty BorderWidthProperty =
        BindableProperty.Create(
            nameof(BorderWidth),
            typeof(int),
            typeof(CustomEntry),
            Device.RuntimePlatform == Device.Android ? 1 : 2);

        // Gets or sets BorderWidth value
        public int BorderWidth
        {
            get { return (int)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }

        public static readonly BindableProperty CornerRadiusProperty =
        BindableProperty.Create(
            nameof(CornerRadius),
            typeof(double),
            typeof(CustomEntry),
            Device.RuntimePlatform == Device.Android ? 6d : 7d);

        // Gets or sets CornerRadius value
        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly BindableProperty IsCurvedCornersEnabledProperty =
        BindableProperty.Create(
            nameof(IsCurvedCornersEnabled),
            typeof(bool),
            typeof(CustomEntry),
            true);

        // Gets or sets IsCurvedCornersEnabled value
        public bool IsCurvedCornersEnabled
        {
            get { return (bool)GetValue(IsCurvedCornersEnabledProperty); }
            set { SetValue(IsCurvedCornersEnabledProperty, value); }
        }

        public static readonly BindableProperty IsKeyboardEnabledProperty =
        BindableProperty.Create(
            nameof(IsKeyboardEnabled),
            typeof(bool),
            typeof(CustomEntry),
            true);

        // Gets or sets IsKeyboardEnabled value
        public bool IsKeyboardEnabled
        {
            get { return (bool)GetValue(IsKeyboardEnabledProperty); }
            set { SetValue(IsKeyboardEnabledProperty, value); }
        }

        public static readonly BindableProperty BorderTypeProperty =
        BindableProperty.Create(
            nameof(BorderType),
            typeof(EEntryType),
            typeof(CustomEntry),
            EEntryType.Border);

        // Gets or sets BorderColor value
        public EEntryType BorderType
        {
            get { return (EEntryType)GetValue(BorderTypeProperty); }
            set { SetValue(BorderTypeProperty, value); }
        }
    }
}
