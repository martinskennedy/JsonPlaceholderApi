namespace JsonPlaceholderApi.Domain.Entities
{
    public class Post
    {
        public int Id { get; set; } // Id do Banco
        public int ExternalId { get; set; } // Id vindo da API
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
