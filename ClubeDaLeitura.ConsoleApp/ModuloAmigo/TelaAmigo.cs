using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo;

public class TelaAmigo : TelaBase
{

    private RepositorioEmprestimo repositorioEmprestimo;

    public TelaAmigo(RepositorioAmigo repositorio, RepositorioEmprestimo repositorioEmprestimo) : base("Amigo", repositorio)
    {
        this.repositorioEmprestimo = repositorioEmprestimo;
    }

    public override void CadastrarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine($"Cadastro de {nomeEntidade}");

        Console.WriteLine();

        Amigo novoRegistro = (Amigo)ObterDados();

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
            Amigo amigoRegistrado = (Amigo)registros[i];    

            if (amigoRegistrado == null) 
                continue;

            if (amigoRegistrado.Nome == novoRegistro.Nome || amigoRegistrado.Telefone == novoRegistro.Telefone)
            {

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Um amigo com este nome ou telefone já foi cadastrado");
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

        Amigo registroAtualizado = (Amigo)ObterDados();

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
            Amigo amigoRegistrado = (Amigo)registros[i];

            if (amigoRegistrado == null)
                continue;

            if (amigoRegistrado.Id != idSelecionado &&
               (amigoRegistrado.Nome == registroAtualizado.Nome ||
                amigoRegistrado.Telefone == registroAtualizado.Telefone)
            )
            {

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Um amigo com este nome ou telefone já foi cadastrado");
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

        Console.WriteLine("Visualização de Amigos");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -10} | {1, -30} | {2, -30} | {3, -30} | {4, -15} ",
            "Id", "Nome", "Responsável", "Telefone" , "Multa Ativa"
        );

        EntidadeBase[] amigos = repositorio.SelecionarRegistros();

        EntidadeBase[] emprestimos = repositorioEmprestimo.SelecionarRegistros();

        for (int i = 0; i < amigos.Length; i++)
        {
            Amigo a = (Amigo)amigos[i];

            if (a == null)
                continue;

            bool amigoTemMultaAtiva = false;


            for (int j = 0; j < emprestimos.Length; j++)
            {
                Emprestimo e = (Emprestimo)emprestimos[j];

                if (e == null) 
                    continue;

                if (a == e.Amigo && e.Multa != null)
                {
                    if(!e.MultaPaga)
                        amigoTemMultaAtiva = true;
                }
            }

            string stringMultaAtiva = amigoTemMultaAtiva ? "Sim" : "Não";

            Console.WriteLine(
              "{0, -10} | {1, -30} | {2, -30} | {3, -30} | {4, -15}",
                a.Id, a.Nome, a.NomeResponsavel, a.Telefone, stringMultaAtiva
            );
        }

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write($"\nDigite ENTER para continuar...");
        Console.ResetColor();

        Console.ReadLine();
    }

    protected override EntidadeBase ObterDados()
    {
        Console.Write("Digite o nome do amigo: ");
        string nome = Console.ReadLine();

        Console.Write("Digite o nome do responsável pelo amigo: ");
        string nomeResponsavel = Console.ReadLine();

        Console.Write("Digite o telefone do amigo ou responsável: ");
        string telefone = Console.ReadLine();

        Amigo amigo = new Amigo(nome, nomeResponsavel, telefone);

        return amigo;   
        
    }
}
