using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CR.DataAccess.Common.Entity
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }

    public abstract class BaseEntity : BaseEntity<long>
    {

    }
}
