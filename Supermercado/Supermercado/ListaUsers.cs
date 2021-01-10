using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Supermercado
{
    class ListaUsers
    {
        public List<User> listaUsers { get;set; }

        public ListaUsers()
        {
            listaUsers = new List<User>();
        }



        public void SaveUsers()
        {
            string localizacaoDoFicheiro = Directory.GetCurrentDirectory();
            string nomeDoFicheiro = "usernames.txt";

            //Validaçao
            if (File.Exists(nomeDoFicheiro))
            {
                
            }

            FileStream fileStream = File.Create(nomeDoFicheiro);
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            foreach (User userAtual in listaUsers)
            {
                binaryFormatter.Serialize(fileStream, userAtual);
            }

            fileStream.Close();
        }

        public void leituraUsers()
        {
            string nomeDoFicheiro = "usernames.txt";

            //Validacao
            if (File.Exists(nomeDoFicheiro))
            {
                FileStream fileStream = File.OpenRead(nomeDoFicheiro);
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                while (fileStream.Position < fileStream.Length)
                {
                    User userlido = binaryFormatter.Deserialize(fileStream) as User;
                    listaUsers.Add(userlido);
                }
                
                fileStream.Close();
            }
            else
            {
                Console.WriteLine("Não existe");
            }
        }


        public void Login()
        {
            Console.Clear();
            Console.Write("\t Username  : ");
            string username = Console.ReadLine();
            Console.Write("\t Password  : ");
            string pw = Console.ReadLine();
            Console.WriteLine("\n");
            Console.Clear();
            
            
            bool encontrou = false;
            foreach(User user in listaUsers)
            {
                if(String.Compare(username, user.username) == 0 && String.Compare(pw, user.password) == 0)
                {
                    encontrou = true;
                }
            }

            if (encontrou == true)
            {
                Console.Clear();
                Console.WriteLine("Logado");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Não existe");
            }
        }
    }
}
