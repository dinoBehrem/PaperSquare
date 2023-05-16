using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Core.Models.Base
{
    public abstract class Base
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
