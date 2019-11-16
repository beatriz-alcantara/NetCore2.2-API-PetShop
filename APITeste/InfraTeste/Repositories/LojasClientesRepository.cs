using Dapper;
using InfraTeste.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfraTeste.Repositories
{
    public class LojasClientesRepository
    {
        private readonly ConnectionStrings con;

        public LojasClientesRepository(ConnectionStrings c)
        {
            con = c;
        }

        public List<LojasClientes> Get(LojasClientes lojacliente)
        {
            List<LojasClientes> ListaLojaCliente = new List<LojasClientes>();
            using (var db = new MySqlConnection(con.MySQL))
            {
                var sql = @"SELECT id, idCliente, idLoja FROM lojas_clientes WHERE (id = @id OR @id = 0) 
                            AND (idCliente = @idCliente OR @idCliente = 0)
                            AND (idLoja = @idLoja OR @idLoja = 0);";
                ListaLojaCliente = db.Query<LojasClientes>(sql, lojacliente).ToList();
            }
            return ListaLojaCliente;
        }

        public LojasClientes Post(LojasClientes lojacliente)
        {
            using (var db = new MySqlConnection(con.MySQL))
            {
                var sql = @"INSERT INTO lojas_clientes(idLoja, idCliente) VALUES (@idLoja, @idCliente);
                            SELECT LAST_INSERT_ID();";
                lojacliente.id = db.ExecuteScalar<int>(sql, lojacliente);
            }
            return lojacliente;
        }

        public void Delete(int id)
        {
            using (var db = new MySqlConnection(con.MySQL))
            {
                var sql = @"DELETE FROM lojas_clientes WHERE id = @id;";
                db.Execute(sql, new { id });
            }
        }
    }
}
