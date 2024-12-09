using UnityEngine;

namespace Player
{
    public class AnimationController : MonoBehaviour
    {
        private Animator _anim;

        void Start()
        {
            _anim = GetComponent<Animator>();
        }

        public void Run(bool isWalk)
        {
            _anim.SetBool("Walk", isWalk);
        }
    }
}

