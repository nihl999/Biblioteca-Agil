using BibliotecaAgil.Main.Core;
using BibliotecaAgil.Main.Infra;
using Xunit;

namespace BibliotecaAgil.Tests
{
    public class TesteBiblioteca
    {
        [Fact]
        public void AdicionarLivro()
        {
            Biblioteca bib = new Biblioteca(new RepositorioLivros());
            var baterMartelo = new Livro("Como fazer sentido e bater o martelo", "Alexandro Aolchique", 2017);
            var basquete101 = new Livro("Sejamos todos feministas", "Chimamanda Ngozi Adichie", 2015);
            var todosFeministas = new Livro("Basquete 101", "Hortência Marcari", 2010);

            var retorno = bib.AdicionarLivro(basquete101);
            Assert.Equal(true, retorno);

        }
        [Fact(Skip = "Mudança na arquitetura")]
        public void AdicionarLivroPorValores()
        {
            Biblioteca bib = new Biblioteca(new RepositorioLivros());

            var retorno = bib.AdicionarLivro("Basquete 101", "Hortência Marcari", 2010);
            Assert.Equal(true, retorno);

        }
        [Fact]
        public void FalhaAdicionarLivroExistente()
        {
            Biblioteca bib = new Biblioteca(new RepositorioLivros());
            var baterMartelo = new Livro("Como fazer sentido e bater o martelo", "Alexandro Aolchique", 2017);
            var todosFeministas = new Livro("Sejamos todos feministas", "Chimamanda Ngozi Adichie", 2015);
            var basquete101 = bib.livros.BuscarPorID(1);

            var retorno = bib.AdicionarLivro(basquete101);
            Assert.Equal(false, retorno);

        }
        [Fact]
        public void RetirarLivro()
        {
            Biblioteca bib = new Biblioteca(new RepositorioLivros());

            var retorno = bib.RetirarLivro(1, "Maria");
            Assert.Equal(true, retorno);

        }
        [Fact]
        public void FalhaRetirarLivroJaEmprestado()
        {
            Biblioteca bib = new Biblioteca(new RepositorioLivros());

            var retorno = bib.RetirarLivro(1, "Maria");
            retorno = bib.RetirarLivro(1, "Joao");
            Assert.Equal(false, retorno);

        }
        [Fact]
        public void DevolverLivro()
        {
            Biblioteca bib = new Biblioteca(new RepositorioLivros());

            var retorno = bib.RetirarLivro(1, "Maria");
            retorno = bib.DevolverLivro(1);
            Assert.Equal(LivroStatus.Disponivel, bib.livros.BuscarPorID(1).Status);
            Assert.Equal(true, retorno);
        }
        [Fact]
        public void DevolverLivroJáDevolvido()
        {
            Biblioteca bib = new Biblioteca(new RepositorioLivros());

            var retorno = bib.RetirarLivro(1, "Maria");
            retorno = bib.DevolverLivro(1);
            retorno = bib.DevolverLivro(1);
            Assert.Equal(LivroStatus.Disponivel, bib.livros.BuscarPorID(1).Status);
            Assert.Equal(false, retorno);
        }
    }
}