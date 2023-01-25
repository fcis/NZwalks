using AutoMapper;
using NZwalks.API.Models.DTO;
using NZwalks.API.Models.Domain;

namespace NZwalks.API.Profiles
{
    public class RegionProfile :Profile
    {
        public RegionProfile()
        {
            CreateMap<NZwalks.API.Models.Domain.Region, NZwalks.API.Models.DTO.Region>()
                .ReverseMap();
        }

    }
}
