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
            
        }


        //Listagem Dos Produtos 
        public void ListarProdutos()
        {
            foreach (Produto produto in stock)
            {
                Console.WriteLine(produto.ToString());
            }

        }


        //Remover Produto da lista
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

        //Atualizar quantidade de stock consoante a quantidade dos produtos que escolheu ao vender
        public int AtualizarStockFatura(string descricao, string quantidade)
        {
   
            foreach (Produto produto in stock)
            {
                if (String.Compare(descricao, produto.descricao) == 0)
                {             
                        produto.quantidade -= int.Parse(quantidade);
                        if (produto.quantidade <= 0) produto.quantidade = 0;
                        SaveStock();
                        return produto.quantidade;
                }
            }
            return 0;
        }

        public int AtualizarStock(string descricao, string quantidade) // Atualizar o stock de produtos , + para adicionar , - para remover 
        {

            string operacao = quantidade.Substring(0, 1);
            string novaQuantidade = quantidade.Substring(1);

            foreach (Produto produto in stock)
            {
                if (String.Compare(descricao, produto.descricao) == 0)
                {
                    if(operacao == "+")
                    {
                        produto.quantidade += int.Parse(novaQuantidade);
                        SaveStock();
                        return produto.quantidade;
                        
                    }

                    if(operacao == "-")
                    {
                        produto.quantidade -= int.Parse(novaQuantidade);
                        if (produto.quantidade < 0) produto.quantidade = 0;
                        SaveStock();
                        return produto.quantidade;
                    }
                }
            }
            return 0;
        }

        //Vender um ou mais produtos
        public int venderProduto(string desc, int quantidade)
        {
            
            foreach(Produto produto in stock)
            {
                if(String.Compare(produto.descricao, desc) == 0)
                {
                    if (produto.quantidade - quantidade < 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Impossivel");
                        Console.ReadKey();
                    }

                    else
                    {
                        produto.quantidade -= quantidade;
                    }
                }
            }

            return 1;
        }



        //Procura o produto que escreveu , e se existir retorna esse produto , senão retorna null e o programa vais breakar
        public Produto getProduto(string descricao)
        {
            foreach(Produto produto in stock)
            {
                if(String.Compare(descricao, produto.descricao) == 0)
                {
                    return produto;
                }
            }
            return null;
        }
    }
}
