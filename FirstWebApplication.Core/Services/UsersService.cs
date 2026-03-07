using Common;
using ServiceContracts;

namespace Services
{
    // business logic must come in services
    public class UsersService : IUsersService, IDisposable
    {
        private List<string> _users;

        public UsersService()
        {
            _users = new List<string>()
            {
                "Salma",
                "Soheil"
            };
        }

        public List<string> GetUsers()
        {
            return _users;
        }

        // called before service destruction
        public void Dispose()
        {
            Util.printValue("OnDispose of Users Service being called.");
        }
    }
}