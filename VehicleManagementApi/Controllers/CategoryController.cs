using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleManagementApi.Models;
using VehicleManagementModels;

namespace VehicleManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            try
            {
                return Ok(await _categoryRepository.GetCategories());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Problem to retrive data from db");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            try
            {
                var result = await _categoryRepository.GetCategory(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Problem to retrive data from db");
            }

        }

        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory(Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest();
                }
                var createdCategory = await _categoryRepository.AddCategory(category);
                return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.CategoryId }, createdCategory);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Problem to add data");
            }

        }

        [HttpPut]
        public async Task<ActionResult<Category>> UpdateCategory(Category category)
        {
            try
            {
                var categoryToUpdate = await _categoryRepository.GetCategory(category.CategoryId);
                if (categoryToUpdate == null)
                {
                    return NotFound($"Category with id = {category.CategoryId} not found");
                }
                return await _categoryRepository.UpdateCategory(category);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Problem to update data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            try
            {
                var categoryToDelete = await _categoryRepository.GetCategory(id);
                if (categoryToDelete == null)
                {
                    return NotFound($"Category with id = {id} not found");
                }

                return await _categoryRepository.DeleteCategory(id);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Problem to date data");
            }

        }
    }
}
