namespace MiniGames
{
    public class MiniGameContext
    {
        private TypesMiniGames _typeMiniGame;
        private float _maxTimeInSeconds;
        private TypeDifficultMiniGames _�urrentDifficult = TypeDifficultMiniGames.NoUse;
        private int _countItems;

        public TypesMiniGames TypeMiniGame { get => _typeMiniGame; set => _typeMiniGame = value; }
        public float MaxTimeInSeconds { get => _maxTimeInSeconds; set => _maxTimeInSeconds = value; }
        public TypeDifficultMiniGames �urrentDifficult 
        {
            get => this._�urrentDifficult;
            set 
            { 
                if(this._�urrentDifficult == TypeDifficultMiniGames.NoUse)
                {
                    this._�urrentDifficult = value;
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

