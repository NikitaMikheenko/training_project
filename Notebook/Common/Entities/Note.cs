using System;
using System.Collections.Generic;

namespace Common
{
    public class Note
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public List<NoteInfoModel> LinkedNotes { get; set; }

        public Category Category { get; set; }

        public string Imglink { get; set; }

        public UserInfoModel User { get; set; }
    }
}
