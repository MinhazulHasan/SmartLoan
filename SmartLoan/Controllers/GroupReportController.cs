using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartLoan.Models;

namespace SmartLoan.Controllers
{
    // [Authorize]
    public class GroupReportController : Controller
    {
        private readonly ApplicationDbContext _db;
        public GroupReportController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult AllGroups()
        {
            IEnumerable<GroupInfo> groups = _db.GroupInfos.ToList();
            return View(groups);
        }
    }
}
