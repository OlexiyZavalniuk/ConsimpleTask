using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Models;

namespace Core
{
	public interface IService
	{
		Task AddCustomerAsync(CustomerDTO customer);

		Task AddProductAsync(ProductDTO product);

		Task AddOrderAsync(OrderDTO order);

		Task<IEnumerable<CustomerShort>> GetCustomersByBirthday(DateTime birthday);

		Task<IEnumerable<CustomerShortWithDate>> GetCustomerWhoOrderedLastDays(int days);

		Task<IEnumerable<TypeCount>> GetTypeCountsFotCustomer(int customerId);

	}
}
