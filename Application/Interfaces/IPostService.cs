using JsonPlaceholderApi.Application.DTOs;

namespace JsonPlaceholderApi.Application.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> FetchAndSavePostsAsync();
        Task<IEnumerable<PostDtoTable>> GetAllPostsAsync();
        Task<IEnumerable<PostDtoTable>> GetPostsByUserIdAsync(int userId);
        Task<PostDtoTable?> UpdateAsync(int id, PostDtoTable postDto);
        Task<bool> DeleteAsync(int id);
    }
}
