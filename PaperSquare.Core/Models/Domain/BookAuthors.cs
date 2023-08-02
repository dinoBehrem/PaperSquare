using PaperSquare.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Core.Models.Domain
{
    public class BookAuthors: IAuditableEntity
    {
        #region Navigation

        public Author Author { get; set; }
        public string AuthorId { get; set; }
        public Book Book { get; set; }
        public string BookId { get; set; }

        #endregion Navigation

        #region Audit

        public string CreatedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOnUtc { get; set; }

        #endregion Audit
    }
}
