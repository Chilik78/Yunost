

namespace ProgressModul
{
    public class CounterSubTask : SubTask
    {
        private readonly int _finalCount;
        private int _currentCount;
        public CounterSubTask(CounterSubTaskModel model) : base(model)
        {
            _finalCount = model.finalCount;
            _currentCount = model.currentCount;
        }

        public override SubTaskModel GetModel
        {
            get => new CounterSubTaskModel(_id, _description, _flow, _stackIndex, _finalCount, _currentCount, _isDone, _friends);
        }

        public void IncreaseCount()
        {
            _currentCount++;
            if(_finalCount == _currentCount) base.SetDone();
        }

        public override bool SetDone()
        {
            if( _isDone ) return _isDone;
            IncreaseCount();
            return _isDone;
        }

        public override string Description => $"{_description} {CurrentCount}/{FinalCount}";

        public int CurrentCount => _currentCount;
        public int FinalCount => _finalCount;
    }
}
