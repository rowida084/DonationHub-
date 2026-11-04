using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Contract
{
    public  interface ISoftDeletable
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAtUtc { get; set; }

        public void Delete()
        {
            IsDeleted = true;
            DeletedAtUtc = DateTime.UtcNow;
        }

       public void UndoDeleted()
        {
            IsDeleted = false;
            DeletedAtUtc = null;
        }
        
    }
}
