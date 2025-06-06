using System;
using System.Diagnostics.Eventing.Reader;
using MySql.Data.MySqlClient;
using Mysqlx.Cursor;

namespace MPtech
{
    public class Program
    {
        // Deixo a conexão como variável de nível da classe para usar depois
        private static MySqlConnection? conn;

        public static void Main()
        {
            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine("Deseja conectar ao banco de dados? (s/n) ");
                string? respConexao = Console.ReadLine()?.ToLower();

                if (respConexao == "s" || respConexao == "sim")
                {
                    string conexao = "server=localhost;port=3306;user=root;password=vini22102007;database=MPtech;";

                    conn = new MySqlConnection(conexao);
                    try
                    {
                        conn.Open();
                        Console.WriteLine("Conexão realizada com sucesso!");

                        // Removida a parte de leitura de dados do banco
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro: " + ex.Message);
                    }
                    // Removi o finally que fecha a conexão para ela ficar aberta
                }
                else if (respConexao == "n" || respConexao == "não")
                {
                    Console.WriteLine("Você escolheu não conectar ao banco de dados.");
                }
                else
                {
                    Console.WriteLine("Opção inválida. Encerrando o programa.");
                    continuar = false;
                    continue;
                }

                Console.WriteLine("================= Seja Bem Vindo! =================");
                Console.WriteLine("================= MPtech =================");

                Console.WriteLine("\n================= Menu Principal =================");
                Console.WriteLine("1 - Cadastrar Produto");
                Console.WriteLine("2 - Cadastrar Cliente");
                Console.WriteLine("3 - Listar Cliente");
                Console.WriteLine("4 - Listar Produto");
                Console.WriteLine("5 - Excluir Produto");
                Console.WriteLine("6 - Excluir Cliente");
                Console.WriteLine("7 - Sair");

                Console.Write("\nEscolha uma opção: ");
                int opErador = Convert.ToInt32(Console.ReadLine());

                if (opErador == 1)
                {
                    Console.WriteLine("\n================= Cadastrar Produto =================");

                    Console.WriteLine("Digite o nome do produto que deseja cadastrar: ");
                    string? nomeProduto = Console.ReadLine();

                    Console.Write("Digite o valor do produto: ");
                    string? precoProduto = Console.ReadLine();

                    Console.Write("Digite a quantidade do produto: ");
                    string? estoqueProduto = Console.ReadLine();

                    string sql = "INSERT INTO produtos (nome, preco, estoque, data_cadastro) VALUES (@nome, @preco, @estoque, NOW())"; // comando SQL para inserir produto
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Adiciona os parâmetros para evitar SQL Injection
                        cmd.Parameters.AddWithValue("@nome", nomeProduto);
                        cmd.Parameters.AddWithValue("@preco", precoProduto);
                        cmd.Parameters.AddWithValue("@estoque", estoqueProduto);

                        try
                        {
                            int linhasAfetadas = cmd.ExecuteNonQuery(); // Executa o comando
                            Console.WriteLine($"Produto cadastrado com sucesso! ({linhasAfetadas} linha(s) inserida(s))");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Erro ao cadastrar produto: " + ex.Message);
                        }
                        
                    }

                }
                else if (opErador == 2)
                {
                    Console.WriteLine("\n================= Cadastrar Cliente =================");

                    Console.WriteLine("Digite o nome do Cliente que deseja cadastrar: ");
                    string? nomeCliente = Console.ReadLine();

                    Console.Write("Digite o email do Cliente: ");
                    string? emailCliente = Console.ReadLine();

                    Console.Write("Digite o telefone do Cliente: ");
                    string? telefoneCliente = Console.ReadLine();

                    string sql = "INSERT INTO clientes (nome, email, telefone, data_cadastro) VALUES (@nome, @email, @telefone, NOW())"; // comando SQL para inserir cliente
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Adiciona os parâmetros para evitar SQL Injection
                        cmd.Parameters.AddWithValue("@nome", nomeCliente);
                        cmd.Parameters.AddWithValue("@telefone", telefoneCliente);
                        cmd.Parameters.AddWithValue("@email", emailCliente);

                        try
                        {
                            int linhasAfetadas = cmd.ExecuteNonQuery(); // Executa o comando
                            Console.WriteLine($"Cliente cadastrado com sucesso! ({linhasAfetadas} linha(s) inserida(s))");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Erro ao cadastrar cliente: " + ex.Message);
                        }
                    }

                }

                else if (opErador == 3)
                {
                    Console.WriteLine("\n================= Listar Cliente =================");
                    Console.WriteLine("Listando clientes cadastrados...");

                    string sql = "SELECT * FROM clientes"; // seleciona a tabela de clientes
                    MySqlCommand cmd = new MySqlCommand(sql, conn); // conexão com o banco de dados
                    MySqlDataReader reader = cmd.ExecuteReader(); // executa o comando e lê os dados

                    while (reader.Read())
                    {
                        Console.WriteLine("\nID" + reader["id"]);
                        Console.WriteLine("Nome: " + reader["nome"]);
                        Console.WriteLine("Email: " + reader["email"]);
                        Console.WriteLine("Telefone: " + reader["telefone"]);
                        Console.WriteLine("Data de Cadastro " + reader["data_cadastro"]);
                    }
                    reader.Close(); // fecha o leitor de dados

                }

                else if (opErador == 4)
                {
                    string sql = "SELECT * FROM produtos"; // seleciona a tabela de produtos
                    MySqlCommand cmd = new MySqlCommand(sql, conn); // conexão com o banco de dados
                    MySqlDataReader reader = cmd.ExecuteReader(); // executa o comando e lê os dados

                    while (reader.Read())
                    {
                        Console.WriteLine("\nID: " + reader["id"]);
                        Console.WriteLine("Nome: " + reader["nome"]);
                        Console.WriteLine("Preço: " + reader["preco"]);
                        Console.WriteLine("Quantidade em estoque: " + reader["estoque"]);
                    }
                    reader.Close(); // fecha o leitor de dados
                }

                else if (opErador == 5)
                {
                    Console.WriteLine("\n================= Excluir Produto =================");
                    Console.Write("Digite o ID do produto que deseja excluir: ");
                    string? idExcluir = Console.ReadLine();

                    string sql = "DELETE FROM produtos WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", idExcluir);

                        try
                        {
                            int linhasAfetadas = cmd.ExecuteNonQuery();
                            if (linhasAfetadas > 0)
                                Console.WriteLine("Produto excluído com sucesso!");
                            else
                                Console.WriteLine("Nenhum produto encontrado com esse ID.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Erro ao excluir produto: " + ex.Message);
                        }
                    }
                }

                else if (opErador == 6)
                {
                    Console.WriteLine("\n================= Excluir Cliente =================");
                    Console.Write("Digite o ID do cliente que deseja excluir: ");
                    string? idExcluir = Console.ReadLine();

                    string sql = "DELETE FROM clientes WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", idExcluir);

                        try
                        {
                            int linhasAfetadas = cmd.ExecuteNonQuery();
                            if (linhasAfetadas > 0)
                                Console.WriteLine("Cliente excluído com sucesso!");
                            else
                                Console.WriteLine("Nenhum cliente encontrado com esse ID.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Erro ao excluir cliente: " + ex.Message);
                        }
                    }
                }

                else
                {
                    continuar = false;
                    Console.WriteLine("Saindo do programa...");
                }

                


            }


            // Fecha a conexão quando o programa terminar, se estiver aberta
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
                Console.WriteLine("Conexão fechada ao sair do programa.");
            }
        }
    }
}
