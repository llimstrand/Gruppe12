namespace
{
    public interface IUserRepository
    {
        void Save(UserEntity use);
    }

    public class UserRepository : IUserRepository
    {
        private List<UserEntity> users;
        public UserRepository()
        {
            users = new List<UserEntity>();
        }

        public void Save(UserEntity use)
        {
            users.Add(user);
        }
    }

    public class UserEntity
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string EmployeeNumber { get; set; }
        public string Team { get; set; }
        public string Role { get; set; }
    }
}