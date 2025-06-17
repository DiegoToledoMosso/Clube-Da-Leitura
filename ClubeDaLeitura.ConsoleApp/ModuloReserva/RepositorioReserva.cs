using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

namespace ClubeDaLeitura.ConsoleApp.ModuloReserva;

public class RepositorioReserva : RepositorioBase
{
    public Reserva[] SelecionarReservaAtivas()
    {
        int contadorReservaAtivas = 0;

        for (int i = 0; i < registros.Length; i++)
        {
            Reserva reservaAtual = (Reserva)registros[i];

            if (reservaAtual == null)
                continue;

            if (reservaAtual.EstaAtiva)
                contadorReservaAtivas++;
        }

        Reserva[] reservaAtivas = new Reserva[contadorReservaAtivas];

        int contadorAuxiliar = 0;

        for (int i = 0; i < registros.Length; i++)
        {
            Reserva reservaAtual = (Reserva)registros[i];

            if (reservaAtual == null)
                continue;

            if (reservaAtual.EstaAtiva)
            {
                reservaAtivas[contadorAuxiliar++] = (Reserva)registros[i];
            }
        }
        return reservaAtivas;
    }
}

