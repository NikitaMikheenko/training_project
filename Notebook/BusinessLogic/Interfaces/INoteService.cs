using Common;
using System.Collections.Generic;

namespace BuisnessLogic
{
    public interface INoteService
    {
        bool AddNote(NoteAddModel model);

        bool EditNote(NoteEditModel model);

        bool DeleteNote(int? id);

        bool EditNoteLinks(List<NoteInfoModel> links, int noteId);

        Note GetNoteById(int id);

        List<NoteInfoModel> GetNotesInfoByUserLogin(string login);

        List<NoteInfoModel> GetLinkedNotesInfoByNoteId(int? noteId);

        List<int> GetLinkedNotesIds(int noteId);
    }
}
