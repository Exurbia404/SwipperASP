using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwipperBackup.Models;

namespace SwipperBackup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingController : Controller
    {
        private Data.DatabaseContext _context;

        public ListingController(Data.DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Listings
        [HttpGet]
        public List<Listing> GetListings()
        {
            return _context.Listings.ToList();
        }

        // GET: api/Listings/5
        [HttpGet("{id}")]
        public Listing GetListings(int id)
        {
            return _context.Listings.FirstOrDefault(b => b.Id == id);
        }

        // PUT: api/Listings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListing(int id, Listing listing)
        {
            if (id != listing.Id)
            {
                return BadRequest();
            }

            _context.Entry(listing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListingExists(id))
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

        // POST: api/Listings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Listing>> PostUser(Listing listing)
        {
            _context.Listings.Add(listing);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetListings), new { id = listing.Id }, listing);
        }

        // DELETE: api/Listings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListing(int id)
        {
            var brand = await _context.Listings.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            _context.Listings.Remove(brand);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ListingExists(int id)
        {
            return _context.Listings.Any(e => e.Id == id);
        }
    }
}
