using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WMProject.Models;
using WMProject.Models.Services;

namespace WMProject.Controllers
{
    public class ProductsController : Controller
    {
        private WMProjectContext db = new WMProjectContext();
		private JsonServices jsonServices = new JsonServices();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

		// GET: JsonProducts
		public ActionResult JsonIndex()
		{
			var products = jsonServices.Deserialize();
						   
			return View(products);
		}

		// GET: Products/Create
		public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
		//Adds new product in both database and in jsonFile.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductName,Description,Category,Manufacturer,Supplier,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();

				var products = jsonServices.Deserialize();
				products.Add(product);

				var jsonString = jsonServices.Serialize(products);
				jsonServices.AddDataToJsonFile(jsonString);

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductName,Description,Category,Manufacturer,Supplier,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
			
				return RedirectToAction("Index");
            }
            return View(product);
        }

		// GET: Products/JsonEdit/5
		public ActionResult JsonEdit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var product = jsonServices.Deserialize().FirstOrDefault(p => p.Id == id);
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(product);
		}

		// POST: Products/JsonEdit/5       
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult JsonEdit([Bind(Include = "Id,ProductName,Description,Category,Manufacturer,Supplier,Price")] Product product)
		{
			if (ModelState.IsValid)
			{
				var products = jsonServices.Deserialize();
				products.Remove(products.FirstOrDefault(p => p.Id == product.Id));
				products.Insert(product.Id - 1, product);

				var jsonString = jsonServices.Serialize(products);
				jsonServices.AddDataToJsonFile(jsonString);

				return RedirectToAction("JsonIndex");
			}
			return View(product);
		}

		protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
