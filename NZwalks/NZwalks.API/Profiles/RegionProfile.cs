using AutoMapper;
using NZwalks.API.Models.DTO;
using NZWalks.API.Models.Domain;

namespace NZwalks.API.Profiles
{
    public class RegionProfile :Profile
    {
        public RegionProfile()
        {
            CreateMap<NZWalks.API.Models.Domain.Region, NZwalks.API.Models.DTO.Region>()
                .ReverseMap();
        }

    }
}
