
namespace ProgressModul
{
    public class TimeControl
    {
        int _currentTime = 0;
        public delegate void TimeChangedHandler(int time, int h, int m);
        public event TimeChangedHandler TimeChanged;


        public int Hours { get; private set; }
        public int Minutes { get; private set; }


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
