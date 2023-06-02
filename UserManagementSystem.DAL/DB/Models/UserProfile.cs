namespace UserManagementSystem.DAL.DB.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}