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
        public string UserName { get; set; }
        public float Hunger { get; set; } = 0.5f;
        public float Thirst { get; set; } = 0.5f;
        public float Boredom { get; set; } = 0.5f;
        public float Loneliness { get; set; } = 0.5f;
        public float Stimulated { get; set; } = 0.5f;
        public float Tired { get; set; } = 0.5f;

        public event PropertyChangedEventHandler PropertyChanged;

        public void LowerStats(float _lowerAmount)
        {
            Hunger = Math.Max(Hunger - _lowerAmount, 0);
            Thirst = Math.Max(Thirst - _lowerAmount, 0);
            Boredom = Math.Max(Boredom - _lowerAmount, 0);
            Loneliness = Math.Max(Loneliness - _lowerAmount, 0);
            Stimulated = Math.Max(Stimulated - _lowerAmount, 0);
            Tired = Math.Max(Tired - _lowerAmount, 0);
        }

        public float TotalMood()
        {
            float mood =  Hunger + Thirst + Boredom + Loneliness + Stimulated + Tired;
            return mood;
        }

        public void Playground(float _higherAmount)
        {
            Stimulated += _higherAmount;
        }
    }
}
