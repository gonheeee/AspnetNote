using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetNote.MVC6.Views
{
    public class Note
    {
        public int NoteNo { get; set; }
        public string NoteTitle { get; set; }
        public string NoteContents { get; set; }
        public int UserNo { get; set; }
    }
}
