using MarketBarcodeSystemAPI.Business.Abstract;
using MarketBarcodeSystemAPI.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketBarcodeSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintsController : ControllerBase
    {
        IComplaintService _complaintService;

        public ComplaintsController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }

        [HttpPost("AddComplaint")]
        public IActionResult AddComplaint(Complaint complaint)
        {
            var result = _complaintService.AddComplaint(complaint);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("DeleteComplaint")]
        public IActionResult DeleteComplaint(Complaint complaint)
        {
            var result = _complaintService.DeleteComplaint(complaint);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetComplaints")]
        public IActionResult GetComplaints(int userId)
        {
            var result = _complaintService.GetComplaints(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        //müdürün göreceği şikayet listesi.
        [HttpPost("GetComplaintsForManager")]
        public IActionResult GetComplaintsForManager(Account account)
        {
            var result = _complaintService.GetComplaintsForManager(account);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
