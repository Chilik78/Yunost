using UnityEngine;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        public float speedOnWalk = 3f;
        public float speedOnRun = 5f;
        public float speedTurn = 360f;

        private Rigidbody _rb;
        private Vector3 _input;
        private float _speed;

        private bool _isFrezed = false;

        public delegate void MovementDelegate(Vector3 position, Quaternion rotation);
        public event MovementDelegate OnMove;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _input = new Vector3();
            _speed = speedOnWalk;
        }

        private void Update()
        {
            if(_isFrezed) return;
            Rotate();
            //Move();
        }

        private void FixedUpdate()
        {
            if (_isFrezed) return;
            Move();
        }

        public void SetFreezed(bool val)
        {
            _isFrezed = val;
            GetComponent<AnimationController>().Run(false);
        }

        private void Move()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            _input = new Vector3(moveHorizontal, 0.0f, moveVertical);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                _speed = speedOnRun;
            }
            else
            {
                _speed = speedOnWalk;
            }

            //_rb.MovePosition(transform.position + (transform.forward * _input.magnitude) * _speed * Time.deltaTime);
            _rb.AddForce(_input.ToIso() * _speed * Time.fixedDeltaTime, ForceMode.Force);
            if(OnMove != null) OnMove(transform.position, transform.rotation);
        }
        private void Rotate()
        {
            if (_input != Vector3.zero)
            {
                Vector3 relative = (transform.position + _input.ToIso()) - transform.position;
                var rotate = Quaternion.LookRotation(relative, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotate, speedTurn * Time.deltaTime);
                GetComponent<AnimationController>().Run(true);
            }
            else
            {
                GetComponent<AnimationController>().Run(false);
            }
        }
    }
}

