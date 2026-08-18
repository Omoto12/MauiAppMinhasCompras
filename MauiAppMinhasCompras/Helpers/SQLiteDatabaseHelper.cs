using MauiAppMinhasCompras.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn; //Conexão do banco de dados não seja substituída;Gerenciar operações com o banco de dados assíncrona

        public SQLiteDatabaseHelper(string path) //inicializa a conexão com o banco de dados e criada a tabela assim que a instância for criada
        {
            _conn = new SQLiteAsyncConnection(path);

            _conn.CreateTableAsync<Produto>().Wait(); // Cria tabela assíncrona Produto com base na classe modelo Produto. Se já existir não faz nada
        }

        public Task<int> Insert(Produto p) //Task retorna um valor inteiro
        {
            return _conn.InsertAsync(p); //Passa o objeto produto para o banco de dados

        }

        public Task<List<Produto>> Update(Produto p) //Método Update, atualiza um registro existente na tabela Produto no BDs, com o ID fornecido no objeto Produto
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";
            return _conn.QueryAsync<Produto>(
            sql, p.Descricao, p.Quantidade, p.Preco, p.Id
            );
        }

        public Task<int> Delete(int id) //Método Delete remove um registro da tabela Produto com base no Id fornecido (assíncrono)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Produto>> GetAll() //Consulta a tabela Produto no BDs e retorna os registros em lista de objetos Produto (assíncrono)
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> Search(string q) //Método Search implementa uma funcionalidade de busca na tabela Produto do BDS SQLite,
                                                    //pode filtrar registros com uma string fornecida
        {
            string sql = "SELECT * Produto WHERE descricao LIKE '%" + q + "%'";
            return _conn.QueryAsync<Produto>(sql);
        }

    }
}
