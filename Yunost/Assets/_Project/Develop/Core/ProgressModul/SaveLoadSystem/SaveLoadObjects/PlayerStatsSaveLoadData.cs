

namespace ProgressModul
{
    class PlayerStatsSaveLoadData : SaveLoadData
    {
        public PlayerStatsSaveLoadData(string id, int Health, int Stamina, float X, float Z, float RotY) : base(id, new object[] { Health, Stamina, X, Z, RotY })
        {

        }
    }
}
