using System;
using System.IO;
using System.Threading.Tasks;
using BulkyBook.DataAccess.UnitOfWork;
using BulkyBook.Models.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            ProductViewModel productViewModel = new ProductViewModel()
            {
                Product = new ProductModel(),
                CategoryList = await _unitOfWork.Category.GetAll(),
                CoverTypeList = await _unitOfWork.CoverType.GetAll()
            };
            if(id == null)
            {
                // View Create
                return View(productViewModel);
            }
            productViewModel.Product = await _unitOfWork.Product.GetById(id.GetValueOrDefault());
            if (productViewModel.Product == null)
            {
                return NotFound();
            }
            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductViewModel req)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if(files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\product");
                    var extenstion = Path.GetExtension(files[0].FileName);

                    if (req.Product.ImageUrl != null)
                    {
                        //Remove old image
                        var imagePath = Path.Combine(webRootPath, req.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using(var filesStream = new FileStream(Path.Combine(uploads,fileName+extenstion), FileMode.Create))
                    {
                        files[0].CopyTo(filesStream);
                    }
                    req.Product.ImageUrl = @"\images\product\" + fileName + extenstion;
                }
                else
                {
                    if (req.Product.Id != 0)
                    {
                        ProductModel objFromDb = await _unitOfWork.Product.GetById(req.Product.Id);
                        req.Product.ImageUrl = objFromDb.ImageUrl;
                    }
                }

                if (req.Product.Id == 0)
                {
                    await _unitOfWork.Product.Add(req.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(req.Product);
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
            var listProduct = await _unitOfWork.Product.GetAll();
            return Json(new { data = listProduct });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var req = await _unitOfWork.Product.GetById(id);
            if(req == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _unitOfWork.Product.Delete(req);
            await _unitOfWork.SaveChanges();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
    }
}