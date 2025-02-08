using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProgressModul;
using ProgressModul.Test;
using System.Collections.Generic;
using UnityEngine;

public class ListOfItems : ISaveLoadObject
{
    static public string PrefsKey => "Items";

    // List для хранения названий предметов
    public List<string> ItemNames = new List<string>();
    public List<Item> AutoInventoryItems = new List<Item>();

    public string ComponentSaveId => "ListOfItems";

    public bool ItemExists(string itemName)
    {
        return ItemNames.Contains(itemName);
    }

    public void RemoveItemFromList(string name)
    {
        ItemNames.Remove(name);
        AutoInventoryItems.RemoveAll(item => item.name == name);
    }

    public SaveLoadData GetSaveLoadData()
    {
        return new InventorySaveLoadData(ComponentSaveId, ItemNames);
    }

    public void RestoreValues(SaveLoadData loadData)
    {
        ItemNames.Clear();
        AutoInventoryItems.Clear();

        if (loadData?.Data == null || loadData.Data.Length < 1)
        {
            Debug.LogError($"Can't restore values.");
            return;
        }

        // [0] - (JArray) with items
        // [1] - (JArray) with items

        var items = ((JArray)loadData.Data[0]).ToObject<List<string>>();
        ItemNames.AddRange(items);

        foreach ( string name in ItemNames)
            AutoInventoryItems.Add(Resources.Load<Item>($"Items/{name}"));
    }

    static public ListOfItems CreateFromPrefs()
    {
        string serializedFile = PlayerPrefs.GetString(PrefsKey);
        PlayerPrefs.DeleteKey(PrefsKey);
        if (string.IsNullOrEmpty(serializedFile))
        {
            Debug.LogError($"Загруженный json {PrefsKey} пустой.");
            return null;
        }
        Debug.Log($"Загрузка json {PrefsKey}");
        SaveLoadData saveLoadData = JsonConvert.DeserializeObject<SaveLoadData>(serializedFile);
        ListOfItems listOfItems = new();
        listOfItems.RestoreValues(saveLoadData);

        return listOfItems;
    }

    public void SaveItemsToPrefs()
    {
        var saveLoadData = GetSaveLoadData();
        var serializedSaveFile = JsonConvert.SerializeObject(saveLoadData);
        PlayerPrefs.SetString(PrefsKey, serializedSaveFile);
    }

    public void SetDefault()
    {
        
    }
}