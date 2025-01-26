using UnityEngine;

public class SeekItem : MonoBehaviour
{
    [SerializeField]
    private GameObject _item;
    [SerializeField]
    private GameObject[] _toDelete;
    private InventoryManager _inventoryManager;
    void Start()
    {
        _inventoryManager = GameObject.Find("GameSystems").GetComponent<InventoryManager>();
        _item.SetActive(false);

        /*var bag = Resources.Load<Item>("Items/bag");
        Debug.Log($"bag: {bag} {_inventoryManager}");
        _inventoryManager.AddItem(bag);*/
    }

    public void Put()
    {
        _item.SetActive(true);
        _inventoryManager.RemoveItemFromInventory("bag");

        foreach (var item in _toDelete)
        {
            Destroy(item);
        }
    }
}
