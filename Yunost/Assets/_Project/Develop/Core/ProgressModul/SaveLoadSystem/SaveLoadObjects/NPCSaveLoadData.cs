

namespace ProgressModul
{
     class NPCSaveLoadData : SaveLoadData
    {
        public NPCSaveLoadData(string id, int loyalty, int maxLoyalty) : base(id, new object[] { loyalty, maxLoyalty }) { }
    }
}
