using JsonPlaceholderApi.Application.DTOs;

namespace JsonPlaceholderApi.Application.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> FetchAndSavePostsAsync();
        Task<IEnumerable<PostDto>> GetAllPostsAsync();
    }
}
