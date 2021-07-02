using System;
using System.Collections.Generic;
using System.Text;
using ApiSkeletonPoc.Core.Models;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface ISocial_MediaService
    {
        int Add(SocialMediaModel model);
        int Update(SocialMediaModel model, int id);
        bool Remove(int id);
    }
}
