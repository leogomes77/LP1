using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Supermercado_v2
{
    class Program
    {
        static void Main(string[] args)
        {
            Stock stock = new Stock();
            Registos registos = new Registos();

            registos.RegistarFuncionario("Jorge", "123", "Repositor");

            //Carregar os ficheiros
            stock.leituraStock();
            registos.leituraRegistos();

            //Login
            Console.WriteLine("---------- LOGIN ----------");
            Console.Write("\t Username  : ");
            string username = Console.ReadLine();
            Console.Write("\t Password  : ");
            string pw = Console.ReadLine();
            Console.WriteLine("\n");

            //resultadoLogin = Cargo do user logado
            string resultadoLogin = registos.Login(username, pw);

            if(String.Compare("gerente", resultadoLogin, true)==0)
            {
                startGerente:
                Console.Clear();
                Console.WriteLine("---------- Menu -> {0} ----------", resultadoLogin);
                Console.WriteLine("\n1 - Adicionar Funcionário");
                Console.WriteLine("2 - Apagar Funcionário");
                Console.WriteLine("3 - Vender");
                Console.WriteLine("0 - Logout");
                string respostaMenu = Console.ReadLine();

                switch (respostaMenu)
                {
                    case "1":
                        Console.Clear();
                        Console.Write("\t Username  : ");
                        string usernameNovo = Console.ReadLine();
                        Console.Write("\t Password  : ");
                        string pwNova = Console.ReadLine();
                        Console.Write("\t Cargo  : ");
                        string cargoNovo = Console.ReadLine();
                        Console.WriteLine("\n");
                        Console.Clear();

                        //RegistarFuncionario() devolve:
                        // 0 se o utilizador já existir
                        // 1 se o registo for aceite
                        int resultadoRegisto = registos.RegistarFuncionario(usernameNovo, pwNova, cargoNovo);

                        if (resultadoRegisto == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Já existe");
                            Console.ReadKey();
                            goto startGerente;
                        }

                        else
                        {
                            Console.WriteLine("Registo efetuado com sucesso");
                            Console.ReadKey();
                            goto startGerente;
                        }
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("Insira o username do funcionario a apagar: ");
                        string userApagar = Console.ReadLine();

                        //apagarFuncionarios devolve:
                        // 1 se apagar
                        // 0 user não existir
                        int apagou = registos.apagarFuncionario(userApagar);
                        if(apagou == 1)
                        {
                            Console.Clear();
                            Console.WriteLine("User apagado com sucesso");
                            Console.ReadKey();
                            goto startGerente;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("User não existe");
                            Console.ReadKey();
                            goto startGerente;
                        }
                        break;

                    case "3":

                        break;

                    case "0":
                        //Dá restart na aplicação
                        //"Logout"
                        var fileName = Assembly.GetExecutingAssembly().Location;
                        System.Diagnostics.Process.Start(fileName);
                        break;

                    default:
                        break;
                }
                
            }

            if(String.Compare("repositor", resultadoLogin, true) == 0)
            {
                startRepositor:
                Console.Clear();
                Console.WriteLine("---------- Menu -> {0} ----------", resultadoLogin);
                Console.WriteLine("\n1 - Listar Produtos");
                Console.WriteLine("2 - Adicionar Produtos");            
                Console.WriteLine("3 - Remover Produtos");
                Console.WriteLine("4 - Remover Produtos");
                Console.WriteLine("5 - Limpar Lista");
                Console.WriteLine("0 - Logout");
                string respostaRepositor = Console.ReadLine();

                switch(respostaRepositor){
                    case "0":
                        //Dá restart na aplicação
                        //"Logout"
                        var fileName = Assembly.GetExecutingAssembly().Location;
                        System.Diagnostics.Process.Start(fileName);
                        break;

                    case "2":
                        //AdicionarProduto
                        //Devolve 1 se adicionar corretamente
                        //Devolve 0 se não adicionar

                        Console.Clear();
                        Console.WriteLine("Escolha a categoria:");
                        Console.WriteLine("1 - Congelados");
                        Console.WriteLine("2 - Prateleira");
                        Console.WriteLine("3 - Enlatados");
                        int escolha = int.Parse(Console.ReadLine());
                        Produto.Categoria categoriaa = (Produto.Categoria)escolha;   
                        
                        Console.Clear();
                        Console.WriteLine("Descrição: ");
                        string descricaoProduto = Console.ReadLine();
                        Console.WriteLine("Preço:");
                        float preço = float.Parse(Console.ReadLine());
                        Console.WriteLine("Quantidade a adicionar:");
                        int quantidade = int.Parse(Console.ReadLine());

                        int resultadoAdicionar = stock.AdicionarProduto(categoriaa, descricaoProduto, preço, quantidade);
                        if(resultadoAdicionar == 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Adicionado com Sucesso!");
                            Console.ReadKey();
                            goto startRepositor;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Nao Adicionou!");
                            Console.ReadKey();
                            goto startRepositor;
                        }

                        break;

                    case "1":
                        Console.Clear();
                        stock.ListarProdutos();
                        Console.ReadKey();
                        goto startRepositor;
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("Insira o produto a apagar: ");
                        string produtoApagar = Console.ReadLine();
                        Console.WriteLine("\nInsira a quantidade a apagar: ");
                        int quantistock = int.Parse(Console.ReadLine());

                        //removerStock devolve:
                        // 1 se apagar
                        // 0 produto não existir
                        int apagou = stock.RemoverStock(produtoApagar,quantistock);
                        if (apagou == 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Produto apagado com sucesso");
                            Console.ReadKey();
                            goto startRepositor;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Produto não existe");
                            Console.ReadKey();
                            goto startRepositor;
                        }
                        break;

                    case "4":
                        Console.Clear();
                        Console.WriteLine("Insira o produto a apagar: ");
                        string produtoApag = Console.ReadLine();
                        int x =stock.RemoverStock(produtoApag);

                        if (x == 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Produto apagado com sucesso");
                            Console.ReadKey();
                            goto startRepositor;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Produto não existe");
                            Console.ReadKey();
                            goto startRepositor;
                        }

                        break;
                      
                    default:

                        break;
                }
            }
            
            else
            {
                Console.Clear();
                Console.WriteLine(resultadoLogin);
                Console.ReadKey();
            }
            
        }
    }
}
