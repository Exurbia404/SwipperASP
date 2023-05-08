namespace SwipperBackup.Models
{
    public class Listing
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string AnimalName { get; set; }
        public string AnimalSpecies { get; set; }
        public string AnimalImageLink { get; set; }
        public int Price { get; set; }
        public bool IsMale { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public DateTime ListingDate { get; set; }

        public Listing(int id, int ownerId, string animalName, string animalSpecies, string animalImageLink, int price, bool isMale, string address, DateTime listingDate, int age)
        {
            Id = id;
            OwnerId = ownerId;
            AnimalName = animalName;
            AnimalSpecies = animalSpecies;
            AnimalImageLink = animalImageLink;
            Price = price;
            IsMale = isMale;
            Address = address;
            ListingDate = listingDate;
            Age = age;
        }
    }
}