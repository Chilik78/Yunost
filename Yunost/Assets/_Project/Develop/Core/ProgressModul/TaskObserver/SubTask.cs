
using System.Collections.Generic;

namespace ProgressModul
{
    public class SubTask
    {
        protected readonly string _id;
        protected string _description;
        protected bool _isDone;
        protected readonly string _flow;
        protected int _stackIndex;
        protected string[] _friends;
        public SubTask(SubTaskModel model)
        {
            _id = model.id;
            _isDone = model.isDone;
            _description = model.description;
            _flow = model.flow;
            _stackIndex = model.stackIndex;
            _friends = model.friends;
        }

        public virtual SubTaskModel GetModel
        {
            get => new SubTaskModel(_id, _description, _flow, _stackIndex, _isDone, _friends);
        }

        public virtual bool SetDone()
        {
            _isDone = true;
            return _isDone;
        }

        public string Id
        {
            get => _id;
        }

        public bool IsDone
        {
            get => _isDone;
        }

        public virtual string Description
        {
            get => _description;
        }

        public void DeacreaseStackIndex()
        {
            _stackIndex--;
        }

        public int StackIndex => _stackIndex;

        public string Flow => _flow;

        public IEnumerable<string> Friends => _friends;

        public override string ToString()
        {
            return GetModel.ToString();
        }
    }

}
