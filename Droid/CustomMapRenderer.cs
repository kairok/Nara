using System;
using System.Collections.Generic;
using System.ComponentModel;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using MasterDetailPageNavigation.Droid.Renderers;
using MasterDetailPageNavigation;
//using static Android.Provider.Settings;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MasterDetailPageNavigation.Droid.Renderers
{
    public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter//, IOnMapReadyCallback
    {
        bool isDrawn;

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);
          
           Device.StartTimer(TimeSpan.FromSeconds(2), Update_data);
            if (e.OldElement != null)
            {
                NativeMap.InfoWindowClick -= OnInfoWindowClick;
            }

        }


        public bool Update_data()
        {

            if (NativeMap == null)
            {
                return true;
            }

            //if (Global.Stop == 0)
            //{
            //    return true;
            //}

                //Device.BeginInvokeOnMainThread(async () =>
                //{
                //    try
                //    {

                //        //var locator = CrossGeolocator.Current;
                //        //locator.DesiredAccuracy = 50;
                //        //var currentpos = await locator.GetPositionAsync(10000);
                //        //Console.WriteLine("Coordinate " + currentpos.Latitude + " " + currentpos.Longitude);
                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine("Fail" + ex);
                //    }
                //});


                NativeMap.Clear();
            //map.Triggers.Add.;
            var Spisok = new List<CustomPin>();
            // var marker = new MarkerOptions();
            NativeMap.Clear();
            //WriteLine("Timer works!!!!--------------------------------------");
            foreach (var pin in ((CustomMap)Element).CustomPins)
            {
                Spisok.Add(pin);
            }

            ((CustomMap)Element).CustomPins.Clear();
            foreach (var pin in Spisok)
            {
               // int ok = 1;
                LatLng latlng = new LatLng(0, 0);
                var testPos = new Position(0, 0);
                if (pin.Id.ToString() == "Xamarin")
                {
                    var Latitude = pin.Pin.Position.Latitude;
                    var Long = pin.Pin.Position.Longitude;
                    latlng = new LatLng(Latitude, Long);
                    testPos = new Position(Latitude, Long);
                }
                else
                {
                    var Latitude = pin.Pin.Position.Latitude + 0.001;
                    var Long = pin.Pin.Position.Longitude + 0.001;
                    latlng = new LatLng(Latitude, Long);
                    testPos = new Position(Latitude, Long);
                }

                var pin2 = new CustomPin
                {
                    Id = pin.Id,
                    Pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = testPos,

                        Label = string.Format("Kairat2:")

                    },

                };
                ((CustomMap)Element).CustomPins.Add(pin2);
                var marker = new MarkerOptions();
                marker.SetPosition(latlng);
                marker.SetTitle(pin.Pin.Label);
                marker.SetSnippet(pin.Pin.Address);
                if (pin.Id.ToString() == "Xamarin")
                    marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.pin));
                else
                    marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.taksi));
                NativeMap.AddMarker(marker);
                isDrawn = true;
            }


            return true;
        }

        public bool OnMapReady()
        {
            Console.WriteLine("OnMapReady!-------------------------------");
            LatLng latlng = new LatLng(43.259711, 76.913514);

            return true;
        }


            bool isMapReady;
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            Console.WriteLine("OnElementPropertyChanged!-------------------------------");

            if (!isMapReady && (NativeMap != null))
            {
                NativeMap.SetInfoWindowAdapter(this);
                NativeMap.InfoWindowClick += OnInfoWindowClick;
                isMapReady = true;
            }

            if (e.PropertyName.Equals("VisibleRegion") && !isDrawn)
            {
                NativeMap.Clear();

                foreach (var pin in ((CustomMap)Element).CustomPins)
                {
                    var marker = new MarkerOptions();
                    marker.SetPosition(new LatLng(pin.Pin.Position.Latitude, pin.Pin.Position.Longitude));
                    marker.SetTitle(pin.Pin.Label);
                    marker.SetSnippet(pin.Pin.Address);
                    if (pin.Id.ToString()=="Xamarin")
                        marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.pin));
                    else
                        marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.taksi));

                    NativeMap.AddMarker(marker);
                }
                isDrawn = true;
            }
        }



        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            Console.WriteLine("OnLayout!-------------------------------");
           // isDrawn = false;
            if (!isMapReady)
                Update_data();
            if (changed)
            {
                Console.WriteLine("OnLayout! changed!  -------------------------------");
                isDrawn = true;
               // isMapReady = true;
                Update_data();
            }
        }

        void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            var customPin = GetCustomPin(e.Marker);
            if (customPin == null)
            {
                throw new Exception("Custom pin not found");
            }

            if (!string.IsNullOrWhiteSpace(customPin.Url))
            {
                var url = Android.Net.Uri.Parse(customPin.Url);
                var intent = new Intent(Intent.ActionView, url);
                intent.AddFlags(ActivityFlags.NewTask);
                Android.App.Application.Context.StartActivity(intent);
            }
        }

        public Android.Views.View GetInfoContents(Marker marker)
        {
            var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
            if (inflater != null)
            {
                Android.Views.View view;

                var customPin = GetCustomPin(marker);
                if (customPin == null)
                {
                    throw new Exception("Custom pin not found");
                }

                if (customPin.Id == "Xamarin")
                {
                    view = inflater.Inflate(Resource.Layout.XamarinMapInfoWindow, null);
                }
                else
                {
                    view = inflater.Inflate(Resource.Layout.MapInfoWindow, null);
                }

                var infoTitle = view.FindViewById<TextView>(Resource.Id.InfoWindowTitle);
                var infoSubtitle = view.FindViewById<TextView>(Resource.Id.InfoWindowSubtitle);

                if (infoTitle != null)
                {
                    infoTitle.Text = marker.Title;
                }
                if (infoSubtitle != null)
                {
                    infoSubtitle.Text = marker.Snippet;
                }

                return view;
            }
            return null;
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }

        CustomPin GetCustomPin(Marker annotation)
        {
            var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);
            foreach (var pin in ((CustomMap)Element).CustomPins)
            {
                if (pin.Pin.Position == position)
                {
                    return pin;
                }
            }
            return null;
        }


    }
}