using JsonPlaceholderApi.Domain.Entities;

namespace JsonPlaceholderApi.Domain.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post> GetByIdAsync(int id);
        Task<IEnumerable<Post>> GetPostsByUserIdAsync(int userId);
        Task AddAsync(Post post);
        Task UpdateAsync(Post post);
        Task DeleteAsync(int id);
    }
}
