using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModalGR_Emprestimo.Emprestimo
{
    public class SimuladorDeEmprestimo
    {
        private Colaborador _colaborador = new Colaborador();
        private ValidadorDeRegras _validador = new ValidadorDeRegras();

        public void ExibeTitulo()
        {
            Console.WriteLine("=======================================================");
            Console.WriteLine();
            Console.WriteLine("                     EMPRESTIMO                        ");
            Console.WriteLine();
            Console.WriteLine("   Desafio prático - Processo de Formação ModalGR 2024 ");
            Console.WriteLine();
            Console.WriteLine("=======================================================");
            Console.WriteLine();
        }

        public void ColetaDadosCadastrais()
        {
            bool dadosValidos = true;
            string admissao;
            string salario;
            string nome;
            do
            {
                if (!dadosValidos)
                {
                    Console.WriteLine("Formato de dados incorretos. Por favor, informe novamente: ");
                    Console.WriteLine();
                }

                Console.Write("Digite o nome do colaborador: ");
                nome = Console.ReadLine();

                Console.Write("Insira a data de admissão (dd/mm/aaaa): ");
                admissao = Console.ReadLine();

                Console.Write("Informe o salário atual: ");
                salario = Console.ReadLine();

                dadosValidos = _validador.ValidaDadosCadastrais(admissao, salario);
            } while (!dadosValidos);

            _colaborador.Nome = nome;
            _colaborador.DataDeAdmissão = DateTime.ParseExact(admissao, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            _colaborador.Salario = double.Parse(salario);


        }

        public bool ValidaTempoAdmissional()
        {
            Console.WriteLine("\nValidando pré-condições de crédito...");
            var preCondicoesValidas = _validador.ValidaPeriodoAdmissional(_colaborador.DataDeAdmissão);
            if (preCondicoesValidas == false)
            {
                Console.WriteLine("Agradecemos seu interesse, mas você não atende aos requisitos mínimos do programa.");
                return false;
            }

            Console.WriteLine("Pré-condições aceitas.");

            return true;

        }

        public void ValidaValorPretendido()
        {
            bool limiteValido = true;
            bool quantiaValida = false;
            bool multiploDeDois = false;
            string valorPretendido;
            do
            {
                if (limiteValido == false)
                {
                    Console.WriteLine("Valor pretendido incorreto. Insira um valor válido: ");
                }

                Console.Write("\nDigite o valor de empréstimo pretendido: ");
                valorPretendido = Console.ReadLine();

                Console.WriteLine("\nValidando o valor pretendido...");
                bool valorPretendidoEhNumerico = double.TryParse(valorPretendido, out double valorDeEmprestimoConvertido);

                if (valorPretendidoEhNumerico)
                {
                    _colaborador.ValorDeEmprestimo = valorDeEmprestimoConvertido;
                    quantiaValida = _validador.ValidaValorPretendido(_colaborador.Salario, valorDeEmprestimoConvertido);
                    multiploDeDois = _validador.ValidaMultiploDeDois(valorDeEmprestimoConvertido);
                }

                limiteValido = quantiaValida && multiploDeDois;

            } while (limiteValido == false);
    
        }

        public double[] DefineGrupoDeNotas()
        {
            double[] resultado = new double[6];
            bool opcaoValida = true;
            string grupoEscolhido;
            do
            {
                if (opcaoValida == false)
                {
                    Console.WriteLine("Opção inválida. Escolha outra opção.");
                    Console.WriteLine();
                }

                Console.WriteLine("\nEscolha um grupo de notas.");
                Console.WriteLine("1 - Notas altas");
                Console.WriteLine("2 - Notas baixas");
                Console.WriteLine("3 - Notas mistas");
                Console.WriteLine();
                Console.Write("Grupo: ");
                grupoEscolhido = Console.ReadLine();

                opcaoValida = grupoEscolhido == "1" || grupoEscolhido == "2" || grupoEscolhido == "3";

            } while(opcaoValida == false);

            if (grupoEscolhido == "1")
            {
                resultado = _validador.CalculaNotasMaiores(_colaborador.ValorDeEmprestimo);
            }
            else if (grupoEscolhido == "2")
            {
                resultado = _validador.CalculaNotasMenores(_colaborador.ValorDeEmprestimo);
            }
            else if (grupoEscolhido == "3")
            {
                resultado = _validador.CalculaNotasMistas(_colaborador.ValorDeEmprestimo);
            }

            return resultado;
        }

        public void ExibeNotasSelecionadas(double[] notasSelecionadas)
        {
            Console.WriteLine("\nSelecionando notas...");
            if (notasSelecionadas[0] > 0) Console.WriteLine("R$ 100 x" + notasSelecionadas[0]);
            if (notasSelecionadas[1] > 0) Console.WriteLine("R$  50 x" + notasSelecionadas[1]);
            if (notasSelecionadas[2] > 0) Console.WriteLine("R$  20 x" + notasSelecionadas[2]);
            if (notasSelecionadas[3] > 0) Console.WriteLine("R$  10 x" + notasSelecionadas[3]);
            if (notasSelecionadas[4] > 0) Console.WriteLine("R$   5 x" + notasSelecionadas[4]);
            if (notasSelecionadas[5] > 0) Console.WriteLine("R$   2 x" + notasSelecionadas[5]);
            Console.WriteLine();
       }
    }
}
