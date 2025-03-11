using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Services
{
     public interface IFirebaseStorageService
    {
        Task<string> UploadFileAsync(IFormFile file, string folderName);
        Task<bool> DeleteFileAsync(string fileUrl);
    }
}
