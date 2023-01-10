using System;

namespace Models
{
	public class Order
	{
		public int OrderId { get; set; }

		public DateTime OrderTime { get; set; }

		public int CustomerId { get; set; }

		// "productId1:count ... productIdN:count"
		public string Products { get; set; }

		public double Price { get; set; }
	}

	public class OrderDTO
	{
		public int CustomerId { get; set; }

		public string Products { get; set; }

	}
}
