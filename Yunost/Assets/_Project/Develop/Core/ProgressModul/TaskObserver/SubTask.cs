

namespace ProgressModul
{
    public class SubTask
    {
        public string Id { get => _model.Id; }
        virtual public string Description { get => _model.Description; }
        public bool IsDone { get => _model.IsDone; private set => _model.IsDone = value; }
        public string Flow { get => _model.Flow; }
        public int StackIndex { get => _model.StackIndex; private set => _model.StackIndex = value; }
        public string[] Friends { get => _model.Friends; }

        private SubTaskModel _model;

        public SubTask(SubTaskModel model)
        {
            _model = model;
        }

        public virtual SubTaskModel GetModel
        {
            get => _model;
        }

        public virtual bool SetDone()
        {
            IsDone = true;
            return IsDone;
        }

        public void DeacreaseStackIndex()
        {
            StackIndex--;
        }

        public override string ToString()
        {
            return GetModel.ToString();
        }
    }

}
