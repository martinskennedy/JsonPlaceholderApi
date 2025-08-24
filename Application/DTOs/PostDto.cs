namespace JsonPlaceholderApi.Application.DTOs
{
    public class PostDto
    {
        public int Id { get; set; } // Id da API
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
