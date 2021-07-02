using ApiSkeletonPoc.Core.Common;
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
    public class JobService : IJobService
    {
        private readonly IBaseService<Job> _baseService;
        private readonly IJobNotesService _jobNotesService;
        public JobService(IBaseService<Job> baseService, IJobNotesService jobNotesService)
        {
            _baseService = baseService;
            _jobNotesService = jobNotesService;
        }
        public int AddJob(JobModel jobModel)
        {
            var jobEntity = _baseService.AddOrUpdate(Mapper.MapJobWithJobModel(jobModel), 0);
            if (jobEntity != null)
            {
                return jobEntity.JobId;
            }
            return 0;
        }

        public bool Delete(int jobId)
        {
            var job = _baseService.Where(a => a.JobId == jobId, "JobNotes").FirstOrDefault();
            if (job != null)
            {
                if (job.JobNotes.Any())
                {
                    foreach (var jobNote in job.JobNotes)
                    {
                        _jobNotesService.DeleteJobNote(jobNote.Id, jobId);
                    }
                }
                _baseService.Remove(jobId);
                return true;
            }
            return false;

        }

        public IEnumerable<JobModel> GetAll(out int count, int pageNum, int pageSize, bool? sortdirection = null, string sortString = null)
        {
            string[] navgationProps = { "JobFrequency", "JobStatus", "JobType" };
            return _baseService.Get(out count, null, navgationProps, pageNum, pageSize).Select(a => Mapper.MapJobModelWithJob(a)).ToList().OrderByDescending(a => a.CreatedDateTime); ;
        }

        public JobModel GetSingle(int jobId)
        {
            string[] navgationProps = { "JobFrequency", "JobStatus", "JobType" };
            return Mapper.MapJobModelWithJob(_baseService.Where(a => a.JobId == jobId, navgationProps).FirstOrDefault());
        }

        public int UpdateJob(int jobId, JobModel jobModel)
        {
            var jobEntity = _baseService.GetById(jobId);
            if (jobEntity != null)
            {
                jobEntity.ContactId = jobModel.ContactId;
                //jobEntity.EndDateTime = jobModel.EndDateTime;
                jobEntity.EstimateDays = jobModel.EstimateDays;
                jobEntity.JobFrequencyId = jobModel.JobFrequencyId;
                jobEntity.JobStatusId = jobModel.JobStatusId;
                jobEntity.JobTypeId = jobModel.JobTypeId;
                jobEntity.ModifiedDateTime = DateTime.Now;
                jobEntity.Name = jobModel.Name;
                jobEntity.Reference = jobModel.Reference;
                jobEntity.StartTime = jobModel.StartTime;
                jobEntity.StartDate = jobModel.StartDate;
                jobEntity.EndTime = jobModel.EndTime;
                jobEntity.EndDate = jobModel.EndDate;
                jobEntity.Day = jobModel.Day;
                jobEntity.Description = jobModel.Description;
                //jobEntity.StartDateTime = jobModel.StartDateTime;
                _baseService.AddOrUpdate(jobEntity, jobId);
                return jobId;
            }
            return 0;
        }

        public void BulkDelete(List<int> ids)
        {
            foreach (var id in ids)
            {
                Delete(id);
            }
        }

        public IEnumerable<JobModel> GetWeeklyRecurringJobs()
        {

            string[] navgationProps = { "JobFrequency", "JobStatus", "JobType" };
            return _baseService.Where(x => x.JobTypeId == (int)Enums.JobType.Recurring && x.JobStatusId == 2, navgationProps).Select(a => Mapper.MapJobModelWithJob(a));
        }
    }
}
