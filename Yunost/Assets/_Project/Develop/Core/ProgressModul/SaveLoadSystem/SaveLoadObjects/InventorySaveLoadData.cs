using System.Collections.Generic;

namespace ProgressModul
{
    public class InventorySaveLoadData : SaveLoadData
    {
        public InventorySaveLoadData(string id, List<string> ItemNames) : base(id, new object[] { ItemNames })
        {
        }
    }
}