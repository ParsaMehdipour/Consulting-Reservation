using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CR.Core.DTOs.RequestDTOs
{
    public class RequestAddNewSpecialtyDto
    {
        [Required(ErrorMessage = "لطفا نام تخصص را وارد کنید")]
        public string Name { get; set; }

        public IFormFile File { get; set; }

        public long? ParentId { get; set; }

        [Required(ErrorMessage = "لطفا ترتیب نمایش تخصص را وارد کنید")]
        public int OrderNumber { get; set; }

    }
}