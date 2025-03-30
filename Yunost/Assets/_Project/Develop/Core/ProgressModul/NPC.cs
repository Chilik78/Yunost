

using UnityEngine;

namespace ProgressModul
{
    public class NPC
    {
        private string _loyaltyKey = "loyalty";
        private string _maxLoayaltyKey = "max_loyalty";
        private string _name;

        private string _getKey(string key) => key + _name;

        public NPC(string name, int loyalty = 0, int maxLoayalty = 100)
        {
            _name = name;
            PlayerPrefs.SetInt(_getKey(_loyaltyKey), loyalty);
            PlayerPrefs.SetInt(_getKey(_maxLoayaltyKey), maxLoayalty);
        }

        public int MaxLoyalty
        {
            get => PlayerPrefs.GetInt(_getKey(_maxLoayaltyKey));
        }

        public int Loyalty
        {
            get => PlayerPrefs.GetInt(_getKey(_loyaltyKey));
            set
            {
                if (value >= 0 && value <= MaxLoyalty)
                {
                    PlayerPrefs.SetInt(_getKey(_loyaltyKey), value);
                }
            }
        }


    }
}


