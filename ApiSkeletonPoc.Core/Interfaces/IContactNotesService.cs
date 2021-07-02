using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface IContactNotesService
    {
        public int SaveContactNote(ContactNotesModel contactNotesModel);
        public List<ContactNotesModel> GetAll(int contactId);
        public void DeleteContactNote(int contactId, int noteId);
        public int UpdateContactNote(int noteId, ContactNotesModel contactNotesModel);
        public void DeleteAllContactNotes(List<ContactNotes> contactNotes);
    }
}
