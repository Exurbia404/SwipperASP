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

        
        [HttpGet]
        [Route("/api/GetListingsForUser")]
        public List<Listing> GetListingsForUser(int id)
        {
            List<Listing> foundListings = _context.Listings.ToList();
            User user = _context.Users.FirstOrDefault(b => b.Id == id);

            //Filter own listings
            foundListings.RemoveAll(listing => listing.OwnerId == id);


            List<int> toFilterListingIds = GetIdsFromString(user.LikedAnimals);
            //Filter liked listings
            foundListings.RemoveAll(listing => toFilterListingIds.Contains(listing.Id));

            return foundListings;
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
            if (listing.AnimalName == "")
            {
                return BadRequest();
            }
            if (listing.AnimalImageLink.Length < 10)
            {
                listing.AnimalImageLink =
                    "https://storage.googleapis.com/sticker-prod/uR9NsRtlRXXKZHgQvPG7/9.thumb128.png";
            }
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

        
        [HttpGet]
        [Route("/api/GetListingByOwner")]
        public List<Listing> GetListingByOwner(int ownerId)
        {
            var listings = _context.Listings.Where(l => l.OwnerId == ownerId).ToList();

            return listings;
        }

        private List<int> GetIdsFromString(string idString)
        {
            List<int> ids = new List<int>();

            // Split the string by commas to get an array of substrings
            string[] idArray = idString.Split(',');

            // Loop through each substring and try to parse it as an integer
            foreach (string id in idArray)
            {
                int parsedId;
                if (int.TryParse(id, out parsedId))
                {
                    ids.Add(parsedId);
                }
            }

            return ids;
        }

        private bool ListingExists(int id)
        {
            return _context.Listings.Any(e => e.Id == id);
        }
    }
}
