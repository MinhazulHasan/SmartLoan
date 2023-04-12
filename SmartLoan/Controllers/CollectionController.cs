using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartLoan.Dtos;
using SmartLoan.Models;
using SmartLoan.ViewModels;
using System.Collections;
using System.Linq;

namespace SmartLoan.Controllers
{
    // [Authorize]
    public class CollectionController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        public CollectionController(ApplicationDbContext db, UserManager<IdentityUser> userManager) { 
            _db= db;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var groups = _db.GroupInfos.ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var group in groups)
            {
                list.Add(new SelectListItem
                {
                    Value= Convert.ToString(group.Id),
                    Text= group.Name
                });
            }

            IEnumerable<Collection> collections = _db.Collections.Include(c => c.GroupInfo).Include(c => c.Status).ToList();

            foreach (var collection in collections)
            {
                var statusIds = _db.CollectionDetails.Where(c => c.CollectionId == collection.Id).Select(c => c.StatusId).ToList();
                int count = 0;
                foreach(var id in statusIds)
                {
                    if(id == MagicNumber.PartiallyCollected)
                    {
                        collection.StatusId = MagicNumber.PartiallyCollected;
                        _db.Collections.Update(collection);
                        break;
                    }else if(id == MagicNumber.Collected)
                    {
                        count++;
                    }
                }
                // To check if all the users pay their due
                
                if(count == statusIds.Count())
                {
                    collection.StatusId = MagicNumber.Collected;
                    _db.Collections.Update(collection);
                }else if(count > 0)
                {
                    collection.StatusId = MagicNumber.PartiallyCollected;
                    _db.Collections.Update(collection);
                }
                _db.SaveChanges();
            }

            var user = await _userManager.GetUserAsync(User);
            collections = _db.Collections.Include(c => c.GroupInfo).Include(c => c.Status).Where(c => c.CreatorId == user.Id).OrderByDescending(c => c.CreationDate).ToList();

            var viewModel = new CollectionViewModel()
            {
                ListItem = list,
                Collections = collections,
                CollectorList = new List<SelectListItem>()
            };

            var allUsers = _userManager.Users.ToList(); 
            
