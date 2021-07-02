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
    public class JobNotesService : IJobNotesService
    {
        private readonly IBaseService<JobNotes> _baseNotesService;
        private readonly IContactNotesService _contactNotesService;
        private readonly IBaseService<Job> _jobService;
        public JobNotesService(IBaseService<JobNotes> baseNotesService, IContactNotesService contactNotesService, IBaseService<Job> jobService)
        {
            _baseNotesService = baseNotesService;
            _contactNotesService = contactNotesService;
            _jobService = jobService;
        }
        public void DeleteAllJobNotes(List<JobNotes> jobNotes)
        {
            foreach (var item in jobNotes)
            {
                _baseNotesService.Remove(item.Id);
            }
        }

        public void DeleteJobNote(int jobId, int noteId)
        {
            var note = _baseNotesService.Where(a => a.JobId == jobId && a.Id == noteId, "Job").FirstOrDefault();
            if (note != null)
            {
                var contactId = note.Job.ContactId;
                ContactNotesModel contactNotesModel = new ContactNotesModel
                {
                    ContactId = contactId,
                    CreatedDateTime = DateTime.Now,
                    Text = $"Ref: #{jobId}  (Deleted) :" + note.Text,
                    Type = note.Type
                };
                _contactNotesService.SaveContactNote(contactNotesModel);
                _baseNotesService.Remove(note.Id);
            }
        }

        public List<JobNotesModel> GetAll(int jobId)
        {
            return _baseNotesService.Where(a => a.JobId == jobId).Select(a => Mapper.MapJobNotesWithJobNotesModel(a)).OrderByDescending(a => a.CreatedDateTime).ToList();
        }

        public int SaveJobNote(JobNotesModel jobNotesModel)
        {
            int noteId = 0;
            var note = _baseNotesService.AddOrUpdate(Mapper.MapJobNotesModelWithJobNotes(jobNotesModel), 0);
            if (note != null)
            {
                noteId = note.Id;
                var job = _jobService.GetById(jobNotesModel.JobId.Value);
                if (job != null)
                {
                    ContactNotesModel contactNotesModel = new ContactNotesModel
                    {
                        ContactId = job.ContactId,
                        CreatedDateTime = DateTime.Now,
                        Text = $"Ref: #{job.JobId}  (New) :" + note.Text,
                        Type = note.Type
                    };
                    _contactNotesService.SaveContactNote(contactNotesModel);
                }

            }
            return noteId;
        }

        public int UpdateJobNote(int noteId, JobNotesModel jobNotesModel)
        {
            var note = _baseNotesService.Where(a => a.Id == noteId, "Job").FirstOrDefault();
            if (note != null)
            {
                note.ModifiedDateTime = DateTime.Now;
                note.Text = jobNotesModel.Text;
                note.Type = jobNotesModel.Type;
                _baseNotesService.AddOrUpdate(note, note.Id);
                var contactId = note.Job.ContactId;
                ContactNotesModel contactNotesModel = new ContactNotesModel
                {
                    ContactId = contactId,
                    CreatedDateTime = DateTime.Now,
                    Text = $"Ref: #{jobNotesModel.JobId}  (Edited) :" + note.Text,
                    Type = note.Type
                };
                _contactNotesService.SaveContactNote(contactNotesModel);
                return note.Id;
            }
            return 0;
        }
    }
}
