using JsonPlaceholderApi.Application.DTOs;
using JsonPlaceholderApi.Application.Interfaces;
using JsonPlaceholderApi.Application.Services;
using Microsoft.AspNetCore.Http;
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
            var posts = await _postService.FetchAndSavePostsAsync();
            return Ok(posts);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAll()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }
    }
}
