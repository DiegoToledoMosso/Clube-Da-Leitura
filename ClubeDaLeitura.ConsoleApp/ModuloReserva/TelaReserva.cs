using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

namespace ClubeDaLeitura.ConsoleApp.ModuloReserva;

public class TelaReserva : TelaBase
{

    private RepositorioReserva repositorioReserva;
    private RepositorioAmigo repositorioAmigo;
    private RepositorioRevista repositorioRevista;
    public TelaReserva(RepositorioReserva repositorio, RepositorioAmigo repositorioAmigo, RepositorioRevista repositorioRevista) : base("Reserva", repositorio)
    {
        repositorioReserva = repositorio;
        this.repositorioAmigo = repositorioAmigo;
        this.repositorioRevista = repositorioRevista;        
    }

    public override char ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine($"1 - Cadastro de {nomeEntidade}");
        Console.WriteLine($"2 - Cancelamento de {nomeEntidade}");
        Console.WriteLine($"3 - Visualização de {nomeEntidade} ativas");        
        Console.WriteLine($"S - Sair");

        Console.WriteLine();

        Console.Write("Digite uma opção válida: ");
        char opcaoEscolhida = Console.ReadLine().ToUpper()[0];

        return opcaoEscolhida;
    }

    public void CadastrarReserva()
    {
        ExibirCabecalho();

        Console.WriteLine($"Cadastro de {nomeEntidade}");

        Console.WriteLine();

        Reserva novaReserva = (Reserva)ObterDados();

        string erros = novaReserva.Validar();

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

        Reserva[] reservasAtivas = repositorioReserva.SelecionarReservaAtivas();

        for (int i = 0; i < reservasAtivas.Length; i++)
        {
            Reserva reservaAtiva = reservasAtivas[i];

            if (novaReserva.Amigo.Id == reservaAtiva.Amigo.Id)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("O amigo selecionado já tem uma reserva ativa!");
                Console.ResetColor();

                Console.Write("\nDigite ENTER para continuar...");
                Console.ReadLine();

                return;
            }
        }

        novaReserva.Inciar();

        repositorio.CadastrarRegistro(novaReserva);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n{nomeEntidade} cadastrada com sucesso!");
        Console.ResetColor();

        Console.ReadLine();
    }

    public void CancelarReserva()
    {

        ExibirCabecalho();

        Console.WriteLine($"Cancelamento de {nomeEntidade}");

        Console.WriteLine();

        VisualizarRegistros(false);


        Console.Write("Digite o ID da reserva que deseja cancelar: ");
        int idReserva = Convert.ToInt32(Console.ReadLine());


        Reserva reservaSelecionada = (Reserva)repositorio.SelecionarRegistroPorId(idReserva);

        if (reservaSelecionada == null)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("A reserva selecionada não existe!");
            Console.ResetColor();

            Console.Write("\nDigite ENTER para continuar...");
            Console.ReadLine();

            return;
        }

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("Deseja confirmar o cancelamento da reserva? (S/N):  ");
        Console.ResetColor();

        string resposta = Console.ReadLine()!;

        if (resposta.ToUpper() == "S")
            return;

        reservaSelecionada.Concluir();


        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n Cancelamento de {nomeEntidade} concluído com sucesso!");
        Console.ResetColor();
        Console.ReadLine();
    }

    public override void VisualizarRegistros(bool exibirCabecalho)
    {
        if (exibirCabecalho == true)
            ExibirCabecalho();

        Console.WriteLine("Visualização de Reservas Ativas");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -5} | {1, -10} | {2, -10} | {3, -25} | {4, -25}",
            "Id", "Amigo", "Revista", "Data da Reserva", "Status"
        );

        Reserva[] reservas = repositorioReserva.SelecionarReservaAtivas();


        for (int i = 0; i < reservas.Length; i++)
        {
            Reserva r = (Reserva)reservas[i];

            if (r == null)
                continue;

            string statusReserva = r.EstaAtiva ? "Ativa" : "Concluída";
                        

            Console.WriteLine(
              "{0, -5} | {1, -10} | {2, -10} | {3, -25} | {4, -25}",
                r.Id, r.Amigo.Nome, r.Revista.Titulo, r.DataAbertura.ToShortDateString(), statusReserva
            );

            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write($"\nDigite ENTER para continuar...");
        Console.ResetColor();

        Console.ReadLine();
    }

    protected override EntidadeBase ObterDados()
    {
        VisualizarAmigos();

        Console.Write("Digite o ID do amigo que irá reservar a revista: ");
        int idAmigo = Convert.ToInt32(Console.ReadLine());

        Amigo amigoSelecionado = (Amigo)repositorioAmigo.SelecionarRegistroPorId(idAmigo);

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("\nAmigo selecionado com sucesso");
        Console.ResetColor();

        VisualizarRevistaDisponiveis();

        Console.Write("Digite o ID da Revista que irá será reservada: ");
        int idRevista = Convert.ToInt32(Console.ReadLine());

        Revista revistaSelecionada = (Revista)repositorioRevista.SelecionarRegistroPorId(idRevista);

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("\nRevista selecionado com sucesso");
        Console.ResetColor();

        Reserva reserva = new Reserva(amigoSelecionado, revistaSelecionada);

        return reserva;
    }

    private void VisualizarAmigos()
    {

        Console.WriteLine();

        Console.WriteLine("Visualização de Amigos");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -10} | {1, -30} | {2, -30} | {3, -30}",
            "Id", "Nome", "Responsável", "Telefone"
        );

        EntidadeBase[] amigos = repositorioAmigo.SelecionarRegistros();


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

    private void VisualizarRevistaDisponiveis()
    {

        Console.WriteLine();

        Console.WriteLine("Visualização de Revistas");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -5} | {1, -20} | {2, -20} | {3, -20} | {4, -10} | {5, -15}",
            "Id", "Titulo", "Numero da Edicao", "Ano da Publicacao", "Caixa", "Status"
        );

        EntidadeBase[] revistasDisponiveis = repositorioRevista.SelecionarRevistasDisponiveis();


        for (int i = 0; i < revistasDisponiveis.Length; i++)
        {
            Revista r = (Revista)revistasDisponiveis[i];

            if (r == null)
                continue;

            Console.WriteLine(
               "{0, -5} | {1, -20} | {2, -20} | {3, -20} | {4, -10} | {5, -15}",
                r.Id, r.Titulo, r.NumeroEdicao, r.AnoPublicacao, r.Caixa.Etiqueta, r.Status
            );
        }

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write($"\nDigite ENTER para continuar...");
        Console.ResetColor();

        Console.ReadLine();
    }
}
