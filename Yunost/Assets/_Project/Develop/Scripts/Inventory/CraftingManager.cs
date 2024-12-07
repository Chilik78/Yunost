using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public GameObject inventoryUI;
    public CraftingSlot inputSlot1;
    public CraftingSlot inputSlot2;
    public CraftingSlot resultSlot;

    public Recipe[] recipes; // ������ ���� ��������� ��������

    private void Start()
    {
        
    }

    public void TryCraft()
    {
        if (inputSlot1.currentItem == null || inputSlot2.currentItem == null)
        {
            Debug.Log("���� �� ������ ����!");
            return;
        }

        foreach (Recipe recipe in recipes)
        {
            if (recipe.CanCraft(inputSlot1.currentItem, inputSlot2.currentItem))
            {
                resultSlot.SetItem(recipe.resultItem);
                ClearInputSlots();
                return;
            }
        }

        Debug.Log("���������� ������� ������� �� ������� �����������.");
    }

    private void ClearInputSlots()
    {
        inputSlot1.ClearSlot();
        inputSlot2.ClearSlot();
    }
}
