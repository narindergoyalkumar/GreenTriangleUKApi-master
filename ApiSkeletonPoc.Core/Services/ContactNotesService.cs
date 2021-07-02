using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiSkeletonPoc.Core.Services
{
    public class ContactNotesService : IContactNotesService
    {
        private readonly IBaseService<ContactNotes> _baseNotesService;
        public ContactNotesService(IBaseService<ContactNotes> baseNotesService)
        {
            _baseNotesService = baseNotesService;
        }
        public void DeleteContactNote(int contactId, int noteId)
        {
            var note = _baseNotesService.Where(a => a.ContactId == contactId && a.Id == noteId).FirstOrDefault();
            if (note != null)
            {
                _baseNotesService.Remove(note.Id);
            }
        }
        public void DeleteAllContactNotes(List<ContactNotes> contactNotes)
        {
            foreach (var item in contactNotes)
            {
                _baseNotesService.Remove(item.Id);
            }
        }
        public List<ContactNotesModel> GetAll(int contactId)
        {
            return _baseNotesService.Where(a => a.ContactId == contactId).Select(a => Mapper.MapContactNotesWithContactNotesModel(a)).OrderByDescending(a=>a.CreatedDateTime).ToList();
        }

        public int SaveContactNote(ContactNotesModel contactNotesModel)
        {
            int noteId = 0;
            var note = _baseNotesService.AddOrUpdate(Mapper.MapContactNotesModelWithContactNotes(contactNotesModel), 0);
            if (note != null)
            {
                noteId = note.Id;
            }
            return noteId;
        }

        public int UpdateContactNote(int noteId, ContactNotesModel contactNotesModel)
        {
            var note = _baseNotesService.GetById(noteId);
            if (note != null)
            {
                note.ModifiedDateTime = DateTime.Now;
                note.Text = contactNotesModel.Text;
                note.Type = contactNotesModel.Type;
                _baseNotesService.AddOrUpdate(note, note.Id);
                return note.Id;
            }
            return 0;
        }
    }
}
