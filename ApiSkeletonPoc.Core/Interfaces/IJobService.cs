using ApiSkeletonPoc.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface IJobService
    {
        IEnumerable<JobModel> GetAll(out int count, int pageNum, int pageSize, bool? sortdirection = null, string sortString = null);
        JobModel GetSingle(int jobId);
        int AddJob(JobModel jobModel);
        int UpdateJob(int jobId, JobModel jobModel);
        bool Delete(int JobId);
        void BulkDelete(List<int> ids);
        IEnumerable<JobModel> GetWeeklyRecurringJobs();
    }
}
