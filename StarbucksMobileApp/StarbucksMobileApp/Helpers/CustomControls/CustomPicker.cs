using Xamarin.Forms;

namespace StarbucksMobileApp.Helpers.CustomControls
{
    public class CustomPicker : Picker
    {
        public static readonly BindableProperty BorderColorProperty =
        BindableProperty.Create(
            nameof(BorderColor),
            typeof(Color),
            typeof(CustomPicker),
            Color.Gray);

        // Gets or sets BorderColor value
        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public static readonly BindableProperty BorderWidthProperty =
        BindableProperty.Create(
            nameof(BorderWidth),
            typeof(int),
            typeof(CustomPicker),
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
            typeof(CustomPicker),
            Device.RuntimePlatform == Device.Android ? 6 : 7);

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
            typeof(CustomPicker),
            true);

        // Gets or sets IsCurvedCornersEnabled value
        public bool IsCurvedCornersEnabled
        {
            get { return (bool)GetValue(IsCurvedCornersEnabledProperty); }
            set { SetValue(IsCurvedCornersEnabledProperty, value); }
        }

        public static readonly BindableProperty BorderTypeProperty =
        BindableProperty.Create(
            nameof(BorderType),
            typeof(EEntryType),
            typeof(CustomPicker),
            EEntryType.Border);

        // Gets or sets BorderColor value
        public EEntryType BorderType
        {
            get { return (EEntryType)GetValue(BorderTypeProperty); }
            set { SetValue(BorderTypeProperty, value); }
        }

    }
}
