using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SMART.Domain;

namespace SMART.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionFacilitiesController : ControllerBase
    {
        private readonly DomainDbContext _context;

        public ProductionFacilitiesController(DomainDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductionFacilities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductionFacility>>> GetProductionFacilities()
        {
            return await _context.ProductionFacilities.ToListAsync();
        }

        // GET: api/ProductionFacilities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductionFacility>> GetProductionFacility(int id)
        {
            var productionFacility = await _context.ProductionFacilities.FindAsync(id);

            if (productionFacility == null)
            {
                return NotFound();
            }

            return productionFacility;
        }

        // PUT: api/ProductionFacilities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductionFacility(int id, ProductionFacility productionFacility)
        {
            if (id != productionFacility.Id)
            {
                return BadRequest();
            }
            if (!CodeExists(productionFacility.Code, id))
            {
                return BadRequest();
            }
            _context.Entry(productionFacility).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductionFacilityExists(id))
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

        // POST: api/ProductionFacilities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductionFacility>> PostProductionFacility(ProductionFacility productionFacility)
        {
            _context.ProductionFacilities.Add(productionFacility);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductionFacility", new { id = productionFacility.Id }, productionFacility);
        }

        // DELETE: api/ProductionFacilities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductionFacility(int id)
        {
            var productionFacility = await _context.ProductionFacilities.FindAsync(id);
            if (productionFacility == null)
            {
                return NotFound();
            }

            _context.ProductionFacilities.Remove(productionFacility);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductionFacilityExists(int id)
        {
            return _context.ProductionFacilities.Any(e => e.Id == id);
        }

        private bool CodeExists(string code, int id)
        {
            var notTheSameCode = true;
            var similarCodeQuerry = _context.ProductionFacilities.Where(e => e.Code.Contains(code));
            similarCodeQuerry = similarCodeQuerry.Except(similarCodeQuerry.Where(a=>a.Id == id));   
            if (similarCodeQuerry.Any())
            {
                foreach (var similarProductionFacility in similarCodeQuerry)    
                {
                    var similarProductionFacilityCode = similarProductionFacility.Code;
                    similarProductionFacilityCode.Replace(code, "");
                    var stringsArray = similarProductionFacilityCode.Split(' ');
                    if (stringsArray.Any())
                    {
                        foreach (var s in stringsArray)
                        {
                            s.Trim(' ');
                            notTheSameCode = notTheSameCode & !(s.IsNullOrEmpty());
                        }
                    }
                }
            }
            return !notTheSameCode;
        }
    }
}
