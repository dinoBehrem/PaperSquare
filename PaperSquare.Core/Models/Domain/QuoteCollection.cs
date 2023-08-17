using PaperSquare.Core.Infrastructure;
using PaperSquare.Core.Models.Base;
using PaperSquare.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Core.Models.Domain
{
    public class QuoteCollection: BaseEntity, IAuditableEntity
    {
        #region Properties

        public string Name { get; set; }

        #endregion Properties

        #region Navigation

        public User User { get; set; }
        public string UserId { get; set; }

        public ICollection<Quote> Quotes { get; set; }

        #endregion Navigation

        #region Audit

        public string CreatedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOnUtc { get; set; }

        #endregion Audit
    }
}
