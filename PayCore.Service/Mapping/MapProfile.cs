using AutoMapper;
using PayCore.Core.DTOs;
using PayCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<ProductDto, ProductCreateDto>().ReverseMap();
            

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();

            CreateMap<Offer, OfferDto>().ReverseMap();
            CreateMap<Offer, OfferUpdateDto>().ReverseMap();
            CreateMap<Offer, OfferCreateDto>().ReverseMap();
            CreateMap<OfferDto, OfferCreateDto>().ReverseMap();
            CreateMap<Offer, OfferGetDto>().ReverseMap();

            CreateMap<Account, AccountCreateDto>().ReverseMap();
            CreateMap<Account, AccountDto>().ReverseMap();

            CreateMap<UserAppDto, UserApp>().ReverseMap();
            

        }
    }

}
