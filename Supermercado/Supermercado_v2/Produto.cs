using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermercado_v2
{
    [Serializable]
    class Produto
    {
        public enum Categoria
        {
            Congelados = 1,
            Prateleira = 2,
            Enlatados = 3
        };

        public Categoria categoria;
        public string descricao;
        public float preço;
        public int quantidade;



        public Produto(Categoria categoria, string descricao, float preço,int quantidade)
        {
            this.categoria = categoria;
            this.descricao = descricao;
            this.preço = preço;
            this.quantidade = quantidade;
           
        }

        public Produto(Produto categoria, string descrição)
        {
        }

        public override string ToString()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            return " | Categoria: " + categoria + " | Descrição: " + descricao + " | Preço: " + preço+ "€" + "  | Quantidade em Stock: " + quantidade;
        }
    }
}
