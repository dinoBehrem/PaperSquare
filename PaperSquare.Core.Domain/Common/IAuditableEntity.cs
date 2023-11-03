﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

﻿namespace PaperSquare.Domain.Common.Interfaces
{
    public interface IAuditableEntity
    {
        string CreatedBy { get; set; }
        DateTime CreatedOnUtc { get; set; }
        string? LastModifiedBy { get; set; }
        DateTime? LastModifiedOnUtc { get; set; }
        string? DeletedBy { get; set; }
        DateTime? DeletedOnUtc { get; set; }
    }
}
