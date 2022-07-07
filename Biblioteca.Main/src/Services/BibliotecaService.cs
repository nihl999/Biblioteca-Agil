using BibliotecaAgil.Main.Core;
using BibliotecaAgil.Main.Infra;
namespace BibliotecaAgil.Main.Services
{
    public class BibliotecaService
    {
        Biblioteca biblioteca = new Biblioteca(new RepositorioLivros());
        public string caminhoCSVPersistencia = null;

        public BibliotecaService(string _caminhoCSVPersistencia)
        {
            caminhoCSVPersistencia = _caminhoCSVPersistencia;
            Desserializar(caminhoCSVPersistencia);
        }
        public BibliotecaService()
        {
            AdicionarLivrosPadrao();
        }

        private void AdicionarLivrosPadrao()
        {
            var baterMartelo = new Livro("Como fazer sentido e bater o martelo", "Alexandro Aolchique", 2017);
            var basquete101 = new Livro("Basquete 101", "Hortência Marcari", 2010);
            var todosFeministas = new Livro("Sejamos todos feministas", "Chimamanda Ngozi Adichie", 2015);
            biblioteca.AdicionarLivro(baterMartelo);
            biblioteca.AdicionarLivro(basquete101);
            biblioteca.AdicionarLivro(todosFeministas);
        }

        public bool DoarLivro(string titulo, string autor, int ano)
        {
            Livro lv = new Livro(titulo, autor, ano);
            return biblioteca.AdicionarLivro(lv);
        }

        public List<Livro> ListarLivrosEmprestadoAPessoa(string nome)
        {
            var livros = biblioteca.ListarTodosOsLivros();
            List<Livro> livrosEmprestados = livros.Where(x => x.EmprestadoPara == nome).ToList();
            return livrosEmprestados;
        }

        public bool Desserializar(string path)
        {
            if (!File.Exists(path))
            {
                AdicionarLivrosPadrao();
                Serializar(path);
                return true;
            }
            else
            {
                try
                {
                    using (var reader = new StreamReader(File.OpenRead(path)))
                    {

                        List<Livro> tempLivros = new List<Livro>();
                        reader.ReadLine();
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(';');
                            if (values.Length != 6)
                            {
                                Console.WriteLine("CSV incompativel! Faltam dados.");
                                throw new ArgumentException();
                            }
                            Livro tempLivro = new Livro(values[1], values[2], int.Parse(values[4]));
                            tempLivro.ID = int.Parse(values[0]);
                            tempLivro.Status = (LivroStatus)int.Parse(values[3]);
                            tempLivro.EmprestadoPara = values[5];

                            tempLivros.Add(tempLivro);
                        }
                        tempLivros.Sort((x, y) => x.ID.CompareTo(y.ID));
                        foreach (var lv in tempLivros)
                        {
                            biblioteca.AdicionarLivro(lv);
                        }
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public bool Serializar(string path)
        {
            try
            {
                using (var writer = File.CreateText(path))
                {
                    writer.WriteLine("ID;TITULO;AUTOR;STATUS;ANO;EMPRESTADOPARA");
                    foreach (var livro in biblioteca.ListarTodosOsLivros())
                    {
                        writer.WriteLine($"{livro.ID};{livro.Titulo};{livro.Autor};{(int)livro.Status};{livro.Ano};{livro.EmprestadoPara}");
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}