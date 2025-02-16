

namespace ProgressModul
{
    public class CounterSubTask : SubTask
    {
        public int FinalCount { get => _model.FinalCount; }
        public int CurrentCount { get => _model.CurrentCount; private set => _model.CurrentCount = value; }
        private CounterSubTaskModel _model;
        public CounterSubTask(CounterSubTaskModel model) : base(model)
        {
            _model = model;
        }

        public override SubTaskModel GetModel
        {
            get => _model;
        }

        public void IncreaseCount()
        {
            CurrentCount++;
            if(FinalCount == CurrentCount) base.SetDone();
        }

        public override bool SetDone()
        {
            if( IsDone ) return IsDone;
            IncreaseCount();
            return IsDone;
        }

        public override string Description => $"{base.Description} {CurrentCount}/{FinalCount}";
    }
}
