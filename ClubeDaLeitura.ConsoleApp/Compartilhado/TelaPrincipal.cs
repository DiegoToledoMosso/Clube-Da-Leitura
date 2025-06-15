using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixas;

namespace ClubeDaLeitura.ConsoleApp.Compartilhado;

public class TelaPrincipal
{
    private char opcaoEscolhida;

    private RepositorioAmigo repositorioAmigo;
    private TelaAmigo telaAmigo;
   
    private RepositorioCaixa repositorioCaixa;
    private TelaCaixa telaCaixa;

    public TelaPrincipal()
    {      
        repositorioAmigo = new RepositorioAmigo();
        telaAmigo = new TelaAmigo(repositorioAmigo);

        repositorioCaixa = new RepositorioCaixa();
        telaCaixa = new TelaCaixa(repositorioCaixa);
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
            return null;

        else if (opcaoEscolhida == '4')
            return null;

        return null;

    }
        
}

