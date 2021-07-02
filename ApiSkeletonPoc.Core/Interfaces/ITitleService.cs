using System;
using System.Collections.Generic;
using System.Text;
using ApiSkeletonPoc.Core.Models;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface ITitleService
    {
        int Add(TitleModel titleModel);
        int Update(TitleModel titleModel);
        bool Remove(int titleId);
    }
}
