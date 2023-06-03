using Newtonsoft.Json;

namespace UserManagementSystem.DAL.Models.ResponseModels
{
    public class AlbumResponseModel
    {
        [JsonProperty("userId")]
        public long UserId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}