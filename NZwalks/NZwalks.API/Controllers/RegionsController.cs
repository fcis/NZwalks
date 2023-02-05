using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repositories;
using NZwalks.API.Models.Domain;
using System.Runtime.CompilerServices;

namespace NZwalks.API.Controllers
{
    [ApiController]
    [Route("Regions")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository,IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task <IActionResult> GetAllRegionsAsync()
        {
           var regions = await regionRepository.GetAllAsync();
            var RegionDto = mapper.Map<IList<Models.DTO.Region>>(regions);
            return Ok(RegionDto);
        }
        [HttpGet]
        [Route("id:guid")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetRegionAsync(id);
            if(region == null)
            {
                return NotFound();
            }
            var RegionDto = mapper.Map<Models.DTO.Region>(region);
            return Ok(RegionDto);
        }
        [HttpPost]
        public async Task< IActionResult> AddRegionsAsync(AddRegionRequest addRegionRequest)
        {
            //Validate Request
            if (!ValidateAddRegionAsync(addRegionRequest))
            {
                return BadRequest(ModelState);
            }

            //from dto  to domain 
            var Region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Area= addRegionRequest.Area,
                Lat = addRegionRequest.Lat ,
                Long= addRegionRequest.Long,
                Population=addRegionRequest.Population,
            };
            Region = await regionRepository.AddRegionAsync(Region);
            var RegionDto = mapper.Map<Models.DTO.Region>(Region);

            return CreatedAtAction(nameof(GetRegionAsync), new { id = RegionDto.Id }, RegionDto);
        }
        [HttpDelete]
        [Route("id:guid")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            var region= await regionRepository.DeleteRegionAsync(id);
            if(region == null) { return NotFound();}
            var RegionDto = mapper.Map<Models.DTO.Region>(region);
            return Ok(RegionDto);


        }
        [HttpPut]
        [Route("id:guid")]
        public async Task<IActionResult> UpdateRegionAsync([FromHeader] Guid id, [FromBody] UpdateRegionRequest updateRegionRequest)
        {
            //cober DTO to domain
            var RegionDomain = new Models.Domain.Region()
            {
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Population = updateRegionRequest.Population,
                Code = updateRegionRequest.Code,
                Name = updateRegionRequest.Name
            };
            var region =await regionRepository.UpdateRegionAsync(id, RegionDomain);
            //return from region domain to DTO
            if (region == null) { return NotFound();}
            var RegionDto = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population
            };
            return Ok(RegionDto);
        }
        #region Private methods

        private bool ValidateAddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            if (addRegionRequest == null)
            {
                ModelState.AddModelError(nameof(addRegionRequest),
                    $"Add Region Data is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(addRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Code),
                    $"{nameof(addRegionRequest.Code)} cannot be null or empty or white space.");
            }

            if (string.IsNullOrWhiteSpace(addRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Name),
                    $"{nameof(addRegionRequest.Name)} cannot be null or empty or white space.");
            }

            if (addRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Area),
                    $"{nameof(addRegionRequest.Area)} cannot be less than or equal to zero.");
            }

            if (addRegionRequest.Population < 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Population),
                    $"{nameof(addRegionRequest.Population)} cannot be less than zero.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        private bool ValidateUpdateRegionAsync(Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            if (updateRegionRequest == null)
            {
                ModelState.AddModelError(nameof(updateRegionRequest),
                    $"Add Region Data is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Code),
                    $"{nameof(updateRegionRequest.Code)} cannot be null or empty or white space.");
            }

            if (string.IsNullOrWhiteSpace(updateRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Name),
                    $"{nameof(updateRegionRequest.Name)} cannot be null or empty or white space.");
            }

            if (updateRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Area),
                    $"{nameof(updateRegionRequest.Area)} cannot be less than or equal to zero.");
            }

            if (updateRegionRequest.Population < 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Population),
                    $"{nameof(updateRegionRequest.Population)} cannot be less than zero.");
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
