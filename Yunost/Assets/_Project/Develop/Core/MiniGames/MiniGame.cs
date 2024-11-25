namespace MiniGames 
{
    abstract public class MiniGame
    {
        protected MiniGameContext _currentContext;
        public virtual void Init(MiniGameContext context) { }
        protected virtual void TakingAwayCharact() { }
        public virtual void TrackingProgressGame() { }
        protected virtual void CalculateResult() { }
        protected virtual void BuildUI() { }
    }
}
