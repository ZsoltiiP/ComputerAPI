using Computer.Models.DtoS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Computer.Models.DtoS;
using Computer.Models;

namespace Computer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        [HttpPost]
        public ActionResult AddNewComputer(AddComputerDto computer)
        {
            try
            {
                var newComp = new Computer.Models.Computer
                {
                    Brand = computer.Brand,
                    Type = computer.Type,
                    Display = computer.Display,
                    Memory = computer.Memory,
                    OsId = computer.OsId
                };

                using (var context = new ComputerDbContext())
                {
                    if (newComp != null)
                    {
                        context.computers.Add(newComp);
                        context.SaveChanges();
                        return StatusCode(201, new { message = "Sikeres hozzáadás.", result = newComp });
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
        public ActionResult GetAllComputer()
        {
            try
            {
                using (var context = new ComputerDbContext())
                {
                    return Ok(new { message = "Sikeres lekérdezés.", result = context.computers.ToList() });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message, result = "" });
            }
        }
        [HttpGet("byid")]
        public ActionResult GetComputerById(int id)
        {
            try
            {
                using (var context = new ComputerDbContext())
                {
                    var computer = context.computers.FirstOrDefault(computer => computer.CompId == id);

                    if (computer != null)
                    {
                        return Ok(new { message = "Sikeres lekérdezés.", result = computer });
                    }

                    return NotFound(new { message = "Nincs ilyen computer.", result = computer });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message, result = "" });
            }
        }
        [HttpDelete]
        public ActionResult DeleteComputer(int id)
        {
            try
            {
                using (var context = new ComputerDbContext())
                {
                    var computer = context.computers.FirstOrDefault(computer => computer.CompId == id);

                    if (computer != null)
                    {
                        context.computers.Remove(computer);
                        context.SaveChanges();
                        return Ok(new { message = "Sikeres törlés.", result = computer });
                    }

                    return NotFound(new { message = "Nincs ilyen computer.", result = computer });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message, result = "" });
            }
        }
        [HttpPut]
        public ActionResult UpdateBlogger(int id, UpdateComputerDto computer)
        {
            try
            {
                using (var context = new ComputerDbContext())
                {
                    var existingComputer = context.computers.FirstOrDefault(x => x.CompId == id);

                    if (existingComputer != null)
                    {
                        existingComputer.Brand = computer.Brand;
                        existingComputer.Type = computer.Type;
                        existingComputer.Display = computer.Display;
                        existingComputer.Memory = computer.Memory;

                        context.computers.Update(existingComputer);
                        context.SaveChanges();
                        return Ok(new { message = "Sikeres frissítés.", result = existingComputer });
                    }

                    return NotFound(new { message = "Nincs ilyen computer.", result = existingComputer });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message, result = "" });
            }
        }
    }
}
