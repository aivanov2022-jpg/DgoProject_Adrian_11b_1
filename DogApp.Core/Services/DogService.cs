using DgoApp.Data;

using DogApp.Core.Contracts;

using DogsApp.Infrastructure.Data.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogApp.Core.Services
{
    public class DogService : IDogService
    {
        private readonly ApplicationDbContext _context;

        public DogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(string name, int age, int breedId, string? picture)
        {
            var breed = _context.Breeds.Find(breedId);
            if (breed == null) return false;

            Dog item = new Dog
            {
                Name = name,
                Age = age,
                Breed = breed,
                Picture = picture
            };
            _context.Dogs.Add(item);
            return _context.SaveChanges() != 0;
        }
        public Dog? GetDogById(int dogId)
        {
            return _context.Dogs.Find(dogId);
        }

        public List<Dog> GetDogs()
        {
            List<Dog> dogs = _context.Dogs.ToList();
            return dogs;
        }

        public List<Dog> GetDogs(string searchStringBreed, string searchStringName)
        {
            IQueryable<Dog> query = _context.Dogs;
            if (!String.IsNullOrEmpty(searchStringBreed))
            {
                query = query.Where(d => d.Breed.Name.Contains(searchStringBreed));
            }
            if (!String.IsNullOrEmpty(searchStringName))
            {
                query = query.Where(d => d.Name.Contains(searchStringName));
            }
            return query.ToList();
        }
        public bool RemoveById(int dogId)
        {
            var dog = GetDogById(dogId);
            if (dog == null)
            {
                return false;
            }
            _context.Dogs.Remove(dog);
            return _context.SaveChanges() != 0;

        }
        public bool UpdateDog(int dogId, string name, int age, int breedId, string? picture)
        {
            var dog = GetDogById(dogId);
            if (dog == null)
            {
                return false;
            }

            var breed = _context.Breeds.Find(breedId);
            if (breed == null) return false;

            dog.Name = name;
            dog.Age = age;
            dog.Breed = breed;
            dog.Picture = picture;
            _context.Update(dog);
            return _context.SaveChanges() != 0;
        }
    }
}
