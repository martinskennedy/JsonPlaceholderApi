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
            // Busca todos os posts da API
            var postsFromApi = await _httpClient.GetFromJsonAsync<List<PostDto>>("https://jsonplaceholder.typicode.com/posts");

            if (postsFromApi == null || !postsFromApi.Any())
                return new List<PostDto>(); // nenhum post na API

            // Pega todos os ExternalIds já existentes no banco
            var existingExternalIds = (await _postRepository.GetAllAsync())
                                       .Select(p => p.ExternalId)
                                       .ToHashSet(); // HashSet para busca rápida

            var newPosts = new List<Post>();

            foreach (var dto in postsFromApi)
            {
                // Só adiciona se ExternalId ainda não existir
                if (!existingExternalIds.Contains(dto.Id))
                {
                    var entity = _mapper.Map<Post>(dto);
                    newPosts.Add(entity);
                }
            }

            // Salva todos os novos posts
            foreach (var entity in newPosts)
            {
                await _postRepository.AddAsync(entity);
            }

            // Retorna apenas os posts que foram inseridos
            var insertedDtos = _mapper.Map<List<PostDto>>(newPosts);
            return insertedDtos;
        }

        public async Task<IEnumerable<PostDtoTable>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllAsync();
            return _mapper.Map<List<PostDtoTable>>(posts);
        }

        public async Task<IEnumerable<PostDtoTable>> GetPostsByUserIdAsync(int userId)
        {
            var posts = await _postRepository.GetPostsByUserIdAsync(userId);
            return _mapper.Map<List<PostDtoTable>>(posts);
        }

        public async Task<PostDtoTable?> UpdateAsync(int id, PostDtoTable postDtoTable)
        {
            var existingPost = await _postRepository.GetByIdAsync(id);

            if (existingPost == null)
            {
                return null;
            }

            existingPost.ExternalId = postDtoTable.ExternalId;
            existingPost.UserId = postDtoTable.UserId;
            existingPost.Title = postDtoTable.Title;
            existingPost.Body = postDtoTable.Body;

            await _postRepository.UpdateAsync(existingPost);

            return _mapper.Map<PostDtoTable>(existingPost);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _postRepository.GetByIdAsync(id);

            if (existing == null)
            {
                return false;
            }

            await _postRepository.DeleteAsync(id);

            return true;
        }
    }
}
