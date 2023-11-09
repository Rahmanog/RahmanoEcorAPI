using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RahmanoEcorAPI.Data;
using RahmanoEcorAPI.Models;

namespace RahmanoEcorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private EcomersDbContext _db;
        public CategoryController(EcomersDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var obj = _db.Category.Where(x => x.Is_Active == "1");
            if (obj.Count() == 0)
            {
                return NotFound("Category not fund");
            }
            return Ok(obj);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Category obj = null;
            try
            {
                obj = _db.Category.Where(x => x.CategoryId == id && x.Is_Active == "1").FirstOrDefault();
                return Ok(obj);
            }catch 
            {
                return NotFound("Category not fund");
            }
        }

        [HttpGet("[action]")]
        public IActionResult GetAll(int pageNum = 1, int pageSize = 5)
        {
            var obj = _db.Category.Where(x => x.Is_Active == "1");
            if (obj.Count() == 0)
            {
                return NotFound("Category not fund");
            }
            return Ok(obj.Skip((pageNum - 1) * pageSize).Take(pageSize));
        }

        [HttpGet("[action]")]
        public IActionResult FindCategory(string mov = "", int pageNum = 1, int pageSize = 5)
        {
            var categorys = from category in _db.Category
                         where category.Is_Active.Equals("1") & category.CategoryName.Contains(mov)
                         select new
                         {
                             CategoryId = category.CategoryId,
                             CategoryName = category.CategoryName,
                             CategoryDescription = category.CategoryDescription
                         };

            if (categorys.Count() == 0)
            {
                return NotFound("Category not fund");
            }
            return Ok(categorys.Skip((pageNum - 1) * pageSize).Take(pageSize));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            category.Is_Active = "1";
            _db.Category.Add(category);
            _db.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Category category)
        {
            category.Is_Active = "1";
            try
            {
                _db.Category.Update(category);
                _db.SaveChanges();
                return Ok("Record Updated Successfully");
            }
            catch
            {
                return NotFound("category not fund");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var obj = _db.Category.Find(id);
            if (obj == null)
            {
                return NotFound("Category not fund");
            }
            obj.Is_Active = "0";
            _db.Category.Update(obj);
            _db.SaveChanges();
            return Ok("Record Deleted Successfully");
        }
    }
}
