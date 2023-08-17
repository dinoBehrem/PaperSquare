using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Shared.Enums
{
    public class BookEnums
    {
        public enum BookFormats
        {
            Paperback = 1,
            Hardcover = 2,
            Kindle = 3,
            Audiobook = 4
        }

        public enum BookStatus
        {
            CurrentlyReading = 1,
            WantToRead = 2,
            Read = 3
        }
    }
}
