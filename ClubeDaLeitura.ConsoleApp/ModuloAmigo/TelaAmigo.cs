using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo;

public class TelaAmigo : TelaBase
{
    public TelaAmigo(RepositorioAmigo repositorio) : base("Amigo", repositorio)
    {
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

        Console.WriteLine($"\n\"{nomeEntidade}\" cadastrado com sucesso!");
        Console.ReadLine();
    }

    public override void VisualizarRegistros(bool exibirCabecalho)
    {
        if (exibirCabecalho == true)
            ExibirCabecalho();

        Console.WriteLine("Visualização de Amigos");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -10} | {1, -30} | {2, -30} | {3, -30}",
            "Id", "Nome", "Responsável", "Telefone" 
        );

        EntidadeBase[] amigos = repositorio.SelecionarRegistros();
        

        for (int i = 0; i < amigos.Length; i++)
        {
            Amigo a = (Amigo)amigos[i];

            if (a == null)
                continue;
            
            

            Console.WriteLine(
              "{0, -10} | {1, -30} | {2, -30} | {3, -30}",
                a.Id, a.Nome, a.NomeResponsavel, a.Telefone
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
