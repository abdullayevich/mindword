﻿using MindWord.Domain.Entities;
using MindWord.Service.ViewModel;
using PagedList;

namespace MindWord.Service.Interfaces.Services
{
    public interface IWordService
    {
        public Task<IPagedList<WordCreateViewModel>> GetPagedListAsync(int pageNumber = 1, int pageSize = 5);

    }
}
