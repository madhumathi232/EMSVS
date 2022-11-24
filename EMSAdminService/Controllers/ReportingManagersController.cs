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
    public class ReportingManagersController : ControllerBase
    {
        private readonly EmsContext _context;

        public ReportingManagersController(EmsContext context)
        {
            _context = context;
        }

        // GET: api/ReportingManagers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportingManager>>> GetReportingManagers()
        {
            return await _context.ReportingManagers.ToListAsync();
        }

        // GET: api/ReportingManagers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportingManager>> GetReportingManager(string id)
        {
            var reportingManager = await _context.ReportingManagers.FindAsync(id);

            if (reportingManager == null)
            {
                return NotFound();
            }

            return reportingManager;
        }

        // PUT: api/ReportingManagers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReportingManager(string id, ReportingManager reportingManager)
        {
            if (id != reportingManager.ManagerName)
            {
                return BadRequest();
            }

            _context.Entry(reportingManager).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportingManagerExists(id))
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

        // POST: api/ReportingManagers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReportingManager>> PostReportingManager(ReportingManager reportingManager)
        {
            _context.ReportingManagers.Add(reportingManager);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ReportingManagerExists(reportingManager.ManagerName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetReportingManager", new { id = reportingManager.ManagerName }, reportingManager);
        }

        // DELETE: api/ReportingManagers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportingManager(string id)
        {
            var reportingManager = await _context.ReportingManagers.FindAsync(id);
            if (reportingManager == null)
            {
                return NotFound();
            }

            _context.ReportingManagers.Remove(reportingManager);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReportingManagerExists(string id)
        {
            return _context.ReportingManagers.Any(e => e.ManagerName == id);
        }
    }
}
