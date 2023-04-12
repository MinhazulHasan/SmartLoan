using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartLoan.Models;
using SmartLoan.ViewModels;
using System.Reflection;

namespace SmartLoan.Controllers
{
    // [Authorize]
    public class LoanController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        public LoanController(ApplicationDbContext db,UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var groups = _db.GroupInfos.ToList();
            var groupsId = groups.Select(i => i.Id).ToList();
            var loanTaken = _db.LoanMasters.Where(m => groupsId.Contains(m.GroupInfoId) && m.InProgress).Select(m => m.GroupInfoId).Distinct().ToList();
            var viewModel = new GroupLoanTakenViewModel()
            {
                GroupInfos = groups,
                LoanTakenList = loanTaken,
            };
            return View(viewModel);
        }

        public IActionResult Plan(int id)
        {
            var memberInfos = _db.GroupWithMembers.Where(m => m.GroupInfoId == id).ToList();
            if(!memberInfos.Any())
            {
                return RedirectToAction("ViewMembers", "Group", new { id = id });
            }

            var group = _db.GroupInfos.SingleOrDefault(g => g.Id == id);
            if(group == null)
            {
                return BadRequest();
            }
            var loan = new Loan()
            {
                GroupInfoId = id,
                GroupInfo= group
            };
            return View(loan);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateLoanId(Loan loan)
        {
            if (loan == null)
            {
                return BadRequest();
            }

            var groupInfo = _db.GroupInfos.SingleOrDefault(g => g.Id == loan.GroupInfoId);

            if (groupInfo == null)
            {
                return NotFound();
            }

            _db.Loans.Add(loan);
            _db.SaveChanges();

            var loanMaster = new LoanMaster()
            {
                GroupInfoId = loan.GroupInfoId,
                LoanId = loan.Id,
                ExpectedTotalAmount = 0,
                CollectedTotalAmount = 0,
                PaymentInCash = 0,
                OnlinePayment = 0,
                CreationDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };
            _db.LoanMasters.Add(loanMaster);
            _db.SaveChanges();

            var memberIds = _db.GroupWithMembers.Where(g => g.GroupInfoId == loan.GroupInfoId).Select(m => m.MemberId).ToList();

            var members = _db.MemberInfos.Where(m => memberIds.Contains(m.Id)).ToList();
            var viewModel = new LoanPlanViewModel()
            {
                MemberInfos = members,
                GroupInfo = groupInfo,
                Loan = loan,
                LoanMaster = loanMaster
            };
            return View("LoanTable",viewModel);
        }

        public IActionResult LoanTable(int id)
        {
            var loan = _db.Loans.SingleOrDefault(s => s.Id== id);
            if(loan == null)
            {
                return NotFound();
            }
            var loanMaster = _db.LoanMasters.SingleOrDefault(s => s.LoanId == loan.Id);
            var groupInfo = _db.GroupInfos.SingleOrDefault(g => g.Id == loan.GroupInfoId);
            var members = _db.MemberInfos.Where(m => m.GroupId == loan.GroupInfoId);
            var viewModel = new LoanPlanViewModel()
            {
                MemberInfos = members,
                GroupInfo = groupInfo,
                Loan = loan,
                LoanMaster = loanMaster
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateLoanDetails(IList<LoanDetail> loanDetails)
        {
            if (loanDetails.Count == 0)
            {
                return BadRequest();
            }
            var loanMaster = _db.LoanMasters.SingleOrDefault(m => m.Id == loanDetails[0].LoanMasterId);
            if (loanMaster == null)
            {
                return NotFound();
            }

            foreach (var loan in loanDetails)
            {
                loanMaster.ExpectedTotalAmount = loanMaster.ExpectedTotalAmount + loan.TotalAmount;
                loanMaster.InstallmentCount = loanMaster.InstallmentCount < loan.Installment ? loan.Installment : loanMaster.InstallmentCount;
                _db.LoanDetails.Add(loan);
            }
            loanMaster.UpdatedDate = DateTime.Now;
            _db.LoanMasters.Update(loanMaster);
            _db.SaveChanges();
            return RedirectToAction("Index","Installment");
        }

        public IActionResult OfficeSubmission(int id)
        {
            var installmentMaster = _db.InstallmentMaster.FirstOrDefault(m => m.Id == id);
            if (installmentMaster == null)
            {
                return NotFound();
            }
            
            var loanMaster = _db.LoanMasters.Include(l => l.Loan).FirstOrDefault(m => m.Id == installmentMaster.LoanMasterId);
            var groupInfo = _db.GroupInfos.FirstOrDefault(g => g.Id == loanMaster.GroupInfoId);
            var users = _userManager.Users.Where(u => u.Id != installmentMaster.CollectorId).ToList();
            var paymentMethod = _db.PaymentMethods.ToList();

            var usersSelectItem = new SelectList(users, "Id", "Email");
            var paymentList = new SelectList(paymentMethod, "Id", "MethodName");

            var viewModel = new OfficeSubmissionViewModel()
            {
                UsersList = usersSelectItem,
                LoanMaster = loanMaster,
                GroupInfo = groupInfo,
                InstallmentMaster = installmentMaster,
                PaymentList = paymentList,
            };
            
            return View(viewModel);
        }

        public IActionResult ReceiveOfficePayment(OfficeSubmissionViewModel officeSubmissionViewModel)
        {
            if (officeSubmissionViewModel == null)
            {
                return BadRequest();
            }

            var installmentMaster = _db.InstallmentMaster.FirstOrDefault(c => c.Id == officeSubmissionViewModel.InstallmentMasterId);
            if(installmentMaster == null)
            {
                return BadRequest();
            }
            var loanOfficeCollection = new LoanOfficeCollection()
            {
                InstallmentMasterId = officeSubmissionViewModel.InstallmentMasterId,
                PaymentMethodId = officeSubmissionViewModel.PaymentMethodId,
                CollectorId = officeSubmissionViewModel.CollectorId,
                SubmitterId = officeSubmissionViewModel.SubmitterId,
                AmountReceived = officeSubmissionViewModel.AmountSubmitted
            };
            installmentMaster.SubmittedAmountToOffice = installmentMaster.SubmittedAmountToOffice + officeSubmissionViewModel.AmountSubmitted;
            _db.InstallmentMaster.Update(installmentMaster);
            _db.LoanOfficeCollections.Add(loanOfficeCollection);
            _db.SaveChanges();
            TempData["suceess"] = "Payment Received";
            return RedirectToAction("AllInstallments","Installment", new { grpId = installmentMaster.GroupInfoId, loanId = installmentMaster.LoanMasterId }); 
        }


        [Route("Loan/LoanOfficeSubmissionForm/{userId}/{groupId}/{loanId}")]
        public IActionResult LoanOfficeSubmissionForm(string userId,int groupId,int loanId)
        {
            var installments = _db.InstallmentDetails.Where(I => I.CollectorId == userId && I.GroupId == groupId && I.LoanId == loanId && !I.SubmittedToOffice).ToList();
            var paymentMethods = _db.PaymentMethods.ToList();
            double totalAmount = 0;
            foreach (var installment in installments) totalAmount += installment.ReceivedAmount;

            var viewModel = new LoanOfficeSubmissionFormViewModel()
            {
                TotalAmount = totalAmount,
                InstallmentDetails = installments,
                Users = new SelectList(_userManager.Users.ToList(), "Id", "email"),
                PaymentMethods = new SelectList(paymentMethods,"Id","MethodName")
            };

            return PartialView("_OfficeSubmissionForm",viewModel);
        }


    }
}
