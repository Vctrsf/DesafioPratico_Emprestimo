using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModalGR_Emprestimo.Emprestimo
{
    public class ValidadorDeRegras
    {


        public bool ValidaPeriodoAdmissional(DateTime dataDeAdmissao)
        {
            var totalDeDias = DateTime.Now.Subtract(dataDeAdmissao).TotalDays;
            var totalDeAnos = totalDeDias / 365;

            if (totalDeAnos >= 5)
            {
                return true;
            }

            return false;
        }

        public bool ValidaValorPretendido(double salario, double valorDeEmprestimo)
        {
            if (valorDeEmprestimo <= 0 || !(valorDeEmprestimo <= 2 * salario))
            {
                return false;
            }
            return true;
        }

        public bool ValidaMultiploDeDois(double valorDeEmprestimo)
        {
            if (valorDeEmprestimo % 2 == 0)
            {
                return true;
            }
            return false;
        }

        public double[] CalculaNotasMaiores(double valorDeEmprestimo)
        {
            double[] resultado = new double[6];

            if (valorDeEmprestimo >= 100)
            {
                resultado[0] = Math.Floor(valorDeEmprestimo / 100);
                valorDeEmprestimo -= 100 * resultado[0];
            }
            else
            {
                resultado[0] = 0;
            }

            if (valorDeEmprestimo >= 50)
            {
                resultado[1] = Math.Floor(valorDeEmprestimo / 50);
                valorDeEmprestimo -= 50 * resultado[1];
            }
            else
            {
                resultado[1] = 0;
            }

            var calculoNotasMenores = CalculaNotasMenores(valorDeEmprestimo);

            resultado[2] = calculoNotasMenores[2];
            resultado[3] = calculoNotasMenores[3];
            resultado[4] = calculoNotasMenores[4];
            resultado[5] = calculoNotasMenores[5];

            return resultado;
        }

        public double[] CalculaNotasMenores(double valorDeEmprestimo)
        {
            double[] resultado = new double[6];

            if (valorDeEmprestimo >= 20)
            {
                resultado[2] = Math.Floor(valorDeEmprestimo / 20);

                valorDeEmprestimo -= 20 * resultado[2];
            }
            else
            {
                resultado[2] = 0;
            }

            if (valorDeEmprestimo >= 10)
            {
                resultado[3] = Math.Floor(valorDeEmprestimo / 10);
                valorDeEmprestimo -= 10 * resultado[3];
            }
            else
            {
                resultado[3] = 0;
            }

            if (valorDeEmprestimo >= 5)
            {
                resultado[4] = Math.Floor(valorDeEmprestimo / 5);
                valorDeEmprestimo -= 5 * resultado[4];
            }
            else
            {
                resultado[4] = 0;
            }

            if (valorDeEmprestimo >= 2)
            {
                resultado[5] = Math.Floor(valorDeEmprestimo / 2);
                valorDeEmprestimo -= 2 * resultado[5];
            }
            else
            {
                resultado[5] = 0;
            }
            
            return resultado;
        }

        public double[] CalculaNotasMistas(double valorDeEmprestimo)
        {
            double[] resultado = new double[6];

            var metadeDoEmprestimo = valorDeEmprestimo / 2;

            var notasMaiores = CalculaNotasMaiores(metadeDoEmprestimo);
            var notasMenores = CalculaNotasMenores(metadeDoEmprestimo);

            resultado[0] = notasMaiores[0] + notasMenores[0];
            resultado[1] = notasMaiores[1] + notasMenores[1];
            resultado[2] = notasMaiores[2] + notasMenores[2];
            resultado[3] = notasMaiores[3] + notasMenores[3];
            resultado[4] = notasMaiores[4] + notasMenores[4];
            resultado[5] = notasMaiores[5] + notasMenores[5];

            return resultado;        
        }

        public bool ValidaDadosCadastrais(string dataAdmissao, string salario)
        {
            bool dataAdmissaoValida = DateTime.TryParseExact(dataAdmissao, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
            bool salarioValido = double.TryParse(salario, out double valorDoSalario);

            if (salarioValido)
            {
                if (valorDoSalario <= 0)
                {
                    salarioValido = false;
                }
            }

            return dataAdmissaoValida && salarioValido;
        }
    }
}
