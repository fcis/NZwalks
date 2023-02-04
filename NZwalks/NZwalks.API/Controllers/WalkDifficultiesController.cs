using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repositories;
using System.Reflection.Emit;

namespace NZwalks.API.Controllers
{
    [ApiController]
    [Route("WalkDifficulties")]
    public class WalkDifficultiesController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultiesController(IWalkDifficultyRepository walkDifficultyRepository , IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("WalkDifficulties")]
        public async Task<IActionResult> GetAllWalksDifficultyAsync()
        {
            var WalksDiffculties = await walkDifficultyRepository.GetAllWalksDifficultyAsync();
            var WalksDiffcultiesDTO = mapper.Map<IList<Models.DTO.WalkDifficulty>>(WalksDiffculties);

            return Ok(WalksDiffcultiesDTO);
        }
        [HttpGet]
        [Route("id:guid")]
        [ActionName("GetWalkDifficultyAsync")]
        public async Task<IActionResult> GetWalkDifficultyAsync (Guid id)
        {
            var WalkDiffuclty = await walkDifficultyRepository.GetWalkDifficultyAsync(id);
            var WalkDiffucltyDTO = mapper.Map<Models.DTO.WalkDifficulty>(WalkDiffuclty);
            return Ok(WalkDiffuclty);
        }
        [HttpPost]
        [Route("WalkDifficulty")]
        public async Task<IActionResult> AddWalkDifficultyAsync([FromBody] AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            var WalkDiffDomain = new Models.Domain.WalkDifficulty()
            {
                Id = new Guid(),
                Code = addWalkDifficultyRequest.Code
            };
             WalkDiffDomain = await walkDifficultyRepository.AddWalkDifficultyAsync(WalkDiffDomain);
            var WalkDiffDTO = mapper.Map<Models.DTO.WalkDifficulty>(WalkDiffDomain);
            return CreatedAtAction(nameof(GetWalkDifficultyAsync), new { id = WalkDiffDTO.Id }, WalkDiffDTO);
        }
        [HttpPut]
        [Route("id:guid")]
        public async Task<IActionResult> UpdateWalkAsync([FromHeader] Guid id, [FromBody] UpdateWalkDifficultyRequest  updateWalkDifficultyRequest)
        {
            var DomainWalk = new NZwalks.API.Models.Domain.WalkDifficulty()
            {
                Code = updateWalkDifficultyRequest.Code

            };
            var updatedWalk = await walkDifficultyRepository.UpdateWalkDifficultyAsync(id, DomainWalk); 
            if (updatedWalk == null)
            {
                return NotFound();
            }
            var walkDTO = new Models.DTO.WalkDifficulty()
            {
                Code= updatedWalk.Code,
            };
            return Ok(walkDTO);
        }
        [HttpDelete]
        [Route("id:guid")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            var Walk = await walkDifficultyRepository.DeleteWalkDifficultyAsync(id);
            if (Walk == null) { return NotFound(); }
            var WalkDto = mapper.Map<Models.DTO.WalkDifficulty>(Walk);
            return Ok(WalkDto);
        }
    }
}
