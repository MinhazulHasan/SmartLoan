using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartLoan.Models;

namespace SmartLoan.Controllers.Api
{
    [Route("api/[controller]/{id}")]
    [ApiController]
    public class CollectorsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public CollectorsController(ApplicationDbContext db)
        {
            _db = db;   
        }
        [HttpGet]
        public JsonResult GetControllers(int id)
        {
            var memberIds = _db.CollectionDetails.Where(c => c.GroupId == id).Select(c => c.MemberInfoId).Distinct().ToList();
            var members = _db.MemberInfos.Where(m => memberIds.Contains(m.Id)).ToList();
            return new JsonResult(Ok(members));

        }
    }
}
