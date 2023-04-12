using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartLoan.Models;
using SmartLoan.ViewModels;

namespace SmartLoan.Controllers
{
    // [Authorize]
    public class GroupController : Controller
    {
        private readonly ApplicationDbContext _db;
        public GroupController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult ViewGroups()
        {
            IEnumerable<GroupInfo> groups = _db.GroupInfos.ToList();
            return View(groups);
        }


        public IActionResult DeleteMemberList(int id)
        {
            var memberIds = _db.GroupWithMembers.Where(g => g.GroupInfoId == id).Select(g => g.MemberId).ToList();

            var groupCollectionIds = _db.CollectionDetails.Where(c => c.GroupId == id).Select(g => g.MemberInfoId).Distinct().ToList();
            var memberIdNotPerformCollection = memberIds.Where(x => !groupCollectionIds.Contains(x)).ToList();

            var groupLoanIds = _db.LoanDetails.Where(l => l.GroupInfoId == id).Select(l => l.MemberInfoId).Distinct().ToList();
            var memberIdNotPerformLoan = memberIds.Where(x => !groupLoanIds.Contains(x)).ToList();

            List<int> memberId = memberIdNotPerformCollection.Intersect(memberIdNotPerformLoan).ToList();
            
            IEnumerable<MemberInfo> memberInfos = _db.MemberInfos.Where(m => memberId.Contains(m.Id)).ToList();

            DeleteMemberViewModel deleteMemberViewModel = new DeleteMemberViewModel
            {
                MemberInfos = memberInfos,
                GroupId = id
            };

            return View(deleteMemberViewModel);
        }


        public IActionResult SingleGroupDetails(int id)
        {
            var group = _db.GroupInfos.Single(g => g.Id == id);
            var leader = _db.MemberInfos.SingleOrDefault(l => l.Id == group.GroupLeaderId);
            var submissionPeriod = _db.SubmissionPeriods.Single(p => p.id == group.SubmissionPeriodId);
            var viewModel = new SingleGroupViewModel
            {
                GroupInfo = group,
                MemberInfo = leader,
                SubmissionPeriod = submissionPeriod,
            };
            return View(viewModel);
        }


        public IActionResult ViewMembers(int id)
        {
            if (id == 0 || id == null)
                return NotFound();

            var memberIds = _db.GroupWithMembers.Where(g => g.GroupInfoId == id).Select(m => m.MemberId).ToList();

            var viewModel = new MemberInfoWithGroupViewModel
            {
                GroupId = id,
                MemberInfos = _db.MemberInfos.Where(m => memberIds.Contains(m.Id)).ToList()
            };

            return View(viewModel);
        }


        public IActionResult CreateGroup()
        {
            IEnumerable<MemberInfo> memberInfos = _db.MemberInfos.ToList();
            IEnumerable<SubmissionPeriod> submissionPeriods = _db.SubmissionPeriods.ToList();
            var viewModel = new NewGroupInfoViewModel
            {
                MemberInfo = memberInfos,
                SubmissionPeriod = submissionPeriods,
                GroupInfo = new GroupInfo { GroupCreatorName = User.Identity.Name }
            };
            return View(viewModel);
        }


        public IActionResult EditGroup(int id)
        {
            GroupInfo group = _db.GroupInfos.Single(g => g.Id == id);
            IEnumerable<MemberInfo> memberInfos = _db.MemberInfos.Where(m => m.GroupId == id).ToList();
            IEnumerable<SubmissionPeriod> submissionPeriods = _db.SubmissionPeriods.ToList();
            var viewModel = new NewGroupInfoViewModel
            {
                MemberInfo = memberInfos,
                SubmissionPeriod = submissionPeriods,
                GroupInfo = group
            };
            return View(viewModel);
        }



        // +++++++++++++++++ CREATE +++++++++++++++++
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateGroup(NewGroupInfoViewModel viewModel)
        {
            if (viewModel.GroupInfo != null)
            {
                _db.GroupInfos.Add(viewModel.GroupInfo);
                _db.SaveChanges();
                TempData["Success"] = "Group created successfully!!!";
                return RedirectToAction("ViewGroups", "Group");
            }
            return View(viewModel);
        }


        // +++++++++++++++++ UPDATE +++++++++++++++++
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditGroup(NewGroupInfoViewModel viewModel)
        {
            if (viewModel.GroupInfo != null)
            {
                _db.GroupInfos.Update(viewModel.GroupInfo);
                _db.SaveChanges();
                TempData["Success"] = "Group updated successfully!!!";
                return RedirectToAction("ViewGroups", "Group");
            }
            return View(viewModel);
        }
    }




}
