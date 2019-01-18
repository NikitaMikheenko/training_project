using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Common
{
    public class NoteEditModel
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The name must not exceed 50 characters!")]
        public string Name { get; set; }

        [StringLength(4000, ErrorMessage = "Note must not exceed 4000 characters!")]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        public List<NoteInfoModel> Notes { get; set; }

        public List<int> LinkedNotesIds { get; set; }

        public List<bool> Checked { get; set; }

        [Required]
        public int? CategoryId { get; set; }

        public string Imglink { get; set; }

        public HttpPostedFileBase ImgFile { get; set; }

        public UserInfoModel User { get; set; }

        public List<Category> Categories { get; set; }
    }
}
