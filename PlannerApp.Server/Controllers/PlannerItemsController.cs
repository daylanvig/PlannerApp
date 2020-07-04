using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlannerApp.Server.Data;
using PlannerApp.Server.Models;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlannerApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlannerItemsController : ControllerBase
    {
        private readonly IRepository<PlannerItem> plannerItemRepository;
        private readonly IMapper mapper;

        public PlannerItemsController(IRepository<PlannerItem> plannerItemRepository, IMapper mapper)
        {
            this.plannerItemRepository = plannerItemRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ICollection<PlannerItemDTO>> GetItems(DateTime? startDate, DateTime? endDate)
        {
            IReadOnlyList<PlannerItem> items;

            if (startDate.HasValue)
            {
                if (endDate.HasValue)
                {
                    items = await plannerItemRepository.ListAsync(item => item.PlannedActionDate.Date >= startDate.Value.Date && item.PlannedEndTime.Date <= endDate.Value.Date);
                }
                else
                {
                    items = await plannerItemRepository.ListAsync(item => item.PlannedActionDate.Date == startDate.Value.Date);
                }
            }
            else
            {
                items = await plannerItemRepository.ListAllAsync();
            }
            
            var dtos = mapper.Map<PlannerItemDTO[]>(items);
            return dtos;
        }

        // GET: /api/PlannerItems/5
        [HttpGet("{id}")]
        public async Task<PlannerItemDTO> GetByID(int id)
        {
            var item = await plannerItemRepository.GetByIdAsync(id);
            var dto = mapper.Map<PlannerItemDTO>(item);
            return dto;
        }

        // POST: /api/PlannerItems
        [HttpPost]
        public async Task<IActionResult> AddNewItem([FromBody] PlannerItemDTO plannerItemDTO)
        {
            var plannerItem = mapper.Map<PlannerItem>(plannerItemDTO);
            await plannerItemRepository.AddAsync(plannerItem);
            plannerItemDTO.ID = plannerItem.ID;
            return CreatedAtAction(nameof(GetByID), new { id = plannerItem.ID }, plannerItemDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditItem(int id, [FromBody] PlannerItemDTO item)
        {
            var dbItem = await plannerItemRepository.GetByIdAsync(id);
            if(dbItem == null)
            {
                return BadRequest();
            }
            mapper.Map(item, dbItem);
            await plannerItemRepository.UpdateAsync(dbItem);
            return Ok(mapper.Map<PlannerItemDTO>(dbItem));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlannerItem(int id)
        {
            var item = await plannerItemRepository.GetByIdAsync(id);
            if(item == null)
            {
                return BadRequest();
            }
            await plannerItemRepository.DeleteAsync(item);
            return NoContent();
        }
    }
}
