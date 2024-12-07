using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 3f;
    public float rotationAngle = 720f;

    private Rigidbody rb;   

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    private void FixedUpdate()
    {
        if (DialogManager.GetInstance().dialogIsPlaying)
        {
            return;
        }
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        movement.Normalize();

        transform.Translate(movement * speed * Time.fixedDeltaTime, Space.World);

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationAngle * Time.deltaTime);
        }
    }
}
