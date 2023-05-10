using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwipperBackup.Data;
using SwipperBackup.Models;
using DevOne.Security.Cryptography.BCrypt;

namespace SwipperBackup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private Data.DatabaseContext _context;

        public UserController(Data.DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public User GetUsers(int id)
        {
            return _context.Users.FirstOrDefault(b => b.Id == id);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmailTaken(user.Email))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (!EmailTaken(user.Email))
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
            }
            else
            {
                return NoContent();
            }

            
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _context.Users.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            _context.Users.Remove(brand);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        [Route("/api/LikeAnimal")]
        public void LikeAnimal(int userId, int animalId)
        {
            _context.Users.Find(userId).LikedAnimals += "," + animalId;
            _context.SaveChanges();
        }

        [HttpPost]
        [Route("/api/DislikeAnimal")]
        public void DislikeAnimal(int userId, int animalId)
        {
            var user = _context.Users.Find(userId);

            if (user != null)
            {
                string likedAnimals = user.LikedAnimals;

                // Remove the disliked animal from the likedAnimals string
                string newLikedAnimalsString = string.Join(",", likedAnimals.Split(',')
                    .Where(animal => animal != animalId.ToString()));

                user.LikedAnimals = newLikedAnimalsString;
                _context.SaveChanges();
            }
        }

        [HttpGet]
        [Route("/api/User/GetFavoriteListings")]
        public List<Listing> GetFavoriteListings(int userId)
        {
            User foundUser = _context.Users.FirstOrDefault(b => b.Id == userId);

            List<int> foundFavoriteIds = GetIdsFromString(foundUser.LikedAnimals);
            List<Listing> foundFavoriteListings = new List<Listing>();
            foreach (int id in foundFavoriteIds)
            {
                foundFavoriteListings.Add(_context.Listings.FirstOrDefault(b => b.Id == id));
            }
            return foundFavoriteListings;
        }

        [HttpPost]
        [Route("/api/Authenticate")]
        public int Authenticate(string email, string password)
        {
            // Retrieve the user from the database using the provided email
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user != null)
            {
                // Compare the provided password with the password stored in the database
                if (user.Password == password)
                {
                    return user.Id;
                }
            }

            return 0;
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


        private bool EmailTaken(string email)
        {
            return _context.Users.Any(e => e.Email == email);
        }
    }
}
