namespace MiniGames
{
    abstract public class MiniGame
    {
        protected MiniGameContext _currentContext;
        public virtual void Init(MiniGameContext context) { }
        protected virtual void TakingAwayCharact() { }
        public virtual void TrackingProgressGame() { }

        public delegate void MiniGameEndHandler(MiniGameResultInfo resultInfo);

        public event MiniGameEndHandler OnMiniGameEnd;
        protected virtual void CalculateResult(MiniGameResultInfo resultInfo) => 
            OnMiniGameEnd?.Invoke(new MiniGameResultInfo(resultInfo.getResultMiniGame, resultInfo.getNumLostItems));
        protected virtual void BuildUI() { }
    }
}
