using Dapper;
using InfraTeste.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfraTeste.Repositories
{
    public class PetRepository
    {
        private readonly ConnectionStrings con;

        public PetRepository(ConnectionStrings c)
        {
            con = c;
        }

        public List<Pets> Get(Pets pet)
        {
            List<Pets> ListaPet = new List<Pets>();
            using (var db = new MySqlConnection(con.MySQL))
            {
                var sql = @"SELECT id, nome, idCliente, especie, isAtivo FROM pets WHERE (id = @id OR @id = 0) 
                            AND (UPPER(nome) LIKE UPPER(CONCAT('%', @nome, '%' )) OR @nome IS NULL)
                            AND isAtivo = @isAtivo
                            AND (idCliente = @idCliente OR @idCliente = 0)
                            AND (UPPER(especie) = UPPER(@especie) OR @especie IS NULL);";
                ListaPet = db.Query<Pets>(sql, pet).ToList();
            }
            return ListaPet;
        }

        public Pets Post(Pets pet)
        {
            using (var db = new MySqlConnection(con.MySQL))
            {
                var sql = @"INSERT INTO pets(nome, idCliente, especie, isAtivo) VALUES (@nome, @idCliente, @especie, @isAtivo);
                            SELECT LAST_INSERT_ID();";
                pet.id = db.ExecuteScalar<int>(sql, pet);
            }
            return pet;
        }

        public void Put(Pets pet)
        {
            using (var db = new MySqlConnection(con.MySQL))
            {
                var sql = @"UPDATE pets SET nome = @nome, idCliente = @idCliente, especie = @especie, isAtivo = @isAtivo WHERE id = @id;";
                db.Execute(sql, pet);
            }
        }

        public void Delete(int id)
        {
            using (var db = new MySqlConnection(con.MySQL))
            {
                var sql = @"UPDATE pets SET isAtivo = 0 WHERE id = @id;";
                db.Execute(sql, new { id });
            }
        }
    }
}
