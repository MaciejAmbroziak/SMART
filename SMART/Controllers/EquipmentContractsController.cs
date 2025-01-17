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
    public class EquipmentContractsController : ControllerBase
    {
        private readonly DomainDbContext _context;

        public EquipmentContractsController(DomainDbContext context)
        {
            _context = context;
        }

        // GET: api/EquipmentContracts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentContract>>> GetEquipmentContracts()
        {
            return await _context.EquipmentContracts.ToListAsync();
        }

        // GET: api/EquipmentContracts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentContract>> GetEquipmentContract(int id)
        {
            var equipmentContract = await _context.EquipmentContracts.FindAsync(id);

            if (equipmentContract == null)
            {
                return NotFound();
            }

            return equipmentContract;
        }

        // PUT: api/EquipmentContracts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipmentContract(int id, EquipmentContract equipmentContract)
        {
            if (id != equipmentContract.Id)
            {
                return BadRequest();
            }

            _context.Entry(equipmentContract).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipmentContractExists(id))
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

        // POST: api/EquipmentContracts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EquipmentContract>> PostEquipmentContract(EquipmentContract equipmentContract)
        {
            _context.EquipmentContracts.Add(equipmentContract);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEquipmentContract", new { id = equipmentContract.Id }, equipmentContract);
        }

        // DELETE: api/EquipmentContracts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipmentContract(int id)
        {
            var equipmentContract = await _context.EquipmentContracts.FindAsync(id);
            if (equipmentContract == null)
            {
                return NotFound();
            }

            _context.EquipmentContracts.Remove(equipmentContract);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EquipmentContractExists(int id)
        {
            return _context.EquipmentContracts.Any(e => e.Id == id);
        }
    }
}
