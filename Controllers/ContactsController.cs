using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using JWTWebApiAuth.Models;
using JWTWebApiAuth.Services;
using Microsoft.AspNetCore.Authorization;

namespace JWTWebApiAuth.Controllers
{
    /// <summary>
    /// Contacts list API controller
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly IDatabaseProvideService _databaseProviderService;

        public ContactsController(IDatabaseProvideService databaseProviderService)
        {
            _databaseProviderService = databaseProviderService;
        }

        [HttpGet]
        public IEnumerable<ContactsModel> GetAll()
        {
            return _databaseProviderService.Get();
        }

        [HttpGet("{id}", Name = "GetContactItem")]
        public IActionResult GetById(int? id)
        {
            if(id == null)
                return BadRequest();

            var item = _databaseProviderService.Get(id);

            if(item == null)
                return NotFound();

            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ContactsModel item)
        {
            if(item == null)
            {
                return BadRequest();
            }

            _databaseProviderService.Create(item);

            return CreatedAtRoute("GetContactItem", new ContactsModel {Id = item.Id}, item);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if(id == null)
                return BadRequest();

            _databaseProviderService.Delete(id);

            return new NoContentResult();
        }
    }
}