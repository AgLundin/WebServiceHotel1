using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using WebServiceHotel1;

namespace WSClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            const string serverUrl = "Http://localhost:54784";
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var responce = client.GetAsync("api/demohotels").Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        var hotels = responce.Content.ReadAsAsync<IEnumerable<DemoHotel>>().Result;
                        foreach(var hotel in hotels)
                        {
                            Console.Write(hotel);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}
