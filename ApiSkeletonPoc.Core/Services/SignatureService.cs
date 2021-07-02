using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ApiSkeletonPoc.Core.Services
{
    public class SignatureService : ISignatureService
    {
        private readonly IBaseService<SignatureDoc> _signatureDocBaseService;
        public SignatureService(IBaseService<SignatureDoc> signatureDocBaseService)
        {
            _signatureDocBaseService = signatureDocBaseService;
            
        }
        public bool InsertSignatureDoc(SignatureDocModel signatureDocModel)
        {
            bool isDocumentAdded = false;
            var entity = _signatureDocBaseService.AddOrUpdate(Mapper.MapSignatureDocWithSignatureDocModel(signatureDocModel), new Guid());
            if (entity != null)
            {
                isDocumentAdded = true;
            }
            return isDocumentAdded;
        }
        public SignatureDocModel GetSingle(Guid docId)
        {
            return Mapper.MapSignatureDocModelWithSignatureDoc(_signatureDocBaseService.GetById(docId));
        }
    }
}
