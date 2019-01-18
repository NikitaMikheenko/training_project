using Common;
using System;
using System.Collections.Generic;

namespace DataAccess
{
    public interface INoteContext
    {
        int? AddNote(Note model);

        bool AddNoteLink(int noteId, int linkedNoteId);

        bool EditNote(Note model);

        Note GetNoteById(int id);

        List<NoteInfoModel> GetNotesInfoByUserId(int id);

        List<NoteInfoModel> GetNotesInfoByCategoryId(int id);

        List<NoteInfoModel> GetNotesInfoByDate(DateTime date);

        List<NoteInfoModel> GetNotesInfoByName(string name);

        List<NoteInfoModel> GetUserNotesInfoByCategoryId(int userId, int categoryId);

        List<NoteInfoModel> GetUserNotesInfoByDate(int userId, DateTime date);

        List<NoteInfoModel> GetUserNotesInfoByName(int userId, string name);

        List<NoteInfoModel> GetLinkedNotesInfoByNoteId(int id);

        bool DeleteNoteById(int id);

        bool DeleteNoteLink(int noteId, int linkedNoteId);

        void DeleteNoteLinkByLinkedNoteId(int linkedNoteId);

        string GetImgLinkByNoteId(int id);
    }
}
