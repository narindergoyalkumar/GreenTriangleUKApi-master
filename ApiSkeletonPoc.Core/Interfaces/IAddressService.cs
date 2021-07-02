using System;
using System.Collections.Generic;
using System.Text;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface IAddressService
    {
        int Add(AddressModel address);
        int Update(AddressModel address, int id);
        AddressModel Get(int id);
        //AddressModel GetByProperties(AddressModel address);
        bool Remove(int addressId);
        IEnumerable<DAL.DataContracts.Address> GetAddressWithContact(string address,int clientId  );
    }
}
