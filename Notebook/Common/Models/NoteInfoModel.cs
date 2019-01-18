using System;

namespace Common
{
    public class NoteInfoModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public Category Category { get; set; }

        public UserInfoModel User { get; set; }
    }
}
