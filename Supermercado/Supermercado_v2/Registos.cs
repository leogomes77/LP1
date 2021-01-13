using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Supermercado_v2
{
    class Registos
    {
        List<User> registos = new List<User>();

        public Registos()
        {
        }

        public Registos(List<User> registos)
        {
            this.registos = registos;
        }

        //Carregar Ficheiro
        public void leituraRegistos()
        {
            string nomeDoFicheiro = "Registos.txt";

            //Validacao
            if (File.Exists(nomeDoFicheiro))
            {
                FileStream fileStream = File.OpenRead(nomeDoFicheiro);
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                while (fileStream.Position < fileStream.Length)
                {
                    User userlido = binaryFormatter.Deserialize(fileStream) as User;
                    registos.Add(userlido);
                }

                fileStream.Close();
            }
            else
            {
                Console.WriteLine("Não existe");
            }
        }

        //Guardar registos dos Funcionarios

        public void SaveRegistos()
        {
            string localizacaoDoFicheiro = Directory.GetCurrentDirectory();
            string nomeDoFicheiro = "Registos.txt";


            //Validação
            if (File.Exists(nomeDoFicheiro))
            {
                File.Delete(nomeDoFicheiro);
            }

            FileStream fileStream = File.Create(nomeDoFicheiro);
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            foreach (User userAtual in registos)
            {
                binaryFormatter.Serialize(fileStream, userAtual);
            }

            fileStream.Close();
        }

        //Login
        public string Login(string username, string password)
        {
            string cargo = "";
            bool encontrou = false;
            foreach (User user in registos)
            {
                if (String.Compare(username, user.username) == 0 && String.Compare(password, user.password) == 0)
                {
                    encontrou = true;
                    cargo = user.cargo;
                }
            }

            if (encontrou == true)
            {
                return cargo;

            }
            else
            {
                Console.Clear();
                
                return "User não existe";
            }
        }


        //Registar Funcionario
        public int RegistarFuncionario(string username, string password, string cargo)
        {
            User novoUser = new User(username, password, cargo);

            
            if(registos.Exists(user => user.username == novoUser.username) == true)
            {
                return 0;
            }

            else
            {
                registos.Add(novoUser);
                SaveRegistos();
                return 1;
            }

        }


        //Remover um Funcionario
        public int apagarFuncionario(string username)
        {
            //RemoveAll devolve o número de elementos da lista que apagou se encontrar a condição
            int numApagados = registos.RemoveAll(user => user.username == username);

            if(numApagados == 1)
            {
                SaveRegistos();
                return 1;
            }

            else { return 0; }
        }
    }
}
