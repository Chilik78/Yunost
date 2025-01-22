using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    public Transform target;
    public RectTransform arrow;
    public float height = 70f;


    void LateUpdate()
    {
        if (target != null && arrow != null)
        {

            Vector3 targetPosition = new Vector3(target.position.x, height, target.position.z);
            transform.position = targetPosition;
            transform.rotation = Quaternion.Euler(90f, 90f, 0f); //27.015 22.317 -6.253    90 90 0

            float playerRotationY = target.eulerAngles.y;
            arrow.rotation = Quaternion.Euler(0, 0, -playerRotationY + 90f); 
            Debug.LogWarning(playerRotationY);
        }
    }
}

