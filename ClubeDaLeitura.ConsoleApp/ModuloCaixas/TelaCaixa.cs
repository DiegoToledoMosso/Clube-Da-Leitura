using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixas;

public class TelaCaixa : TelaBase
{
    public TelaCaixa(RepositorioCaixa repositorio) : base("Caixa", repositorio)
    {

    }

    public override void VisualizarRegistros(bool exibirCabecalho)
    {
        if (exibirCabecalho == true)
            ExibirCabecalho();

        Console.WriteLine("Visualização de Caixas");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -10} | {1, -30} | {2, -30} | {3, -30}",
            "Id", "Etiqueta", "Cor", "Dias de Empréstimos"
        );

        EntidadeBase[] caixas = repositorio.SelecionarRegistros();


        for (int i = 0; i < caixas.Length; i++)
        {
            Caixa c = (Caixa)caixas[i];

            if (c == null)
                continue;


            Console.WriteLine(
              "{0, -10} | {1, -30} | {2, -30} | {3, -30}",
                c.Id, c.Etiqueta, c.Cor, c.DiasEmprestimo
            );
        }

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write($"\nDigite ENTER para continuar...");
        Console.ResetColor();

        Console.ReadLine();
    }

    protected override EntidadeBase ObterDados()
    {
        Console.Write("Digite a etiqueta da caixa: ");
        string etiqueta = Console.ReadLine();

        Console.Write("Digite a cor da caixa: ");
        string cor = Console.ReadLine();

        Console.Write("Dias de empréstimos (opcional) ");

        bool conseguiuConverter = int.TryParse(Console.ReadLine(), out int diasEmprestimo);

        Caixa caixa;

        if (conseguiuConverter)
            caixa = new Caixa(etiqueta, cor, diasEmprestimo);
        else
            caixa = new Caixa(etiqueta, cor);        
        
        return caixa;

    }
}