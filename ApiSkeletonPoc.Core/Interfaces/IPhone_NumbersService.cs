using System;
using System.Collections.Generic;
using System.Text;
using ApiSkeletonPoc.Core.Models;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface IPhone_NumbersService
    {
        int Add(PhoneModel phoneNumber);
        int Update(PhoneModel phoneNumber,int id);
        PhoneModel Get(int id);
        PhoneModel GetByNumber(string number);
        bool Remove(int phoneId);
    }
}
