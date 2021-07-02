using ApiSkeletonPoc.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface ISignatureService
    {
        bool InsertSignatureDoc(SignatureDocModel signatureDocModel);
        SignatureDocModel GetSingle(Guid docId);
    }
}
