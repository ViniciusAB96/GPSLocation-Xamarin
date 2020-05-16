using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace XamarinGPS
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        Location startLocation;
        public MainPage()
        {
            InitializeComponent();
            GetLocation();
        }
        private async void GetLocation()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var position = await locator.GetPositionAsync(TimeSpan.FromMilliseconds(100));
                if (CrossGeolocator.Current.IsGeolocationEnabled)
                {
                    lbLatitude.Text = String.Format("Latitude: {0}", position.Latitude.ToString());
                    lbLongitude.Text = String.Format("Latitude: {0}", position.Longitude.ToString());

                    startLocation = new Location(position.Latitude, position.Longitude);
                }
                else
                {
                    await DisplayAlert("Message", "GPS not enabled", "OK");
                }
            }
            catch (Exception err)
            {
                await DisplayAlert("Erro no serviço de previsão do tempo", err.Message, "OK");
            }
        }

        private void btCalcularDistancia_Clicked(object sender, EventArgs e)
        {
            try
            { 
            Location locationEnd = new Location(Convert.ToDouble(LatitudeDest.Text), Convert.ToDouble(LongitudeDest.Text));
            double distance =  Math.Round(Location.CalculateDistance(startLocation, locationEnd, 0),2);

            lbDistanciaAteDestino.Text = String.Format("Distância: {0} km",distance.ToString());
            }
            catch(Exception ex)
            {
                DisplayAlert("Erro!", ex.Message, "OK");
            }
        }
    }
}
