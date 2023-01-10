
namespace Models
{
	public class Product : ProductDTO
	{
		public int ProductId { get; set; }


	}

	public class ProductDTO
	{
		public string Name { get; set; }

		public ProductType ProductType { get; set; }

		public string Code { get; set; }

		public double Price { get; set; }
	}

	public enum ProductType
	{
		type1,
		type2,
		type3
	}

	public class TypeCount
	{
		public ProductType Type { get; set; }
		public int Count { get; set; }
	}
}
