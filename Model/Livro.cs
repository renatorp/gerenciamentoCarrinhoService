namespace gerenciamentoCarrinhoService.Model
{
    public class Livro
    {
        public Livro(int id, string titulo, string autor, double preco) {
            this.id = id;
            this.titulo = titulo;
            this.autor = autor;
            this.preco = preco;
        }

        public Livro() { }

        private int id;
        private string titulo;
        private string autor;
        private double preco;
        public int Id
        {
            get { return id;}
            set { id= value;}
        }

        public string Titulo{
            get {return titulo;}
            set {titulo = value;}
        }
         public string Autor{
            get {return autor;}
            set {autor= value;}
        }    

        public double Preco{
            get {return preco;}
            set {preco = value;}
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            return this.Id.Equals(((Livro)obj).Id);
        }
        
    }
}