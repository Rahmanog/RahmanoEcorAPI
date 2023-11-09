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
    public class ApplicationTypeController : ControllerBase
    {
        private EcomersDbContext _db;
        public ApplicationTypeController(EcomersDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var obj = _db.ApplicationType.Where(x => x.Is_Active == "1");
            if (obj.Count() == 0)
            {
                return NotFound("Application Type not fund");
            }
            return Ok(obj);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ApplicationType obj = null;
            try
            {
                obj = _db.ApplicationType.Where(x => x.TypeId == id && x.Is_Active == "1").FirstOrDefault();
                return Ok(obj);
            }
            catch
            {
                return NotFound("Category not fund");
            }
        }

        [HttpGet("[action]")]
        public IActionResult GetAll(int pageNum = 1, int pageSize = 5)
        {
            var obj = _db.ApplicationType.Where(x => x.Is_Active == "1");
            if (obj.Count() == 0)
            {
                return NotFound("Application Type not fund");
            }
            return Ok(obj.Skip((pageNum - 1) * pageSize).Take(pageSize));
        }

        [HttpGet("[action]")]
        public IActionResult FindApplicationType(string mov = "", int pageNum = 1, int pageSize = 5)
        {
            var applicationTypes = from applicationType in _db.ApplicationType
                            where applicationType.Is_Active.Equals("1") & applicationType.TypeName.Contains(mov)
                            select new
                            {
                                CategoryId = applicationType.TypeId,
                                CategoryName = applicationType.TypeName,
                                CategoryDescription = applicationType.TypeDescripton
                            };

            if (applicationTypes.Count() == 0)
            {
                return NotFound("Application Type not fund");
            }
            return Ok(applicationTypes.Skip((pageNum - 1) * pageSize).Take(pageSize));
        }

        [HttpPost]
        public IActionResult Post([FromBody] ApplicationType applicationType)
        {
            applicationType.Is_Active = "1";
            _db.ApplicationType.Add(applicationType);
            _db.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        public IActionResult Put([FromBody] ApplicationType applicationType)
        {
            applicationType.Is_Active = "1";
            try
            {
                _db.ApplicationType.Update(applicationType);
                _db.SaveChanges();
                return Ok("Record Updated Successfully");
            }
            catch
            {
                return NotFound("applicationType not fund");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var obj = _db.ApplicationType.Find(id);
            if (obj == null)
            {
                return NotFound("Application Type not fund");
            }
            obj.Is_Active = "0";
            _db.ApplicationType.Update(obj);
            _db.SaveChanges();
            return Ok("Record Deleted Successfully");
        }
    }
}
