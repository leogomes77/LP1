using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Supermercado_v2
{
    class Stock
    {
        List<Produto> stock = new List<Produto>();

        public Stock(List<Produto> stock)
        {
            this.stock = stock;
        }

        public Stock()
        {
        }


        //Guardar Stock
        public void SaveStock()
        {
            string localizacaoDoFicheiro = Directory.GetCurrentDirectory();
            string nomeDoFicheiro = "Stock.txt";


            //Validação
            if (File.Exists(nomeDoFicheiro))
            {
                File.Delete(nomeDoFicheiro);
            }

            FileStream fileStream = File.Create(nomeDoFicheiro);
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            foreach (Produto produtoAtual in stock)
            {
                binaryFormatter.Serialize(fileStream, produtoAtual);
            }

            fileStream.Close();
        }


        //Carregar do ficheiro para memória
        public void leituraStock()
        {
            string nomeDoFicheiro = "Stock.txt";

            //Validacao
            if (File.Exists(nomeDoFicheiro))
            {
                FileStream fileStream = File.OpenRead(nomeDoFicheiro);
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                while (fileStream.Position < fileStream.Length)
                {
                    Produto produtoLido = binaryFormatter.Deserialize(fileStream) as Produto;
                    stock.Add(produtoLido);
                }

                fileStream.Close();
            }
            else
            {
                Console.WriteLine("Ficheiro Não existe");
            }
        }

        //Adicinar e Guardar um Produto 
        public int AdicionarProduto(Produto.Categoria categoria, string descrição, float preço, int quantidade)
        {
            Produto novoProduto = new Produto(categoria, descrição, preço, quantidade);

            bool existe = false;
            //Ver se já existe
            foreach (Produto produto in stock)
            {
                if (String.Compare(novoProduto.descricao, produto.descricao) == 0)
                {
                    existe = true;
                    produto.quantidade += quantidade;
                }
            }
            if (existe == true)
            {
                Console.WriteLine("Produto Adicionado com Sucesso");
                SaveStock();
                return 1;
            }
            else
            {
                stock.Add(novoProduto);
                SaveStock();
                return 1;
            }
            return 0;
        }


        //Listagem Dos Produtos 
        public void ListarProdutos()
        {
            foreach (Produto produto in stock)
            {
                Console.WriteLine(produto.ToString());
            }

        }

        //Remover Produtos

        public int RemoverStock(string descricao, int quantidade)
        {
            int encontrou = 0;

            //Procura na lista de produto , o produtos em questão e se a quantidade do produto for maior que 1 ele subtrai 1 quantidade
            foreach (Produto produto in stock)
            {
                if (String.Compare(descricao, produto.descricao) == 0 && produto.quantidade > 0)
                {
                    encontrou = 1;
                    produto.quantidade -= quantidade;
                    if(produto.quantidade <= 0)
                    {
                        produto.quantidade = 0;
                    }
                }
            }
            if (encontrou == 1)
            {
                SaveStock();
                return 1;
            }
            //Remove o produto da lista se a quantidade for 1 
            if (encontrou == 1)
            {
                int produtoApagado = stock.RemoveAll(Produto => Produto.descricao == descricao);
                SaveStock();
                return 1;
            }
            //Se nao for nenhum destes casos retorna 0
            else
            {
                return 0;
            }
        }


        //Limpar o Stock Todo

        public int RemoverStock(string descricao)
        {
            bool encontrou = false;
            foreach (Produto produto in stock)
            {
                if (String.Compare(descricao, produto.descricao) == 0)
                {
                    encontrou = true;
                                      
                }
            }

            if (encontrou == true)
            {
                int produtoApagado = stock.RemoveAll(Produto => Produto.descricao == descricao);
                SaveStock();
                return 1;
            }
            return 0;
        }

        //Vender um produto ou muitos
    }
}
