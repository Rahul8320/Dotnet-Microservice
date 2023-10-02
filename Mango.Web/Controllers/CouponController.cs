﻿using Mango.Web.Models;
using Mango.Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Mango.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? list = new();

            ResponseDto? response = await _couponService.GetAllCouponsAsync();

            if(response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result)!);
            }
            return View(list);
        }

        [HttpGet]
        public IActionResult CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(AddCouponDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _couponService.CreateCouponAsync(model);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CouponDelete(Guid couponId)
        {
			ResponseDto? response = await _couponService.GetCouponByIdAsync(couponId);

			if (response != null && response.IsSuccess)
			{
				CouponDto? model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result)!);
                return View(model);
			}
			return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto model)
        {
            
            ResponseDto? response = await _couponService.DeleteCouponAsync(model.CouponId);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CouponIndex));
            }
            
            return View(model);
        }
    }
}