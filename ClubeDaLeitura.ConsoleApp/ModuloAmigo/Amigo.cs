using ClubeDaLeitura.ConsoleApp.Compartilhado;
using System.Text.RegularExpressions;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo;

public class Amigo : EntidadeBase
{
    public string Nome { get; set; }
    public string NomeResponsavel { get; set; }
    public string Telefone { get; set; }

    public Amigo(string nome, string nomeResponsavel, string telefone)
    {
        Nome = nome;
        NomeResponsavel = nomeResponsavel;
        Telefone = telefone;
    }

    public override void AtualizarRegistro(EntidadeBase registroAtualizado)
    {
        Amigo amigoAtualizado = (Amigo)registroAtualizado;

        this.Nome = amigoAtualizado.Nome;
        this.NomeResponsavel = amigoAtualizado.NomeResponsavel;
        this.Telefone = amigoAtualizado.Telefone;
    }

    public override string Validar()
    {
        string erros = string.Empty;

        if (Nome.Length < 3 || Nome.Length > 100)
            erros += "O campo \"Nome\" deve conter entre 3 e 100 caracteres.";

        if (NomeResponsavel.Length < 3 || NomeResponsavel.Length > 100)
            erros += "O campo \"Nome do Responsável\" deve conter entre 3 e 100 caracteres.";

        if (!Regex.IsMatch(Telefone, @"^\(?\d{2}\)?\s?(9\d{4}|\d{4})-?\d{4}$"))
            erros += "O campo \"Telefone\" deve seguir o padrão (DDD) 90000-0000.";

        return erros;
    }
}