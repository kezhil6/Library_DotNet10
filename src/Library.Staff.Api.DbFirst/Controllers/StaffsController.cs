using Library.Staff.Api.DbFirst.DTOs;
using Library.Staff.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Staff.Api.DbFirst.Controllers
{
[ApiController]
[Route("api/v1/[controller]")]
public class StaffsController : ControllerBase
{
        private readonly IStaffRepository _repo;
        public StaffsController(IStaffRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAllStaffs()
        {
            var staffs = _repo.GetAllStaffs();
            return Ok(staffs);
        }

        [HttpPost]
        public IActionResult AddStaff([FromBody] StaffDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Staff object is null");
            }

            var staff = new Core.Entities.Staff
            {
                Name = dto.Name,
                Role = dto.Role
            };

            _repo.AddStaff(staff);
            return Ok();
        }
    }
}
