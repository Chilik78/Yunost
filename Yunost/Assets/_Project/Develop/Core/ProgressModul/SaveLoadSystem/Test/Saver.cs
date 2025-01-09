using EasyButtons;
using UnityEngine;

namespace ProgressModul.Test
{
    /// <summary>
    /// Helper monobehaviour for saving from unity inspector.
    /// </summary>
    public class Saver : MonoBehaviour
    {
        [SerializeField] private InventoryHolder _inventoryHolder;
        TaskObserver taskObserver;

        private SaveLoadSystem _saveLoadSystem;

        private void Start()
        {
            _saveLoadSystem ??= new();
            TextAsset initTasksJson = Resources.Load<TextAsset>("InitTasks");
            taskObserver = new(initTasksJson.text);
            _saveLoadSystem.AddToSaveLoad(_inventoryHolder.Inventory);
            _saveLoadSystem.AddToSaveLoad(taskObserver);
        }

        [Button("Save to file")]
        private void Save()
        {
            _saveLoadSystem.SaveGame(SaveType.File);
        }

        [Button("Load from file")]
        private void Load()
        {
            _saveLoadSystem.LoadGame(SaveType.File);
            _inventoryHolder.UpdateItems();
            foreach (var task in taskObserver.GetInProgressTasks)
            {
                Debug.Log(task.Name);
                foreach(var subTask in task.GetInProgressSubTasks) Debug.Log(subTask.Description);
            } 
            foreach (var task in taskObserver.GetDoneTasks) Debug.Log(task.Description);
        }
    }
}