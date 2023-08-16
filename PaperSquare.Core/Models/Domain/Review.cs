using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Core.Models.Domain
{
    public abstract class Review
    {
        #region Properties

        public int Rating { get; set; }
        public string? Comment { get; set; }

        #endregion Properties
    }
}
