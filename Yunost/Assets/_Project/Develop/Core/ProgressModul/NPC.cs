

using UnityEngine;

namespace ProgressModul
{
    public class NPC : ISaveLoadObject
    {
        private string _name;

        private int _loyalty = 0;
        private int _maxLoyalty = 100;

        public NPC(string name, int loyalty = 0, int maxLoyalty = 100)
        {
            _name = name;
            _loyalty = loyalty;
            _maxLoyalty=maxLoyalty;
        }

        public string Name => _name;

        public int MaxLoyalty
        {
            get => _maxLoyalty;
        }

        public int Loyalty
        {
            get => _loyalty;
            set
            {
                if (value >= 0 && value <= MaxLoyalty)
                {
                    _loyalty = value;
                }
            }
        }

        public string ComponentSaveId => $"NPC_{Name}";

        public SaveLoadData GetSaveLoadData()
        {
            return new GameTimeControlSaveLoadData(ComponentSaveId, Loyalty, MaxLoyalty);
        }

        public void RestoreValues(SaveLoadData loadData)
        {
            if (loadData?.Data == null || loadData.Data.Length < 2)
            {
                Debug.LogError($"Can't restore values.");
                return;
            }

            // [0] - (field)
            // [1] - (filed)

            Loyalty = int.Parse(loadData.Data[0].ToString());

            _maxLoyalty = int.Parse(loadData.Data[1].ToString());
        }

        public void SetDefault()
        {

        }
    }
}


