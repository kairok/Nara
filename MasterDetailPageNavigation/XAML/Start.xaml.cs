using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;


using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace MasterDetailPageNavigation
{
   

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Start : ContentPage
	{
        //public string Phone = "";
        //private static readonly HttpClient client = new HttpClient();

        //private readonly string helloUrl = "http://agvue.ru/Home/Hello";

        private readonly string naraurl = "http://178.88.161.75:5002/api/v1/nara/finduser/";
       // private readonly string naraurl = "http://178.88.161.75:5002/api/v1/nara/orderall/";

        public Start ()
		{
			InitializeComponent ();
		}

        private async void Button_click1(object sender, EventArgs args)
        {
            string pasword = password.Text;
            Global.Phone = phone.Text;

            //  Send code API
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "phone", Global.Phone },
                {"password", pasword }
                
            };
            FormUrlEncodedContent form = new FormUrlEncodedContent(dic);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "token 4e4ca13260c8b91da42d0fa2fd52c409f4d55259");
           // HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, URL);

            HttpResponseMessage response = await client.PostAsync(naraurl, form);
            //HttpResponseMessage response = await client.GetAsync(naraurl);

            string content = await response.Content.ReadAsStringAsync();

            try
            {
                var result = JObject.Parse(content);
                // result["data"]["data"][0]["fio"]

                if (result["data"].ToString() == "Not found code!")
                {
                    await DisplayAlert("Ошибка!", "Не правильный введеный код или номер телефона. Введите заново. ", "OK");
                }
                else
                {
                    //await DisplayAlert("Result", result, "OK");
                    Page pg2 = new MainPage();

                    await Navigation.PushAsync(pg2);
                }
            }
            catch
            {
                await DisplayAlert("Ошибка!", "Ошибка на сервере! Зайдите в приложение позже.", "OK");
            }
            
            

            
        }

    }
}