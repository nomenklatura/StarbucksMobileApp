using System.Threading.Tasks;
using Xamarin.Forms;

namespace StarbucksMobileApp.Helpers.CustomControls
{
    public class CustomCollectionView : CollectionView
    {
        private View _customHeader, _secondContent, _sticyHeader;

        public CustomCollectionView()
        {
            Scrolled += GoodCollectionView_Scrolled;
        }

        [TypeConverter(typeof(ReferenceTypeConverter))]
        public View CustomHeader
        {
            set
            {
                _customHeader = value;
                _customHeader.SizeChanged += (o, e) => this.Header = new BoxView() { HeightRequest = _customHeader.Height };
            }
        }

        [TypeConverter(typeof(ReferenceTypeConverter))]
        public View StickyHeader
        {
            set => _sticyHeader = value;
        }

        [TypeConverter(typeof(ReferenceTypeConverter))]
        public View SecondContent
        {
            set => _secondContent = value;
        }

        private async void GoodCollectionView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            double scrollY = e.VerticalOffset < 0 ? 0 : e.VerticalOffset;
            double minimumHeight = _customHeader.Height - _sticyHeader.Height;

            scrollY = scrollY > minimumHeight ? minimumHeight : scrollY;

            //Show or hide the header and scroll the second view
            await Task.WhenAll(_customHeader?.TranslateTo(0, -scrollY, 50),
                _secondContent?.TranslateTo(0, -scrollY + _customHeader.Height, 50));
        }
    }
}
