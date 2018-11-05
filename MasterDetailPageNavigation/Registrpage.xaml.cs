using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;

using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MasterDetailPageNavigation
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Registrpage : ContentPage
	{
        private static readonly HttpClient client = new HttpClient();
        //private readonly string helloUrl = "http://agvue.ru/Home/Hello";

        private readonly string naraurl = "http://178.88.161.75:5002/registr";

        public Registrpage()
		{
			InitializeComponent ();
		}


        private async void Button_click1(object sender, EventArgs args)
        {

            string phon = phone.Text;
            string pasw = password.Text;
            string fam = fio.Text;
            string ema = email.Text;

            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                {"fio", fam },
                { "phone", phon },
                {"email", ema },
                {"password", pasw }
            };
            FormUrlEncodedContent form = new FormUrlEncodedContent(dic);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "token 4e4ca13260c8b91da42d0fa2fd52c409f4d55259");

            HttpResponseMessage response = await client.PostAsync(naraurl, form);

            string result = await response.Content.ReadAsStringAsync();
            string content = await response.Content.ReadAsStringAsync();

            


            try
            {
                var pars = JObject.Parse(content);
                // result["data"]["data"][0]["fio"]

                if (pars["data"].ToString() == "Not found code!")
                {
                    await DisplayAlert("Ошибка!", "Проверьте правильность введенной информации. ", "OK");
                }
                else
                {
                    await DisplayAlert("Вам отправлен на ваш номер СМС", "Запомните код.", "OK");
                    //await DisplayAlert("Result", result, "OK");
                    //Page pg2 = new Master();

                    //await Navigation.PushAsync(pg2);
                }
            }
            catch
            {
                await DisplayAlert("Ошибка!", "Ошибка на сервере! Зайдите в приложение позже.", "OK");
            }


            //Page pg2 = new Registr2(phon);

            ////NavigationPage.SetHasBackButton(pg1, false); // Without back
            ////NavigationPage.SetHasNavigationBar(pg1, false);

            //await Navigation.PushAsync(pg2);
        }

        private async void Start_click1(object sender, EventArgs args)
        {
            Page pg2 = new Start();

            //NavigationPage.SetHasBackButton(pg1, false); // Without back
            //NavigationPage.SetHasNavigationBar(pg1, false);

            await Navigation.PushAsync(pg2);
        }


    }
}