using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace ClassyRankReveal
{
    public static class Api
    {
        public static string OffsetsUrl = "https://raw.githubusercontent.com/uk-classy/csgo_addresses/master/address.json";
        public static HttpClient Client = new HttpClient();
        public static async void GetOffsetsFromWeb()
        {
            HttpResponseMessage Response = await Client.GetAsync(OffsetsUrl);
            if (Response != null)
            {
                string JsonStr = await Response.Content.ReadAsStringAsync();
                if (JsonStr != null)
                {
                    JObject parsed = JObject.Parse(JsonStr);
                    foreach (JProperty x in parsed["signatures"])
                    {
                        string name = x.Name;
                        JToken value = x.Value;

                        if (name == "dwEntityList")
                        {
                            MemOffsets.EntityListPointer = Convert.ToInt32(value);
                        }

                        if (name == "dwClientState")
                        {
                            MemOffsets.ClientStatePointer = Convert.ToInt32(value);
                        }

                        if (name == "dwClientState_PlayerInfo")
                        {
                            MemOffsets.ClientStatePlayerInfo = Convert.ToInt32(value);
                        }

                    }

                    foreach (JProperty x in parsed["netvars"])
                    {
                        string name = x.Name;
                        JToken value = x.Value;


                        if (name == "m_iTeamNum")
                        {
                            MemOffsets.TeamOffset = Convert.ToInt32(value);
                        }

                        if (name == "m_iCompetitiveWins")
                        {
                            MemOffsets.PlayerWinsOffset = Convert.ToInt32(value);
                        }

                        if (name == "m_iCompetitiveRanking")
                        {
                            MemOffsets.PlayerRankOffset = Convert.ToInt32(value);
                        }
                     

                    }
                }

            }

        }
    }
}
