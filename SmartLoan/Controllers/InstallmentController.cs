using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Versioning;
using SmartLoan.Models;
using SmartLoan.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;

namespace SmartLoan.Controllers
{
    // [Authorize]
    public class InstallmentController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        public InstallmentController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            List<InstallmentCollection> viewModel = new List<InstallmentCollection>();
            //var loanMasters = _db.LoanMasters.AsNoTracking().Where(l => l.Id > 5).Select(l => new
            //{
            //    name = l.OnlinePayment,
            //    add = l.CreationDate
            //}).ToList();
            var loanMasters = _db.LoanMasters.ToList();
            foreach (var loanMaster in loanMasters)
            {
                Loan loan = _db.Loans.Single(l => l.Id == loanMaster.LoanId);
                GroupInfo groupInfo = _db.GroupInfos.Single(g => g.Id == loanMaster.GroupInfoId);
                viewModel.Add(new InstallmentCollection
                {
                    LoanMaster = loanMaster,
                    Groupname = groupInfo.Name,
                    LoanName = loan.LoanId
                });
            }
            return View(viewModel);
            //var loanMasters = _db.LoanMasters.Include(l => l.Loan).ToList();
            //var groupId = loanMasters.Select(l => l.GroupInfoId).ToList();
            //var groups = _db.GroupInfos.Where(g => groupId.Contains(g.Id)).ToList();
        }

        [Route("Installment/InstallmentPopUp/{grpId}/{loanId}")]
        public IActionResult InstallmentPopUp(int grpId, int loanId)
        {
            var loanMaster = _db.LoanMasters.SingleOrDefault(l => l.LoanId == loanId && l.GroupInfoId == grpId && l.InProgress);

            InstallmentPopUpViewModel viewModel = new InstallmentPopUpViewModel
            {
                LoanMaster = loanMaster,
                CollectorList = new List<SelectListItem>(),
            };

            var allUsers = _userManager.Users.ToList();

            foreach (var singleuser in allUsers)
            {
                viewModel.CollectorList.Add(new SelectListItem()
                {
                    Value = singleuser.Id,
                    Text = singleuser.UserName
                });
            }
            return PartialView("_InstallmentPartial", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateInstallmentDetails(InstallmentPopUpViewModel modelFromPopUp)
        {
            LoanMaster loanMaster = _db.LoanMasters.SingleOrDefault(lm => lm.LoanId == modelFromPopUp.LoanMaster.LoanId && lm.GroupInfoId == modelFromPopUp.LoanMaster.GroupInfoId && lm.InProgress);
            if (loanMaster == null) return NotFound();

            loanMaster.InstallmentComplete += 1;
            _db.LoanMasters.Update(loanMaster);

            IEnumerable<LoanDetail> loanDetails = _db.LoanDetails.Where(l => l.LoanMasterId == loanMaster.Id && l.GroupInfoId == loanMaster.GroupInfoId && l.InProcess).ToList();

            InstallmentMaster installmentMaster = new InstallmentMaster();
            installmentMaster.InstallmentNumber = loanMaster.InstallmentComplete;
            installmentMaster.LoanMasterId = loanMaster.Id;
            installmentMaster.CollectorId = modelFromPopUp.CollectorId;
            installmentMaster.ExpectedAmount = 0;
            installmentMaster.GroupInfoId = 0;
            installmentMaster.ExpectedAmount = 0;
            _db.InstallmentMaster.Add(installmentMaster);
            _db.SaveChanges();

            foreach (var loan in loanDetails)
            {
                InstallmentDetails installmentDetails = new InstallmentDetails();
                installmentDetails.MemberInfoId = loan.MemberInfoId;
                installmentDetails.GroupId = loan.GroupInfoId;
                installmentDetails.InstallmentAmount = loan.AmountPerInstallment;
                installmentDetails.InstalllmentNumber = loanMaster.InstallmentComplete;
                installmentDetails.StatusId = 1;
                installmentDetails.CollectorId = modelFromPopUp.CollectorId;
                installmentDetails.LoanMasterId = loanMaster.Id;
                installmentDetails.LoanId = loanMaster.LoanId;
                installmentDetails.InstallmentMasterId = installmentMaster.Id;

                installmentMaster.GroupInfoId = loan.GroupInfoId;
                installmentMaster.ExpectedAmount += loan.AmountPerInstallment;

                _db.InstallmentDetails.Add(installmentDetails);
            }

            _db.InstallmentMaster.Update(installmentMaster);
            _db.SaveChanges();

            return RedirectToAction("AllInstallments", new { grpId = installmentMaster.GroupInfoId, loanId = installmentMaster.LoanMasterId });
        }



        [Route("Installment/AllInstallments/{grpId}/{loanId}")]
        public IActionResult AllInstallments(int grpId, int loanId)
        {
            IEnumerable<InstallmentMaster> installmentMasters = _db.InstallmentMaster.Where(im => im.LoanMasterId == loanId && im.GroupInfoId == grpId).ToList();

            var GroupID = installmentMasters.First().GroupInfoId;
            var group = _db.GroupInfos.Single(g => g.Id == GroupID);
            var LoanID = installmentMasters.First().LoanMasterId;
            var loan = _db.Loans.Single(l => l.Id == LoanID);
            var loanMaster = _db.LoanMasters.SingleOrDefault(c => c.Id == LoanID);
            if(loanMaster == null)
            {
                return BadRequest();
            }

            InstallmentSummeryViewModel viewModel = new InstallmentSummeryViewModel
            {
                InstallmentMasters = installmentMasters,
                GroupName = group.Name,
                Loan = loan,
                LoanMaster = loanMaster,
            };

            return View(viewModel);
        }


        [Route("Installment/SingleInstallmentDetails/{grpId}/{installmentNum}/{loanId}")]
        public IActionResult SingleInstallmentDetails(int grpId, int installmentNum,int loanId)
        {
            IEnumerable<InstallmentDetails> installmentDetails = _db.InstallmentDetails.Include(i => i.MemberInfo).Where(i => i.InstalllmentNumber == installmentNum && i.GroupId == grpId && i.LoanId == loanId).ToList();
            var master = _db.InstallmentMaster.SingleOrDefault(c => c.Id == installmentDetails.Select(c => c.InstallmentMasterId).First());

            if(master== null)
            {
                return BadRequest();
            }

            var viewModel = new InstallmentDetailsAndListViewModel()
            {
                InstallmentDetails = installmentDetails,
                InstallmentMaster = master
            };
            
            return View(viewModel);
        }

        public IActionResult UpdateInstallment(IList<InstallmentDetails> installmentsList)
        {
            if(installmentsList.Count== 0)
            {
                return BadRequest();
            }
            var master = _db.InstallmentMaster.SingleOrDefault(c => c.Id == installmentsList[0].InstallmentMasterId);
            var loanMaster = _db.LoanMasters.SingleOrDefault(c => c.Id == installmentsList[0].LoanMasterId);
            if(master == null || loanMaster == null)
            {
                return BadRequest();
            }
            int fullreceivedItem = 0;
            bool partiallycollected = false;

            foreach(var item in installmentsList)
            {
                if(item.DueAmount == 0)
                {
                    item.StatusId = MagicNumber.Collected;
                    fullreceivedItem++;
                }else if(item.ReceivedAmount != 0)
                {
                    item.StatusId = MagicNumber.PartiallyCollected;
                    partiallycollected = true;
                }
                master.CollectedAmount += item.ReceivedAmount;
                loanMaster.CollectedTotalAmount += item.ReceivedAmount;
                _db.InstallmentDetails.Update(item);
            }
            if(loanMaster.InstallmentCount == installmentsList[0].InstalllmentNumber)
            {
                loanMaster.InProgress = false;
            }

            if(fullreceivedItem == installmentsList.Count)
            {
                master.StatusId = MagicNumber.Collected;
            }else if(partiallycollected)
            {
                master.StatusId = MagicNumber.PartiallyCollected;
            }
            _db.InstallmentMaster.Update(master);
            _db.LoanMasters.Update(loanMaster);
            _db.SaveChanges();
            return RedirectToAction("AllInstallments", new { grpId = master.GroupInfoId, loanId = master.LoanMasterId });
        }

    }
}

