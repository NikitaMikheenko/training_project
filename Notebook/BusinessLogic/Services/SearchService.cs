using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Common;
using DataAccess;

namespace BuisnessLogic
{
    public class SearchService : ISearchService
    {
        private readonly INoteContext noteContext = new NoteContext();

        private readonly IUserContext userContext = new UserContext();

        private readonly ICategoryContext categoryContext = new CategoryContext();

        public SearchModel GetAdvSearchResponse(SearchModel model)
        {
            int? userId = userContext.GetUserIdByLogin(model.UserLogin);

            if (userId == null || model.RequestText.Length == 0)
            {
                return model;
            }

            switch (model.SearchType)
            {
                case 0:
                    model.NotesInfo = noteContext.GetNotesInfoByName(model.RequestText);
                    break;

                case 1:
                    if (Regex.IsMatch(model.RequestText, @"(18[0-9]{2}|[2-9][0-9]{3})-([1-9]|0[1-9]|1[012])-(0[1-9]|1[0-9]|2[0-9]|3[01])"))
                    {
                        model.NotesInfo = noteContext.GetNotesInfoByDate(Convert.ToDateTime(model.RequestText));
                    }
                    break;

                case 2:
                    int? categoryId = categoryContext.GetCategoryIdByName(model.RequestText);

                    if (categoryId != null)
                    {
                        model.NotesInfo = noteContext.GetNotesInfoByCategoryId((int)categoryId);
                    }
                    break;

                case 3:
                    int? id = userContext.GetUserIdByLogin(model.RequestText);

                    if (id != null)
                    {
                        model.NotesInfo = noteContext.GetNotesInfoByUserId((int)id);
                    }
                    break;
            }

            if (model.NotesInfo != null)
            {
                model.NotesInfo = SortList(model.NotesInfo, (int)userId);
            }

            return model;
        }

        public SearchModel GetSearchResponse(SearchModel model)
        {
            int? userId = userContext.GetUserIdByLogin(model.UserLogin);

            if (userId == null || model.RequestText.Length == 0)
            {
                return model;
            }

            switch (model.SearchType)
            {
                case 0:
                    model.NotesInfo = noteContext.GetUserNotesInfoByName((int)userId, model.RequestText);
                    break;

                case 1:
                    if (Regex.IsMatch(model.RequestText, @"(18[0-9]{2}|[2-9][0-9]{3})-(0[1-9]|1[012])-(0[1-9]|1[0-9]|2[0-9]|3[01])"))
                    {
                        model.NotesInfo = noteContext.GetUserNotesInfoByDate((int)userId, Convert.ToDateTime(model.RequestText));
                    }
                    break;

                case 2:
                    int? categoryId = categoryContext.GetCategoryIdByName(model.RequestText);

                    if (categoryId != null)
                    {
                        model.NotesInfo = noteContext.GetUserNotesInfoByCategoryId((int)userId, (int)categoryId);
                    }
                    break;
            }

            return model;
        }

        public List<NoteInfoModel> SortList(List<NoteInfoModel> list, int userId)
        {
            List<NoteInfoModel> sorted = new List<NoteInfoModel>();

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].User.Id == userId)
                {
                    list[i].User = null;

                    sorted.Add(list[i]);

                    list.Remove(list[i]);
                }
            }

            foreach (var item in list)
            {
                sorted.Add(item);
            }

            return sorted;
        }
    }
}
