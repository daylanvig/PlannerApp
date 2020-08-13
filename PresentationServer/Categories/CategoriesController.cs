using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Persistence;
using Domain.Categories;
using PlannerApp.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationServer.Categories
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await categoryRepository.ListAllAsync();
            return categories.OrderBy(c => c.Description).Select(c => mapper.Map<CategoryDTO>(c)).ToList();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return mapper.Map<CategoryDTO>(category);
        }

        //// PUT: api/Categories/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCategory(int id, Category category)
        //{
        //    if (id != category.ID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(category).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CategoryExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> AddCategory(CategoryDTO categoryDTO)
        {
            var category = await categoryRepository.AddAsync(mapper.Map<Category>(categoryDTO));
            return CreatedAtAction(nameof(CategoriesController.GetCategory), new { id = category.ID }, mapper.Map<CategoryDTO>(category));
        }

        //// DELETE: api/Categories/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Category>> DeleteCategory(int id)
        //{
        //    var category = await _context.Category.FindAsync(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Category.Remove(category);
        //    await _context.SaveChangesAsync();

        //    return category;
        //}
    }
}
