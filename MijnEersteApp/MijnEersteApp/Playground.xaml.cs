using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Xamarin.Essentials;
using System.Timers;

namespace MijnEersteApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Playground : ContentPage
    {
        private HttpClient client = new HttpClient();
        private List<Creature> creatureList = new List<Creature>();

        private float higherAmount = 0.1f;

        public Creature Creature { get; set; } = new Creature
        {
           
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

            GoToPlayground(Creature);
            await creatureDataStore.UpdateItem(Creature);
        }

        public Playground()
        {
            InitializeComponent();
            GetPlayers();

            Timer updatePlaygroundTimer = new System.Timers.Timer();
            updatePlaygroundTimer.Elapsed += new ElapsedEventHandler(UpdateStats);
            updatePlaygroundTimer.Interval = 5000;
            updatePlaygroundTimer.Enabled = true;
        }

        void UpdateStats(object source, ElapsedEventArgs e)
        {
            Creature.Stimulated = Math.Min(Creature.Stimulated + higherAmount, 1);
            Creature.Tired = Math.Max(Creature.Tired - higherAmount, 0);
            Creature.Boredom = Math.Min(Creature.Boredom + higherAmount, 1);
            UpdateCreature();
        }

        void UpdateScreen()
        {
            for (int i = 0; i < creatureList.Count && i < 10; i++)
            {
                App.Current.Resources["name" + i] = creatureList[i].Name;
                App.Current.Resources["user" + i] = creatureList[i].UserName;
            }
        }

        private async void GetPlayers()
        {
            var response = await client.GetAsync("https://tamagotchi.hku.nl/api/Playground");
            if (response.IsSuccessStatusCode)
            {
                
                string creatureAsText = await response.Content.ReadAsStringAsync();

                PlaygoundEntry[] playgoundEntries = JsonConvert.DeserializeObject<PlaygoundEntry[]>(creatureAsText);

                foreach (PlaygoundEntry creatureEntry in playgoundEntries)
                {
                    creatureList.Add(creatureEntry.creature);
                }
            }
            UpdateScreen();
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

        public async Task<bool> LeavePlayground(Creature item)
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

        protected override async void OnDisappearing()
        {
            UpdateCreature();
            creatureList.Clear();
            LeavePlayground(Creature);
        }

        //function that updates the creature
        private async void UpdateCreature()
        {
            var creatureDataStore = DependencyService.Get<IDataStore<Creature>>();
            await creatureDataStore.UpdateItem(Creature);
        }
    }
}