using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloCaixas;

public class TelaRevista : TelaBase
{
    private RepositorioCaixa repositorioCaixa;

    public TelaRevista(RepositorioRevista repositorio, RepositorioCaixa repositorioCaixa) : base("Revista", repositorio)
    {
        this.repositorioCaixa = repositorioCaixa;
    }

    public override void VisualizarRegistros(bool exibirCabecalho)
    {
        if (exibirCabecalho == true)
            ExibirCabecalho();

        Console.WriteLine("Visualização de Revistas");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -10} | {1, -30} | {2, -20} | {3, -20} | {4, -20} | {5, -20}",
            "Id", "Titulo", "Numero da Edicao", "Ano da Publicacao","Caixa",  "Status"
        );

        EntidadeBase[] revistas = repositorio.SelecionarRegistros();


        for (int i = 0; i < revistas.Length; i++)
        {
            Revista r = (Revista)revistas[i];

            if (r == null)
                continue;


            Console.WriteLine(
               "{0, -10} | {1, -30} | {2, -20} | {3, -20} | {4, -20} | {5, -20}",
                r.Id, r.Titulo, r.NumeroEdicao, r.AnoPublicacao,r.Caixa.Etiqueta, r.Status
            );
        }

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write($"\nDigite ENTER para continuar...");
        Console.ResetColor();

        Console.ReadLine();
    }

    protected override EntidadeBase ObterDados()
    {
        Console.Write("Digite o Titulo da revista: ");
        string titulo = Console.ReadLine();

        Console.Write("Digite o numero de edição da revista: ");
        int numeroEdicao = Convert.ToInt32(Console.ReadLine());

        Console.Write("Digite o ano de publicação da revista: ");
        int anoPublicacao = Convert.ToInt32(Console.ReadLine());

        VisualizarCaixas();

        Console.Write("\nDigite o id da caixa selecionada: ");
        int idCaixa = Convert.ToInt32(Console.ReadLine());

        Caixa caixaSelecionada = (Caixa)repositorioCaixa.SelecionarRegistroPorId(idCaixa);

        Revista revista = new Revista(titulo, numeroEdicao, anoPublicacao, caixaSelecionada);

        return revista;
    }

    public void VisualizarCaixas()
    {
       
        Console.WriteLine("Visualização de Caixas");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -10} | {1, -30} | {2, -30} | {3, -30}",
            "Id", "Etiqueta", "Cor", "Dias de Empréstimos"
        );

        EntidadeBase[] caixas = repositorioCaixa.SelecionarRegistros();


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
    }
}