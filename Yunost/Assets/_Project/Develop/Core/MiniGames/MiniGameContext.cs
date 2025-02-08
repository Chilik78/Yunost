namespace MiniGames
{
    public class MiniGameContext
    {
        private TypesMiniGames _typeMiniGame;
        private float _maxTimeInSeconds;
        private TypeDifficultMiniGames _ñurrentDifficult = TypeDifficultMiniGames.NoUse;
        private int _countItems;

        public TypesMiniGames TypeMiniGame { get => _typeMiniGame; set => _typeMiniGame = value; }
        public float MaxTimeInSeconds { get => _maxTimeInSeconds; set => _maxTimeInSeconds = value; }
        public TypeDifficultMiniGames getCurrentDifficult {get => this._ñurrentDifficult;}
        public int getCountItems { get => _countItems; }

        public MiniGameContext(TypesMiniGames inptTypeMiniGame, TypeDifficultMiniGames difficult, float inptMaxTimeInSeconds, int countItems)
        {
            this._typeMiniGame = inptTypeMiniGame;
            this._ñurrentDifficult = difficult;
            this._maxTimeInSeconds = inptMaxTimeInSeconds;
            this._countItems = countItems;
        }
    }
}

