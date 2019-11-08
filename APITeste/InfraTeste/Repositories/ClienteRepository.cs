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
        public Cliente Post(Cliente vm)
        {
            using (var db = new MySqlConnection(con.MySQL))
            {
                var sql = @"INSERT INTO clientes (nome, cpf) VALUES (@nome, @cpf);
                            SELECT LAST_INSERT_ID();";
                vm.id = db.ExecuteScalar<int>(sql, vm);
            }
            return vm;
        }
        public List<Cliente> Get(Cliente vm)
        {
            var lista = new List<Cliente>();
            using (var db = new MySqlConnection(con.MySQL))
            {
                var sql = @"SELECT id, nome, cpf FROM clientes WHERE 
                            (id = @id OR @id = 0) AND (UPPER(nome) LIKE UPPER(CONCAT('%', @nome, '%')) OR @nome IS NULL) AND (cpf = @cpf OR @cpf IS NULL);";
                lista = db.Query<Cliente>(sql, vm).OrderBy(el => el.id).ToList();
            }
            return lista;
        }
        public void Put(Cliente vm)
        {
            using (var db = new MySqlConnection(con.MySQL))
            {
                var sql = @"UPDATE clientes SET nome = @nome, cpf = @cpf WHERE id = @id";
                db.Execute(sql, vm);
            }
        }
        public void Delete(int id)
        {
            using (var db = new MySqlConnection(con.MySQL))
            {
                var sql = @"DELETE FROM clientes WHERE id = @id";
                db.Execute(sql, new { id });
            }
        }
    }
}
