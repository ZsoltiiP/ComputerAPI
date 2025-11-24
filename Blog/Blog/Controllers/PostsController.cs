using Blog.Models.DtoS;
using BlogApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        [HttpPost]
        public ActionResult AddNewPost(AddPostDto addPostDto)
        {
            try
            {
                var post = new Posts
                {
                    Category = addPostDto.Category,
                    Post = addPostDto.Posts,
                    RegTime = DateTime.Now,
                    ModTime = DateTime.Now,
                    BloggerId = addPostDto.BloggerId
                };
                using (var context = new BlogDbContext()) 
                {
                    if (post != null)
                    {
                        context.posts.Add(post);
                        context.SaveChanges();
                        return StatusCode(201, new {message = "Sikeres hozzáadás", result = post});
                    }
                    return BadRequest(new { message = "Sikertelen hozzáadás!", Results = post});
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Sikertelen hozzáadás!", Results = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult GetAllPosts()
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    return Ok(new { message = "Sikeres lekérdezés.", result = context.posts.ToList() });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message, result = "" });
            }
        }
        [HttpGet("byid")]
        public ActionResult GetPostById(int id)
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    var posts = context.posts.FirstOrDefault(post => post.Id == id);

                    if (posts != null)
                    {
                        return Ok(new { message = "Sikeres lekérdezés.", result = posts });
                    }

                    return NotFound(new { message = "Nincs ilyen poszt.", result = posts });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message, result = "" });
            }
        }
        [HttpDelete]
        public ActionResult DeletePost(int id)
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    var post = context.posts.FirstOrDefault(post => post.Id == id);

                    if (post != null)
                    {
                        context.posts.Remove(post);
                        context.SaveChanges();
                        return Ok(new { message = "Sikeres törlés.", result = post });
                    }

                    return NotFound(new { message = "Nincs ilyen bloggers.", result = post });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message, result = "" });
            }
        }
        [HttpPut]
        public ActionResult UpdatePost(int id, UpdatePostDto updatePostDto)
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    var existingPost = context.posts.FirstOrDefault(x => x.Id == id);
                    if(existingPost != null)
                    {
                        existingPost.Category = updatePostDto.Category;
                        existingPost.Post = updatePostDto.Posts;
                        existingPost.ModTime = DateTime.Now;
                        context.posts.Update(existingPost);
                        context.SaveChanges();
                        return Ok(new { message = "Sikeres modositas.", result = existingPost });
                    }
                    return BadRequest(new { message = "Nincs ilyen bejegyzés!", result = existingPost });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, result = "" });
            }
        }
        
    }
}
