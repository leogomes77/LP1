using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Supermercado_v2
{
    class ListaFaturas
    {
        List<Fatura> listaFaturas = new List<Fatura>();

        public ListaFaturas()
        {
        }


        public ListaFaturas(List<Fatura> listaFaturas)
        {
            this.listaFaturas = listaFaturas;
        }

        public void SaveFaturas()
        {
            string localizacaoDoFicheiro = Directory.GetCurrentDirectory();
            string nomeDoFicheiro = "Faturas.txt";


            //Validação
            if (File.Exists(nomeDoFicheiro))
            {
                File.Delete(nomeDoFicheiro);
            }

            FileStream fileStream = File.Create(nomeDoFicheiro);
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            foreach (Fatura faturaAtual in listaFaturas)
            {
                binaryFormatter.Serialize(fileStream, faturaAtual);
            }

            fileStream.Close();
        }


        public void leituraFaturas()
        {
            string nomeDoFicheiro = "Faturas.txt";

            //Validacao
            if (File.Exists(nomeDoFicheiro))
            {
                FileStream fileStream = File.OpenRead(nomeDoFicheiro);
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                while (fileStream.Position < fileStream.Length)
                {
                    Fatura faturaLida = binaryFormatter.Deserialize(fileStream) as Fatura;
                    listaFaturas.Add(faturaLida);
                }

                fileStream.Close();
            }
            else
            {
                Console.WriteLine("Ficheiro Não existe");
            }
        }

        public int RegistarFatura(string funcionario, string cliente, float preçoTotal, Produto produto, int quantidade)
        {
            Fatura novaFatura = new Fatura(funcionario, cliente, preçoTotal, produto, quantidade);

            listaFaturas.Add(novaFatura);
            SaveFaturas();
            return 0;
            

        }
        public int RegistarFatura(string funcionario, string cliente, float preçoTotal, List<Produto> listaProdutos, ArrayList quantidades)
        {
            Fatura novaFatura = new Fatura(funcionario, cliente, preçoTotal, listaProdutos, quantidades);

            listaFaturas.Add(novaFatura);
            SaveFaturas();
            return 0;


        }

        public Fatura GetFatura(string funcionario, string cliente, float preço, List<Produto> listaProdutos)
        {
            foreach(Fatura fatura in listaFaturas)
            {
                if(fatura.nomeFuncionario == funcionario && fatura.nomeCliente == cliente && preço == fatura.preçoTotal && listaProdutos == fatura.listaProdutos)
                {
                    return fatura;
                }
            }
            return null;
        }
    }
}
