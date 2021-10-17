using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MijnEersteApp
{
    public class Creature : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float Hunger { get; set; } = 0.5f;
        public float Thirst { get; set; } = 0.5f;
        public float Boredom { get; set; } = 0.5f;
        public float Loneliness { get; set; } = 0.5f;
        public float Stimulated { get; set; } = 0.5f;
        public float Tired { get; set; } = 0.5f;

        public event PropertyChangedEventHandler PropertyChanged;

        public void LowerStats()
        {

        }
    }
}
