﻿using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface INotesRL
    {

        public bool GenerateNote(UserNotes notes, long userId);
        public UserNotes UpdateNotes(UserNotes notes, long NotesId);
        public IEnumerable<Notes> GetAllNotes(long UserId);
        public bool DeleteNotesOfUser(long NotesId);
    }
}
