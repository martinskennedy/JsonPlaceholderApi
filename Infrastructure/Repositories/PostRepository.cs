using JsonPlaceholderApi.Domain.Entities;
using JsonPlaceholderApi.Domain.Interfaces;
using JsonPlaceholderApi.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace JsonPlaceholderApi.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _context;

        public PostRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task AddAsync(Post post)
        {
            var exists = await _context.Posts.AnyAsync(p => p.ExternalId == post.ExternalId);
            if (exists)
                throw new InvalidOperationException($"Post com ExternalId {post.ExternalId} já existe.");
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }
    }
}
