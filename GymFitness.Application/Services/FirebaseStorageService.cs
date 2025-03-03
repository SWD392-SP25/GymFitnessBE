using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using GymFitness.Application.Abstractions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

public class FirebaseStorageService : IFirebaseStorageService
{
    private readonly string _bucketName;

    public FirebaseStorageService()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("firebase_config.json", optional: false, reloadOnChange: true)
            .Build();

        _bucketName = config["storage_bucket"] ?? throw new Exception("Firebase Storage Bucket không được tìm thấy trong firebase_config.json!");

        if (FirebaseApp.DefaultInstance == null)
        {
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile("firebase_config.json")
            });
        }
    }

    public string GetBucketName() => _bucketName;

    public async Task<string> UploadFileAsync(IFormFile file, string folderName)
    {
        if (file == null || file.Length == 0) throw new ArgumentException("File không hợp lệ");

        try
        {
            string fileName = $"{folderName}/{Guid.NewGuid()}_{file.FileName}";
            using var stream = file.OpenReadStream();

            var storage = StorageClient.Create();
            await storage.UploadObjectAsync(_bucketName, fileName, file.ContentType, stream);

            return $"https://firebasestorage.googleapis.com/v0/b/{_bucketName}/o/{Uri.EscapeDataString(fileName)}?alt=media";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khi upload file: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> DeleteFileAsync(string fileUrl)
    {
        try
        {
            var storage = StorageClient.Create();
            var fileName = fileUrl.Split(new string[] { "/o/" }, StringSplitOptions.None)[1].Split('?')[0];
            await storage.DeleteObjectAsync(_bucketName, Uri.UnescapeDataString(fileName));

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khi xóa file: {ex.Message}");
            return false;
        }
    }
}
