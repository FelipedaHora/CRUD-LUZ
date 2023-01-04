using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Npgsql;
namespace CrudPlayers
{
    public class DataBase : IDatabase
    {
        NpgsqlConnection _connection;
        private static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=masterkey;Database=TesteDB");
        }

        public void Atualizar(Player player)
        {
            _connection = GetConnection();
            try
            {
                using (NpgsqlConnection connection = GetConnection())
                {

                    connection.Open();
                    string query = $@"update public.tb_player set nickname = '{player.nickname}', function = '{player.function}' where id = {player.id}";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }

        }

        public void Deletar(Player player)
        {
            _connection = GetConnection();
            try
            {
                using (NpgsqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string query = $@"delete from public.tb_player where id = {player.id}";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }

        }

        public void Inserir(Player player)
        {
            _connection = GetConnection();
            try
            {
                using (NpgsqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string query = $@"insert into tb_player(nickname,function)values('{player.nickname}','{player.function}')";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
        }

        public List<Player> Listar()
        {
            List<Player> listar = new List<Player>();
            try
            {
            using (NpgsqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string query = @"select * from tb_player";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
                    
                    using(NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Player player = new Player()
                            {
                                id = reader.GetInt32(0),
                                nickname = reader.GetString(1),
                                function= reader.GetString(2)
                            };
                            listar.Add(player);
                        }
                    }
                    connection.Close();           
                }
            }
             catch (Exception e)
             {
                 MessageBox.Show(e.Message.ToString());
             }
            return listar;
        }

    }
}
