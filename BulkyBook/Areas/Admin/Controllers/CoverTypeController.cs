using System;
using System.Threading.Tasks;
using BulkyBook.DataAccess.UnitOfWork;
using BulkyBook.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
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
            CoverTypeModel coverType = new CoverTypeModel();
            if(id == null)
            {
                // View Create
                return View(coverType);
            }
            coverType = await _unitOfWork.CoverType.GetById(id.GetValueOrDefault());
            if(coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CoverTypeModel req)
        {
            if (ModelState.IsValid)
            {
                if(req.Id == 0)
                {
                    await _unitOfWork.CoverType.Add(req);
                }
                else
                {
                    _unitOfWork.CoverType.Update(req);
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
            var listCoverType = await _unitOfWork.CoverType.GetAll();
            return Json(new {data = listCoverType});
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var req = await _unitOfWork.CoverType.GetById(id);
            if(req == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _unitOfWork.CoverType.Delete(req);
            await _unitOfWork.SaveChanges();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
    }
}