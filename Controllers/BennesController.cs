using GestionBornesCollecte.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace GestionBornesCollecte.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class BennesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BennesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetBennes")]
        public IActionResult GetAll()
        {
            var bennes = _context.Bennes.ToList();
            return Ok(bennes);
        }
    }
}
