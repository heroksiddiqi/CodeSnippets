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
                TempData["confirmation_msg"] = "Created Successfully";
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
                TempData["confirmation_msg"] = "Edited Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(any_name_obj);
            }
        }
        
        // GET for Delete
        public IActionResult Delete(int? Id)
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

        // POST for Delete
        [HttpPost,ActionName("DeletePOST")]
        /* "ActionName" is optional if "asp-action"
        is already set in the form*/
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? Id)
        {
            var any_name_obj = _db.Categories.Find(Id);

            if (any_name_obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(any_name_obj);
            _db.SaveChanges();
            //SYNTAX: TempData["Key_Name"] = "Output Message" 
            TempData["confirmation_msg"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
    /* Will might test EDIT routes with these 2 lines later
                var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == Id);
                var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == Id);
    */
}

