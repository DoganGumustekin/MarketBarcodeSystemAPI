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
        [HttpGet("GetComplaintsForManager")]
        public IActionResult GetComplaintsForManager(int accountId)
        {
            var result = _complaintService.GetComplaintsForManager(accountId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetComplaintsForUser")]
        public IActionResult GetComplaintsForUser(int userId)
        {
            var result = _complaintService.GetComplaintsForUser(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        //Müdürün complaint için ischeck false yapma. Yani şikayeti kontrol
        //etti müdür ve WorkMan e bildirim gönderdi burada bildirim ve mesaj ayarla.
        //complaint teki ischecked true olursa frontend te Userin şikayet listesinde kontrol edildi 
        //yazacak.
        [HttpPost("ComplaintChecked")]
        public IActionResult ComplaintChecked(Complaint complaint)
        {
            var result = _complaintService.ComplaintChecked(complaint);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
