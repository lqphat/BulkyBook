using System;
using System.Threading.Tasks;
using BulkyBook.DataAccess.UnitOfWork;
using BulkyBook.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            CategoryModel category = new CategoryModel();
            if(id == null)
            {
                // View Create
                return View(category);
            }
            category = await _unitOfWork.Category.GetById(id.GetValueOrDefault());
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CategoryModel req)
        {
            if (ModelState.IsValid)
            {
                if(req.Id == 0)
                {
                    await _unitOfWork.Category.Add(req);
                }
                else
                {
                    _unitOfWork.Category.Update(req);
                }
                await _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(req);
        }
        

        #region CALL API SERVICES
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var listCategory = await _unitOfWork.Category.GetAll();
            return Json(new {data = listCategory});
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var req = await _unitOfWork.Category.GetById(id);
            if(req == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _unitOfWork.Category.Delete(req);
            await _unitOfWork.SaveChanges();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
    }
}