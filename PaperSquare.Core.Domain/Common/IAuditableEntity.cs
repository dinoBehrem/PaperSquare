using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Core.Domain.Common
{
    public interface IAuditableEntity
    {
        string CreatedBy { get; set; }
        DateTime CreatedOnUtc { get; set; }
        string? LastModifiedBy { get; set; }
        DateTime? LastModifiedOnUtc { get; set; }
    }
}
