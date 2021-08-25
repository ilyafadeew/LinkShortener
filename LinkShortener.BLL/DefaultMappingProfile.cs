using AutoMapper;
using LinkShortener.BLL.ViewModels;
using LinkShortener.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkShortener.BLL;

    public class DefaultMappingProfile : Profile
    {
        public DefaultMappingProfile()
        {
         CreateMap<LinkInfo, LinkInfoViewModel>().ReverseMap();
        }
    }

