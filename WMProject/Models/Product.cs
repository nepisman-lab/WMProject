using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMProject.Models
{
	public class Product
	{
		public int Id { get; set; }

		public string ProductName { get; set; }

		public string Description { get; set; }

		public string Category { get; set; }

		public string Manufacturer { get; set; }

		public string Supplier { get; set; }

		public decimal Price { get; set; }
	}
}