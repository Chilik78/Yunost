using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public Item item;
    

    public GameObject textObject;
    public GameObject cylinderObject;
    public float activationDistance = 1f;
    public Transform objectFind;
    private bool isTextActive = false;
    public Transform player;

    private void Start()
    {
        

        if (textObject != null && cylinderObject != null)
        {
            textObject.SetActive(false);
            cylinderObject.SetActive(false);
        }

        if (objectFind.name == "Plane")
        {
            objectFind = GameObject.Find("Plane").transform;
        }
        else if (objectFind.name == "Screwdriver_Tool")
        {
            objectFind = GameObject.Find("Screwdriver_Tool").transform;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            //InventoryManager inventory = other.GetComponent<InventoryManager>();
            InventoryManager inventory = GameObject.Find("GameSystems").GetComponent<InventoryManager>();
            if (inventory != null)
            {
                inventory.ShowPickupUI(this);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") )
        {

            //InventoryManager inventory = other.GetComponent<InventoryManager>();
            InventoryManager inventory = GameObject.Find("GameSystems").GetComponent<InventoryManager>();
            if (inventory != null)
            {
                inventory.HidePickupUI();
            }

        }
    }

    private void Update()
    {
        if (objectFind != null && textObject != null && cylinderObject != null)
        {
            float distance = Vector3.Distance(objectFind.position, player.position);

            if (distance <= activationDistance && !isTextActive)
            {
                textObject.SetActive(true);
                cylinderObject.SetActive(true);
                //Debug.Log($"Объект в зоне . Дистанция: {distance}"); 
            }
            else if (distance > activationDistance)
            {
                textObject.SetActive(false);
                cylinderObject.SetActive(false);
                isTextActive = false;
                //Debug.Log("Объект не в зоне");
            }

            if (Input.GetKeyDown(KeyCode.F) && textObject.activeSelf)
            {
                textObject.SetActive(false);
                cylinderObject.SetActive(false);
                isTextActive = true;

                //Debug.Log("Объект в инвентаре");
            }
        }

    }



}
