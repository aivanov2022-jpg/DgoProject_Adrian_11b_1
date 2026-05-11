using DgoApp.Data;
using DgoApp.Models;
using DgoApp.Models.Breed;
using DgoApp.Models.Dog;

using DogApp.Core.Contracts;

using DogsApp.Infrastructure.Data.Domain;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Net.Cache;

namespace DgoApp.Controllers
{
    public class DogController : Controller
    {
        private readonly IDogService  _dogService;
        private readonly IBreedService _breedService;
        public DogController(IDogService dogService, IBreedService breedService)
        {
            this._dogService = dogService;
            this._breedService = breedService;
        }

        public IActionResult Success()
        {
            return this.View();
        }






        // GET: DogController
        public ActionResult Index(string searchStringBreed, string searchStringName)
        {
           List<DogAllViewModel> dogs = _dogService.GetDogs(searchStringBreed, searchStringName)
                .Select(dogFromDb => new DogAllViewModel()
                {
                    Id = dogFromDb.Id,
                    Name = dogFromDb.Name,
                    Age = dogFromDb.Age,
                    BreedName = dogFromDb.Breed.Name,
                    Picture = dogFromDb.Picture
                }).ToList();
            return this.View(dogs);
        }




        // GET: DogController/Details/5
        public IActionResult Details(int id)
        {
           Dog? item = _dogService.GetDogById(id);
            if (item == null)
            {
                return NotFound();
            }
            DogDetailsViewModel dog = new DogDetailsViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Age = item.Age,
                BreedName = item.Breed.Name,
                Picture = item.Picture
            };
            return View(dog);
        }

        // GET: DogController/Create
        public ActionResult Create()
        {
            var dog = new DogCreateViewModel();
            dog.Breeds = _breedService.GetBreeds()
                .Select(c => new BreedPairViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();
                return View(dog);
        }

        // POST: DogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm]DogCreateViewModel dog)
        {
            if (ModelState.IsValid)
            {
               var createdId = _dogService.Create(dog.Name, dog.Age,dog.BreedId, dog.Picture);

                if (createdId)
                {
                    return this.RedirectToAction(nameof(Index));
                }

               
            }
            return this.View(); ;
        }

        // GET: DogController/Edit/5
        public IActionResult Edit(int id)
        {
            Dog? item = _dogService.GetDogById(id);
            if (item == null)
            {
                return NotFound();
            }
            
            DogEditViewModel dog = new DogEditViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Age = item.Age,
                BreedId = item.BreedId,
                Picture = item.Picture
            };
            dog.Breeds = _breedService.GetBreeds()
                .Select(c => new BreedPairViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();
            return View(dog);
        }

        // POST: DogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, DogEditViewModel bindingModel)
        {
            if (ModelState.IsValid)
            {
               var updated = _dogService.UpdateDog(id, bindingModel.Name, bindingModel.Age, bindingModel.BreedId, bindingModel.Picture);
                if (updated)
                {
                    return this.RedirectToAction("Index");
                }
            }
            return View(bindingModel);

        }

        // GET: DogController/Delete/5
        public IActionResult Delete(int id)
        {
            Dog? item = _dogService.GetDogById(id);
            if (item == null)
            {
                return NotFound();
            }
           
         
            DogDetailsViewModel dog = new DogDetailsViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Age = item.Age,
                BreedName = item.Breed.Name,
                Picture = item.Picture
            };
            return View(dog);
        }

        // POST: DogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
         var deleted = _dogService.RemoveById(id);
            if (deleted)
            {
                return this.RedirectToAction("Index", "Dog");
            }
            else
            {
                return View();
            }
              

        }
        
    }
}
