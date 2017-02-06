using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LEGO_Server.Models;
using LEGO_Server.Repository;
using System.Net;

namespace LEGO_Server.Controllers
{
    [Route("api/[controller]/")]
    public class BrickController : Controller
    {

        public IBrickRepository BrickRepository { get; set; }

        public BrickController(IBrickRepository _repo)
        {
            BrickRepository = _repo;
        }
        [HttpGet]
        public IEnumerable<BrickUnit> GetAll()
        {
            return BrickRepository.GetAll();
        }
        [HttpGet("{id}", Name= "GetBricks")]
        public IActionResult GetByID(string id)
        {
            var item = BrickRepository.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody]BrickUnit item)
        {
            /*
            if (item == null)
            {
                return BadRequest();
            }
            */
            BrickRepository.Add(item);
            return CreatedAtRoute("GetBricks", new { Controller = "Brick", id = item.ID }, item);            
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]BrickUnit item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var ItemToUpdate = BrickRepository.Find(id);
            if (ItemToUpdate == null)
            {
                return NotFound();
            }
            item.ID = id;
            BrickRepository.Update(item);
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            BrickRepository.Remove(id);
        }
    }
}
