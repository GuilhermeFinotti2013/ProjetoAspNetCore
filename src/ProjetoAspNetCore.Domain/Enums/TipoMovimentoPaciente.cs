using System.ComponentModel;

namespace ProjetoAspNetCore.Domain.Enums
{
    public enum TipoMovimentoPaciente
    {
        [Description("Entrada de Paciente")] Entrada = 1,
        [Description("Saída de Paciente")] Saida
    }
}