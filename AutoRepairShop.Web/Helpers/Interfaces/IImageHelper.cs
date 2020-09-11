using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Helpers.Interfaces
{
    public interface IImageHelper
    {

        Task<string> UploadImageAsync(IFormFile imageFile, string folder);

        void RemovePictureAsync(string imagePath, string path);

    }
}
