using System.Data.SqlClient;
using APBD6Kox.Models;

namespace APBD6Kox.Repositories;

public class AnimalRepository : IAnimalRepository
{
    private readonly IConfiguration _configuration;

    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<Animal> GetAnimals(string orderBy)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT IdAnimal, Name,Description,Category,Area from Animal ORDER BY Name ";
        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
        IList<Animal> animals = new List<Animal>();
        while (sqlDataReader.Read())
        {
            animals.Add(new Animal
            {
                IdAnimal = (int)sqlDataReader["IdAnimal"],
                Name = sqlDataReader["Name"].ToString(),
                Description = sqlDataReader["Description"].ToString(),
                Category = sqlDataReader["Category"].ToString(),
                Area = sqlDataReader["Area"].ToString()
            });
        }

        return animals;
    }

    public int AddAnimal(Animal animal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "INSERT into Animal (IdAnimal, Name,Description,Category,Area) VALUES (@IdAnimal,@Name, @Description, @Category, @Area)";
        sqlCommand.CommandText = "INSERT INTO Animal(Name, Description, Category, Area) VALUES(@Name, @Description, @Category, @Area)";
        sqlCommand.Parameters.AddWithValue("@Name", animal.Name);
        sqlCommand.Parameters.AddWithValue("@Description", animal.Description);
        sqlCommand.Parameters.AddWithValue("@Category", animal.Category);
        sqlCommand.Parameters.AddWithValue("@Area", animal.Area);
        
        int affectedRows = sqlCommand.ExecuteNonQuery();
        return affectedRows;
    }

    public int UpdateAnimal(Animal animal)
    {
        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            connection.Open();
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "UPDATE Animal SET Name = @Name, Description = @Description, Category = @Category, Area = @Area WHERE IdAnimal = @IdAnimal";

                sqlCommand.Parameters.AddWithValue("@Name", animal.Name);
                sqlCommand.Parameters.AddWithValue("@Description", animal.Description);
                sqlCommand.Parameters.AddWithValue("@Category", animal.Category);
                sqlCommand.Parameters.AddWithValue("@Area", animal.Area);
                sqlCommand.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);

                int affectedRows = sqlCommand.ExecuteNonQuery();
                return affectedRows;
            }
        }
    }

    public int DeleteAnimal(int idAnimal)
    {
        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            connection.Open();
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal";
                sqlCommand.Parameters.AddWithValue("@IdAnimal", idAnimal);

                int affectedRows = sqlCommand.ExecuteNonQuery();
                return affectedRows;
            }
        }
    }
}