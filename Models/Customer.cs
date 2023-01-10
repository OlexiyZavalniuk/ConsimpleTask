using System;

namespace Models
{
	public class Customer
	{
		public int CustomerId { get; set; }

		public string Name { get; set; }

		public DateTime Birthday { get; set; }

		public DateTime RegistrationDate { get; set; }
	}

	public class CustomerShort
	{
		public int CustomerId { get; set; }

		public string Name { get; set; }
	}

	public class CustomerShortWithDate : CustomerShort
	{
		public DateTime LastOrderTime { get; set; }
	}

	public class CustomerDTO
	{
		public string Name { get; set; }

		public DateTime Birthday { get; set; }
	}
}