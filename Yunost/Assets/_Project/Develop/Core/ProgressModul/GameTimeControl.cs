using UnityEngine;

namespace ProgressModul
{
    public class GameTimeControl : ISaveLoadObject
    {
        private int _mainTime = 0;
        private int _sideTime = 0;

        public delegate void GameTimeHandler(int time);

        public event GameTimeHandler MainTimeChanged;
        public event GameTimeHandler SideTimeChanged;

        public GameTimeControl() { }

        public GameTimeControl(int mainTime, int sideTime) 
        {
            _mainTime = mainTime;
            _sideTime = sideTime;
        }

        public int MainTime
        {
            get => _mainTime;

            set
            {
                if (value >= 0)
                {
                    _mainTime = value;
                    if (MainTimeChanged != null)
                    {
                        MainTimeChanged(MainTime);
                    }
                }
            }
        }

        public int SideTime
        {
            get => _sideTime;

            set
            {
                if (value >= 0)
                {
                    _sideTime = value;
                    if (SideTimeChanged != null)
                    {
                        SideTimeChanged(SideTime);
                    }
                }
            }
        }

        public string ComponentSaveId => "GameTimeControl";

        public SaveLoadData GetSaveLoadData()
        {
            return new GameTimeControlSaveLoadData(ComponentSaveId, MainTime, SideTime);
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

            MainTime = int.Parse(loadData.Data[0].ToString());

            SideTime = int.Parse(loadData.Data[1].ToString());
        }

        public void SetDefault()
        {

        }

    }
}
