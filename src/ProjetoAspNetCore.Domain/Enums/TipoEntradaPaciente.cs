using System.ComponentModel;

namespace ProjetoAspNetCore.Domain.Enums
{
    public enum TipoEntradaPaciente
    {
        [Description("Internação")] Internacao = 1,
        [Description("Emergência")] Emergencia,
        [Description("Tranferência")] Transferencia,
        [Description("Ambulatório")] Ambulatorio,
        [Description("Sem Prontuário")] SemProntuario
    }
}