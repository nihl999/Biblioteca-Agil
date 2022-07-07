using BibliotecaAgil.Main.Core;

namespace BibliotecaAgil.Main.Infra
{
    public class RepositorioLivros : IRepository<Livro>
    {
        List<Livro> _livros = new List<Livro>();

        public List<Livro> BuscarTodos()
        {
            return _livros;
        }

        public void Adicionar(Livro entity)
        {
            _livros.Add(entity);
        }

        public bool Atualizar(Livro entity)
        {
            var lv = _livros.FirstOrDefault(x => x == entity);
            if (lv == null)
            {
                return false;
            }
            lv = entity;
            return true;
        }

        public Livro BuscarPorID(int ID)
        {
            var lv = _livros.Find(x => x.ID == ID);
            return lv;
        }

        public Livro Existe(Livro entity)
        {
            var lv = _livros.FirstOrDefault(x => x == entity);
            return lv;
        }

        public bool Remover(Livro entity)
        {
            var lv = _livros.FirstOrDefault(x => x == entity);
            if (lv == null)
            {
                return false;
            }
            _livros.Remove(entity);
            return true;
        }

    }
}