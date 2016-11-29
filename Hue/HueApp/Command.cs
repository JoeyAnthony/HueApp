using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Hue
{
    class Command
    {
        SendReceive Connection = new SendReceive();
        Client ClientInfo = new Client();

        public Command()
        {

        }

        //get all lamps from the bridge
        async public Task<ObservableCollection<Lamp>> GetAllLamps(MainPage page)
        {
            Uri AllInfo = new Uri($"http://{ClientInfo.IP}:{ClientInfo.Port}/api/{ClientInfo.UserName}");
            JObject data = await Connection.GET(AllInfo);
            //ERROR CATHING NOT TESTED!!!
            if (data == null)
            {
                page.ErrorOccurred(404,"Connction error: Are you connected to the bridge?");
                return null;
            }
            else if (data[0].ToString().Contains("error"))
            {
                string desc = (string)data[0].SelectToken("error").SelectToken("description");
                int type = (int)data[0].SelectToken("error").SelectToken("type");
                page.ErrorOccurred(type, desc);
                return null;
            }

            ObservableCollection<Lamp> lamps = new ObservableCollection<Lamp>();

            foreach (JProperty x in data.SelectToken("lights"))
            {
                Lamp lamp = new Lamp();


                foreach (JProperty z in x.Value.SelectToken("state"))
                {
                    if (z.Name.Contains("on"))
                    {
                        lamp.IsOn = (bool)z.Value;
                    }
                    if (z.Name.Contains("bri"))
                    {
                        lamp.Brightness = (int)z.Value;
                    }
                    if (z.Name.Contains("hue"))
                    {
                        lamp.Hue = (int)z.Value;
                    }
                    if (z.Name.Contains("sat"))
                    {
                        lamp.Saturation = (int)z.Value;
                    }
                }

                string a = x.Name;

                lamp.Number = int.Parse(x.Name);
                lamp.Type = (string)x.Value.SelectToken("type");
                lamp.Name = (string)x.Value.SelectToken("name");
                lamp.ModelID = (string)x.Value.SelectToken("modelid");
                lamp.UniqueID = (string)x.Value.SelectToken("uniqueid");
                lamps.Add(lamp);
            }
            return lamps;
        }
        //create an account on the bridge
        public async void CreateAccount(string name, string appname, MainPage page)
        {
            Uri AccountCreate = new Uri($"http://{ClientInfo.IP}:{ClientInfo.Port}/api");
            string body = $"{{\"devicetype\":\"{appname}#{name}\"}}";
            JArray response = await Connection.POST(AccountCreate, body);

            if (response == null)
            {
                page.ErrorOccurred(404, "Connction error: Are you connected to the bridge?");
                return;
            }
            else if (response[0].ToString().Contains("error"))
            {
                string desc = (string)response[0].SelectToken("error").SelectToken("description");
                int type = (int)response[0].SelectToken("error").SelectToken("type");
                page.ErrorOccurred(type, desc);
                return;
            }
            else
            {
                ClientInfo.UserName = response[0].SelectToken("success").SelectToken("username").ToString();
            }
        }

        //send command to turn lamp on or off
        public async void LampSwitch(int lampId, bool on, MainPage page)
        {
            Uri lampState = new Uri($"http://{ClientInfo.IP}:{ClientInfo.Port}/api/{ClientInfo.UserName}/lights/{lampId}/state");
            string body;
            if (on)
            {
                body = "{\"on\":false}";
            }
            else
            {
                body = "{\"on\":true}";
            }
            
            var response = await Connection.PUT(lampState, body);

            if (response == null)
            {
                page.ErrorOccurred(404, "Connction error: Are you connected to the bridge?");
                return;
            }
            else if (response[0].ToString().Contains("error"))
            {
                string desc = (string)response[0].SelectToken("error").SelectToken("description");
                int type = (int)response[0].SelectToken("error").SelectToken("type");
                page.ErrorOccurred(type, desc);
                return;
            }
        }

        //set the hue sat or bri of a lamp
        public async void HueSatBri(int lampId, int hue, int sat, int bri, MainPage page)
        {
            Uri lampState = new Uri($"http://{ClientInfo.IP}:{ClientInfo.Port}/api/{ClientInfo.UserName}/lights/{lampId}/state");
            string body = $"{{\"sat\":{sat}, \"bri\":{bri},\"hue\":{hue}}}";

            var response = await Connection.PUT(lampState, body);

            if (response == null)
            {
                page.ErrorOccurred(404, "Connction error: Are you connected to the bridge?");
                return;
            }
            else if (response[0].ToString().Contains("error"))
            {
                string desc = (string)response[0].SelectToken("error").SelectToken("description");
                int type = (int)response[0].SelectToken("error").SelectToken("type");
                page.ErrorOccurred(type, desc);
                return;
            }
        }
    }
}
