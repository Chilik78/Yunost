using System;

namespace ProgressModul
{
    public class TimeControl
    {
        int _currentTime = 0;
        public event Action TimeChanged;

        public TimeControl(int currentTime)
        {
            this.currentTime = currentTime;
        }

        public int currentTime
        {
            get => _currentTime;
            set
            {
                if (value > 0)
                {
                    _currentTime = value;
                    if (TimeChanged != null)
                    {
                        TimeChanged();
                    }
                }
            }
        }
    }
}
