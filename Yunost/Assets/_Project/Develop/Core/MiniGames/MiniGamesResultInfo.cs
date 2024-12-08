namespace MiniGames
{
    public class MiniGamesResultInfo
    {
        private TypeResultMiniGames _resultMiniGame;
        private int _numLostItems;

        public TypeResultMiniGames getResultMiniGame { get => _resultMiniGame; }
        public int getNumLostItems { get => _numLostItems; }

        public MiniGamesResultInfo(TypeResultMiniGames resultMiniGame, int numLostItems)
        {
            _resultMiniGame = resultMiniGame;
            _numLostItems = numLostItems;
        }
    }
}

