using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixas;

public class TelaCaixa : TelaBase
{
    public TelaCaixa(RepositorioCaixa repositorio) : base("Caixa", repositorio)
    {

    }
    public override void CadastrarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine($"Cadastro de {nomeEntidade}");

        Console.WriteLine();

        Caixa novoRegistro = (Caixa)ObterDados();

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
            Caixa CaixaRegistrada = (Caixa)registros[i];

            if (CaixaRegistrada == null)
                continue;

            if (CaixaRegistrada.Etiqueta == novoRegistro.Etiqueta)
            {

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Uma caixa já contém essa etiqueta registrada");
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

        Caixa registroAtualizado = (Caixa)ObterDados();

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
            Caixa caixaRegistrada = (Caixa)registros[i];

            if (caixaRegistrada == null)
                continue;

            if (caixaRegistrada.Id != idSelecionado &&
               (caixaRegistrada.Etiqueta == registroAtualizado.Etiqueta )
            )
            {

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Uma caixa já contém essa etiqueta registrada");
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