using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    public class StudentStats
    {
        private StudentManager _manager = new StudentManager();

        public async Task<int> GetStudentCount()
        {
            return await Task.FromResult(_manager.GetAll.Count());
        }

        public async Task<int> GetStudentCountByGpa(int id)
        {
            return await Task.FromResult(_manager.GetStudentsByID(id).Count());
        }
    }

    public class StudentStatsSingleton
    {
        private static StudentStatsSingleton instance;
        public static StudentManager _manager;

        public async Task<int> GetStudentCount()
        {
            return await Task.FromResult(_manager.GetAll.Count());
        }

        public async Task<int> GetStudentCountByGpa(int id)
        {
            return await Task.FromResult(_manager.GetStudentsByID(id).Count());
        }

        private StudentStatsSingleton() { }

        public static StudentStatsSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StudentStatsSingleton();
                    _manager = new StudentManager();
                }
                return instance;
            }
        }
    }
}
