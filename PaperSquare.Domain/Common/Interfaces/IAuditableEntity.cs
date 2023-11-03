<<<<<<<< HEAD:PaperSquare.Core.Domain/Common/IAuditableEntity.cs
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Core.Domain.Common
========
﻿namespace PaperSquare.Domain.Common.Interfaces
>>>>>>>> 51f1736be53fc24fcd21487f1571f61b9adf021a:PaperSquare.Domain/Common/Interfaces/IAuditableEntity.cs
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
