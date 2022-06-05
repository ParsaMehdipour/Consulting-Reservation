namespace CR.Core.DTOs.ResultDTOs
{
    public class Select2Item
    {
        public long? id { get; set; }

        public string text { get; set; }

        public bool selected { get; set; }

        public bool disabled { get; set; }

    }

    public class Select2ItemForProduct
    {
        public string id { get; set; }

        public string text { get; set; }

        public bool selected { get; set; }

        public bool disabled { get; set; }

    }
}