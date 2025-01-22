using Microsoft.AspNetCore.Mvc;
using ContactManager.Application.Interfaces;
using ContactManager.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace ContactManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _repository;
        private readonly IMemoryCache _cache;

        public ContactsController(IContactRepository repository, IMemoryCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        // GET: api/contacts?ddd=21
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? ddd)
        {
            try
            {
                var cacheKey = $"contacts_{ddd}";
                if (!_cache.TryGetValue(cacheKey, out IEnumerable<Contact> contacts))
                {
                    contacts = await _repository.GetAllAsync();

                    if (!string.IsNullOrEmpty(ddd))
                    {
                        contacts = contacts.Where(c => c.DDD == ddd).ToList();
                    }

                    // Adiciona os dados ao cache por 5 minutos
                    _cache.Set(cacheKey, contacts, TimeSpan.FromMinutes(5));
                }

                return Ok(contacts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // POST: api/contacts
        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _repository.AddAsync(contact);
                return CreatedAtAction(nameof(GetAll), new { id = contact.Id }, contact);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // PUT: api/contacts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Contact updatedContact)
        {
            try
            {
                var existingContact = await _repository.GetByIdAsync(id);

                if (existingContact == null)
                {
                    return NotFound($"Contato com ID {id} não encontrado.");
                }

                existingContact.Name = updatedContact.Name;
                existingContact.Phone = updatedContact.Phone;
                existingContact.Email = updatedContact.Email;
                existingContact.DDD = updatedContact.DDD;

                await _repository.UpdateAsync(existingContact);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // DELETE: api/contacts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var contact = await _repository.GetByIdAsync(id);

                if (contact == null)
                {
                    return NotFound($"Contato com ID {id} não encontrado.");
                }

                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
