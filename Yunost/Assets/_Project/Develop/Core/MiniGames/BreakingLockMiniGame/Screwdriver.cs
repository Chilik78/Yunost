using UnityEngine;

namespace MiniGames 
{ 
    namespace BreakingLock
    {
        public class Screwdriver
        {
            private GameObject _screwdriver; // GameObject отвертки

            public Screwdriver()
            {
                _screwdriver = GameObject.Find("Screwdriver_Tool");
            }

            public void Rotate(float currPosPick)
            {
                if (currPosPick < 0)
                {
                    _screwdriver.transform.localEulerAngles = new Vector3(0, 20, 0);
                }
                else
                {
                    _screwdriver.transform.localEulerAngles = new Vector3(0, -10, 0);
                }
            }
        }
    }
}

