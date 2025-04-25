

using UnityEngine;

namespace ProgressModul
{
    public class TimeControl : ISaveLoadObject
    {
        private int _currentTime;
        private int _hours;
        private int _minutes;
        public delegate void TimeChangedHandler(int time, int h, int m);
        public event TimeChangedHandler TimeChanged;


        public int Hours { get => _hours; private set => _hours = value; }
        public int Minutes { get => _minutes; private set => _minutes = value; }

        public TimeControl() { }
        public TimeControl(int h, int m)
        {
            SetTimeFormat(h, m);
        }

        public void SetTimeFormat(int h, int m)
        {
            Hours = h;
            Minutes = m;
            float hf = (m + 60 * h) / 60f;
            CurrentTime = (int)(hf / 24f * 1000f);

            if (TimeChanged != null)
            {
                TimeChanged(CurrentTime, Hours, Minutes);
            }
        }

        public void AddTime(int h, int m)
        {
            float hf = (m + 60 * h) / 60f;
            CurrentTime += (int)(hf / 24f * 1000f);
        }

        public void AddTime(int time)
        {
            CurrentTime = CurrentTime + time;
        }

        public string ComponentSaveId => "TimeControl";

        public SaveLoadData GetSaveLoadData()
        {
            return new TimeControlSaveLoadData(ComponentSaveId, CurrentTime, Hours, Minutes);
        }

        public void RestoreValues(SaveLoadData loadData)
        {
            if (loadData?.Data == null || loadData.Data.Length < 3)
            {
                Debug.LogError($"Can't restore values.");
                return;
            }

            // [0] - (field)
            // [1] - (filed)
            // [2] - (field)

            CurrentTime = int.Parse(loadData.Data[0].ToString());

            Hours = int.Parse(loadData.Data[1].ToString());

            Minutes = int.Parse(loadData.Data[2].ToString());
        }

        public void SetDefault()
        {
            
        }

        public int CurrentTime
        {
            get => _currentTime;
            set
            {
                if (value > 0)
                {
                    _currentTime = value;
                }
            }
        }

       
    }
}
