

namespace ProgressModul
{
    public class NPC
    {
        int _loyalty;
        int _maxLoayalty;

        public NPC(int loyalty, int maxLoayalty = 100)
        {
            _loyalty = loyalty;
            _maxLoayalty = maxLoayalty;
        }

        public int Loyalty
        {
            get => _loyalty;
            set
            {
                if (_loyalty >= 0 && value <= _maxLoayalty)
                {
                    _loyalty = value; ;
                }
            }
        }


    }
}


