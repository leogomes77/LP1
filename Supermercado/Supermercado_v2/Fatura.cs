using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermercado_v2
{
    [Serializable]
    class Fatura
    {
        public string nomeFuncionario;
        public string nomeCliente;
        public float preçoTotal;
        public Produto produto;
        public List<Produto> listaProdutos;
        public ArrayList quantidades;
        public int quantidade;
        public Fatura()
        {
        }

        public Fatura(string nomeFuncionario, string nomeCliente, float preçoTotal, Produto produto, int quantidade)
        {
            this.nomeFuncionario = nomeFuncionario;
            this.nomeCliente = nomeCliente;
            this.preçoTotal = preçoTotal;
            this.produto = produto;
            this.quantidade = quantidade;
        }

        public Fatura(string nomeFuncionario, string nomeCliente, float preçoTotal, List<Produto> listaProdutos, ArrayList quantidades)
        {
            this.nomeFuncionario = nomeFuncionario;
            this.nomeCliente = nomeCliente;
            this.preçoTotal = preçoTotal;
            this.listaProdutos = listaProdutos;
            this.quantidades = quantidades;
        }

        public override string ToString()
        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            int i = 0;
            string faturaString =  "#######################################################\n"
                +"\t            Fatura  \n " 
                + " |Nome Funcionario:" + nomeFuncionario
                + "\n |Nome Cliente: " + nomeCliente
                + "\n |Preço Total: " + preçoTotal +"€\n";

            foreach(Produto produto in listaProdutos)
            {
                faturaString += produto.ToString2() + " " + " | Quantidade: " + quantidades[i] + " |\n";
                i++;
            }

            faturaString += "\n#######################################################\n";
            return faturaString;
                
        }

        public string ToString2()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string faturaString = "#######################################################\n"
               + "\t               Fatura   \n" 
               + "\n|Nome Funcionario:" + nomeFuncionario
               + "\n|Nome Cliente: " + nomeCliente
               + "\n|Preço Total: " + preçoTotal + "€\n";
            faturaString += "\n#######################################################\n";

            return faturaString;
        }
    }
}
