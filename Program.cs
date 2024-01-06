using Dapper;
using Dapper.Model;
using System.Data.SqlClient;

namespace Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                UpdateCategories(connection);
                ListCategories(connection);
                CreateCategories(connection);
            }
        }

        static void ListCategories(SqlConnection connection)
        {
            //caso em um exemplo os nomes sejam em portugues, usamos AS CODIGO, AS TITULO 
            var categories = connection.Query<Category>("Select[ID], [Title] FROM[category]");
            foreach (var item in categories)
            {
                Console.WriteLine(item.Id);
            }
        }
        static void CreateCategories(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "Amazon";
            category.Description = "Categoria destinada a servicos aws";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;


            var insertSql = $@"INSERT INTO 
                [Category] 
            VALUES(
                @Id, 
                @Title,
                @Url,
                @Summary,
                @Order,
                @Description,
                @Featured)";

            connection.Execute(insertSql, new
            {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            });
        }
        static void UpdateCategories(SqlConnection connection)
        {
            var updateQuery = "UPDATE [Category] SET [Title]=@title WHERE [Id]=@id";
            var rows = connection.Execute(updateQuery, new
            {
                id = new Guid("2"),
                title = "FrontEnd 2024"
            });

            Console.WriteLine($"{rows} Registros atualizados");
        }
    }
}