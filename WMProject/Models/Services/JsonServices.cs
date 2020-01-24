using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.IO;

namespace WMProject.Models.Services
{
	public class JsonServices
	{
		private readonly string jsonFilePath = @"C:\Users\Dejan\source\repos\GitHub\WMProject\WMProject\Content\JsonFiles\ProductJSON.json";

		public string GetJsonString()
			=> File.ReadAllText(jsonFilePath);

		public void AddDataToJsonFile(string jsonString)
			=> File.WriteAllText(jsonFilePath, jsonString);

		public string Serialize(List<Product> products)
			=> JsonConvert.SerializeObject(products, Formatting.Indented);

		public List<Product> Deserialize()
			=> JsonConvert.DeserializeObject<List<Product>>(GetJsonString());
	}
}