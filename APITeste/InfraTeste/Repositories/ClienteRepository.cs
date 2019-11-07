using InfraTeste.Models;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

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
            using (var db = new SqlConnection(con.MySQL))
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
            using (var db = new SqlConnection(con.MySQL))
            {
                var sql = @"SELECT id, nome, cpf FROM clientes WHERE 
                            (id = @id OR @id = 0) AND (nome = @nome or @nome is null) AND (cpf = @cpf OR @cpf is null);";
                vm.id = db.ExecuteScalar<int>(sql, vm);
            }
            return vm;
        }
    }
}
