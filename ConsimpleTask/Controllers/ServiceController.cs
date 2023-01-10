using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

using Core;
using Models;

namespace ConsimpleTestTask.Controllers
{
	public class ServiceController : ActionController<ServiceController>
	{
		private readonly IService _service;

		public ServiceController(ILogger<ServiceController> logger, IService clothesService) : base(logger)
		{
			_service = clothesService;
		}

		[Route("addcustomer")]
		[HttpPost]
		public async Task<IActionResult> AddCustomer([FromBody] CustomerDTO customer)
		{
			return await ExecuteActionWithoutResultAsync(() =>
			{
				return _service.AddCustomerAsync(customer);
			});
		}

		[Route("addproduct")]
		[HttpPost]
		public async Task<IActionResult> AddProduct([FromBody] ProductDTO product)
		{
			return await ExecuteActionWithoutResultAsync(() =>
			{
				return _service.AddProductAsync(product);
			});
		}

		[Route("addorder")]
		[HttpPost]
		public async Task<IActionResult> AddOrder([FromBody] OrderDTO order)
		{
			return await ExecuteActionWithoutResultAsync(() =>
			{
				return _service.AddOrderAsync(order);
			});
		}

		[Route("birthday/{birthday}")]
		[HttpGet]
		public async Task<IActionResult> GetCustomersByBirthday(DateTime birthday)
		{
			return await ExecuteActionAsync(() =>
			{
				return _service.GetCustomersByBirthday(birthday);
			});
		}

		[Route("typecounts/{id}")]
		[HttpGet]
		public async Task<IActionResult> GetTypeCountsFotCustomer(int id)
		{
			return await ExecuteActionAsync(() =>
			{
				return _service.GetTypeCountsFotCustomer(id);
			});
		}

		[Route("lastorders/{days}")]
		[HttpGet]
		public async Task<IActionResult> GetCustomerWhoOrderedLastDays(int days)
		{
			return await ExecuteActionAsync(() =>
			{
				return _service.GetCustomerWhoOrderedLastDays(days);
			});
		}
	}
}
