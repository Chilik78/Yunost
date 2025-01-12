using System.Collections.Generic;

public static class ListOfItems
{
    // List для хранения названий предметов
    public static List<string> ItemNames = new List<string>();
    public static List<Item> autoInventoryItems = new List<Item>();
    public static bool ItemExists(string itemName)
    {
        return ItemNames.Contains(itemName);
    }

    public static void RemoveItemFromList(string name)
    {
        ItemNames.Remove(name);
        autoInventoryItems.RemoveAll(item => item.name == name);
    }
}