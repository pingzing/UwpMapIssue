using System;
using Windows.Devices.Geolocation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Media;

namespace MapIssue
{
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer _mapUpdateTimer = new DispatcherTimer();
        private BasicGeoposition _point1 = new BasicGeoposition { Altitude = 0, Latitude = 38.976352, Longitude = -120.592449 };
        private BasicGeoposition _point2 = new BasicGeoposition { Altitude = 0, Latitude = 52.726063, Longitude = -72.353684 };
        private Border _xamlElement;

        public MainPage()
        {
            this.InitializeComponent();
            _mapUpdateTimer.Interval = TimeSpan.FromSeconds(1);
            _mapUpdateTimer.Tick += _mapUpdateTimer_Tick;
            _xamlElement = new Border()
            {
                Child = new TextBlock { Text = "I am a MapControl child." },
                Background = new SolidColorBrush(Colors.Red)
            };
            MainMap.Children.Add(_xamlElement);
            MapControl.SetLocation(_xamlElement, new Geopoint(_point1));
            _mapUpdateTimer.Start();
        }

        private void _mapUpdateTimer_Tick(object sender, object e)
        {
            var currentPoint = MapControl.GetLocation(_xamlElement);
            if (currentPoint.Position.Latitude == _point1.Latitude && currentPoint.Position.Longitude == _point1.Longitude)
            {
                // Move to _point2
                MapControl.SetLocation(_xamlElement, new Geopoint(_point2));
            }
            else
            {
                // Move back to _point1
                MapControl.SetLocation(_xamlElement, new Geopoint(_point1));
            }
        }
    }
}
