namespace BW_ECOMMERCE.Services
{
    public interface IFileService
    {
        string ConvertToBase64(IFormFile file);
        void SaveFileToDatabase(string base64String, string fileName);
    }
}
