using HeadHunter.Services.DTOs.Industry.Dtos.Industries.Categories;
using HeadHunter.Services.Exceptions;
using HeadHunter.Services.Services.IndustryCategories;
using Microsoft.AspNetCore.Mvc;

namespace HeadHunter.WebApi.Controllers
{
    [ApiController]
    [Route("api/industry-categories")]
    public class IndustryCategoryController : ControllerBase
    {
        private readonly IIndustryCategoryService industryCategoryService;

        public IndustryCategoryController(IIndustryCategoryService industryCategoryService)
        {
            this.industryCategoryService = industryCategoryService ?? throw new ArgumentNullException(nameof(industryCategoryService));
        }

        [HttpPost]
        public async Task<IActionResult> CreateIndustryCategory(IndustryCategoryCreateModel industryCategoryCreateModel)
        {
            try
            {
                var createdIndustryCategory = await industryCategoryService.CreateAsync(industryCategoryCreateModel);
                return Ok(createdIndustryCategory);
            }
            catch (CustomException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIndustryCategory(long id, IndustryCategoryUpdateModel industryCategoryUpdateModel)
        {
            try
            {
                var updatedIndustryCategory = await industryCategoryService.UpdateAsync(id, industryCategoryUpdateModel);
                return Ok(updatedIndustryCategory);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIndustryCategory(long id)
        {
            var isDeleted = await industryCategoryService.DeleteAsync(id);
            if (isDeleted)
                return NoContent();

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIndustryCategoryById(long id)
        {
            var industryCategory = await industryCategoryService.GetByIdAsync(id);
            if (industryCategory != null)
                return Ok(industryCategory);

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIndustryCategories()
        {
            var industryCategories = await industryCategoryService.GetAllAsync();
            return Ok(industryCategories);
        }
    }
}