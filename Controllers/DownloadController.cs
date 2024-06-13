using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDbECommerce.Services.CategoryServices;

namespace MongoDbECommerce.Controllers
{
    public class DownloadController : Controller
    {
        private readonly ICategoryService _categoryService;
        public DownloadController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> OrdersPdf()
        {
            // 1.ADIM - Veri Çekme --------------------------------------------------------------------------
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("MongoDbECommerce");
            var collection = database.GetCollection<BsonDocument>("Orders");
            var collectionOrderLine = database.GetCollection<BsonDocument>("OrderLines");

            var filter = Builders<BsonDocument>.Filter.Empty;
            var documents = await collection.Find(filter).ToListAsync();
            var documentsOrderLines = await collectionOrderLine.Find(filter).ToListAsync();

            // 2.ADIM - Pdf Oluşturma --------------------------------------------------------------------------
            Document pdfDoc = new Document();
            PdfWriter.GetInstance(pdfDoc, new FileStream("orders.pdf", FileMode.Create));

            pdfDoc.Open();
            foreach (var item in documents)
            {
                // İlişkili tablodan veri alma
                foreach (var documentOrderLine in documentsOrderLines)
                {
                    if (item[0].ToString() == documentOrderLine[4].ToString())
                    {
                        item[5] = documentOrderLine;                       
                    }
                }
                pdfDoc.Add(new Paragraph(item.ToString()));
            }
            pdfDoc.Close();

            // 3.ADIM - İndirme --------------------------------------------------------------------------
            byte[] fileBytes = System.IO.File.ReadAllBytes("orders.pdf");
            var son = File(fileBytes, "application/pdf", "orders.pdf");

            return son;
        }

        public async Task<IActionResult> ProductsXlsx()
        {
            // 1.ADIM - Veri Çekme --------------------------------------------------------------------------
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("MongoDbECommerce");
            var collection = database.GetCollection<BsonDocument>("Products");
            var categories = await _categoryService.GetAllCategoryAsync();

            var filter = Builders<BsonDocument>.Filter.Empty;
            var documents = await collection.Find(filter).ToListAsync();

            // product.category alanına, categoryName bilgileri alınır
            foreach (var item in documents)
            {
                foreach (var category in categories)
                {
                    if (category.CategoryId == item[8])
                    {
                        item[9] = category.CategoryName;
                    }
                }
            }

            // 2.ADIM - Exel Oluşturma --------------------------------------------------------------------------
            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("veriler");

            // başlık satırı
            var titleRow = ws.Row(1);
            titleRow.Cell(1).Value = "ProductName";
            titleRow.Cell(2).Value = "ProductDescription";
            titleRow.Cell(3).Value = "ProductImage";
            titleRow.Cell(4).Value = "ProductPrice";
            titleRow.Cell(5).Value = "ProductStock";
            titleRow.Cell(6).Value = "ProductStatus";
            titleRow.Cell(7).Value = "Category";

            // veriler eklenir
            int row = 2;
            foreach (var item in documents)
            {
                ws.Cell(row, 1).Value = item["ProductName"].AsString;
                ws.Cell(row, 2).Value = item["ProductDescription"].AsString;
                ws.Cell(row, 3).Value = item["ProductImage"].AsString;
                ws.Cell(row, 4).Value = item["ProductPrice"].AsString;
                ws.Cell(row, 5).Value = item["ProductStock"].AsInt32;
                ws.Cell(row, 6).Value = item["ProductStatus"].AsBoolean;
                ws.Cell(row, 7).Value = item["Category"].AsString;
                row++;
            }
            wb.SaveAs("products.xlsx");

            // 3.ADIM - İndirme --------------------------------------------------------------------------
            byte[] fileBytes = System.IO.File.ReadAllBytes("products.xlsx");
            var son = File(fileBytes, "application/xlsx", "products.xlsx");

            return son;
        }
    }
}
