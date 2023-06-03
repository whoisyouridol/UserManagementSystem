using Newtonsoft.Json;

namespace UserManagementSystem.DAL.Models.ResponseModels
{
    public class PostResponseModel
    {
        [JsonProperty("userId")]
        public long UserId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }
        [JsonIgnore]
        public List<CommentResponseModel> Comments{ get; set; }
    }
}
