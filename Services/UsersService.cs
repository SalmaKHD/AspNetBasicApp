using ServiceContracts;

namespace Services
{
    // business logic must come in services
    public class UsersService: IUsersService
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
    }
}
