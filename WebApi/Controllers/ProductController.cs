
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        public ProductController(ApplicationDbContext db) 
        {
            this.db = db;     
        }

        [HttpPost]
        [Route("AddProduct")]
        public IActionResult AddProduct(Product p)
        {
            db.products.Add(p);
            db.SaveChanges();
            return Ok("Product Added Succesfully");
        }

        [HttpGet]
        [Route("/GetAllProduct")]
        public IActionResult GetAllProduct()
        {
           var data= db.products.ToList();
            return Ok(data);    
        }

        [HttpDelete]
        [Route("DeleteProd/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var data = db.products.Find(id);
            db.products.Remove(data);
            db.SaveChanges();   
            return Ok("Product deleted successfully");
        }
        [HttpPut]
        [Route("UpdateProd")]
        public IActionResult UpdateProduct(Product p)
        {
            if (p == null || p.Id <= 0)
            {
                return BadRequest("Invalid product data.");
            }

            var existingProduct = db.products.Find(p.Id);
            if (existingProduct == null)
            {
                return NotFound("Product not found.");
            }

            existingProduct.Pname = p.Pname;
            existingProduct.Price = p.Price;
            existingProduct.Pcat = p.Pcat;
            existingProduct.ImagePath = p.ImagePath;
            // Update other properties as needed

            db.products.Update(existingProduct);
            db.SaveChanges();

            return Ok("Product updated successfully");
        }




        //[HttpPut]
        //[Route("UpdateProd")]
        //public IActionResult UpdateProduct(Product p)
        //{
        //    //db.Entry(p).State = EntityState.Modified;
        //    db.products.Update(p);
        //    db.SaveChanges();
        //    return Ok("Products Added Successfully");


        //}

        //[HttpPut]
        //[Route("UpdateProd/{id}")]
        //public IActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
        //{
        //    if (updatedProduct == null || updatedProduct.Id != id)
        //    {
        //        return BadRequest("Product ID mismatch");
        //    }

        //    var existingProduct = db.products.Find(id);
        //    if (existingProduct == null)
        //    {
        //        return NotFound("Product not found");
        //    }

        //    existingProduct.Pname = updatedProduct.Pname;
        //    existingProduct.Price = updatedProduct.Price;
        //    existingProduct.Pcat = updatedProduct.Pcat;
        //    // Update other properties as needed

        //    db.products.Update(existingProduct);
        //    db.SaveChanges();

        //    return Ok("Product updated successfully");
        //}



        [HttpGet]
            [Route("GetProdByName/{name}")]
            public IActionResult GetProductByName(string name)
            {
                var product = db.products.FirstOrDefault(p => p.Pname == name);
                if (product == null)
                {
                    return NotFound("Product not found");
                }

                return Ok(product);
            }

        [HttpGet]
        [Route("GetTopFiveProductsByPrice")]
        public IActionResult GetTopFiveProductsByPrice()
        {
            var topFiveProducts = db.products
                                    .OrderByDescending(p => p.Price)
                                    .Take(5)
                                    .ToList();

            return Ok(topFiveProducts);
        }

        [HttpDelete]
        [Route("DeleteMultipleProducts")]
        public IActionResult DeleteProducts([FromBody] List<int> productIds)
        {
            if (productIds == null || !productIds.Any())
            {
                return BadRequest("No product IDs provided");
            }

            var productsToDelete = db.products.Where(p => productIds.Contains(p.Id)).ToList();

            if (!productsToDelete.Any())
            {
                return NotFound("No products found for the provided IDs");
            }

            db.products.RemoveRange(productsToDelete);
            db.SaveChanges();

            return Ok("Products deleted successfully");
        }



        [HttpGet]
        [Route("GetProdById/{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = db.products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound("Product not found");
            }

            return Ok(product);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "uploads", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { filePath = path });
        }

        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var path = Path.Combine("uploads", fileName);

            if (!System.IO.File.Exists(path))
                return NotFound("File not found.");

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, GetContentType(path), fileName);
        }

        private string GetContentType(string path)
        {
            var types = new Dictionary<string, string>
        {
            { ".txt", "text/plain" },
            { ".pdf", "application/pdf" },
            { ".doc", "application/vnd.ms-word" },
            { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
            { ".xls", "application/vnd.ms-excel" },
            { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
            { ".png", "image/png" },
            { ".jpg", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".gif", "image/gif" },
            { ".csv", "text/csv" }
        };

            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types.ContainsKey(ext) ? types[ext] : "application/octet-stream";
        }
    }
}

