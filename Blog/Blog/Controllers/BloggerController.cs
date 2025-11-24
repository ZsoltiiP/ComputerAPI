using Blog.Models.DtoS;
using BlogApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloggerController : ControllerBase
    {
        [HttpPost]
        public ActionResult AddNewBlogger(AddBloggerDto blogger)
        {
            try
            {
                var newBlogger = new Blogger
                {
                    Name = blogger.Name,
                    Password = blogger.Password,
                    Email = blogger.Email
                };

                using (var context = new BlogDbContext())
                {
                    if (newBlogger != null)
                    {
                        context.bloggers.Add(newBlogger);
                        context.SaveChanges();
                        return StatusCode(201, new { message = "Sikeres hozzáadás.", result = newBlogger });
                    }

                    return NotFound(new { message = "Nincs blogger.", result = "" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, result = "" });
            }
        }

        [HttpGet]
        public ActionResult GetAllBlogger()
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    return Ok(new { message = "Sikeres lekérdezés.", result = context.bloggers.ToList() });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message, result = "" });
            }
        }

        [HttpGet("byid")]
        public ActionResult GetBloggerById(int id)
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    var blogger = context.bloggers.FirstOrDefault(blogger => blogger.Id == id);

                    if (blogger != null)
                    {
                        return Ok(new { message = "Sikeres lekérdezés.", result = blogger });
                    }

                    return NotFound(new { message = "Nincs ilyen bloggers.", result = blogger });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message, result = "" });
            }
        }

        [HttpDelete]
        public ActionResult DeleteBlogger(int id)
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    var blogger = context.bloggers.FirstOrDefault(blogger => blogger.Id == id);

                    if (blogger != null)
                    {
                        context.bloggers.Remove(blogger);
                        context.SaveChanges();
                        return Ok(new { message = "Sikeres törlés.", result = blogger });
                    }

                    return NotFound(new { message = "Nincs ilyen bloggers.", result = blogger });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message, result = "" });
            }
        }

        [HttpPut]
        public ActionResult UpdateBlogger(int id, UpdateBloggerDto blogger)
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    var existingBlogger = context.bloggers.FirstOrDefault(x => x.Id == id);

                    if (existingBlogger != null)
                    {
                        existingBlogger.Name = blogger.Name;
                        existingBlogger.Password = blogger.Password;
                        existingBlogger.Email = blogger.Email;

                        context.bloggers.Update(existingBlogger);
                        context.SaveChanges();
                        return Ok(new { message = "Sikeres frissítés.", result = existingBlogger });
                    }

                    return NotFound(new { message = "Nincs ilyen bloggers.", result = existingBlogger });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message, result = "" });
            }
        }
        //Összetett lekérdezések

        [HttpGet("withPosts")]
        public ActionResult GetBloggerWithPosts()
        {
            try
            {
                using(var context = new BlogDbContext())
                {
                    var bloggerWithPosts = context.bloggers.Include(x => x.Posts).ToList();
                    return Ok(new { message = "Sikeres frissítés.", result = bloggerWithPosts });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, result = "" });
            }
        }
        [HttpGet("withOwnPosts")]
        public ActionResult GetBloggerOwnPosts(int id)
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    var bloggerWithOwnPosts = context.bloggers.Include(x => x.Posts).FirstOrDefault(x => x.Id == id);
                    return Ok(new { message = "Sikeres lekérdezés.", result = bloggerWithOwnPosts });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, result = "" });
            }
        }

        [HttpGet("results")]
        public ActionResult GetBloggerResult(int id)
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    var bloggerNameWithCategory = context.bloggers.Include(x => x.Posts).FirstOrDefault(x =>x.Id == id);
                    var blogger = new { UserName = bloggerNameWithCategory.Name, Categories = bloggerNameWithCategory.Posts.Select() };
                    return Ok(new { message = "Sikeres lekérdezés.", result = bloggerNameWithCategory });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, result = "" });
            }
        }
    }
}