﻿using Core.Entities.Concrete;
using MarketBarcodeSystemAPI.Core.Entities.Concrete;
using MarketBarcodeSystemAPI.Core.Utilities.Results;
using MarketBarcodeSystemAPI.Entities.Concrete;
using IResult = MarketBarcodeSystemAPI.Core.Utilities.Results.IResult;

namespace MarketBarcodeSystemAPI.Business.Abstract
{
    public interface IComplaintService
    {
        IResult AddComplaint(Complaint complaint);
        IResult DeleteComplaint(Complaint complaint);
        IDataResult<List<Complaint>> GetComplaints(int userId);
        IDataResult<List<ComplaintForManagerModel>> GetComplaintsForManager(int AccountKey);
        IDataResult<List<ComplaintForUserModel>> GetComplaintsForUser(int userId);
        IResult ComplaintChecked(Complaint complaint);
    }
}
