using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMART.Domain;

namespace SMART.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessEquipmentsController : ControllerBase
    {
        private readonly DomainDbContext _context;

        public ProcessEquipmentsController(DomainDbContext context)
        {
            _context = context;
        }

        // GET: api/ProcessEquipments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcessEquipment>>> GetProcessEquipments()
        {
            return await _context.ProcessEquipments.ToListAsync();
        }

        // GET: api/ProcessEquipments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcessEquipment>> GetProcessEquipment(int id)
        {
            var processEquipment = await _context.ProcessEquipments.FindAsync(id);

            if (processEquipment == null)
            {
                return NotFound();
            }

            return processEquipment;
        }

        // PUT: api/ProcessEquipments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProcessEquipment(int id, ProcessEquipment processEquipment)
        {
            if (id != processEquipment.Id)
            {
                return BadRequest();
            }

            _context.Entry(processEquipment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcessEquipmentExists(id))
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

        // POST: api/ProcessEquipments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProcessEquipment>> PostProcessEquipment(ProcessEquipment processEquipment)
        {
            _context.ProcessEquipments.Add(processEquipment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProcessEquipment", new { id = processEquipment.Id }, processEquipment);
        }

        // DELETE: api/ProcessEquipments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcessEquipment(int id)
        {
            var processEquipment = await _context.ProcessEquipments.FindAsync(id);
            if (processEquipment == null)
            {
                return NotFound();
            }

            _context.ProcessEquipments.Remove(processEquipment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProcessEquipmentExists(int id)
        {
            return _context.ProcessEquipments.Any(e => e.Id == id);
        }
    }
}
