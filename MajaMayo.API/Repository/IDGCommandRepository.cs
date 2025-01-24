﻿using MajaMayo.API.DTOs;
using MajaMayo.API.Models;

namespace MajaMayo.API.Repository
{
    public interface IDGCommandRepository
    {
        Task<string> InsertDeltaGeneraliApprovedUsers(ICollection<DGApprovedUserResponse> dGApproveds);
    }
}
