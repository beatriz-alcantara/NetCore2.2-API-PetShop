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

        public List<Lojas> Get(Lojas loja)
        {
            List<Lojas> ListaLoja = new List<Lojas>();
            using(var db = new MySqlConnection(con.MySQL))
            {
                var sql = @"SELECT id, nome, isAtivo FROM lojas WHERE (id = @id OR @id = 0) 
                            AND (UPPER(nome) LIKE UPPER(CONCAT('%', @nome, '%' )) OR @nome IS NULL)
                            AND isAtivo = @isAtivo;";
                ListaLoja = db.Query<Lojas>(sql, loja).ToList();
            }
            return ListaLoja;
        }

        public Lojas Post(Lojas loja)
        {
            using (var db = new MySqlConnection(con.MySQL))
            {
                var sql = @"INSERT INTO lojas(nome, isAtivo) VALUES (@nome, @isAtivo);
                            SELECT LAST_INSERT_ID();";
                loja.id = db.ExecuteScalar<int>(sql, loja);
            }
            return loja;
        }

        public void Put(Lojas loja)
        {
            using (var db = new MySqlConnection(con.MySQL))
            {
                var sql = @"UPDATE lojas SET nome = @nome, isAtivo = @isAtivo WHERE id = @id;";
                db.Execute(sql, loja);
            }
        }

        public void Delete(int id)
        {
            using (var db = new MySqlConnection(con.MySQL))
            {
                var sql = @"UPDATE lojas SET isAtivo = 0 WHERE id = @id;";
                db.Execute(sql, new { id });
            }
        }
    }
}
