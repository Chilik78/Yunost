

namespace ProgressModul
{
    public class Task
    {
        private string _id;
        private string _name;
        private string _description;
        private bool _isDone = false;

        public Task(string name, string description, string id) {
            _name = name;
            _description = description;
            _id = id;
        }

        public void setDone()
        {
            _isDone = true;
        }

        public string Id
        {
            get => _id;
        }

        public string Name 
        {
            get => _name;
        }

        public string Description
        {
            get => _description;
        }

        public bool isDone
        {
            get => _isDone;
        }
    }
}
