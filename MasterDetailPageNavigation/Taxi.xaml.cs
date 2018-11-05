using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using System.Diagnostics;
//using Xamarin.Forms.Xaml;

namespace MasterDetailPageNavigation
{
    

    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Taxi : ContentPage
	{

       

        CustomMap map;
        CustomPin pin;


        public Taxi ()
		{
			InitializeComponent ();
            

            Global.Stop = 1;

            Console.WriteLine("Taxi!------------------");
            //Map map;
            map = new CustomMap
            {
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand,
                IsShowingUser = true
            };
            map.CustomPins = new List<CustomPin> { };
            map.MapType = MapType.Street;
            // map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(55.237208, 10.479160), Distance.FromMeters(500)));
            map.IsShowingUser = false;

            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            Content = stack;


            var testPos = new Position(43.249711, 76.923514);  // new Position(43.249711, 76.903514);

            pin = new CustomPin
            {
                Id = "Taxi",
                Pin = new Pin
                {
                    Type = PinType.Place,
                    Position = testPos,

                    Label = string.Format("adresse:")

                },

            };

          //  Spisok.Add(pin);
            map.CustomPins.Add(pin);

            map.Pins.Add(pin.Pin);


            testPos = new Position(43.259711, 76.913514);

            var pin2 = new CustomPin
            {
                Id = "Taxi",
                Pin = new Pin
                {
                    Type = PinType.Place,
                    Position = testPos,

                    Label = string.Format("Kairat:")

                },

            };
           // Spisok.Add(pin2);

            map.CustomPins.Add(pin2);
            map.Pins.Add(pin2.Pin);


            //Device.BeginInvokeOnMainThread(async () =>
            //{
            //    await RandomMethodAsync(testPos);
            //});

            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(43.259711, 76.913514), Distance.FromKilometers(2)));

            

        }
    }
}