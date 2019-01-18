using Common;
using System.Collections.Generic;

namespace BuisnessLogic
{
    public interface ISearchService
    {
        SearchModel GetSearchResponse(SearchModel model);

        //Adv - Advanced. Used by Editor and Admin
        SearchModel GetAdvSearchResponse(SearchModel model);

        List<NoteInfoModel> SortList(List<NoteInfoModel> list, int userId);
    }
}
