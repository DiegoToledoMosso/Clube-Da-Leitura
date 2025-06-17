using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixas;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.ModuloReserva;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.Compartilhado;

public class TelaPrincipal
{
    private char opcaoEscolhida;

    private RepositorioAmigo repositorioAmigo;
    private TelaAmigo telaAmigo;
   
    private RepositorioCaixa repositorioCaixa;
    private TelaCaixa telaCaixa;

    private RepositorioRevista repositorioRevista;
    private TelaRevista telaRevista;

    private RepositorioEmprestimo repositorioEmprestimo;
    private TelaEmprestimo telaEmprestimo;

    private RepositorioReserva repositorioReserva;
    private TelaReserva telaReserva;


    public TelaPrincipal()
    {      
        repositorioAmigo = new RepositorioAmigo();        
        repositorioCaixa = new RepositorioCaixa(); 
        repositorioRevista = new RepositorioRevista();   
        repositorioEmprestimo = new RepositorioEmprestimo();
        repositorioReserva = new RepositorioReserva();


        telaAmigo = new TelaAmigo(repositorioAmigo, repositorioEmprestimo);
        telaCaixa = new TelaCaixa(repositorioCaixa);
        telaRevista = new TelaRevista(repositorioRevista, repositorioCaixa);
        telaEmprestimo = new TelaEmprestimo(repositorioEmprestimo, repositorioAmigo, repositorioRevista);      
        telaReserva = new TelaReserva(repositorioReserva, repositorioAmigo, repositorioRevista);
    }

    public void ApresentarMenuPrincipal()
    {
        Console.Clear();

        Console.WriteLine("----------------------------------------");
        Console.WriteLine("|           Clube da Leitura           |");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        Console.WriteLine("1 - Controle de Amigos");
        Console.WriteLine("2 - Controle de Caixas");
        Console.WriteLine("3 - Controle de Revistas");
        Console.WriteLine("4 - Controle de Empréstimos");
        Console.WriteLine("5 - Controle de Reservas");
        Console.WriteLine("S - Sair");

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        opcaoEscolhida = Console.ReadLine()[0];        
    }

    public TelaBase ObeterTela()
    {
        if (opcaoEscolhida == '1')
            return telaAmigo;

        else if (opcaoEscolhida == '2')
            return telaCaixa;

        else if (opcaoEscolhida == '3')
            return telaRevista;

        else if (opcaoEscolhida == '4')
            return telaEmprestimo;

        else if (opcaoEscolhida == '5')
            return telaReserva;

        return null;

    }
        
}

