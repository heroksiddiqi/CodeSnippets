using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CategoryController : Controller
    {
        
        
        private readonly ApplicationDbContext _db; //imported App_Name.Data

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories; 
            return View(objCategoryList);
        }

        // GET for Create
        public IActionResult Create()
        {
            return View();
        }

        // POST for Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category any_name_obj) 
        {
            // Let's say we'll not let users to put same name and DisplayOrder
            if (any_name_obj.Name == any_name_obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and Order cannot be same.");
                ModelState.AddModelError("DisplayOrder", "Name and Order cannot be same.");
            }
            
            // To check is Form Datas are submitted correctly
            if (ModelState.IsValid)
            {
                _db.Categories.Add(any_name_obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(any_name_obj);
            }  
        }

        
        // GET for Edit_View
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            
                var categoryFromDb = _db.Categories.Find(Id);
                
                if (categoryFromDb == null)
                {
                    return NotFound();
                }
            
            return View(categoryFromDb);
        }

        // POST for Edit_View
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category any_name_obj)
        {
            if (any_name_obj.Name == any_name_obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and Order cannot be same.");
                ModelState.AddModelError("DisplayOrder", "Name and Order cannot be same.");
            }

            if (ModelState.IsValid)
            {
                
                _db.Categories.Update(any_name_obj);

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(any_name_obj);
            }
        }

        // End Edit
        /* Will test with these 2 line later
            var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == Id);
            var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == Id);
        */
    }
}