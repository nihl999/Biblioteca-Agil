using BibliotecaAgil.Main.Infra;

namespace BibliotecaAgil.Main.Core
{
    public class Biblioteca
    {
        public IRepository<Livro> livros;
        private int idAtual = 0;

        public Biblioteca(IRepository<Livro> _livros)
        {
            livros = _livros;
        }

        public bool AdicionarLivro(Livro lv)
        {
            if (livros.Existe(lv) != null) return false;
            lv.ID = idAtual;
            idAtual++;
            livros.Adicionar(lv);
            return true;
        }

        public bool RetirarLivro(int id, string nomePessoa)
        {
            var livroARetirar = livros.BuscarPorID(id);
            if (livroARetirar == null || livroARetirar.Status == LivroStatus.Indisponivel) return false;
            livroARetirar.Status = LivroStatus.Indisponivel;
            livroARetirar.EmprestadoPara = nomePessoa;
            livros.Atualizar(livroARetirar);
            return true;
        }

        public bool DevolverLivro(int id)
        {
            var livroARetirar = livros.BuscarPorID(id);
            if (livroARetirar == null || livroARetirar.Status == LivroStatus.Disponivel) return false;
            livroARetirar.Status = LivroStatus.Disponivel;
            livroARetirar.EmprestadoPara = null;
            livros.Atualizar(livroARetirar);
            return true;
        }

        public List<Livro> ListarTodosOsLivros()
        {
            return livros.BuscarTodos();
        }
    }
}