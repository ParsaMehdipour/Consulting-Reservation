using System.Collections.Generic;

namespace CR.Core.DTOs.ResultDTOs
{
    public class Select2Response
    {
        public List<Select2Item> items { get; set; }

        public int countFiltered { get; set; }
    }

    public class Select2ResponseForProduct
    {
        public List<Select2ItemForProduct> items { get; set; }

        public int countFiltered { get; set; }
    }
}