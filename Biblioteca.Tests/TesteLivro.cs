using BibliotecaAgil.Main.Core;
using Xunit;
namespace BibliotecaAgil.Tests;

public class UnitTest1
{
    [Fact]
    public void CriarLivroCorreto()
    {
        Livro lv = new Livro("Livro de teste 1", "Autor de teste", 2002);

        Assert.Equal(lv.Status, LivroStatus.Disponivel);
    }

    [Fact]
    public void EmprestimoLivroDisponivel()
    {
        string pessoa = "Mariazinha";
        Livro lv = new Livro("Livro de teste 1", "Autor de teste", 2002);
        LivroEmprestimoResposta res = lv.Emprestar(pessoa);

        Assert.Equal(lv.EmprestadoPara, pessoa);
        Assert.Equal(lv.Status, LivroStatus.Indisponivel);
        Assert.Equal(res.sucesso, true);
    }
    [Fact]
    public void EmprestimoLivroJaEmprestado()
    {
        string pessoa = "Mariazinha";
        string pessoa2 = "Carlinhos";
        Livro lv = new Livro("Livro de teste 1", "Autor de teste", 2002);
        lv.Emprestar(pessoa);
        LivroEmprestimoResposta res = lv.Emprestar(pessoa2);

        Assert.Equal(lv.Status, LivroStatus.Indisponivel);
        Assert.Equal(lv.EmprestadoPara, pessoa);
        Assert.Equal(res.sucesso, false);
    }
    [Fact]
    public void DevolverLivroDisponivel()
    {
        string pessoa = "Mariazinha";
        Livro lv = new Livro("Livro de teste 1", "Autor de teste", 2002);
        LivroEmprestimoResposta res = lv.DevolverLivro();

        Assert.Equal(lv.EmprestadoPara, null);
        Assert.Equal(lv.Status, LivroStatus.Disponivel);
        Assert.Equal(res.sucesso, false);
    }
    [Fact]
    public void DevolverLivroJaEmprestado()
    {
        string pessoa = "Mariazinha";
        Livro lv = new Livro("Livro de teste 1", "Autor de teste", 2002);
        LivroEmprestimoResposta res = lv.Emprestar(pessoa);
        res = lv.DevolverLivro();

        Assert.Equal(lv.Status, LivroStatus.Disponivel);
        Assert.Equal(lv.EmprestadoPara, null);
        Assert.Equal(res.sucesso, true);
    }

}