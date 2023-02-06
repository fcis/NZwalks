using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repositories;
using System.Runtime.CompilerServices;
using Walk = NZwalks.API.Models.DTO.Walk;

namespace NZwalks.API.Controllers
{
    [ApiController]
    [Route("Walks")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDifficultyRepository walkDifficultyRepository;

        public WalksController(IWalkRepository walkRepository,IMapper mapper,IRegionRepository regionRepository
            ,IWalkDifficultyRepository walkDifficultyRepository)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
            this.regionRepository = regionRepository;
            this.walkDifficultyRepository = walkDifficultyRepository;
        }
        [HttpGet]
        [Route("Walks")]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            var Walks = await walkRepository.GetAllWalksAsync();
            var WalksDTO = mapper.Map<IList<Walk>>(Walks);

            return Ok(WalksDTO);
        }
        [HttpGet]
        [Route("id:guid")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walk = await walkRepository.GetWalkAsync(id);
            var walkDTO = mapper.Map<Walk>(walk);   
            return Ok(walkDTO);
        }
        [HttpPost]
        [Route("Walk")]
        public async Task<IActionResult> AddWalkAsync([FromBody] AddWalkRequest addWalkRequest)
        {
            //if (! await ValidateAddWalkAsync(addWalkRequest))
            //{
            //    return BadRequest(ModelState);

            //}
            //convert dto to domain object
            var walkDomain = new Models.Domain.Walk()
            {
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };
            //pass domain object to repository
            walkDomain = await walkRepository.AddWalkAsync(walkDomain);
            //convert domain object back to dto object 
            var WalkDto = mapper.Map<Walk>(walkDomain);
            return CreatedAtAction(nameof(GetWalkAsync), new { id = WalkDto.Id }, WalkDto);
            //send response to client 
        }
        [HttpPut]
        [Route("id:guid")]
        public async Task<IActionResult> UpdateWalkAsync([FromHeader] Guid id, [FromBody]  UpdatedWalkRequest updatedWalkRequest)
        {
            //if (!(await ValidateUpdateWalkAsync(updatedWalkRequest)))
            //{
            //    return BadRequest(ModelState);
            //}
            var DomainWalk = new NZwalks.API.Models.Domain.Walk()
            {
                Length = updatedWalkRequest.Length,
                Name = updatedWalkRequest.Name,
                RegionId = updatedWalkRequest.RegionId,
                WalkDifficultyId = updatedWalkRequest.WalkDifficultyId
            };
            var updatedWalk = await walkRepository.UpdateWalkAsync(id, DomainWalk);
            if (updatedWalk == null)
            {
                return NotFound();
            }
            var walkDTO = new Models.DTO.Walk()
            {
                Id = updatedWalk.Id,
                Name = updatedWalk.Name,
                RegionId = updatedWalk.RegionId,
                Length = updatedWalk.Length,
                WalkDifficultyId = updatedWalk.WalkDifficultyId
            };
            return Ok(walkDTO);
        }
        [HttpDelete]
        [Route("id:guid")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            var Walk = await walkRepository.DeleteWalkAsync(id);
            if (Walk == null) { return NotFound(); }
            var WalkDto = mapper.Map<Models.DTO.Walk>(Walk);
            return Ok(WalkDto);
        }
        #region Private methods

        private async Task<bool> ValidateAddWalkAsync(Models.DTO.AddWalkRequest addWalkRequest)
        {
            //if (addWalkRequest == null)
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest),
            //        $"{nameof(addWalkRequest)} cannot be empty.");
            //    return false;
            //}

            //if (string.IsNullOrWhiteSpace(addWalkRequest.Name))
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest.Name),
            //        $"{nameof(addWalkRequest.Name)} is required.");
            //}

            //if (addWalkRequest.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest.Length),
            //        $"{nameof(addWalkRequest.Length)} should be greater than zero.");
            //}

            var region = await regionRepository.GetRegionAsync(addWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.RegionId),
                    $"{nameof(addWalkRequest.RegionId)} is invalid.");
            }

            var walkDifficulty = await walkDifficultyRepository.GetWalkDifficultyAsync(addWalkRequest.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.WalkDifficultyId),
                       $"{nameof(addWalkRequest.WalkDifficultyId)} is invalid.");

            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateUpdateWalkAsync(UpdatedWalkRequest updatedWalkRequest)
        {
            //if (updateWalkRequest == null)
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequest),
            //        $"{nameof(updateWalkRequest)} cannot be empty.");
            //    return false;
            //}

            //if (string.IsNullOrWhiteSpace(updateWalkRequest.Name))
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequest.Name),
            //        $"{nameof(updateWalkRequest.Name)} is required.");
            //}

            //if (updateWalkRequest.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequest.Length),
            //        $"{nameof(updateWalkRequest.Length)} should be greater than zero.");
            //}

            var region = await regionRepository.GetRegionAsync(updatedWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(updatedWalkRequest.RegionId),
                    $"{nameof(updatedWalkRequest.RegionId)} is invalid.");
            }

            var walkDifficulty = await walkDifficultyRepository.GetWalkDifficultyAsync(updatedWalkRequest.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(updatedWalkRequest.WalkDifficultyId),
                       $"{nameof(updatedWalkRequest.WalkDifficultyId)} is invalid.");

            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
