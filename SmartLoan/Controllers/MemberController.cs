using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartLoan.Models;
using SmartLoan.ViewModels;

namespace SmartLoan.Controllers
{
    // [Authorize]
    public class MemberController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        public MemberController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            SelectList members = new SelectList(_db.MemberInfos.ToList(), "Id", "Name");
            return View(members);
        }

        public IActionResult AllMembers()
        {
            IEnumerable<MemberInfo> allMembers = _db.MemberInfos.ToList();
            return View(allMembers);
        }

        public IActionResult AddMultipleMembersInGroup()
        {
            SelectList groups = new SelectList(_db.GroupInfos.ToList(), "Id", "Name");
            IEnumerable<MemberInfo> members = _db.MemberInfos.ToList();

            GroupWithMemberViewModel viewModel = new GroupWithMemberViewModel
            {
                MemberInfos = members,
                Groups = groups
            };

            return View(viewModel);
        }




        public IActionResult PerMemberCollectionHistory(int id)
        {
            var groupIds = _db.GroupWithMembers.Where(x => x.MemberId == id).Select(x => x.GroupInfoId).ToList();

            List<CollectionReport> collectionReports = new List<CollectionReport>();

            foreach(var groupId in groupIds)
            {
                List<MemberCollection> memberCollections = new List<MemberCollection>();

                IQueryable<CollectionDetails> collectionDetails = _db.CollectionDetails.Where(c => c.MemberInfoId == id && c.GroupId == groupId);
                List<IdentityUser> identityUsers = _userManager.Users.ToList();
                GroupInfo groupinfo = _db.GroupInfos.Single(g => g.Id == groupId);

                double totalDeposit = 0;
                double totalPayInCash = 0;
                double totalOnlinePay = 0;
                double totalDue = 0;

                foreach (var collection in collectionDetails)
                {
                    MemberCollection singleMemberCollection = new()
                    {
                        CollectorName = identityUsers.Where(i => i.Id == collection.CollectorId).Select(c => c.UserName).First(),
                        CollectionDate = collection.CollectionDate,
                        CollectionAmount = groupinfo.AmountToDeposit - collection.AmountToPay,
                        CashPayment = collection.PaymentInCash,
                        OnlinePayment = collection.PaymentOnline,
                        DuePayment = collection.AmountToPay
                    };

                    totalDeposit += ((double)groupinfo.AmountToDeposit - collection.AmountToPay);
                    totalPayInCash += collection.PaymentInCash;
                    totalOnlinePay += collection.PaymentOnline;
                    totalDue += collection.AmountToPay;

                    memberCollections.Add(singleMemberCollection);
                }

                CollectionReport collectionReport = new CollectionReport()
                {
                    MemberCollection = memberCollections,
                    GroupName = groupinfo.Name,
                    TotalDeposit = totalDeposit,
                    TotalPayInCash= totalPayInCash,
                    TotalOnlinePay= totalOnlinePay,
                    TotalDue = totalDue
                };

                collectionReports.Add(collectionReport);
            }

            CollectionHistoryViewModel viewModel = new CollectionHistoryViewModel()
            {
                CollectionReports = collectionReports,
                MemberInfo = _db.MemberInfos.Single(m => m.Id == id)
            };

            return PartialView("_PerMemberCollectionHistory", viewModel);
        }


        public IActionResult SingleMemberDetails(int id)
        {
            var member = _db.MemberInfos.Single(m => m.Id == id);
            var group = _db.GroupInfos.SingleOrDefault(g => member.GroupId == g.Id);
            var marritialStatus = _db.MarritialStatuses.Single(m => m.Id == member.MarritialStatusId);
            var viewModel = new SingleMemberViewModel
            {
                MarritialStatus = marritialStatus,
                GroupInfo = group,
                MemberInfo = member,
            };
            return View(viewModel);
        }


        public IActionResult AddMember(int id)
        {
            IEnumerable<MarritialStatus> marritialStatuses = _db.MarritialStatuses.ToList();
            var viewModel = new NewMemberInfoViewModel()
            {
                MarritialStatus = marritialStatuses,
                GroupId = id,
                MemberInfo = new MemberInfo { GroupId= id }
            };
            return View(viewModel);
        }


        public IActionResult EditMember(int id)
        {
            IEnumerable<MarritialStatus> marritialStatuses = _db.MarritialStatuses.ToList();
            MemberInfo member = _db.MemberInfos.Single(m => m.Id == id);
            var viewModel = new NewMemberInfoViewModel()
            {
                MarritialStatus = marritialStatuses,
                MemberInfo = member
            };
            return View(viewModel);
        }


        // +++++++++++++++++ CREATE +++++++++++++++++
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMember(NewMemberInfoViewModel newMemberInfoViewModel)
        {
            if (newMemberInfoViewModel.MemberInfo != null)
            {
                _db.MemberInfos.Add(newMemberInfoViewModel.MemberInfo);
                _db.SaveChanges();
                TempData["Success"] = "Member created successfully!!!";

                if (newMemberInfoViewModel.MemberInfo.GroupId == 0)
                    return RedirectToAction("AllMembers");
                else
                {
                    GroupWithMember groupWithMember = new GroupWithMember
                    {
                        GroupInfoId = newMemberInfoViewModel.MemberInfo.GroupId,
                        MemberId = newMemberInfoViewModel.MemberInfo.Id
                    };
                    _db.GroupWithMembers.Add(groupWithMember);
                    _db.SaveChanges();
                    return RedirectToAction("ViewMembers", "Group", new { id = newMemberInfoViewModel.MemberInfo.GroupId });
                }
            }
            return View(newMemberInfoViewModel);
        }


        // +++++++++++++++++ EDIT +++++++++++++++++
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMember(NewMemberInfoViewModel newMemberInfoViewModel)
        {
            if (newMemberInfoViewModel.MemberInfo != null)
            {
                _db.MemberInfos.Update(newMemberInfoViewModel.MemberInfo);
                _db.SaveChanges();
                TempData["Success"] = "Group updated successfully!!!";
                return RedirectToAction("ViewMembers", "Group", new { id = newMemberInfoViewModel.MemberInfo.GroupId });
            }
            return View(newMemberInfoViewModel);
        }
    }
}
