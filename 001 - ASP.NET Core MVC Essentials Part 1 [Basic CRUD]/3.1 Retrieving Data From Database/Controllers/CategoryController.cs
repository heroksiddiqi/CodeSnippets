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
        // Start
        
        private readonly ApplicationDbContext _db; //imported App_Name.Data

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            
            // <Category> is the Dbset name for the model
            IEnumerable<Category> objCategoryList = _db.Categories; //imported model_name
            return View(objCategoryList);
        }

        // End
    }
}