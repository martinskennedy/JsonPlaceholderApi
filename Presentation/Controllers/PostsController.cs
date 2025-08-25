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

        /// <summary>
        /// Lê os Posts da API externa e grava no Banco de Dados.
        /// </summary>
        [HttpPost("fetch")]
        public async Task<ActionResult<IEnumerable<PostDto>>> FetchAndSavePosts()
        {
            try
            {
                var posts = await _postService.FetchAndSavePostsAsync();
                if (posts == null || !posts.Any())
                {
                    return NotFound("Nenhum post foi inserido");
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

        /// <summary>
        /// Retorna todos os Posts do Banco de Dados.
        /// </summary>
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

        /// <summary>
        /// Consulta um Post existente pelo UserId.
        /// </summary>
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

        /// <summary>
        /// Atualiza um Post existente pelo Id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PostDto postDto)
        {
            try
            {
                if (postDto == null)
                {
                    return BadRequest("Dados Inválidos");
                }

                var updated = await _postService.UpdateAsync(id, postDto);

                if (updated == null)
                {
                    return NotFound($"Post com Id: {id} não encontrado");
                }
                return Ok(updated);

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

        /// <summary>
        /// Remove um Post pelo Id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _postService.DeleteAsync(id);

                if (!deleted)
                {
                    return NotFound($"Post com Id: {id} não encontrado");
                }

                return Ok("Registro deletado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro inesperado: {ex.Message}" });
            }
        }
    }
}
