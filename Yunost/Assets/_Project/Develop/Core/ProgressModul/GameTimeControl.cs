using UnityEngine;

namespace ProgressModul
{
    public class GameTimeControl
    {
        private string _mainKey = "main_time";
        private string _sideKey = "side_time";

        public delegate void GameTimeHandler(int time);

        public event GameTimeHandler MainTimeChanged;
        public event GameTimeHandler SideTimeChanged;

        public GameTimeControl() { }

        public GameTimeControl(int mainTime, int sideTime) 
        {
            PlayerPrefs.SetInt(_mainKey, mainTime);
            PlayerPrefs.SetInt(_sideKey, sideTime);
        }

        public int MainTime
        {
            get => PlayerPrefs.GetInt(_mainKey);

            set
            {
                if (value >= 0)
                {
                    PlayerPrefs.SetInt(_mainKey, value);
                    if (MainTimeChanged != null)
                    {
                        MainTimeChanged(MainTime);
                    }
                }
            }
        }

        public int SideTime
        {
            get => PlayerPrefs.GetInt(_sideKey);

            set
            {
                if (value >= 0)
                {
                    PlayerPrefs.SetInt(_sideKey, value);
                    if (SideTimeChanged != null)
                    {
                        SideTimeChanged(SideTime);
                    }
                }
            }
        }
    }
}
