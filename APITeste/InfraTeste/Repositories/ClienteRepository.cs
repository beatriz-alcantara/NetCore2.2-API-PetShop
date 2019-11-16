using InfraTeste.Models;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;

namespace InfraTeste.Repositories
{
    public class ClienteRepository
    {
        private readonly ConnectionStrings con;
        public ClienteRepository(ConnectionStrings c)
        {
            con = c;
        }
        public Clientes Post(Clientes vm)
        {
            using (var db = new MySqlConnection(con.MySQL))
            {
                var sql = @"INSERT INTO clientes (nome, cpf, isAtivo) VALUES (@nome, @cpf, 1);
                            SELECT LAST_INSERT_ID();";
                vm.id = db.ExecuteScalar<int>(sql, vm);
            }
            return vm;
        }
        public List<Clientes> Get(Clientes vm)
        {
            var lista = new List<Clientes>();
            using (var db = new MySqlConnection(con.MySQL))
            {
                var sql = @"SELECT id, nome, cpf, isAtivo FROM clientes WHERE 
                            (id = @id OR @id = 0) 
                             AND (UPPER(nome) LIKE UPPER(CONCAT('%', @nome, '%')) OR @nome IS NULL) 
                             AND (cpf = @cpf OR @cpf IS NULL)
                             AND isAtivo = @isAtivo;";
                lista = db.Query<Clientes>(sql, vm).OrderBy(el => el.id).ToList();
            }
            return lista;
        }
        public void Put(Clientes vm)
        {
            using (var db = new MySqlConnection(con.MySQL))
            {
                var sql = @"UPDATE clientes SET nome = @nome, cpf = @cpf, isAtivo = @isAtivo WHERE id = @id";
                db.Execute(sql, vm);
            }
        }
        public void Delete(int id)
        {
            using (var db = new MySqlConnection(con.MySQL))
            {
                var sql = @"UPDATE clientes SET isAtivo = 0 WHERE id = @id";
                db.Execute(sql, new { id });
            }
        }
    }
}
