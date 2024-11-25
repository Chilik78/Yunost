namespace MiniGames
{
    public class MiniGameContext
    {
        private TypesMiniGames _typeMiniGame;
        private float _maxTimeInSeconds;
        private TypeDifficultMiniGames _ñurrentDifficult = TypeDifficultMiniGames.NoUse;

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
        public MiniGameContext(TypesMiniGames inptTypeMiniGame, float inptMaxTimeInSeconds)
        {
            this._typeMiniGame = inptTypeMiniGame;   
            this._maxTimeInSeconds = inptMaxTimeInSeconds;   
        }
    }
}

