using GestionBornesCollecte.Api.Data;
using GestionBornesCollecte.Api.Dtos;
using GestionBornesCollecte.Api.Dtos.Common;
using GestionBornesCollecte.Api.Models;
using GestionBornesCollecte.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


        // GET: api/Bennes

        [HttpGet("GetBennes")]
        public async Task<ActionResult<IEnumerable<BenneDto>>> GetAll()
        {
            var bennes = await _context.Bennes
                .Select(b => new BenneDto
                {
                    Id = b.Id,
                    Nom = b.Nom,
                    Localisation = b.Localisation,
                    CapaciteMax = b.CapaciteMax
                })
                .ToListAsync();

            return Ok(bennes);
        }


        // GET: api/Bennes/overview

        [HttpGet("overview")]
        public async Task<IActionResult> GetOverview([FromServices] IBenneService benneService)
        {
            var overview = await benneService.GetOverviewAsync();
            return Ok(overview);
        }


        // GET: api/Bennes/{id}

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BenneDetailDto>> GetById(int id)
        {
             var benne = await _context.Bennes
            .Where(b => b.Id == id)
            .Select(b => new
            {
                b.Id,
                b.Nom,
                b.Localisation,
                b.CapaciteMax,
                DerniereMesure = b.Mesures
                .OrderByDescending(m => m.Timestamp)
                .Select(m => new MesureDto
                {
                    Timestamp = m.Timestamp,
                    NiveauRemplissage = m.NiveauRemplissage,
                    BatterieVolt = m.BatterieVolt
                })
                .FirstOrDefault()
            })
            .FirstOrDefaultAsync();

            if (benne == null)
                return NotFound();

            var result = new BenneDetailDto
            {
                Id = benne.Id,
                Nom = benne.Nom,
                Localisation = benne.Localisation,
                CapaciteMax = benne.CapaciteMax,
                DerniereMesure = benne.DerniereMesure
            };

            return Ok(result);
        }


        // GET: api/Bennes/{id}/mesures

        [HttpGet("{id}/mesures")]
        public async Task<ActionResult<PagedResult<MesureDto>>> GetMesures(int id, int page = 1, int pageSize = 50)
        {
            var query = _context.Mesures
                .Where(m => m.BenneId == id)
                .OrderByDescending(m => m.Timestamp);

            var total = await query.CountAsync();

            var mesures = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(m => new MesureDto
                {
                    NiveauRemplissage = m.NiveauRemplissage,
                    BatterieVolt = m.BatterieVolt,
                    Timestamp = m.Timestamp
                })
                .ToListAsync();

            return Ok(new PagedResult<MesureDto>
            {
                Total = total,
                Page = page,
                PageSize = pageSize,
                Items = mesures
            });
        }


        // POST: api/Bennes

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBenneDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var benne = new Benne
            {
                Nom = dto.Nom,
                Localisation = dto.Localisation,
                CapaciteMax = dto.CapaciteMax
            };

            _context.Bennes.Add(benne);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = benne.Id },
                benne
            );
        }

        // PUT: api/Bennes/{id}

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateBenneDto dto)
        {
            var benne = await _context.Bennes.FindAsync(id);
            if (benne == null)
                return NotFound();

            if (dto.Nom != null)
                benne.Nom = dto.Nom;

            if (dto.Localisation != null)
                benne.Localisation = dto.Localisation;

            if (dto.CapaciteMax.HasValue)
                benne.CapaciteMax = dto.CapaciteMax.Value;

            await _context.SaveChangesAsync();
            return NoContent();
        }


        // DELETE: api/Bennes/{id}

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var benne = await _context.Bennes.FindAsync(id);
            if (benne == null)
                return NotFound();

            _context.Bennes.Remove(benne);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
