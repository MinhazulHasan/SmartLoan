using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SmartLoan.Models
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<MemberInfo> MemberInfos { get; set; }

        public DbSet<MarritialStatus> MarritialStatuses { get; set; }

        public DbSet<GroupInfo> GroupInfos { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Collection> Collections { get; set; }

        public DbSet<SubmissionPeriod> SubmissionPeriods { get; set; }
        public DbSet<GroupName> GroupNames { get; set; }
        public DbSet<CollectionDetails> CollectionDetails { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<OfficeCollection> OfficeCollections { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanMaster> LoanMasters { get; set; }
        public DbSet<LoanDetail> LoanDetails { get; set; }
        public DbSet<InstallmentDetails> InstallmentDetails { get; set; }
        public DbSet<InstallmentMaster> InstallmentMaster { get; set; }
        public DbSet<GroupWithMember> GroupWithMembers { get; set; }
        public DbSet<LoanOfficeCollection> LoanOfficeCollections { get; set; }
        public DbSet<UsersLoginSession> UsersLoginSessions { get; set; }
    }
}
