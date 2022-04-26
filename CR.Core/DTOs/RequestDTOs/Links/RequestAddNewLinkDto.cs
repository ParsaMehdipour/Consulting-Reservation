using System.ComponentModel.DataAnnotations;

namespace CR.Core.DTOs.RequestDTOs.Links
{
    public class RequestAddNewLinkDto
    {
        [Required(ErrorMessage = "لطفا نام لینک را وارد کنید")]
        public string Name { get; set; }

        [Required(ErrorMessage = "لطفا مقدار لینک را وارد کنید")]
        public string SearchKey { get; set; }

        [Required(ErrorMessage = "لطفا ترتیب نمایش لینک را وارد کنید")]
        public int OrderNumber { get; set; }

        public long? ParentId { get; set; }
    }
}
