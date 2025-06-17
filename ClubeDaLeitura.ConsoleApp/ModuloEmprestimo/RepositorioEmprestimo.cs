using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

public class RepositorioEmprestimo : RepositorioBase
{
    public Emprestimo[] SelecionarEmprestimosAtivos()
    {
        int contadorEmprestimoAtivos = 0;

        for (int i = 0; i < registros.Length; i++)
        {
            Emprestimo emprestimoAtual = (Emprestimo)registros[i];

            if (registros[i] == null)
                continue;

            if (emprestimoAtual.Status == "Aberto" || emprestimoAtual.Status == "Atrasado")
                contadorEmprestimoAtivos++;
        }

        Emprestimo[] emprestimoAtivos = new Emprestimo[contadorEmprestimoAtivos];

        int contadorAuxiliar = 0;

        for (int i = 0; i < registros.Length; i++)
        {
            Emprestimo emprestimoAtual = (Emprestimo)registros[i];

            if (emprestimoAtual == null)
                continue;

            if (emprestimoAtual.Status == "Aberto" || emprestimoAtual.Status == "Atrasado")
            {
                emprestimoAtivos[contadorAuxiliar++] = (Emprestimo)registros[i];
            }
                
        }

        return emprestimoAtivos;
    }

    public EntidadeBase[] SelecionarEmprestimosComMulta()
    {
        int contadorEmprestimosComMulta = 0;

        for (int i = 0; i < registros.Length; i++)
        {
            Emprestimo emprestimoAtual = (Emprestimo)registros[i];

            if (registros[i] == null)
                continue;

            if (emprestimoAtual.Status == "Aberto" && emprestimoAtual.Multa != null && !emprestimoAtual.MultaPaga)
                contadorEmprestimosComMulta++;
        }

        Emprestimo[] emprestimosComMulta = new Emprestimo[contadorEmprestimosComMulta];

        int contadorAuxiliar = 0;

        for (int i = 0; i < registros.Length; i++)
        {
            Emprestimo emprestimoAtual = (Emprestimo)registros[i];

            if (emprestimoAtual == null)
                continue;

            if (emprestimoAtual.Status == "Aberto" || emprestimoAtual.Multa != null && !emprestimoAtual.MultaPaga)
            {
                emprestimosComMulta[contadorAuxiliar++] = (Emprestimo)registros[i];
            }

        }

        return emprestimosComMulta;
    }
}


