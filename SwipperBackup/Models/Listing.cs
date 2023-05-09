namespace SwipperBackup.Models
{
    public class Listing
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string AnimalName { get; set; }
        public string AnimalSpecies { get; set; }
        public string AnimalBreed { get; set; }
        public string AnimalSize { get; set; }
        public string AnimalImageLink { get; set; }
        public int Price { get; set; }
        public bool IsMale { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public DateTime ListingDate { get; set; }

        public Listing(int id, int ownerId, string animalName, string animalSpecies, string animalBreed, string animalSize, string animalImageLink, int price, bool isMale, string description, string address, DateTime listingDate, int age)
        {
            Id = id;
            OwnerId = ownerId;
            AnimalName = animalName;
            AnimalSpecies = animalSpecies;
            AnimalBreed = animalBreed;
            AnimalImageLink = animalImageLink;
            AnimalSize = animalSize;
            Price = price;
            IsMale = isMale;
            Description = description;
            Address = address;
            ListingDate = listingDate;
            Age = age;
        }
    }
}