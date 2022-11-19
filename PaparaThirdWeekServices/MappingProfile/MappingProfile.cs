using AutoMapper;
using PaparaThirdWeek.Domain.Entities;
using PaparaThirdWeek.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Services.MappingProfile
{
   public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>().ReverseMap();
        }
    }
}
 