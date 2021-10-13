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
        //stats
        private static float food = 50;
        private static float drink = 50;
        private static float attention = 50;
        private static float rest = 50;
        private static float social = 50;
        private static List<float> stats = new List<float>() { food, drink, attention, rest, social };

        //control stats
        private float lowerStatIntervan = 5000;
        private static float lowerStatAmount = 5;
        private float addStatAmount = 5;

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

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            for (int i = 0; i < stats.Count; i++)
            {
                stats[i] -= lowerStatAmount;
                if(stats[i] < 0)
                {
                    stats[i] = 0;
                }
                Console.WriteLine(stats[i]);
            }
        }

        private static void UpdateStats(object source, ElapsedEventArgs e)
        {
            App.Current.Resources["FillFood"] = stats[0] / 500 * 20;
            App.Current.Resources["FillColorFood"] = MoodColor(stats[0], 50);
            
            App.Current.Resources["FillDrink"] = stats[1] / 500 * 20;
            App.Current.Resources["FillColorDrink"] = MoodColor(stats[1], 50);
            
            App.Current.Resources["FillSleep"] = stats[2] / 500 * 20;
            App.Current.Resources["FillColorSleep"] = MoodColor(stats[2], 50);
            
            App.Current.Resources["FillPlay"] = stats[3] / 500 * 20;
            App.Current.Resources["FillColorPlay"] = MoodColor(stats[3], 50);
            
            App.Current.Resources["FillSocial"] = stats[4] / 500 * 20;
            App.Current.Resources["FillColorSocial"] = MoodColor(stats[4], 50);

            float mood = stats.Take(5).Sum();
            App.Current.Resources["LabelColor"] = MoodColor(mood, 230);
        }


        void Food_Clicked(object sender, System.EventArgs e)
        {
            stats[0] += addStatAmount;
            if (stats[0] > 100)
            {
                stats[0] = 100;
            }
            ((Button)sender).Text = $"Food: {stats[0]}";
        }
        void Drink_Clicked(object sender, System.EventArgs e)
        {
            stats[1] += addStatAmount;
            if (stats[1] > 100)
            {
                stats[1] = 100;
            }
            ((Button)sender).Text = $"Drink: {stats[1]}";
        }
        void Sleep_Clicked(object sender, System.EventArgs e)
        {
            stats[2] += addStatAmount;
            if (stats[2] > 100)
            {
                stats[2] = 100;
            }
            ((Button)sender).Text = $"Rest: {stats[2]}";
        }
        void Play_Clicked(object sender, System.EventArgs e)
        {
            stats[3] += addStatAmount;
            if (stats[3] > 100)
            {
                stats[3] = 100;
            }
            ((Button)sender).Text = $"Attention: {stats[3]}";
        }
        void Social_Clicked(object sender, System.EventArgs e)
        {
            stats[4] += addStatAmount;
            if (stats[4] > 100)
            {
                stats[4] = 100;
            }
            ((Button)sender).Text = $"Social: {stats[4]}";
        }
    }
}
