using System.Collections.Generic;

namespace Common
{
    public class SearchModel
    {
        public string UserLogin { get; set; }

        public int SearchType { get; set; }

        public string RequestText { get; set; }

        public List<NoteInfoModel> NotesInfo { get; set; }
    }
}
