using PaperSquare.Core.Infrastructure;
using PaperSquare.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Core.Models.Domain
{
    public class BookSeriesFollowers: IAuditableEntity
    {
        #region Navigation

        public User Follower { get; set; }
        public string FollowerId { get; set; }
        
        public BookSeries BookSeries { get; set; }
        public string BookSeriesId { get; set; }

        #endregion Navigation

        #region Audit

        public string CreatedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOnUtc { get; set; }

        #endregion Audit
    }
}
