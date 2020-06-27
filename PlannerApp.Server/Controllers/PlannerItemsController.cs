using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design;
using PlannerApp.Core.Models;
using PlannerApp.Server.Data;
using PlannerApp.Server.Models;

namespace PlannerApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ICollection<PlannerItemDTO>> GetAllItems()
        {
            var items = await plannerItemRepository.ListAllAsync();
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
    }
}
