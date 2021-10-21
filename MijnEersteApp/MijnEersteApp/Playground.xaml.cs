using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MijnEersteApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Playground : ContentPage
    {

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
    }
}