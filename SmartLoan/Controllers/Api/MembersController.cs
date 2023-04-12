using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartLoan.Models;
using SmartLoan.ViewModels;

namespace SmartLoan.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        private readonly UserManager<IdentityUser> _userManager;
        public MembersController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }


        // POST: /api/members +++++++++++++++ Post a single member
        [HttpPost]
        public JsonResult AddMemberIntoGroup([FromForm] BulkInsertViewModel bulkInsertViewModel)
        {
            int gId = bulkInsertViewModel.GroupId;
            var existingMemberIds = _db.GroupWithMembers.Where(g => g.GroupInfoId == gId).Select(g => g.MemberId).ToList();
            foreach(var memberId in bulkInsertViewModel.MemberIds)
            {
                if(!existingMemberIds.Contains(memberId))
                {
                    GroupWithMember groupWithMember = new GroupWithMember
                    {
                        MemberId = memberId,
                        GroupInfoId = bulkInsertViewModel.GroupId
                    };
                    _db.GroupWithMembers.Add(groupWithMember);
                }
            }
            _db.SaveChanges();
            return new JsonResult(Ok("Members are added Successfully"));
        }


        [Route("/Api/members/{groupId}/{memberId}")]
        [HttpDelete]
        public JsonResult DeleteMember(int groupId, int memberId)
        {
            var member = _db.GroupWithMembers.SingleOrDefault(m => m.MemberId == memberId && m.GroupInfoId == groupId);
            if (member == null)
                return new JsonResult(BadRequest(member));

            _db.GroupWithMembers.Remove(member);
            _db.SaveChanges();

            return new JsonResult(Ok(member));
        }


        [Route("/Api/members/GetGroups/{id}")]
        [HttpGet]
        public JsonResult GetGroups(int id)
        {
            var grpIdList = _db.GroupWithMembers.Where(g => g.MemberId == id).Select(g => g.GroupInfoId).ToList();
            SelectList groups = new SelectList(_db.GroupInfos.Where(g => grpIdList.Contains(g.Id)).ToList(), "Id", "Name");

            return new JsonResult(groups);
        }
    }
}
