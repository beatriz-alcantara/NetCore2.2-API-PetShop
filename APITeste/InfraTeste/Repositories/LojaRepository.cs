using Dapper;
using InfraTeste.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfraTeste.Repositories
{
    public class LojaRepository
    {
        private readonly ConnectionStrings con;

        public LojaRepository (ConnectionStrings c)
        {
            con = c;
        }

        public List<Loja> Get(Loja loja)
        {
            List<Loja> ListaLoja = new List<Loja>();
            using(var db = new MySqlConnection(con.MySQL))
            {
                var sql = @"SELECT id, nome FROM lojas WHERE (id = @id OR @id = 0) AND (UPPER(nome) LIKE UPPER(CONCAT('%', @nome, '%' )) OR @nome IS NULL);";
                ListaLoja = db.Query<Loja>(sql, loja).ToList();
            }
            return ListaLoja;
        }

        public Loja Post(Loja loja)
        {
            using (var db = new MySqlConnection(con.MySQL))
            {
                var sql = @"INSERT INTO lojas(nome) VALUES (@nome);
                            SELECT LAST_INSERT_ID();";
                loja.id = db.ExecuteScalar<int>(sql, loja);
            }
            return loja;
        }

        public void Put(Loja loja)
        {
            using (var db = new MySqlConnection(con.MySQL))
            {
                var sql = @"UPDATE lojas SET nome = @nome WHERE id = @id;";
                db.Execute(sql, loja);
            }
        }

        public void Delete(int id)
        {
            using (var db = new MySqlConnection(con.MySQL))
            {
                var sql = @"DELETE FROM lojas WHERE id = @id;";
                db.Execute(sql, new { id });
            }
        }
    }
}
