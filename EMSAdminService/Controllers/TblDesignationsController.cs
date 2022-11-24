using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EMSAdminService.Models;

namespace EMSAdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblDesignationsController : ControllerBase
    {
        private readonly EmsContext _context;

        public TblDesignationsController(EmsContext context)
        {
            _context = context;
        }

        // GET: api/TblDesignations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblDesignation>>> GetTblDesignations()
        {
            return await _context.TblDesignations.ToListAsync();
        }

        // GET: api/TblDesignations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblDesignation>> GetTblDesignation(string id)
        {
            var tblDesignation = await _context.TblDesignations.FindAsync(id);

            if (tblDesignation == null)
            {
                return NotFound();
            }

            return tblDesignation;
        }

        // PUT: api/TblDesignations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblDesignation(string id, TblDesignation tblDesignation)
        {
            if (id != tblDesignation.Designation)
            {
                return BadRequest();
            }

            _context.Entry(tblDesignation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblDesignationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TblDesignations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblDesignation>> PostTblDesignation(TblDesignation tblDesignation)
        {
            _context.TblDesignations.Add(tblDesignation);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TblDesignationExists(tblDesignation.Designation))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTblDesignation", new { id = tblDesignation.Designation }, tblDesignation);
        }

        // DELETE: api/TblDesignations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblDesignation(string id)
        {
            var tblDesignation = await _context.TblDesignations.FindAsync(id);
            if (tblDesignation == null)
            {
                return NotFound();
            }

            _context.TblDesignations.Remove(tblDesignation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblDesignationExists(string id)
        {
            return _context.TblDesignations.Any(e => e.Designation == id);
        }
    }
}
