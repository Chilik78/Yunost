using UnityEngine;

public class MarkData : MonoBehaviour
{

    [SerializeField]
    private string _id;
    public string ID { get => _id; private set => _id = value; }

    public void SetObjectToMe(Transform objectTransform)
    {
        objectTransform.position = transform.position;
    }
}
