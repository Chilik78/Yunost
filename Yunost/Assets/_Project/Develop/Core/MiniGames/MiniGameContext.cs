namespace MiniGames
{
    public class MiniGameContext
    {
        private TypesMiniGames _typeMiniGame;
        private float _maxTimeInSeconds;
        private TypeDifficultMiniGames _�urrentDifficult = TypeDifficultMiniGames.NoUse;

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
        public MiniGameContext(TypesMiniGames inptTypeMiniGame, float inptMaxTimeInSeconds)
        {
            this._typeMiniGame = inptTypeMiniGame;   
            this._maxTimeInSeconds = inptMaxTimeInSeconds;   
        }
    }
}

