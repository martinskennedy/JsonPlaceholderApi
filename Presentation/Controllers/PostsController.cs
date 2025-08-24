using JsonPlaceholderApi.Application.DTOs;
using JsonPlaceholderApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JsonPlaceholderApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }


        [HttpPost("fetch")]
        public async Task<ActionResult<IEnumerable<PostDto>>> FetchAndSavePosts()
        {
            try
            {
                var posts = await _postService.FetchAndSavePostsAsync();
                if (posts == null || !posts.Any())
                {
                    return NotFound("Nenhum post foi encontrado");
                }
                return Ok(posts);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = $"Erro de validação: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno ao salvar posts: {ex.Message}" });
            }
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAll()
        {
            try
            {
                var posts = await _postService.GetAllPostsAsync();
                if (posts == null || !posts.Any())
                {
                    return NotFound("Nenhum Post foi encontrado");
                }
                return Ok(posts);
            }
            catch (ArgumentException ex) // Erros de regra de negócio (parâmetros inválidos, etc.)
            {
                return BadRequest(new { message = $"Erro de validação: {ex.Message}" });
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPostsByUserId(int userId)
        {
            try
            {
                var posts = await _postService.GetPostsByUserIdAsync(userId);
                if (posts == null || !posts.Any())
                {
                    return NotFound($"Nenhum Usuário foi encontrado com o Id: {userId}");
                }
                return Ok(posts);
            }
            catch (ArgumentException ex) // Erros de regra de negócio (parâmetros inválidos, etc.)
            {
                return BadRequest(new { message = $"Erro de validação: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

    }
}
