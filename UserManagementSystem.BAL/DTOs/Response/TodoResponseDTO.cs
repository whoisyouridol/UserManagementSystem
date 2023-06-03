namespace UserManagementSystem.BAL.DTOs.Response
{
    public class TodoResponseDTO
    {
        public long UserId { get; set; }
        public long Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
    }
}
