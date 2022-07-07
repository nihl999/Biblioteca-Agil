namespace BibliotecaAgil.Main.Core;

public class Livro
{
    public int ID { get; set; }
    public string Titulo { get; private set; }
    public string Autor { get; private set; }
    public LivroStatus Status { get; set; }
    public int Ano { get; private set; }
    public string? EmprestadoPara { get; set; }

    public Livro(string _titulo, string _autor, int _ano)
    {
        ID = 0;
        Titulo = _titulo;
        Autor = _autor;
        Ano = _ano;
        Status = LivroStatus.Disponivel;
        EmprestadoPara = null;
    }

    public LivroEmprestimoResposta Emprestar(string _pessoa)
    {
        if (Status == LivroStatus.Disponivel)
        {
            EmprestadoPara = _pessoa;
            Status = LivroStatus.Indisponivel;
            return new LivroEmprestimoResposta { sucesso = true, mensagem = $"Livro emprestado com sucesso para: {_pessoa}" };
        }
        return new LivroEmprestimoResposta { sucesso = false, mensagem = "Não foi possivel emprestar este livro" };
    }

    public LivroEmprestimoResposta DevolverLivro()
    {
        if (Status == LivroStatus.Indisponivel)
        {
            Status = LivroStatus.Disponivel;
            EmprestadoPara = null;
            return new LivroEmprestimoResposta { sucesso = true, mensagem = "Livro devolvido com sucesso!" };
        }
        return new LivroEmprestimoResposta { mensagem = "Livro não está emprestado!" };
    }
}
