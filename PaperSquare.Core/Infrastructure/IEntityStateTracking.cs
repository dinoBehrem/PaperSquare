using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Core.Infrastructure
{
    public interface IEntityStateTracking
    {
        string CreatedBy { get; set; }
        DateTime CreatedOnUtc { get; set; }
        string? LastModifiedBy { get; set; }
        DateTime? LastModifiedOnUtc { get; set; }
    }
}
