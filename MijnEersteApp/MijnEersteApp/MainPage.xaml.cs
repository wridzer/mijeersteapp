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

        //lowering stats
        private float lowerStatIntervan = 5000;
        private float LowerStatAmount = 5;

        public MainPage()
        {
            InitializeComponent();
            System.Timers.Timer removeStatTimer = new System.Timers.Timer();
            removeStatTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            removeStatTimer.Interval = lowerStatIntervan;
            removeStatTimer.Enabled = true;
        }

        public float TotalMood()
        {
            float color;
            float mood = food + drink + attention + rest + social;
            if (mood > 150) color = 000000;
            else color = 111111;
            return color;
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            for (int i = 0; i < stats.Count; i++)
            {
                stats[i] -= 5;
                Console.WriteLine(stats[i]);
            }
        }

        void Food_Clicked(object sender, System.EventArgs e)
        {
            stats[0]++;
            ((Button)sender).Text = $"Food: {stats[0]}";
        }
        void Drink_Clicked(object sender, System.EventArgs e)
        {
            stats[1]++;
            ((Button)sender).Text = $"Drink: {stats[1]}";
        }
        void Sleep_Clicked(object sender, System.EventArgs e)
        {
            stats[2]++;
            ((Button)sender).Text = $"Rest: {stats[2]}";
        }
        void Play_Clicked(object sender, System.EventArgs e)
        {
            stats[3]++;
            ((Button)sender).Text = $"Attention: {stats[3]}";
        }
        void Social_Clicked(object sender, System.EventArgs e)
        {
            stats[4]++;
            ((Button)sender).Text = $"Social: {stats[4]}";
        }
    }
}
