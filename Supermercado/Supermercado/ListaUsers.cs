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


            //Validação
            if (File.Exists(nomeDoFicheiro))
            {
                File.Delete(nomeDoFicheiro);
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
            Console.WriteLine("---------- LOGIN ----------");
            Console.Write("\t Username  : ");
            string username = Console.ReadLine();
            Console.Write("\t Password  : ");
            string pw = Console.ReadLine();
            Console.WriteLine("\n");
            Console.Clear();
            string cargo = "";
            
            
            bool encontrou = false;
            foreach(User user in listaUsers)
            {
                if(String.Compare(username, user.username) == 0 && String.Compare(pw, user.password) == 0)
                {
                    encontrou = true;
                    cargo = user.cargo;
                }
            }

            if (encontrou == true)
            {
                Console.Clear();
                Console.WriteLine("Logado");
                MenuPrincipal(username, cargo);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Não existe");
            }
        }

        public void RegistarFuncionario()
        {
            Console.Clear();
            Console.WriteLine("---------- REGISTAR FUNCIONARIO ----------");
            Console.Write("\t Username  : ");
            string usernameNovo = Console.ReadLine();
            Console.Write("\t Password  : ");
            string pwNova = Console.ReadLine();
            Console.Write("\t Cargo  : ");
            string cargoNovo = Console.ReadLine();
            Console.WriteLine("\n");
            Console.Clear();

            leituraUsers();

            bool existe = false;
            //Ver se já existe
            foreach(User user in listaUsers)
            {
                if (String.Compare(usernameNovo, user.username) == 0)
                {
                    existe = true;
                }
                
            }

            if (existe == true)
            {
                Console.Clear();
                Console.WriteLine("Já existe");
                Console.ReadKey();
                Login();
            }

            else
            {
                User novoUser = new User(usernameNovo, pwNova, cargoNovo);
                listaUsers.Add(novoUser);
                SaveUsers();
                Console.Clear();
                Console.WriteLine("Registo Efetuado");
                Login();
            }
        }

        public void MenuPrincipal(string username, string cargo)
        {
            Console.Clear();
            
            Console.WriteLine("---------- Bom dia, {0} ----------", username);
            string resposta = "";
           
            
            
            if (String.Compare("gerente", cargo, true) == 0)
            {
                Console.WriteLine("1 - Registar Funcionário");
                Console.WriteLine("2 - Apagar Funcionário");
                Console.WriteLine("3 - Vender");
                resposta = Console.ReadLine();

                switch (resposta)
                {
                    case "1":
                        Console.Clear();
                        RegistarFuncionario();
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("Username do Funcionario a apagar: ");
                        string funcionario = Console.ReadLine();
                        int apagou = ApagarFuncionario(funcionario);
                        if (apagou == 1) {
                            Console.Clear();
                            Console.WriteLine("Apagado com Sucesso");
                            Console.ReadKey();
                            MenuPrincipal(username,cargo);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("User não existe");
                            Console.ReadKey();
                            MenuPrincipal(username, cargo);
                        }
                        break;

                    case "3":
                        Console.Clear();
                        //Vender;
                        break;

                    default:
                        Console.WriteLine("Não é possivel");
                        MenuPrincipal(username, cargo);
                        break;
                }

                
            }
            if (String.Compare("repositor", cargo, true) == 0)
            {
                Console.WriteLine("1 - Adicionar Novo Produto");
               
                resposta = Console.ReadLine();
                switch (resposta)
                {
                    case "1":
                        Console.Clear();
                        //Adicionar Produto
                        break;

                    case "2":
                        Console.Clear();
                        //Vender();
                        break;

                    default:
                        Console.WriteLine("Não é possivel");
                        MenuPrincipal(username,cargo);
                        break;
                }


            }
        }


        public int ApagarFuncionario(string funcionario)
        {
            foreach(User user in listaUsers)
            {
                if (String.Compare(user.username, funcionario) == 0)
                {
                    listaUsers.Remove(user);
                    SaveUsers();
                    return 1;
                }
            }
            
            return 0;
        }
    }
}
