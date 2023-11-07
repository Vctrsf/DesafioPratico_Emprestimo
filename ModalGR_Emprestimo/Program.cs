using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ModalGR_Emprestimo.Emprestimo;

var emprestimo = new SimuladorDeEmprestimo();

try
{
    emprestimo.ExibeTitulo();
    emprestimo.ColetaDadosCadastrais();

    bool tempoAdmissionalValido = emprestimo.ValidaTempoAdmissional();
    if (tempoAdmissionalValido == false) return;

    emprestimo.ValidaValorPretendido();

    var grupoDenotas = emprestimo.DefineGrupoDeNotas();
    emprestimo.ExibeNotasSelecionadas(grupoDenotas);
}
catch (Exception ex)
{
    Console.WriteLine("Erro: " + ex.Message);
}