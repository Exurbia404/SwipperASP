# SwipperASP

A simple version of the Swipper API using ASP.net and local storage. Make sure that your pulling from the 'release' branch

### Folder structure:

* bin (this contains the data, will be relevant later on)
* Controllers
	* ListingController.cs
	* UserController.cs
* Data
	* LocalStorage.cs
* Model
	* Listing.cs
	* User.cs
* Program.cs
* Startup.cs

#### LocalStorage.cs

A simple class to store two lists on your local device. When initalized in Startup.cs this class will automatically load or create an empty lists, so no setup is required. It contains two functions:

**SaveLists()** Saves lists on drive

**LoadLists()** Loads lists from drive, or when no lists are found creates two empty ones ready for use

#### Finding Data (bin)

It might come in handy to actually see these lists. They are located in the bin folder under:
*SwipperASP\SwipperBackup\bin\Debug\net6.0\Data*


Here you will find Listings.json and Users.json (don't worry if they are not there yet, use the API and it will automatically create them for you)

When the project is running you can go to https://localhost:7145/api/listing or https://localhost:7145/api/user to see these files in the API.
