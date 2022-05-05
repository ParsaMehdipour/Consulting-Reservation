namespace CR.Core.DTOs.Links
{
    public class LinkForAdminPanelDto
    {
        public long Id { get; set; }
        public string PersianTitle { get; set; }
        public LinkForAdminPanelDto Parent { get; set; }
        public string CreatedDate { get; set; }
        public bool HasChildren { get; set; }
        public int OrderNumber { get; set; }
    }
}
