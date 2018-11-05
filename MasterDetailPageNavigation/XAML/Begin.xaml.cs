using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MasterDetailPageNavigation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
   // [assembly: XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class Begin : CarouselPage
    {
		public Begin ()
		{
			InitializeComponent ();
		}


        public async void Button2(object sender, EventArgs args)
        {
            Page pg1 = new Registrpage();
            await Navigation.PushAsync(pg1);
        }


    }
}