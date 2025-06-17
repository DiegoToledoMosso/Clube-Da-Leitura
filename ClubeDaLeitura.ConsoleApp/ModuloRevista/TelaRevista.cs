using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloCaixas;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista;

public class TelaRevista : TelaBase
{
    private RepositorioCaixa repositorioCaixa;

    public TelaRevista(RepositorioRevista repositorio, RepositorioCaixa repositorioCaixa) : base("Revista", repositorio)
    {
        this.repositorioCaixa = repositorioCaixa;
    }

    public override void CadastrarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine($"Cadastro de {nomeEntidade}");

        Console.WriteLine();

        Revista novoRegistro = (Revista)ObterDados();

        string erros = novoRegistro.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(erros);
            Console.ResetColor();

            Console.Write("\nDigite ENTER para continuar...");
            Console.ReadLine();

            CadastrarRegistro();

            return;
        }

        EntidadeBase[] registros = repositorio.SelecionarRegistros();

        for (int i = 0; i < registros.Length; i++)
        {
            Revista RevistaRegistrada = (Revista)registros[i];

            if (RevistaRegistrada == null)
                continue;

            if (RevistaRegistrada.Titulo == novoRegistro.Titulo || RevistaRegistrada.NumeroEdicao == novoRegistro.NumeroEdicao)
            {

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Uma revista já contem esse título ou número de edição cadastrada");
                Console.ResetColor();

                Console.Write("\nDigite ENTER para continuar...");
                Console.ReadLine();

                CadastrarRegistro();

                return;
            }
        }

        repositorio.CadastrarRegistro(novoRegistro);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n{nomeEntidade} cadastrado com sucesso!");
        Console.ResetColor();

        Console.ReadLine();
    }

    public override void EditarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine($"Edição de {nomeEntidade}");

        Console.WriteLine();

        VisualizarRegistros(false);

        Console.Write("Digite o id do registro que deseja selecionar: ");
        int idSelecionado = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine();

        Revista registroAtualizado = (Revista)ObterDados();

        string erros = registroAtualizado.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(erros);
            Console.ResetColor();

            Console.Write("\nDigite ENTER para continuar...");
            Console.ReadLine();

            EditarRegistro();

            return;
        }

        EntidadeBase[] registros = repositorio.SelecionarRegistros();

        for (int i = 0; i < registros.Length; i++)
        {
            Revista revistaRegistrada = (Revista)registros[i];

            if (revistaRegistrada == null)
                continue;

            if (revistaRegistrada.Id != idSelecionado &&
               (revistaRegistrada.Titulo == registroAtualizado.Titulo ||
               revistaRegistrada.NumeroEdicao == registroAtualizado.NumeroEdicao
               )
            )
            {

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Uma revista já contem esse título ou número de edição cadastrada");
                Console.ResetColor();

                Console.Write("\nDigite ENTER para continuar...");
                Console.ReadLine();

                EditarRegistro();

                return;
            }
        }
        repositorio.EditarRegistro(idSelecionado, registroAtualizado);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n{nomeEntidade} editado com sucesso!");
        Console.ResetColor();

        Console.ReadLine();
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