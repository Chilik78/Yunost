using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Crafting/Recipe")]
public class Recipe : ScriptableObject
{
    public Item inputItem1;
    public Item inputItem2;
    public Item resultItem;

    public bool CanCraft(Item item1, Item item2)
    {
        return (item1 == inputItem1 && item2 == inputItem2) ||
               (item1 == inputItem2 && item2 == inputItem1);
    }
}
