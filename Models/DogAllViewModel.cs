using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;

namespace DgoApp.Models
{
    public class DogAllViewModel : Controller
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; } = null!;

        [Display(Name = "Age")]
        public int Age { get; set; }

        [Display(Name = "Breed")]
        public string Breed { get; set; } = null!;

        [Display(Name = "Dog Picture")]
        public string? Picture { get; set; }









        // GET: DogAllViewModel
        public ActionResult Index()
        {
            return View();
        }

        // GET: DogAllViewModel/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DogAllViewModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DogAllViewModel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DogAllViewModel/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DogAllViewModel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DogAllViewModel/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DogAllViewModel/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
