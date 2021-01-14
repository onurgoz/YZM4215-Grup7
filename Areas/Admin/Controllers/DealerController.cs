using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YZM4215_Grup7.ApiServices.Interfaces;
using YZM4215_Grup7.CustomFilters;
using YZM4215_Grup7.Models;

namespace YZM4215_Grup7.Areas.Admin.Controllers
{
    [Area("Admin")]
    [JwtAuthorize(Roles = "Admin")]
    public class DealerController : Controller
    {
        private readonly IDealerService _dealerService;
        public DealerController(IDealerService dealerService)
        {
            _dealerService = dealerService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _dealerService.GetAllDealersAsync();
            if (data == null)
            {
                return View();
            }
            else
            {
                return View(data);
            }
        }


        public IActionResult AddDealer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDealer(DealerAddModel model)
        {
            if (ModelState.IsValid)
            {
	            model.Address = model.Address + "  -" + model.City + "/" + model.District;
                await _dealerService.AddDealerAsync(model);
                return RedirectToAction("Index", "Dealer");
            }
            else
            {
                return View(model);
            }
        }


        public async Task<IActionResult> UpdateDealer(int id)
        {
	        var dealer = await _dealerService.GetDealerByIdAsync(id);

	        string address = dealer.Address.Substring(0, dealer.Address.LastIndexOf("-"));
	        string district = dealer.Address.Split("/").Last();
	        Regex r = new Regex(@"-(.+?)/");
	        MatchCollection mc = r.Matches(dealer.Address);
	        string city = mc[0].Groups[1].Value;


	        return View(new DealerListModel
	        {
		        Id = dealer.Id,
		        Name = dealer.Name,
		        Address = address,
		        Email = dealer.Email,
		        PhoneNumber = dealer.PhoneNumber,
		        City = city,
		        District = district,
		        AppUserId = dealer.AppUserId
	        });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDealer(DealerListModel model){
            if (ModelState.IsValid)
            {
	            model.Address = model.Address + "  -" + model.City + "/" + model.District;
                await _dealerService.UpdateDealerAsync(model);
                return RedirectToAction("Index","Dealer");
            }
            else{
                return View(model);
            }
        }

        public async Task<IActionResult> DeleteDealer(int id){
            await _dealerService.DeleteDealerAsync(id);
            return RedirectToAction("Index","Dealer");
        }
    }
}