using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;

namespace MijnEersteApp
{
    public partial class MainPage : ContentPage
    {
        //control stats
        private float lowerStatIntervan = 10000;
        private static float lowerStatAmount = 0.1f;
        private float addStatAmount = 0.2f;

        //Make Creature
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

        public MainPage()
        {
            InitializeComponent();
            System.Timers.Timer removeStatTimer = new System.Timers.Timer();
            System.Timers.Timer updateTimer = new System.Timers.Timer();
            App.Current.Resources["LabelColor"] = MoodColor(250, 230);

            //Timers
            removeStatTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            removeStatTimer.Interval = lowerStatIntervan;
            removeStatTimer.Enabled = true;
            updateTimer.Elapsed += new ElapsedEventHandler(UpdateStats);
            updateTimer.Interval = 100;
            updateTimer.Enabled = true;
        }

        public static Xamarin.Forms.Color MoodColor(float mood, float changePoint)
        {
            Xamarin.Forms.Color color;
            if (mood > changePoint) color = Color.FromHex("#1ccf00");
            else color = Color.FromHex("#cf0000");
            return color;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Creature.LowerStats(lowerStatAmount);
        }

        private void UpdateStats(object source, ElapsedEventArgs e)
        {
            App.Current.Resources["FillFood"] = Creature.Hunger * 5;
            App.Current.Resources["FillColorFood"] = MoodColor(Creature.Hunger, 0.5f);
            
            App.Current.Resources["FillDrink"] = Creature.Thirst * 5;
            App.Current.Resources["FillColorDrink"] = MoodColor(Creature.Thirst, 0.5f);
            
            App.Current.Resources["FillBored"] = Creature.Boredom * 5;
            App.Current.Resources["FillColorBored"] = MoodColor(Creature.Boredom, 0.5f);
            
            App.Current.Resources["FillLonely"] = Creature.Loneliness * 5;
            App.Current.Resources["FillColorLonely"] = MoodColor(Creature.Loneliness, 0.5f);
            
            App.Current.Resources["FillStim"] = Creature.Stimulated * 5;
            App.Current.Resources["FillColorStim"] = MoodColor(Creature.Stimulated, 0.5f);

            App.Current.Resources["FillSleep"] = Creature.Tired * 5;
            App.Current.Resources["FillColorSleep"] = MoodColor(Creature.Tired, 0.5f);

            App.Current.Resources["LabelColor"] = MoodColor(Creature.TotalMood(), 3);

            UpdateCreature();
        }


        void Food_Clicked(object sender, System.EventArgs e)
        {
            Creature.Hunger += addStatAmount;
            if (Creature.Hunger > 1)
            {
                Creature.Hunger = 1;
            }
            ((Button)sender).Text = $"Feed";
        }
        void Drink_Clicked(object sender, System.EventArgs e)
        {
            Creature.Thirst += addStatAmount;
            if (Creature.Thirst > 1)
            {
                Creature.Thirst = 1;
            }
            ((Button)sender).Text = $"Drink";
        }
        void Bored_Clicked(object sender, System.EventArgs e)
        {
            Creature.Boredom += addStatAmount;
            if (Creature.Boredom > 1)
            {
                Creature.Boredom = 1;
            }
            ((Button)sender).Text = $"Play";
        }
        void Lonely_Clicked(object sender, System.EventArgs e)
        {
            Creature.Loneliness += addStatAmount;
            if (Creature.Loneliness > 1)
            {
                Creature.Loneliness = 1;
            }
            ((Button)sender).Text = $"Attention";
        }
        void Stim_Clicked(object sender, System.EventArgs e)
        {
            Creature.Stimulated += addStatAmount;
            if (Creature.Stimulated > 1)
            {
                Creature.Stimulated = 1;
            }
            ((Button)sender).Text = $"Social";
        }
        void Sleep_Clicked(object sender, System.EventArgs e)
        {
            Creature.Tired += addStatAmount;
            if (Creature.Tired > 1)
            {
                Creature.Tired = 1;
            }
            ((Button)sender).Text = $"Sleep";
        }

        private async void UpdateCreature()
        {
            var creatureDataStore = DependencyService.Get<IDataStore<Creature>>();
            await creatureDataStore.UpdateItem(Creature);
        }
    }
}
