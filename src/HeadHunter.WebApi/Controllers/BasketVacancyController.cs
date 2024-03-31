using HeadHunter.Services.DTOs.Vacancies.Dtos.BasketVacancies;
using HeadHunter.Services.Services.BasketVacancies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeadHunter.API.Controllers
{
    [ApiController]
    [Route("api/basketvacancies")]
    public class BasketVacancyController : ControllerBase
    {
        private readonly IBasketVacancyService _basketVacancyService;

        public BasketVacancyController(IBasketVacancyService basketVacancyService)
        {
            _basketVacancyService = basketVacancyService;
        }

        [HttpPost]
        public async Task<ActionResult<BasketVacancyViewModel>> CreateBasketVacancy(BasketVacancyCreateModel basketVacancy)
        {
            try
            {
                var createdBasketVacancy = await _basketVacancyService.CreateAsync(basketVacancy);
                return Ok(createdBasketVacancy);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BasketVacancyViewModel>> UpdateBasketVacancy(long id, BasketVacancyUpdateModel basketVacancy)
        {
            try
            {
                var updatedBasketVacancy = await _basketVacancyService.UpdateAsync(id, basketVacancy);
                return Ok(updatedBasketVacancy);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasketVacancy(long id)
        {
            try
            {
                var result = await _basketVacancyService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BasketVacancyViewModel>> GetBasketVacancyById(long id)
        {
            try
            {
                var basketVacancy = await _basketVacancyService.GetByIdAsync(id);
                return Ok(basketVacancy);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BasketVacancyViewModel>>> GetAllBasketVacancies()
        {
            try
            {
                var basketVacancies = await _basketVacancyService.GetAllAsync();
                return Ok(basketVacancies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}