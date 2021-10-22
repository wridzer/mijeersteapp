using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace MijnEersteApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Playground : ContentPage
    {
        private HttpClient client = new HttpClient();

        public Creature Creature { get; set; } = new Creature
        {
            Name = "Koelekikker",
            UserName = "Wridzer",
            Hunger = 0.5f,
            Thirst = 0.5f,
            Boredom = 0.5f,
            Loneliness = 0.5f,
            Stimulated = 0.5f,
            Tired = 0.5f
        };

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var creatureDataStore = DependencyService.Get<IDataStore<Creature>>();
            Creature = await creatureDataStore.ReadItem();
            if (Creature == null)
            {
                Creature = new Creature { Name = "Koelekikker" };
                await creatureDataStore.CreateItem(Creature);
            }

            await creatureDataStore.UpdateItem(Creature);
        }

        public Playground()
        {
            InitializeComponent();

        }

        private async void GetPlayers()
        {
            var response = await client.GetAsync("https://tamagotchi.hku.nl/api/Playground");
            if (response.IsSuccessStatusCode)
            {

                //returns creatures

                //convert creatures back

                /*
                string creatureAsText = await response.Content.ReadAsStringAsync();

                Creature creature = JsonConvert.DeserializeObject<Creature>(creatureAsText);

                Preferences.Set("MyCreatureID", creature.ID);
                */

            }
        }

        private async Task<bool> GoToPlayground(Creature item)
        {
            string creatureAsText = JsonConvert.SerializeObject(item);

            try
            {
                var response = await client.PostAsync("https://tamagotchi.hku.nl/api/Playground/" + item.ID, new StringContent(creatureAsText, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        private async Task<bool> LeavePlayground(Creature item)
        {
            string creatureAsText = JsonConvert.SerializeObject(item);

            try
            {
                var response = await client.DeleteAsync("https://tamagotchi.hku.nl/api/Playground/" + item.ID);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}