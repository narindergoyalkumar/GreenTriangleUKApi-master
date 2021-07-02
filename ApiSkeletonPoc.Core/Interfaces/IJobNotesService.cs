using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface IJobNotesService
    {
        public int SaveJobNote(JobNotesModel jobNotesModel);
        public List<JobNotesModel> GetAll(int jobId);
        public void DeleteJobNote(int jobId, int noteId);
        public int UpdateJobNote(int noteId, JobNotesModel jobNotesModel);
        public void DeleteAllJobNotes(List<JobNotes> jobNotes);
    }
}
