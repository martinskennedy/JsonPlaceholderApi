using JsonPlaceholderApi.Application.DTOs;

namespace JsonPlaceholderApi.Application.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> FetchAndSavePostsAsync();
        Task<IEnumerable<PostDto>> GetAllPostsAsync();
        Task<IEnumerable<PostDto>> GetPostsByUserIdAsync(int userId);
        Task<PostDto?> UpdateAsync(int id, PostDto postDto);
        Task<bool> DeleteAsync(int id);
    }
}
