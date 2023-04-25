using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwipperBackup.Models;

namespace SwipperBackup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingController : Controller
    {
        private Data.LocalStorage _localStorage;

        public ListingController(Data.LocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        // GET: api/Listings
        [HttpGet]
        public List<Listing> GetListings()
        {
            return _localStorage.Listings.ToList();
        }

        // GET: api/Listings/5
        [HttpGet("{id}")]
        public Listing GetListings(int id)
        {
            return _localStorage.Listings.FirstOrDefault(b => b.Id == id);
        }

        // PUT: api/Listings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutListing(int id, Listing listing)
        {
            if (id != listing.Id)
            {
                return BadRequest();
            }

            var existingListing = _localStorage.Listings.FirstOrDefault(u => u.Id == id);
            if (existingListing == null)
            {
                return NotFound();
            }

            existingListing.Id = listing.Id;
            existingListing.AnimalName = listing.AnimalName;
            existingListing.AnimalSpecies = listing.AnimalSpecies;
            existingListing.AnimalImageLink = listing.AnimalImageLink;


            // Update other properties as needed

            return NoContent();
        }

        // POST: api/Listings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Listing>> PostUser(Listing listing)
        {
            _localStorage.Listings.Add(listing);

            return CreatedAtAction(nameof(GetListings), new { id = listing.Id }, listing);
        }

        // DELETE: api/Listings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListing(int id)
        {
            foreach (Listing listing in _localStorage.Listings)
            {
                if (listing.Id == id)
                {
                    Listing foundListing = listing;
                    _localStorage.Listings.Remove(foundListing);
                }
            }

            return NoContent();
        }

        private bool ListingExists(int id)
        {
            return _localStorage.Listings.Any(e => e.Id == id);
        }
    }
}
