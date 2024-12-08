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
        public TypeDifficultMiniGames ÑurrentDifficult 
        {
            get => this._ñurrentDifficult;
            set 
            { 
                if(this._ñurrentDifficult == TypeDifficultMiniGames.NoUse)
                {
                    this._ñurrentDifficult = value;
                }
            } 
        }
        public int getCountItems { get => _countItems; }

        public MiniGameContext(TypesMiniGames inptTypeMiniGame, float inptMaxTimeInSeconds, int countItems)
        {
            this._typeMiniGame = inptTypeMiniGame;   
            this._maxTimeInSeconds = inptMaxTimeInSeconds;
            this._countItems = countItems;
        }
    }
}

