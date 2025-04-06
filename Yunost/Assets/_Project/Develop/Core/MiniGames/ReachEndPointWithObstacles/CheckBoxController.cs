using UnityEngine;
using UnityEngine.UI;

namespace MiniGames
{
    namespace ReachEndPointWithObstacles
    {
        public class CheckBoxController : MonoBehaviour
        {
            private int _countEndpoints;
            public void Init(int countEndpoints)
            {
                _countEndpoints = countEndpoints;
                HideSomeCheckboxes();
                TurnOffCheckBoxes();
            }

            private void HideSomeCheckboxes()
            {
                for(int i = _countEndpoints; i < 3; i++)
                {
                    GameObject.Find($"CheckBox {i + 1}").SetActive(false);
                }
            }

            private void TurnOffCheckBoxes()
            {
                for (int i = 0; i < _countEndpoints; i++)
                {
                    GameObject.Find($"CheckBox {i + 1}").transform.GetChild(0).transform.gameObject.SetActive(false);
                }
            }

            public void TurnOnCheckBox(int index)
            {
                GameObject.Find($"CheckBox {index}").transform.GetChild(0).transform.gameObject.SetActive(true);
            }
        }
    }
}