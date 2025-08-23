using AutoMapper;
using JsonPlaceholderApi.Application.DTOs;
using JsonPlaceholderApi.Application.Interfaces;
using JsonPlaceholderApi.Domain.Entities;
using JsonPlaceholderApi.Domain.Interfaces;

namespace JsonPlaceholderApi.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, HttpClient httpClient, IMapper mapper)
        {
            _postRepository = postRepository;
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostDto>> FetchAndSavePostsAsync()
        {
            var postsFromApi = await _httpClient.GetFromJsonAsync<List<PostDto>>("https://jsonplaceholder.typicode.com/posts");

            if (postsFromApi != null)
            {
                var entities = _mapper.Map<List<Post>>(postsFromApi);
                foreach (var entity in entities)
                {
                    await _postRepository.AddAsync(entity);
                }
            }

            return postsFromApi;
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllAsync();
            return _mapper.Map<List<PostDto>>(posts);
        }
    }
}
