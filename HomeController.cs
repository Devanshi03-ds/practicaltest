using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mono.TextTemplating;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly practicaltestContext _context;
        public HomeController(ILogger<HomeController> logger, practicaltestContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            var data = _context.Users.ToList();
            return View(data);
        }
        [HttpGet]
        public IActionResult CreateUser()
        {
            VMUser user = new VMUser();
            user.DDLState = _context.States.ToList();
            user.DDLCity = _context.Cities.ToList();
            var states = new List<string> { "Gujarat", "Maharashtra" };
            var stateOptions = new SelectList(states);
            ViewBag.States = stateOptions;
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(VMUser user)
        {
            try
            {
                var Count = _context.Users.Where(x => x.Name == user.Name).ToList();
                if (Count.Count != 0)
                {
                    ViewBag.error = "User Already Exists";
                    return View(user);
                }
                else
                {
                    User dri = new Models.User();
                    dri.Name = user.Name;
                    dri.Phone = user.Phone;
                    dri.Email = user.Email;
                    dri.Address = user.Address;
                    dri.StateId = user.StateId;
                    dri.CityId = user.CityId;

                    await _context.Users.AddAsync(dri);
                    await _context.SaveChangesAsync();
                    ViewBag.Message = "Data Inserted Successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                return View(user);
            }

            //_context.Users.Add(user);
            //_context.SaveChanges();
            //ViewBag.message = "";
            //return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditUser(int Id)
        {
            if (Id == 0)
            {
                return RedirectToAction("Index");
            }
            var getEmpdetails = await _context.Users.FindAsync(Id);

            VMUser driver1 = new VMUser();
            // VMDriver vMDriver = new VMDriver();
            driver1.Name = getEmpdetails.Name;
            driver1.Phone = getEmpdetails.Phone;
            driver1.Email = getEmpdetails.Email;
            driver1.Address = getEmpdetails.Address;
            driver1.StateId = getEmpdetails.StateId;
            driver1.CityId = getEmpdetails.CityId;
            driver1.DDLState = _context.States.ToList();
            driver1.DDLCity = _context.Cities.ToList();

            return View(driver1);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(VMUser user)
        {
            var student = _context.Users.Where(s => s.Name == user.Name).FirstOrDefault();
            if (student == null)
            {
            }
            else
            {
                student.Name = user.Name;
                student.Phone = user.Phone;
                student.Email = user.Email;
                student.Address = user.Address;
                student.CityId = user.CityId;
                student.StateId = user.StateId;

                _context.Users.Update(student);
                await _context.SaveChangesAsync();
                ViewBag.Message = "Data Updated Successfully";
            }

           
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                User driver = new User { UserId = id };
                _context.Users.Remove(driver);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        public JsonResult GetCities(string state)
        {
            var cities = new List<string>();
            switch (state)
            {
                case "Gujarat":
                    cities.Add("Surat");
                    cities.Add("Bardoli");
                    cities.Add("Baroda");
                    break;
                case "Maharashtra":
                    cities.Add("Mumbai");
                    cities.Add("Pune");
                    break;
            }
            //Add JsonRequest behavior to allow retrieving states over http get  
            return Json(cities);
        }
        //[HttpPost]
        //public JsonResult AjaxMethod(string type, int value)
        //{
        //    VMUser model = new VMUser();
        //    switch (type)
        //    {
        //        case "ddlCountries":
        //            model.States = (from User in this._context.States
        //                            where User.StateId == value
        //                            select new SelectListItem
        //                            {
        //                                Value = User.StateId.ToString(),
        //                                Text = User.StateName
        //                            }).ToList();
        //            break;
        //        case "ddlStates":
        //            model.Cities = (from User in this._context.Cities
        //                            where User.CityId == value
        //                            select new SelectListItem
        //                            {
        //                                Value = User.CityId.ToString(),
        //                                Text = User.CityName
        //                            }).ToList();
        //            break;
        //    }
        //    return Json(model);
        //}

        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
