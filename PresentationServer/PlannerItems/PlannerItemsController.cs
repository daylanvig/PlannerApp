using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.PlannerItems.Commands.CreatePlannerItem;
using Application.PlannerItems.Commands.Shared;
using Application.PlannerItems.Queries.GetCompletedItems;
using Application.PlannerItems.Queries.GetOverdueItems;
using Application.PlannerItems.Queries.GetPlannerItemsByDate;
using Application.PlannerItems.Queries.Common;
using Domain.PlannerItems;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.PlannerItems.Commands.EditPlannerItem;

namespace PresentationServer.PlannerItems
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlannerItemsController : ControllerBase
    {
        private readonly IRepository<PlannerItem> plannerItemRepository;
        private readonly IMapper mapper;
        private readonly IGetPlannerItemsByDateQuery getPlannerItemsByDateQuery;
        private readonly IGetOverdueItemsQuery getOverdueItemsQuery;
        private readonly IGetCompletedItemsQuery getCompletedItemsQuery;
        private readonly ICreatePlannerItemCommand createPlannerItemCommand;
        private readonly IEditPlannerItemCommand editPlannerItemCommand;

        public PlannerItemsController(IRepository<PlannerItem> plannerItemRepository, IMapper mapper, IGetPlannerItemsByDateQuery getPlannerItemsByDateQuery, IGetOverdueItemsQuery getOverdueItemsQuery, IGetCompletedItemsQuery getCompletedItemsQuery, ICreatePlannerItemCommand createPlannerItemCommand, IEditPlannerItemCommand editPlannerItemCommand)
        {
            this.plannerItemRepository = plannerItemRepository;
            this.mapper = mapper;
            this.getPlannerItemsByDateQuery = getPlannerItemsByDateQuery;
            this.getOverdueItemsQuery = getOverdueItemsQuery;
            this.getCompletedItemsQuery = getCompletedItemsQuery;
            this.createPlannerItemCommand = createPlannerItemCommand;
            this.editPlannerItemCommand = editPlannerItemCommand;
        }

        [HttpGet]
        public async Task<IEnumerable<PlannerItemModel>> GetItems(DateTime? startDate, DateTime? endDate)
        {
            return await getPlannerItemsByDateQuery.Execute(startDate, endDate);      
        }

        [HttpGet("Completed")]
        public async Task<IEnumerable<PlannerItemModel>> GetCompletedItems()
        {
            return await getCompletedItemsQuery.Execute();
        }

        [HttpGet("Overdue")]
        public async Task<IEnumerable<PlannerItemModel>> GetOverdueItems()
        {
            return await getOverdueItemsQuery.Execute();
        }

        // GET: /api/PlannerItems/5
        [HttpGet("{id}")]
        public async Task<PlannerItemCreateEditModel> GetByID(int id)
        {
            var item = await plannerItemRepository.GetByIdAsync(id);
            var dto = mapper.Map<PlannerItemCreateEditModel>(item);
            return dto;
        }

        // POST: /api/PlannerItems
        [HttpPost]
        public async Task<IActionResult> AddNewItem([FromBody] PlannerItemCreateEditModel plannerItemDTO)
        {
            var plannerItem = await createPlannerItemCommand.Execute(plannerItemDTO);
            return CreatedAtAction(nameof(GetByID), new { id = plannerItem.ID }, plannerItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditItem(int id, [FromBody] PlannerItemCreateEditModel editModel)
        {
            PlannerItemModel item;
            try
            {
                item = await editPlannerItemCommand.Execute(id, editModel);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            return Ok(item);
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
