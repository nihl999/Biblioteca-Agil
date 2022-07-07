namespace BibliotecaAgil.Main.Infra
{
    public interface IRepository<TEntity>
    {
        public void Adicionar(TEntity entity);
        public bool Remover(TEntity entity);
        public bool Atualizar(TEntity entity);
        public TEntity BuscarPorID(int ID);
        public TEntity Existe(TEntity entity);
        public List<TEntity> BuscarTodos();
    }
}