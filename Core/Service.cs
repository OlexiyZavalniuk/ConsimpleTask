using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Models;
using Database;
using System.Xml.Linq;


namespace Core
{
	public class Service : IService
	{
		private readonly ApplicationContext _db;

		public Service(ApplicationContext appContext)
		{
			_db = appContext;
		}

		public async Task AddCustomerAsync(CustomerDTO customer)
		{
			var toAdd = new Customer()
			{
				Birthday = customer.Birthday,
				Name = customer.Name,
				RegistrationDate = DateTime.Now,
			};

			_db.Customers.Add(toAdd);
			await _db.SaveChangesAsync();
		}

		public async Task AddProductAsync(ProductDTO product)
		{
			var toAdd = new Product()
			{
				Name = product.Name,
				ProductType = product.ProductType,
				Price = product.Price,
				Code = product.Code,
			};

			_db.Products.Add(toAdd);
			await _db.SaveChangesAsync();
		}

		public async Task AddOrderAsync(OrderDTO order)
		{
			var price = 0.0;
			var products = order.Products.Split(' ');
			foreach (var product in products)
			{
				var prd = product.Split(':');
				var productObj = await _db.Products.FirstAsync(p => p.ProductId == int.Parse(prd[0]));
				price += productObj.Price * int.Parse(prd[1]);
			}

			var toAdd = new Order()
			{
				OrderTime = DateTime.Now,
				CustomerId = order.CustomerId,
				Products = order.Products,
				Price = price,
			};

			_db.Orders.Add(toAdd);
			await _db.SaveChangesAsync();
		}


		public async Task<IEnumerable<CustomerShort>> GetCustomersByBirthday(DateTime birthday)
		{
			var result = new List<CustomerShort>();
			var customers = await _db.Customers.Where(c => c.Birthday == birthday).ToListAsync();

			foreach (var customer in customers)
			{
				result.Add(new CustomerShort
				{
					CustomerId = customer.CustomerId,
					Name = customer.Name
				});
			}

			return result;
		}

		public async Task<IEnumerable<CustomerShortWithDate>> GetCustomerWhoOrderedLastDays(int days)
		{
			var result = new List<CustomerShortWithDate>();
			var orders = await _db.Orders.Where(o => o.OrderTime.AddDays(days) > DateTime.Now)
				// 
				// doesn't work, I don't know why :(
				//
				//.OrderByDescending(o => o.OrderTime)
				//.GroupBy(o => o.CustomerId)
				//.Select(o => o.FirstOrDefault())
				.ToListAsync();

			foreach (var order in orders)
			{
				var customer = await _db.Customers.FirstAsync(c => c.CustomerId == order.CustomerId);
				result.Add(new CustomerShortWithDate
				{
					CustomerId = order.CustomerId,
					Name = customer.Name,
					LastOrderTime = order.OrderTime
				});
			}

			return result;
		}

		public async Task<IEnumerable<TypeCount>> GetTypeCountsFotCustomer(int customerId)
		{
			var result = new List<TypeCount>();
			var orders = await _db.Orders.Where(o => o.CustomerId == customerId).ToListAsync();

			var counts = new int[Enum.GetNames(typeof(ProductType)).Length];
			
			foreach (var order in orders)
			{
				var products = order.Products.Split(' ');
				foreach (var product in products)
				{
					var pr = product.Split(':');
					var productObj = await _db.Products.FirstAsync(p => p.ProductId == int.Parse(pr[0]));
					counts[(int)productObj.ProductType] += int.Parse(pr[1]);
				}
			}

			for (var i = 0; i < counts.Length; i++)
			{
				result.Add(new TypeCount
				{
					Type = (ProductType)i,
					Count = counts[i]
				});
			}

			return result;
		}

	}
}