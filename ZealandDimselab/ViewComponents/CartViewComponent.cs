using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Helpers;
using ZealandDimselab.Models;

namespace ZealandDimselab.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public CartViewComponent()
        {

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Item> Cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int returnAmount = 0;
            if (Cart != null)
            {
                returnAmount = Cart.Count;
            }
            return View("default", returnAmount);
        }
    }
}
