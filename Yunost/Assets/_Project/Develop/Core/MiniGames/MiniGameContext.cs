namespace MiniGames
{
    public class MiniGameContext
    {
        private TypesMiniGames _typeMiniGame;
        private TypeDifficultMiniGames _�urrentDifficult;
        private int _countItems;

        public TypesMiniGames TypeMiniGame { get => _typeMiniGame; set => _typeMiniGame = value; }
        public TypeDifficultMiniGames getCurrentDifficult {get => this._�urrentDifficult;}
        public int getCountItems { get => _countItems; }

        public MiniGameContext(TypesMiniGames inptTypeMiniGame, TypeDifficultMiniGames difficult, int countItems)
        {
            this._typeMiniGame = inptTypeMiniGame;
            this._�urrentDifficult = difficult;
            this._countItems = countItems;
        }
    }
}

