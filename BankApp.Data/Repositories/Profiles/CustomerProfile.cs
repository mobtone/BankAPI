using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Features;
using BankApp.Data.DTOs;
using BankApp.Domain.Models;

namespace BankApp.Data.Repositories.Profiles
{
    public class CustomerProfile : Profile
    {
        //denna klass skapar ett mappningsschema mellan entitets/modelsklassen Customer och CreateCustomerDto
        //den ärver från automappers basklass Profile

        public CustomerProfile()
        {
            CreateMap<CreateCustomerDto, Customer>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Givenname, opt => opt.MapFrom(src => src.Firstname))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Lastname))
                .ForMember(dest => dest.Streetaddress,
                    opt => opt.MapFrom(src => src.Address)) // Assuming Address field stores full address
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Zipcode, opt => opt.MapFrom(src => src.Zipcode))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.Emailaddress, opt => opt.MapFrom(src => src.Email));
               
               // .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.DateOfBirth));


        }
    }
}