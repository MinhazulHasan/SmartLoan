using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartLoan.Models;
using SmartLoan.ViewModels;

namespace SmartLoan.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        public ReportController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult SelectMembers()
        {
            SelectList members = new SelectList(_db.MemberInfos.ToList(), "Id", "Name");
            return View(members);
        }
        
        
        public IActionResult SelectCollector()
        {
            SelectList collector = new SelectList(_userManager.Users.ToList(), "Id", "UserName");
            return View(collector);
        }


        public async Task<IActionResult> CollectorReport(string id)
        {
            var collectorReports = _db.LoanOfficeCollections.Where(c => c.CollectorId == id).ToList();

            List<CollectorReport> collectorReportList = new List<CollectorReport>();

            foreach(var colllectorReport in collectorReports)
            {
                var submitter = await _userManager.FindByIdAsync(colllectorReport.SubmitterId);
                var installmentMaster = _db.InstallmentMaster.SingleOrDefault(im => im.Id == colllectorReport.InstallmentMasterId);
                var group = _db.GroupInfos.SingleOrDefault(g => g.Id == installmentMaster.GroupInfoId);

                CollectorReport report = new CollectorReport
                {
                    InstallmentNumber = installmentMaster.InstallmentNumber,
                    PaymentMethod = colllectorReport.PaymentMethodId,
                    SubmittorName = submitter.UserName,
                    CollectionDate = colllectorReport.CollectionDate,
                    ReceivedAmount = colllectorReport.AmountReceived,
                    GroupName = group.Name,
                    SubmittedAmountToOffice = installmentMaster.SubmittedAmountToOffice
                };

                collectorReportList.Add(report);
            }

            return PartialView("_CollectorReportPartial", collectorReportList);
        }




        [Route("Report/MemberReport/{memberId}/{groupId}")]
        public async Task<IActionResult> MemberReport(int memberId, int groupId)
        {
            IEnumerable<LoanDetail> loanDetails = _db.LoanDetails.Where(ld => ld.MemberInfoId == memberId && ld.GroupInfoId == groupId).ToList();
            List<ReportTableForMemberLoan> memberLoanReports = new List<ReportTableForMemberLoan>();
            foreach(var loanDetail in loanDetails)
            {
                Loan loan = _db.Loans.Single(l => l.Id == loanDetail.LoanMasterId);
                ReportTableForMemberLoan memberLoan = new ReportTableForMemberLoan
                {
                    LoanMasterId = loanDetail.LoanMasterId,
                    LoanName = loan.LoanId,
                    Amount = loanDetail.Amount,
                    Period = loanDetail.Period,
                    InstallmentNumber = loanDetail.Installment,
                    InterestRate = loanDetail.InterestRate,
                    ProcessingFee = loanDetail.ProcessingFee,
                    OtherCharge = loanDetail.OtherCharge,
                    TotalAmount = loanDetail.TotalAmount,
                    AmountPerInstallment = loanDetail.AmountPerInstallment,
                    InProcess = loanDetail.InProcess
                };
                memberLoanReports.Add(memberLoan);
            }

            IEnumerable<InstallmentDetails> installmentDetails = _db.InstallmentDetails.Where(i => i.MemberInfoId == memberId && i.GroupId == groupId).ToList();
            List<ReportTableForMemberInstallment> memberInstallmentReports = new List<ReportTableForMemberInstallment>();
            foreach(var installmentDetail in installmentDetails)
            {
                var user = await _userManager.FindByIdAsync(installmentDetail.CollectorId);
                Loan loan = _db.Loans.Single(l => l.Id == installmentDetail.LoanMasterId);
                ReportTableForMemberInstallment memberInstallment = new ReportTableForMemberInstallment
                {
                    CollectorName = user.UserName,
                    LoanName = loan.LoanId,
                    InstallmentNumber = installmentDetail.InstalllmentNumber,
                    LoanMasterId = installmentDetail.LoanMasterId,
                    InstallmentAmount= installmentDetail.InstallmentAmount,
                    ReceivedAmount = installmentDetail.ReceivedAmount,
                    Penalty = installmentDetail.PenaltyCharge,
                    DueAmount = installmentDetail.DueAmount,
                    Status = installmentDetail.StatusId,
                    InstallmentDate = installmentDetail.InstallmentDate,
                    ReceivedtDate = installmentDetail.ReceivedDate
                };
                memberInstallmentReports.Add(memberInstallment);
            }

            MemberInfo member = _db.MemberInfos.Single(m => m.Id == memberId);
            GroupInfo group = _db.GroupInfos.Single(m => m.Id == groupId);
            MemberReportViewModel viewModel = new MemberReportViewModel
            {
                Name= member.Name,
                FatherName= member.FatherName,
                Nid= member.Nid,
                BirthDate= member.BirthDate,
                MobileNumber= member.MobileNumber,
                Address = member.Address,
                GroupName = group.Name,
                ReportTableForMemberLoans = memberLoanReports,
                ReportTableForMemberInstallments= memberInstallmentReports
            };
            //List<string> groupNames = _db.GroupInfos.Where(g => grpIds.Contains(g.Id)).Select(g => g.Name).ToList();
            return PartialView("_MemberReportPartial", viewModel);
        }



        public IActionResult AllGroups()
        {
            IEnumerable<GroupInfo> groups = _db.GroupInfos.ToList();
            return View(groups);
        }

        public IActionResult LoanGroupDetails(int id)
        {
            List<LoanMasterReport> LoanMasterReports = new List<LoanMasterReport>();

            GroupInfo group = _db.GroupInfos.Single(g => g.Id == id);
            IEnumerable<LoanMaster> loanMasters = _db.LoanMasters.Where(lm => lm.GroupInfoId == id).ToList();

            foreach(LoanMaster loanMaster in loanMasters)
            {
                IEnumerable<LoanDetail> loanDetails = _db.LoanDetails.Where(ld => ld.LoanMasterId == loanMaster.LoanId && ld.GroupInfoId == loanMaster.GroupInfoId).ToList();
                List<LoanDetailsReport> particularLoanDetailsReports = new List<LoanDetailsReport>();

                foreach(LoanDetail loanDetail in loanDetails)
                {
                    MemberInfo memberInfo = _db.MemberInfos.Single(m => m.Id == loanDetail.MemberInfoId);
                    LoanDetailsReport loanDetailsReport = new LoanDetailsReport
                    {
                        MasterId = loanDetail.LoanMasterId,
                        GroupName = group.Name,
                        MemberName = memberInfo.Name,
                        Amount= loanDetail.Amount,
                        Period= loanDetail.Period,
                        Installment= loanDetail.Installment,
                        InterestRate= loanDetail.InterestRate,
                        ProcessingFee= loanDetail.ProcessingFee,
                        OtherCharge= loanDetail.OtherCharge,
                        TotalAmount= loanDetail.TotalAmount,
                        TotalFee= loanDetail.TotalFee
                    };
                    particularLoanDetailsReports.Add(loanDetailsReport);
                }

                Loan loan = _db.Loans.Single(l => l.Id == loanMaster.LoanId);
                LoanMasterReport loanMasterReport = new LoanMasterReport
                {
                    LoanId = loanMaster.LoanId,
                    LoanName = loan.LoanId,
                    ExpectedTotalAmount= loanMaster.ExpectedTotalAmount,
                    CollectedTotalAmount= loanMaster.CollectedTotalAmount,
                    CreationDate= loanMaster.CreationDate,
                    UpdatedDate= loanMaster.UpdatedDate,
                    InstallmentCount= loanMaster.InstallmentCount,
                    InstallmentComplete= loanMaster.InstallmentComplete,
                    InProgress = loanMaster.InProgress,
                    LoanDetailsReports = particularLoanDetailsReports
                };
                LoanMasterReports.Add(loanMasterReport);
            }
            return View(LoanMasterReports);
        }




        public IActionResult InstallmentGroupDetails(int id)
        {
            List<InstallmentMasterReport> InstallmentMasterReports = new List<InstallmentMasterReport>();

            GroupInfo group = _db.GroupInfos.Single(g => g.Id == id);
            IEnumerable<InstallmentMaster> installmentMasters = _db.InstallmentMaster.Where(Im => Im.GroupInfoId == id).ToList();

            foreach(InstallmentMaster installmentMaster in installmentMasters)
            {
                IEnumerable<InstallmentDetails> installmentDetails = _db.InstallmentDetails.Where(x => x.InstalllmentNumber == installmentMaster.InstallmentNumber && x.GroupId == id).ToList();

                List<InstallmentDetailsReport> particularInstallmentDetailsReports = new List<InstallmentDetailsReport>();

                foreach (InstallmentDetails installment in installmentDetails)
                {
                    MemberInfo memberInfo = _db.MemberInfos.Single(m => m.Id == installment.MemberInfoId);
                    InstallmentDetailsReport installmentDetailsReport = new InstallmentDetailsReport
                    {
                        InstallmentNumber = installment.InstalllmentNumber,
                        MemberInfoId = installment.MemberInfoId,
                        MemberName = memberInfo.Name,
                        InstallmentAmount = installment.InstallmentAmount,
                        ReceivedAmount = installment.ReceivedAmount,
                        PenaltyCharge = installment.PenaltyCharge,
                        DueAmount = installment.DueAmount,
                        Status =installment.StatusId,
                        InstallmentDate = installment.InstallmentDate,
                        ReceivedtDate = installment.ReceivedDate
                    };
                    particularInstallmentDetailsReports.Add(installmentDetailsReport);
                }

                var collector = _userManager.Users.Single(u => u.Id == installmentMaster.CollectorId);

                InstallmentMasterReport installmentMasterReport = new InstallmentMasterReport()
                {
                    GroupId = id,
                    GroupName = group.Name,
                    InstallmentNum = installmentMaster.InstallmentNumber,
                    ExpectedAmount = installmentMaster.ExpectedAmount,
                    CollectedAmount = installmentMaster.CollectedAmount,
                    CreationDate = installmentMaster.CreationDate,
                    LastUpdatedDate = installmentMaster.LastUpdatedDate,
                    CollectorName = collector.UserName,
                    InstallmentDetailsReports = particularInstallmentDetailsReports
                };

                InstallmentMasterReports.Add(installmentMasterReport);
            }
            return View(InstallmentMasterReports);
        }

        public IActionResult LoanCollection()
        {
            var users = _userManager.Users.ToList();
            var userSelectItem = new SelectList(users, "Id", "UserName");
            return View(userSelectItem);
        }

        public async Task<IActionResult> LoanCollectionReportAsync(string id)
        {
            var installmentMasters = _db.InstallmentMaster.Include(i => i.LoanMaster.Loan.GroupInfo).Where(i => i.CollectorId == id).ToList();
            IdentityUser user = await _userManager.FindByIdAsync(id);

            var viewModel = new UserAndInstallmentDetailsViewModel()
            {
                User = user,
                InstallmentMasters = installmentMasters
            };

            return PartialView(viewModel);
        }

    }
}
