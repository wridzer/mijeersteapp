using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MijnEersteApp
{
    public class RemoteCreatureStore : IDataStore<Creature>
    {
        private HttpClient client = new HttpClient();

        public async Task<bool> CreateItem(Creature item)
        {
            string creatureAsText = JsonConvert.SerializeObject(item);

            try
            {
                var response = await client.PostAsync("https://tamagotchi.hku.nl/api/Creatures", new StringContent(creatureAsText, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string postedCreatureAsText = await response.Content.ReadAsStringAsync();

                    Creature postedCreature = JsonConvert.DeserializeObject<Creature>(postedCreatureAsText);

                    Preferences.Set("MyCreatureID", postedCreature.ID);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (HttpRequestException e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteItem(Creature item)
        {
            Preferences.Remove("MyCreatureID");
            return true;
        }

        public async Task<Creature> ReadItem()
        {
            int creatureID = Preferences.Get("MyCreatureID", 0);
            if (creatureID == 0)
            {
                return null;
            }

            var response = await client.GetAsync("https://tamagotchi.hku.nl/api/Creatures/2");
            if (response.IsSuccessStatusCode)
            {
                string creatureAsText = await response.Content.ReadAsStringAsync();

                Creature creature = JsonConvert.DeserializeObject<Creature>(creatureAsText);

                Preferences.Set("MyCreatureID", creature.ID);

                return creature;
            }

            return null;
        }

        public async Task<bool> UpdateItem(Creature item)
        {
            string creatureAsText = JsonConvert.SerializeObject(item);

            try
            {
                var response = await client.PostAsync("https://tamagotchi.hku.nl/api/Creatures", new StringContent(creatureAsText, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string postedCreatureAsText = await response.Content.ReadAsStringAsync();

                    Creature postedCreature = JsonConvert.DeserializeObject<Creature>(postedCreatureAsText);

                    Preferences.Set("MyCreatureID", postedCreature.ID);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (HttpRequestException e)
            {
                return false;
            }
        }
    }
}
