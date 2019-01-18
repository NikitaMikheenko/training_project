using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Common;
using DataAccess;

namespace BuisnessLogic
{
    public class NoteService : INoteService
    {
        private readonly INoteContext noteContext = new NoteContext();

        private readonly IUserContext userContext = new UserContext();

        public bool AddNote(NoteAddModel model)
        {
            int? userId = userContext.GetUserIdByLogin(model.User.Login);

            if (userId == null || model.CategoryId == null)
            {
                return false;
            }

            string path = null;

            if (model.ImgFile != null)
            {
                path = SaveImgFile(model.ImgFile);
            }

            int? noteId = noteContext.AddNote(new Note()
            {
                Name = model.Name,
                Date = DateTime.Now.Date,
                Description = model.Description,
                Category = new Category()
                {
                    Id = model.CategoryId
                },
                Imglink = path,
                User = new UserInfoModel()
                {
                    Id = userId
                }
            });

            List<NoteInfoModel> linkedNotes = GetCheckedNotes(model.Notes, model.Checked);

            if (noteId != null && linkedNotes != null)
            {
                return EditNoteLinks(linkedNotes, (int)noteId);
            }

            return false;
        }

        public bool DeleteNote(int? id)
        {
            if (id != null)
            {
                bool del = DeleteImgByNoteId((int)id);

                noteContext.DeleteNoteLinkByLinkedNoteId((int)id);

                return del & noteContext.DeleteNoteById((int)id);
            }

            return false;
        }

        public bool EditNote(NoteEditModel model)
        {
            int? userId = userContext.GetUserIdByLogin(model.User.Login);

            if (userId == null || model.CategoryId == null || model.Id == null)
            {
                return false;
            }

            string path = model.Imglink;

            if (model.ImgFile != null)
            {
                path = SaveImgFile(model.ImgFile);

                DeleteImgByNoteId((int)model.Id);
            }

            bool edit = noteContext.EditNote(new Note()
            {
                Id = model.Id,
                Name = model.Name,
                Date = model.Date,
                Description = model.Description,
                Category = new Category()
                {
                    Id = model.CategoryId
                },
                Imglink = path,
                User = new UserInfoModel()
                {
                    Id = userId
                }
            });

            List<NoteInfoModel> linkedNotes = GetCheckedNotes(model.Notes, model.Checked);

            if (linkedNotes != null)
            {
                return edit & EditNoteLinks(linkedNotes, (int)model.Id);
            }

            return edit;
        }

        public bool EditNoteLinks(List<NoteInfoModel> links, int noteId)
        {
            if (links == null)
            {
                return true;
            }

            bool result = true;

            List<NoteInfoModel> oldLinks = noteContext.GetLinkedNotesInfoByNoteId(noteId);

            if (oldLinks == null)
            {
                foreach (var item in links)
                {
                    result = result & noteContext.AddNoteLink(noteId, (int)item.Id);
                }

                return result;
            }

            foreach (var item in oldLinks)
            {
                result = result & noteContext.DeleteNoteLink(noteId, (int)item.Id);
            }

            foreach (var item in links)
            {
                result = result & noteContext.AddNoteLink(noteId, (int)item.Id);
            }

            return result;
        }

        public Note GetNoteById(int id)
        {
            return noteContext.GetNoteById(id);
        }

        public List<NoteInfoModel> GetNotesInfoByUserLogin(string login)
        {
            int? userId = userContext.GetUserIdByLogin(login);

            if (userId != null)
            {
                return noteContext.GetNotesInfoByUserId((int)userId);
            }

            return null;
        }

        public bool DeleteImgByNoteId(int id)
        {
            string path = noteContext.GetImgLinkByNoteId(id);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);

                return true;
            }

            return false;
        }

        public List<NoteInfoModel> GetLinkedNotesInfoByNoteId(int? noteId)
        {
            if (noteId != null)
            {
                return noteContext.GetLinkedNotesInfoByNoteId((int)noteId);
            }

            return null;
        }

        public List<NoteInfoModel> GetCheckedNotes(List<NoteInfoModel> notes, List<bool> Checked)
        {
            List<NoteInfoModel> linkedNotes = new List<NoteInfoModel>();

            for (int i = 0; i < notes.Count; i++)
            {
                if (Checked[i])
                {
                    linkedNotes.Add(notes[i]);
                }
            }

            return linkedNotes;
        }

        public string SaveImgFile(HttpPostedFileBase imgFile)
        {
            string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Images"), Path.GetFileName(imgFile.FileName));

            imgFile.SaveAs(path);

            return path;
        }

        public List<int> GetLinkedNotesIds(int noteId)
        {
            List<NoteInfoModel> linkedNotes = GetLinkedNotesInfoByNoteId(noteId);

            List<int> linkedNotesIds = new List<int>();

            if (linkedNotes != null)
            {
                foreach (var item in linkedNotes)
                {
                    linkedNotesIds.Add((int)item.Id);
                }

                return linkedNotesIds;
            }

            return null;
        }
    }
}
