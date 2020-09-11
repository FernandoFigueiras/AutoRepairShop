using AutoRepairShop.Web.Helpers.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Helpers.Classes
{
    public class ImageHelper : IImageHelper
    {

        public async Task<string> UploadImageAsync(IFormFile imageFile, string folder)
        {
            var guid = Guid.NewGuid().ToString();
            var file = $"{guid}.jpg";



            string path = Path.Combine(
                Directory.GetCurrentDirectory(),
                $"wwwroot\\StoreImages\\{folder}",
                file);


            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"~/StoreImages/{folder}/{file}";
        }


        public void RemovePictureAsync(string imagePath, string path)
        {

            var substrinf = $"~/StoreImages/{path}";

            var file = imagePath.Substring(substrinf.Length+1);

            var pathfinal = Path.Combine(
                Directory.GetCurrentDirectory(),
                $"wwwroot\\StoreImages\\{path}",
                file);


            var exists = File.Exists(pathfinal);

            if (exists)
            {
                File.Delete(pathfinal);
            }
        }
    }
}
