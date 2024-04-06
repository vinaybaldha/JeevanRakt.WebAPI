using AutoMapper;
using JeevanRakt.Core.Domain.Entities;
using JeevanRakt.Core.DTO.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeevanRakt.Core.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BloodInventory, BloodInventoryResponseDTO>()
            .ForMember(dest => dest.DonorResponseDTO, opt => opt.MapFrom(src => src.Donor));

            CreateMap<Donor, DonorResponseDTO>();

            CreateMap<BloodRequest, BloodRequestResponseDTO>()
                .ForMember(dest => dest.RecipientResponseDTO, opt => opt.MapFrom(src => src.Recipient));

            CreateMap<Recipient, RecipientResponseDTO>();
        }
    }
}
