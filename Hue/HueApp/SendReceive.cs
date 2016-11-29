using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.IO;

namespace Hue
{
    class SendReceive
    {

        public SendReceive()
        {
            Debug.WriteLine("All set");
        }
        //send GET messages
        public async Task<JObject> GET(Uri command)
        {
            try
            {
                JObject data = null;

                HttpClient HttpClient = new HttpClient();
                var response = await HttpClient.GetAsync(command);

                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                data = JObject.Parse(json);

                return data;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
            
        }
        //send POST messages
        public async Task<JArray> POST(Uri uri, string body)
        {
            JArray data = null;
            try
            {
                HttpClient HttpClient = new HttpClient();

                HttpStringContent content = new HttpStringContent(body);
                var response = await HttpClient.PostAsync(uri, content);

                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                data = JArray.Parse(json);

                return data;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        //send PUT messages
        public async Task<JArray> PUT(Uri uri, string body)
        {
            JArray data = null;
            try
            {
                HttpClient HttpClient = new HttpClient();

                HttpStringContent content = new HttpStringContent(body);
                var response = await HttpClient.PutAsync(uri, content);

                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                data = JArray.Parse(json);

                return data;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
    }

}

