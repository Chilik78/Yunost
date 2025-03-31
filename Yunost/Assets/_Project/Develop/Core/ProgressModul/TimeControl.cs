
using UnityEngine;

namespace ProgressModul
{
    public class TimeControl
    {
        private string _currentTimeKey = "current_time";
        private string _hoursKey = "hours";
        private string _minutesKey = "minutes";
        public delegate void TimeChangedHandler(int time, int h, int m);
        public event TimeChangedHandler TimeChanged;


        public int Hours { get => PlayerPrefs.GetInt(_hoursKey); private set => PlayerPrefs.SetInt(_hoursKey, value); }
        public int Minutes { get => PlayerPrefs.GetInt(_minutesKey); private set => PlayerPrefs.SetInt(_minutesKey, value); }

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

        public int CurrentTime
        {
            get => PlayerPrefs.GetInt(_currentTimeKey);
            set
            {
                if (value > 0)
                {
                    PlayerPrefs.SetInt(_currentTimeKey, value);
                }
            }
        }
    }
}
