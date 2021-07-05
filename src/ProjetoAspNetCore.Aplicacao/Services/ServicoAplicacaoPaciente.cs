using AutoMapper;
using ProjetoAspNetCore.Aplicacao.Interfaces;
using ProjetoAspNetCore.Aplicacao.ViewModels;
using ProjetoAspNetCore.Domain.Entities;
using ProjetoAspNetCore.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.Aplicacao.Services
{
    public class ServicoAplicacaoPaciente : IServicoAplicacaoPaciente
    {
        private readonly IPacienteRepository _repositorioPaciente;
        private readonly IMapper _mapper;

        public ServicoAplicacaoPaciente(IPacienteRepository repositorioPaciente, IMapper mapper)
        {
            _repositorioPaciente = repositorioPaciente;
            _mapper = mapper;
        }

        public async Task<List<EstadoPaciente>> ListarEstadoPacienteApp()
        {
            return await _repositorioPaciente.ListarEstadosPaciente();
        }

        public async Task<PacienteViewModel> ObterPacienteComEstadoPacienteApp(Guid pacienteId)
        {
            return _mapper.Map<PacienteViewModel>(await _repositorioPaciente.ObterPacienteComEstadoPaciente(pacienteId));
        }

        public async Task<IEnumerable<PacienteViewModel>> ObterPacientesComEstadoPacienteApp()
        {
            return _mapper.Map<IEnumerable<PacienteViewModel>>(await _repositorioPaciente.ListarPacientesComEstado());
        }

        public async Task<IEnumerable<PacienteViewModel>> ObterPacientesPorEstadoPacienteApp(Guid estadoPacienteId)
        {
            return _mapper.Map<IEnumerable<PacienteViewModel>>(await _repositorioPaciente.ObterPacientesPorEstadoPaciente(estadoPacienteId));
        }

        public bool TemPaciente(Guid pacienteId)
        {
            return _repositorioPaciente.TemPaciente(pacienteId);
        }
    }
}
