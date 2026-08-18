using MauiAppMinhasCompras.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        static SQLiteDatabaseHelper _db; //Cria uma variável p/ guardar a referência do BDs;
                                         //variável é compartilhada por todo o aplicativo.
        public static SQLiteDatabaseHelper Db //É uma propriedade global que permite que outra tela do aplicativo
                                              //acesse o banco de dados chamando o App.Dd
        {
            get
            {
                if(_db == null) //evita repetição de arquivos e conexões
                {
                 string path = Path.Combine(
                 Environment.GetFolderPath(
                 Environment.SpecialFolder.LocalApplicationData),
                 "banco_sqlite_compras.db3");

                 _db = new SQLiteDatabaseHelper(path);
                }

                return _db;
            }
        }
    }
}