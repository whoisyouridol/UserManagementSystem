using UserManagementSystem.DAL.Models.ResponseModels;

namespace UserManagementSystem.BAL.DTOs.Response
{
    public class PostResponseDTO
    {
        public long UserId { get; set; }
        public long Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public List<CommentResponseModel> Comments { get; set; }
    }
}