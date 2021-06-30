using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoAspNetCore.Aplicacao.ViewModel;
using AutoMapper;
using ProjetoAspNetCore.Domain.Entities;

namespace ProjetoAspNetCore.Aplicacao.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Paciente, PacienteViewModel>().ReverseMap();
        }
    }
}
