using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RahmanoEcorAPI.Data;
using RahmanoEcorAPI.Models;
using Microsoft.EntityFrameworkCore;
using RahmanoForOCS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace RahmanoEcorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private EcomersDbContext _db;
        private readonly IConfiguration _configuration;
        private string alamat;
        public ProductController(EcomersDbContext db, IConfiguration iConfig)
        {
            _db = db;
            _configuration = iConfig;
            alamat = (_configuration.GetValue<string>("MyAPISetting:ImageFolder")) + "product";
        }

        [HttpGet]
        public IActionResult Get()
        {
            var obj = _db.Product.Where(x => x.Is_Active == "1").Include(u => u.Category).Include(u => u.ApplicationType);
            if (obj.Count() == 0)
            {
                return NotFound("Product not fund");
            }
            return Ok(obj);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product product = _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).FirstOrDefault(u => u.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            product.imageUrl = alamat.Replace("wwwroot\\", "") + @"\" + product.imageUrl;
            product.imageUrl = product.imageUrl.Replace("\\", "/");

            return Ok(product);
        }

        [HttpGet("[action]/{id}")]
        public IActionResult GetVM(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategorySelectList = _db.Category.Select(i => new SelectListItem
                {
                    Text = i.CategoryName,
                    Value = i.CategoryId.ToString()
                }),
                ApplicationTypeSelectList = _db.ApplicationType.Select(i => new SelectListItem
                {
                    Text = i.TypeName,
                    Value = i.TypeId.ToString()
                })
            };

            if (id == 0)
            {
                return Ok(productVM);
            }
            else
            {
                Product prd = _db.Product.Find(id);
                if (prd != null)
                {
                    prd.imageUrl = alamat.Replace("wwwroot\\", "") + @"\" + prd.imageUrl;
                    prd.imageUrl = prd.imageUrl.Replace("\\", "/");
                    productVM.Product = prd;
                    return Ok(productVM);
                }
                else
                {
                    return NotFound(productVM);
                }
            }
        }

        [HttpGet("[action]")]
        public IActionResult GetAllVM()
        {
            HomeVM homeVM = new HomeVM()
            {
                Products = _db.Product.Where(u => u.Is_Active == "1").Include(u => u.Category).Include(u => u.ApplicationType),
                Categories = _db.Category
            };
            foreach (Product prod in homeVM.Products)
            {
                prod.imageUrl = _configuration.GetValue<string>("MyAPISetting:imgDir") + prod.imageUrl;
            }
            return Ok(homeVM);
        }

        [HttpGet("[action]/{id}")]
        public IActionResult GetDM(int id)
        {
            DetailsVM DetailsVM = new DetailsVM()
            {
                Product = _db.Product.Include(u => u.Category).Include(u => u.ApplicationType)
                .Where(u => u.Id == id).FirstOrDefault(),
                ExistsInCart = false
            };
            return Ok(DetailsVM);
        }

        [HttpPost("[action]")]
        public IActionResult selectChart([FromBody] List<int> prodInCart)
        {
            //IEnumerable<Product> prodList = _db.Product.Where(u => prodInCart.Contains(u.Id));
            //return Ok(prodList);

            var obj = _db.Product.Where(u => prodInCart.Contains(u.Id));
            if (obj.Count() == 0)
            {
                return NotFound("Product not fund");
            }
            return Ok(obj);
        }


        [HttpGet("[action]")]
        public IActionResult GetAll(int pageNum = 1, int pageSize = 5)
        {
            IEnumerable<Product> obj = _db.Product.Where(x => x.Is_Active == "1").Include(u => u.Category).Include(u => u.ApplicationType);
            if (obj.Count() == 0)
            {
                return NotFound("Category not fund");
            }
            return Ok(obj.Skip((pageNum - 1) * pageSize).Take(pageSize));
        }

        [HttpPost]
        public IActionResult Post([FromForm] Product product)
        {
            var guid = Guid.NewGuid();
            //var filePath = Path.Combine("wwwroot\\images\\product", guid + ".jpg");
            var filePath = Path.Combine(alamat, guid + ".jpg");
            if (product.Image != null)
            {
                var fileStream = new FileStream(filePath, FileMode.Create);
                product.Image.CopyTo(fileStream);
                product.imageUrl = filePath.Remove(0, alamat.Length + 1);
            }

            product.Is_Active = "1";
            _db.Product.Add(product);
            _db.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        public IActionResult Put([FromForm] Product product)
        {
            var guid = Guid.NewGuid();
            if (product.Image != null)
            {
                string extension = product.Image.FileName.Substring(product.Image.FileName.IndexOf("."), product.Image.FileName.Length - product.Image.FileName.IndexOf("."));
                string filePath = Path.Combine(alamat, guid + extension);
                var fileStream = new FileStream(filePath, FileMode.Create);
                product.Image.CopyTo(fileStream);
                product.imageUrl = guid.ToString() + extension;
            }

            try
            {
                product.Is_Active = "1";
                if(product.Id == 0) { _db.Product.Add(product); }
                else { _db.Product.Update(product); }
                
                _db.SaveChanges();
                return Ok("Record Updated Successfully");
            }
            catch (Exception err)
            {
                return NotFound(err.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var obj = _db.Product.Find(id);
            if (obj == null)
            {
                return NotFound("Product not fund");
            }
            obj.Is_Active = "0";
            _db.Product.Update(obj);
            _db.SaveChanges();
            return Ok("Record Deleted Successfully");
        }
    }
}