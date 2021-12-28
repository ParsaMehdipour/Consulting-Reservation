using CR.Core.DTOs.Images;
using Microsoft.AspNetCore.Hosting;

namespace CR.Core.Services.Interfaces.Images
{
    public interface IImageUploaderService
    {
        string Execute(UploadImageDto dto);
    }
}