            foreach(var singleuser in allUsers)
            {
                viewModel.CollectorList.Add(new SelectListItem()
                {
                    Value = singleuser.Id,
                    Text = singleuser.UserName
                });
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Create(CollectionDto collectionDto)
        {
            var user = await _userManager.GetUserAsync(User);
            var memberIDs = _db.GroupWithMembers.Where(g => g.GroupInfoId == collectionDto.GroupNameId).Select(g => g.MemberId).ToList();
            var membersCount = memberIDs.Count();
            var amountToPay = _db.GroupInfos.Where(c => c.Id == collectionDto.GroupNameId).Select(c => c.AmountToDeposit).FirstOrDefault();

            var collection = new Collection();
            collection.CreationDate = DateTime.Now;
            collection.CollectionDate = collectionDto.CollectionDate;
            collection.EstimateAmount = Convert.ToDouble(membersCount*amountToPay);
            collection.CollectedAmount = 0;
            collection.GroupInfoId = collectionDto.GroupNameId;
            collection.StatusId = MagicNumber.Due;
            collection.CreatorId = user.Id;
            collection.CollectionId = collectionDto.CollectionId;
            collection.CollectorId = collectionDto.CollectorId;
            _db.Add(collection);
            _db.SaveChanges();

            var members = _db.MemberInfos.Where(m => memberIDs.Contains(m.Id)).ToList();
            var group = _db.GroupInfos.SingleOrDefault(m => m.Id == collectionDto.GroupNameId);

            foreach(var member in members)
            {
                var collectionDetails = new CollectionDetails()
                {
                    MemberInfoId = member.Id,
                    GroupId = collectionDto.GroupNameId,
                    StatusId = MagicNumber.Due,
                    CollectionDate = DateTime.Now,
                    LateFee = 0,
                    CollectionId = collection.Id,
                    AmountToPay = group.AmountToDeposit,
                    CollectorId = collectionDto.CollectorId
                };
                _db.CollectionDetails.Add(collectionDetails);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            IEnumerable<CollectionDetails> collectionData = _db.CollectionDetails.Include(c => c.MemberInfo).Where(c => c.CollectionId == id).ToList();
            var collectorId = collectionData.Select(c => c.CollectorId).FirstOrDefault();
            string? collectorMessage = null;
            IdentityUser CollectorInfo = null;
            if (collectorId == null)
            {
                collectorMessage = "No collector was mentioned!";
            }
            else
            {
                CollectorInfo = await _userManager.FindByIdAsync(collectorId);
            }
            int groupId = collectionData.Select(c => c.GroupId).First();
            double TotalAmount = _db.GroupInfos.Where(g => g.Id == groupId).Select(g => g.AmountToDeposit).First();
            Collection collection = _db.Collections.SingleOrDefault(c => c.Id == id);

            List<SelectListItem> PaymentMethodListItem = new List<SelectListItem>();
            var PaymentMethod = _db.PaymentMethods.ToList();

            foreach(var methodType in PaymentMethod)
            {
                PaymentMethodListItem.Add(new SelectListItem()
                {
                    Value = Convert.ToString(methodType.Id),
                    Text = methodType.MethodName
                });
            }
            
            var viewmodel = new CollectionUpdateViewModel()
            {
                Collection = collection,
                CollectionDetails = collectionData,
                CollectionUpdate = new CollectionUpdate()
                {
                    Route = id
                },
                TotalAmount= TotalAmount,
                PaymentMethodListItem = PaymentMethodListItem,
                NoCollector = collectorMessage,
                CollectorInfo = CollectorInfo,
            };
            return View(viewmodel);
        }
        public IActionResult Update(CollectionUpdate collectionUpdate)
        {
            var collection = _db.Collections.SingleOrDefault(c => c.Id == collectionUpdate.Route);
            var collectionDetails = _db.CollectionDetails.Single(c => c.Id == collectionUpdate.Id);
            var amount = collectionDetails.AmountToPay - collectionUpdate.Amount;
            if(amount > 0 && collectionDetails.AmountToPay > amount)
            {
                collectionDetails.StatusId = MagicNumber.PartiallyCollected;
            }
            else if(amount == 0)
            {
                collectionDetails.StatusId = MagicNumber.Collected;
            }
            collection.CollectedAmount = collection.CollectedAmount + collectionUpdate.Amount;
            collectionDetails.AmountToPay = amount;
            collectionDetails.PaymentMethodId = collectionUpdate.PaymentId;
            if (collectionUpdate.PaymentId == MagicNumber.CashPayment)
            {
                collection.PaymentInCash = collectionUpdate.Amount + collection.PaymentInCash;
                collectionDetails.PaymentInCash = collectionUpdate.Amount + collectionDetails.PaymentInCash;
            }
            else if(collectionUpdate.PaymentId == MagicNumber.DigitalPayment)
            {
                collection.PaymentOnline = collectionUpdate.Amount + collection.PaymentOnline;
                collectionDetails.PaymentOnline = collectionUpdate.Amount + collectionDetails.PaymentOnline;
            }
            _db.CollectionDetails.Update(collectionDetails);
            _db.Collections.Update(collection);
            _db.SaveChanges();
            return RedirectToAction("Details",new { id = collectionUpdate.Route });
        }

        public IActionResult SubmitCollection(int id)
        {
            var collection = _db.Collections.FirstOrDefault(c => c.Id == id);
            var collectors = _userManager.Users.Where(c => c.Id != collection.CollectorId).ToList();
            var paymentMethods = _db.PaymentMethods.ToList();
            var officeCollectionViewModel = new OfficeCollectionViewModel()
            {
                SingleCollection = collection,
                CollectorList = new List<SelectListItem>(),
                PaymentMethod = new List<SelectListItem>()

            };
            foreach(var collector in collectors)
            {
                officeCollectionViewModel.CollectorList.Add(new SelectListItem()
                {
                    Value = collector.Id,
                    Text = collector.UserName
                });
            }
            foreach(var payment in paymentMethods)
            {
                officeCollectionViewModel.PaymentMethod.Add(new SelectListItem()
                {
                    Value = Convert.ToString(payment.Id),
                    Text = payment.MethodName
                });
            }
            return View(officeCollectionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitCollection(OfficeCollectionDto officeCollectionDto)
        {

            var collection = _db.Collections.SingleOrDefault(c => c.Id == officeCollectionDto.CollectionId);

            var officeCollection = new OfficeCollection();
            officeCollection.CollectionId = officeCollectionDto.CollectionId;
            officeCollection.CollectorId = officeCollectionDto.CollectorId;
            officeCollection.GroupInfoId= officeCollectionDto.GroupInfoId;
            officeCollection.SubmitterId= officeCollectionDto.SubmitterId;
            officeCollection.Amount = officeCollectionDto.Amount;
            officeCollection.PaymentMethod = officeCollectionDto.PaymentMethod;
            officeCollection.CollectionDate = DateTime.Now;
            collection.OfficeSubmittedAmount = collection.OfficeSubmittedAmount + officeCollectionDto.Amount;
            _db.OfficeCollections.Add(officeCollection);
            _db.Collections.Update(collection);
            _db.SaveChanges();
            TempData["Success"] = "Office Collection Successful!";
            return RedirectToAction("Index", "Collection");
        }
        public async Task<IActionResult> OfficeCollectionPerCollector(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var officeCollectionsPerCollector = _db.OfficeCollections.Include(c => c.Collection).Include(c => c.Collection.GroupInfo).Where(c => c.SubmitterId == id);
            var collectedAmount = _db.CollectionDetails.Where(c => c.CollectorId == user.Id).Sum(c => c.PaymentInCash);
            var submittedAmount = officeCollectionsPerCollector.Sum(c => c.Amount);

            var viewModel = new CollectionPerCollectorViewModel()
            {
                OfficeCollectionsPerCollector = officeCollectionsPerCollector.ToList(),
                User = user,
                CollectedAmount = collectedAmount,
                SubmittedAmount = submittedAmount,
            };
            return PartialView(viewModel);
        }

        public IActionResult History()
        {
            var users = _userManager.Users.ToList();
            var list = new List<SelectListItem>();
            foreach (var user in users)
            {
                list.Add(new SelectListItem()
                {
                    Value = user.Id,
                    Text = user.UserName
                });
            }
            return View(list);
        }

    }
}
