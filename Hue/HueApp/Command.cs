﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Hue
{
    public class Command
    {
        private SendReceive Connection = new SendReceive();
        public Client ClientInfo = new Client();

        public Command()
        {

        }

        //get all lamps from the bridge
        async public Task<ObservableCollection<Lamp>> GetAllLamps(MainPage page)
        {
            Uri AllInfo = new Uri($"http://{ClientInfo.IP}:{ClientInfo.Port}/api/{ClientInfo.UserName}");
            var response = await Connection.GET(AllInfo);
            if (response == null)
            {
                page.ErrorOccurred(404,"Connction error: Are you connected to the bridge?");
                page.connected = false;
                return null;
            }
            else if (response is JArray)
            {
                JArray array = (JArray)response;
                string desc = (string)response[0].SelectToken("error").SelectToken("description");
                int type = (int)response[0].SelectToken("error").SelectToken("type");
                if (type == 3)
                {
                    desc = "Have you logged in to the bridge yet?";
                    page.connected = false;
                }
                page.ErrorOccurred(type, desc);
                return null;
            }
            else
            {
                JObject data = (JObject)response;
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

           
        }
        //create an account on the bridge
        public async Task<bool> CreateAccount(string name, string appname, MainPage page)
        {
            Uri AccountCreate = new Uri($"http://{ClientInfo.IP}:{ClientInfo.Port}/api");
            string body = $"{{\"devicetype\":\"{appname}#{name}\"}}";
            JArray response = await Connection.POST(AccountCreate, body);

            if (response == null)
            {
                page.ErrorOccurred(420, "Connction error: Are you connected to the bridge?");
                page.connected = false;
                return false;
            }
            else if (response[0].ToString().Contains("error"))
            {
                string desc = (string)response[0].SelectToken("error").SelectToken("description");
                int type = (int)response[0].SelectToken("error").SelectToken("type");
                page.ErrorOccurred(type, desc);
                if (type == 3)
                {
                    desc = "Have you logged in to the bridge yet?";
                    page.connected = false;
                }
                return false;
            }
            else
            {
                ClientInfo.UserName = response[0].SelectToken("success").SelectToken("username").ToString();
                return true;
            }
        }

        //send command to turn lamp on or off
        public async Task<bool> LampSwitch(int lampId, bool on, MainPage page)
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
                page.connected = false;
                return on;
            }
            else if (response.ToString().Contains("error"))
            {
                string desc = (string)response[0].SelectToken("error").SelectToken("description");
                int type = (int)response[0].SelectToken("error").SelectToken("type");
                page.ErrorOccurred(type, desc);
                return on;
            }
            else
            {
               return (bool) response[0].SelectToken("success").SelectToken($"/lights/{lampId}/state/on");
            }
        }

        //set the hue sat or bri of a lamp
        public async Task<int[]> HueSatBri(int lampId, int hue, int sat, int bri, MainPage page)
        {
            Uri lampState = new Uri($"http://{ClientInfo.IP}:{ClientInfo.Port}/api/{ClientInfo.UserName}/lights/{lampId}/state");
            string body = $"{{\"sat\":{sat}, \"bri\":{bri},\"hue\":{hue}}}";

            JArray response = await Connection.PUT(lampState, body);

            if (response == null)
            {
                page.ErrorOccurred(404, "Connction error: Are you connected to the bridge?");
                page.connected = false;
                return null;
            }
            else if (response.ToString().Contains("error"))
            {
                string desc = (string)response[0].SelectToken("error").SelectToken("description");
                int type = (int)response[0].SelectToken("error").SelectToken("type");
                page.ErrorOccurred(type, desc);
                return null;
            }
            else
            {
                int[] color = new int[3];
                foreach (JObject a in response)
                {
                    if (a.SelectToken("success").SelectToken($"/lights/{lampId}/state/hue") != null)
                    {
                        color[0] = (int)a.SelectToken("success").SelectToken($"/lights/{lampId}/state/hue");
                    }
                    else if (a.SelectToken("success").SelectToken($"/lights/{lampId}/state/sat") != null)
                    {
                        color[1] = (int)a.SelectToken("success").SelectToken($"/lights/{lampId}/state/sat");
                    }
                    else if (a.SelectToken("success").SelectToken($"/lights/{lampId}/state/bri") != null)
                    {
                        color[2] = (int)a.SelectToken("success").SelectToken($"/lights/{lampId}/state/bri");
                    }
                }
                return color;
            }
        }

        //UNFINISHED
        ////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////
        public async Task<ObservableCollection<Group>> GetGroups(MainPage page)
        {
            Uri AllInfo = new Uri($"http://{ClientInfo.IP}:{ClientInfo.Port}/api/{ClientInfo.UserName}");
            var response = await Connection.GET(AllInfo);
            if (response == null)
            {
                page.ErrorOccurred(404, "Connction error: Are you connected to the bridge?");
                return null;
            }
            else if (response is JArray)
            {
                JArray array = (JArray)response;
                string desc = (string)response[0].SelectToken("error").SelectToken("description");
                int type = (int)response[0].SelectToken("error").SelectToken("type");
                if (type == 3)
                {
                    desc = "Have you logged in to the bridge yet?";
                }
                page.ErrorOccurred(type, desc);
                return null;
            }
            else
            {
                JObject data = (JObject)response;
                ObservableCollection<Group> groups = new ObservableCollection<Group>();

                data.SelectToken("groups");
                return groups;
            }

        }
        ////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////
    }
}

