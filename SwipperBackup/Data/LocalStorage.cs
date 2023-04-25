using SwipperBackup.Models;

namespace SwipperBackup.Data
{
    public class LocalStorage
    {
        public List<User> Users { get; set; }
        public List<Listing> Listings { get; set; }

        public LocalStorage()
        {
            //TODO: change this to actually retrieve data
            Users = new List<User>();
            Listings = new List<Listing>();
        }
    }
}
