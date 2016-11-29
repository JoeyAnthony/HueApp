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

        async public Task<ObservableCollection<Lamp>> GetAllLamps()
        {
            Uri AllInfo = new Uri($"http://{ClientInfo.IP}:{ClientInfo.Port}/api/{ClientInfo.UserName}");
            JObject data = await Connection.GET(AllInfo);
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

        public async void CreateAccount(string name, string appname)
        {
            Uri AccountCreate = new Uri($"http://{ClientInfo.IP}:{ClientInfo.Port}/api");
            //string body = "{\"devicetype\":\"" + appname + "#" + name + "\"}";
            string body = $"{{\"devicetype\":\"{appname}#{name}\"}}";
            JArray response = await Connection.POST(AccountCreate, body);
            if (response == null)
            {
                //Big error occurred
            }
           
            string s = response[0].ToString();
            if (s.Contains("error"))
            {
                ArrayError(response);
                return;
            }
            else
            {
                ClientInfo.UserName = response[0].SelectToken("success").SelectToken("username").ToString();
            }
        }

        public async void LampSwitch(int lampId, bool on)
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
            
            await Connection.PUT(lampState, body);
        }

        public async void HueSatBri(int lampId, int hue, int sat, int bri)
        {
            Uri lampState = new Uri($"http://{ClientInfo.IP}:{ClientInfo.Port}/api/{ClientInfo.UserName}/lights/{lampId}/state");
            string body = $"{{\"sat\":{sat}, \"bri\":{bri},\"hue\":{hue}}}";

            await Connection.PUT(lampState, body);
        }

        public void ArrayError(JArray J)
        {
            J[0].SelectToken("error");
        }

        public void Error()
        {

        }
    }
}
