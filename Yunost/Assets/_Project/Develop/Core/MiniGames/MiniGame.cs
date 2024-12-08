namespace MiniGames
{
    abstract public class MiniGame
    {
        protected MiniGameContext _currentContext;
        public virtual void Init(MiniGameContext context) { }
        protected virtual void TakingAwayCharact() { }
        public virtual void TrackingProgressGame() { }

        public delegate void MiniGameEndHandler(MiniGamesResultInfo resultInfo);

        public event MiniGameEndHandler OnMiniGameEnd;
        protected virtual void CalculateResult(MiniGamesResultInfo resultInfo) => 
            OnMiniGameEnd?.Invoke(new MiniGamesResultInfo(resultInfo.getResultMiniGame, resultInfo.getNumLostItems));
        protected virtual void BuildUI() { }
    }
}
