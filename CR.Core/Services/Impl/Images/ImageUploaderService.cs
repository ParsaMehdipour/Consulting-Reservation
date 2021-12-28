using CR.Core.DTOs.Images;
using CR.Core.Services.Interfaces.Images;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Linq;

namespace CR.Core.Services.Impl.Images
{
    public class ImageUploaderService : IImageUploaderService
    {
        private readonly IHostingEnvironment _environment;

        public ImageUploaderService(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public string Execute(UploadImageDto dto)
        {
            string folder = $@"images/{dto.Folder}";

            var uploadsRootFolder = Path.Combine(_environment.WebRootPath, folder);
            if (!Directory.Exists(uploadsRootFolder))
            {
                Directory.CreateDirectory(uploadsRootFolder);
            }

            string[] splitFileName = dto.File.FileName.Split(".");
            string fileFormat = splitFileName.Last();
            string fileName =  DateTime.Now.Ticks.ToString() + "." + fileFormat;
            var filePath = Path.Combine(uploadsRootFolder, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                dto.File.CopyTo(fileStream);
            }

            return folder + "/" + fileName;
        }
    }
}
