using System.Collections.Generic;
namespace gerenciamentoCarrinhoService.Model
{
    public class Carrinho
    {
        public Carrinho(int id, List<Livro> livros) {
            this.id = id;
            this.livros = livros;
        }

        private int id;
        private List<Livro> livros;
        
        public int Id
        {
            get { return id;}
            set { id= value;}
        }

         public List<Livro> Livros{
            get {return livros;}
            set {livros= value;}
        }    
    }
}