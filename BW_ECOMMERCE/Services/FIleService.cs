
using System.Data.SqlClient;


namespace BW_ECOMMERCE.Services
{
    public class FileService : IFileService
    {
        private readonly DatabaseContext _dbContext;

        public FileService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string ConvertToBase64(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                var fileBytes = memoryStream.ToArray();
                return Convert.ToBase64String(fileBytes);
            }
        }

        public void SaveFileToDatabase(string base64String, string fileName)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var query = "INSERT INTO Images (FileName, Base64Content) VALUES (@FileName, @Base64Content)";
                using (var command = new SqlCommand(query, (SqlConnection)connection))
                {
                    command.Parameters.AddWithValue("@FileName", fileName);
                    command.Parameters.AddWithValue("@Base64Content", base64String);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
