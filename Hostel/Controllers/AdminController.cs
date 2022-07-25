using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hostel.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult GetMembers()
        {
            HostelDBContext context = new HostelDBContext();
            var members = context.Members.Select(m => new { m.MemberId, m.MemberName }).ToList();
            return Json(members, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddMessCharges()
        {
            if ((string)Session["type"] == "Admin")
            {
                HostelDBContext context = new HostelDBContext();
                List<SelectListItem> MemberList = new List<SelectListItem>();
                MemberList.Add(new SelectListItem { Text = "Select", Value = "" });

                foreach (Member member in context.Members.ToList())
                {
                    SelectListItem li = new SelectListItem();
                    li.Text = member.MemberName;
                    li.Value = member.MemberId.ToString();
                    MemberList.Add(li);
                }
                ViewBag.MemberList = MemberList;
                ViewBag.Rooms = context.Rooms.ToList();
                return View();
            }
            else
            {
                return Redirect("/User/Error");
            }
        }

        public ActionResult VisitorIndex()
        {
            if ((string)Session["type"] == "Admin")
            {
                HostelDBContext context = new HostelDBContext();
                var logs = context.VisitorLogs.OrderByDescending(c => c.DateAdded).ToList();
                return View(logs);
            }
            else
            {
                return Redirect("/User/Error");
            }
        }

        public ActionResult AddVisitors()
        {
            if ((string)Session["type"] == "Admin")
            {
                return View();
            }
            else
            {
                return Redirect("/User/Error");
            }
        }

        [HttpPost]
        public ActionResult AddVisitors(VisitorLogs modal)
        {
            if ((string)Session["type"] == "Admin")
            {
                HostelDBContext context = new HostelDBContext();
                context.VisitorLogs.Add(new VisitorLogs
                {
                    VisitorName = modal.VisitorName,
                    VisitorCNIC = modal.VisitorCNIC,
                    VisitorCardNo = modal.VisitorCardNo,
                    StudentName = modal.StudentName,
                    DateAdded = DateTime.Now
                });
                context.SaveChanges();
                return Json(new { }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Redirect("/User/Error");
            }
        }

        public ActionResult AddMessDishes()
        {
            if ((string)Session["type"] == "Admin")
            {
                return View();
            }
            else
            {
                return Redirect("/User/Error");
            }
        }

        [HttpPost]
        public ActionResult AddMessCharges(MessChargesViewModel modal)
        {
            if ((string)Session["type"] == "Admin")
            {
                HostelDBContext context = new HostelDBContext();
                context.MessCharges.Add(new MessCharges
                {
                    MemberId = modal.MemberId,
                    DateAdded = DateTime.Now,
                    Charges = modal.Charges
                });
                context.SaveChanges();
                return Json(new { }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Redirect("/User/Error");
            }
        }

        [HttpPost]
        public ActionResult AddMessDishes(MessDishes modal)
        {
            if ((string)Session["type"] == "Admin")
            {
                HostelDBContext context = new HostelDBContext();
                context.MessDishes.Add(new MessDishes
                {
                    DateAdded = DateTime.Now,
                    DishName = modal.DishName,
                    Price = modal.Price
                });
                context.SaveChanges();
                return Json(new { }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Redirect("/User/Error");
            }
        }

        public ActionResult GetMessCharges(MessCharges charges)
        {
            if ((string)Session["type"] == "Admin")
            {
                HostelDBContext context = new HostelDBContext();
                var messCharges = context.MessCharges.Include("Member").OrderByDescending(c => c.DateAdded).ToList();
                return View(messCharges);
            }
            else
            {
                return Redirect("/User/Error");
            }
        }

        public ActionResult MessDishesIndex()
        {
            if ((string)Session["type"] == "Admin")
            {
                HostelDBContext context = new HostelDBContext();
                var messDishes = context.MessDishes.OrderByDescending(c => c.DateAdded).ToList();
                return View(messDishes);
            }
            else
            {
                return Redirect("/User/Error");
            }
        }

        // GET: Admin
        public ActionResult Index()
        {
            if ((string)Session["type"] == "Admin")
            {
                HostelDBContext context = new HostelDBContext();
                try
                {
                    ViewBag.totalemployee = context.Employees.Count();
                    ViewBag.totalMember = context.Members.Count();
                    ViewBag.categoryId = 0;
                    ViewBag.category = "None";
                    ViewBag.member = 0;
                    ViewBag.balance = 0;
                    int x = 0;
                    int y = 0;
                    float salarycost = 0;
                    float billingcost = 0;
                    float purchasecost = 0;

                    RoomCategory category = null;
                    foreach (RoomCategory c in context.RoomCategories)
                    {
                        HostelDBContext context1 = new HostelDBContext();
                        y = context1.Members.Where(m => m.Room.CategoryId == c.CategoryId).Count();
                        if (y > x)
                        {
                            x = y;
                            category = c;
                        }
                    }
                    salarycost = context.EmployeeSalaries.Sum(s => s.AmmountPaid);
                    billingcost = context.Bills.Sum(b => b.Ammount);
                    purchasecost = context.Purchases.Sum(p => p.Price);
                    float totalcost = salarycost + billingcost + purchasecost;
                    ViewBag.balance = context.MembersPayment.Sum(m => m.Credit) - totalcost;
                    ViewBag.categoryId = category.CategoryId;
                    ViewBag.category = category.CategoryName + "(" + category.RoomType + ")";
                    ViewBag.member = x;
                }
                catch { }

                List<DataPoint> Income = new List<DataPoint>();
                List<DataPoint> Expense = new List<DataPoint>();


                foreach (Building b in context.Buildings)
                {
                    float salary = 0;
                    float billing = 0;
                    float purchase = 0;
                    float income = 0;
                    HostelDBContext context1 = new HostelDBContext();
                    try
                    {
                        salary = context1.EmployeeSalaries.Where(s => s.Employee.BuildingId == b.BuildingId).Sum(s => s.AmmountPaid);
                        billing = context1.Bills.Where(bi => bi.BuildingId == b.BuildingId).Sum(bi => bi.Ammount);
                        purchase = context1.Purchases.Where(p => p.BuildingId == b.BuildingId).Sum(p => p.Price);
                        income = context1.MembersPayment.Where(m => m.BuildingId == b.BuildingId).Sum(m => m.Credit);
                    }
                    catch { }
                    float cost = salary + billing + purchase;
                    Income.Add(new DataPoint(income, b.BuildingName));
                    Expense.Add(new DataPoint(cost, b.BuildingName));
                }
                List<DataPoint> YearlyProfit = new List<DataPoint>{
                    new DataPoint(2800000, "2013" ),
                    new DataPoint(3500000, "2014"),
                    new DataPoint(3800000,"2015" ),
                    new DataPoint(3600000, "2016"),
                    new DataPoint(5000000, "2017"),
                };

                ViewBag.YearlyProfit = JsonConvert.SerializeObject(YearlyProfit);
                ViewBag.Income = JsonConvert.SerializeObject(Income);
                ViewBag.Expense = JsonConvert.SerializeObject(Expense);
                return View();
            }
            else
            {
                return Redirect("/User/Error");
            }
        }

        public ActionResult Report(string id = "", string op = "")
        {
            if ((string)Session["type"] == "Admin")
            {
                HostelDBContext context = new HostelDBContext();
                float totalincome = 0;
                float totalcost = 0;
                List<Array> arraylist = new List<Array>();
                foreach (Building b in context.Buildings)
                {

                    float salary = 0;
                    float billing = 0;
                    float purchase = 0;
                    float income = 0;
                    float profit = 0;
                    HostelDBContext context1 = new HostelDBContext();
                    try
                    {
                        if (id == "" && op == "")
                        {
                            salary = context1.EmployeeSalaries.Where(s => s.Employee.BuildingId == b.BuildingId).Sum(s => s.AmmountPaid);
                            billing = context1.Bills.Where(bi => bi.BuildingId == b.BuildingId).Sum(bi => bi.Ammount);
                            purchase = context1.Purchases.Where(p => p.BuildingId == b.BuildingId).Sum(p => p.Price);
                            income = context1.MembersPayment.Where(m => m.BuildingId == b.BuildingId).Sum(m => m.Credit);
                        }
                        else
                        {
                            DateTime dateto = Convert.ToDateTime(op);
                            DateTime datefrom = Convert.ToDateTime(id);
                            salary = context1.EmployeeSalaries.Where(s => s.Employee.BuildingId == b.BuildingId && s.PayDate >= datefrom && s.PayDate <= dateto).Sum(s => s.AmmountPaid);
                            billing = context1.Bills.Where(bi => bi.BuildingId == b.BuildingId && bi.BilledDate >= datefrom && bi.BilledDate <= dateto).Sum(bi => bi.Ammount);
                            purchase = context1.Purchases.Where(p => p.BuildingId == b.BuildingId && p.PurchaseDate >= datefrom && p.PurchaseDate <= dateto).Sum(p => p.Price);
                            income = context1.MembersPayment.Where(m => m.BuildingId == b.BuildingId && m.Date >= datefrom && m.Date <= dateto).Sum(m => m.Credit);
                        }

                    }
                    catch { }
                    float cost = salary + billing + purchase;
                    profit = income - cost;
                    totalcost += cost;
                    totalincome += income;
                    string[] arr = new string[] { b.BuildingName, income.ToString(), cost.ToString(), profit.ToString() };
                    arraylist.Add(arr);
                }
                ViewBag.totalcost = totalcost;
                ViewBag.totalincome = totalincome;
                ViewBag.totalprofit = totalincome - totalcost;
                ViewBag.list = arraylist;
                return View();
            }
            else
            {
                return Redirect("/User/Error");
            }
        }
    }
}