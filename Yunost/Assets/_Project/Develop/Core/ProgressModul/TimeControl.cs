
namespace ProgressModul
{
    public class TimeControl
    {
        int _currentTime = 0;
        public delegate void TimeChangedHandler(int time);
        public event TimeChangedHandler TimeChanged;

        public TimeControl(int currentTime)
        {
            this.CurrentTime = currentTime;
        }

        public TimeControl(int h, int m)
        {
            SetTimeFormat(h, m);
        }

        public void SetTimeFormat(int h, int m)
        {
            float hf = (m + 60 * h) / 60f;
            CurrentTime = (int)(hf / 24f * 1000f);
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
                    if (TimeChanged != null)
                    {
                        TimeChanged(_currentTime);
                    }
                }
            }
        }
    }
}
