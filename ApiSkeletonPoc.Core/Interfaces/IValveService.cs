using ApiSkeletonPoc.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface IValveService
    {
        IEnumerable<ValveModel> GetAll(out int count, int pageNum, int pageSize, bool? sortdirection = null, string sortString = null);
        ValveModel GetValveDetailByQR(string qrId);
        ValveModel InsertValveDtails(ValveModel valveModel);
        ValveModel UpdateValveDetails(Guid valveId, ValveModel valveModel);
        bool IsValveExist(string valveId);
        bool IsQrIdExist(string qrId);
        IEnumerable<ValveModel> SearchById(string valveId);
    }
}
