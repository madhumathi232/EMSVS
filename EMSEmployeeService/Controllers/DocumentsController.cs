using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EMSEmployeeService.Models;
using System.Configuration;
using Microsoft.AspNetCore.Cors;
using System.Net;
using EMSEmployeeService.Models;

namespace EMSEmployeeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly EmsContext _context;
        private readonly string AppDirectory = @"D:\wwwroot";
        private static List<FileRecord> fileDB = new List<FileRecord>();

        public DocumentsController(EmsContext context)
        {
            _context = context;
            var a=Directory.GetCurrentDirectory();
        }

        // GET: api/Documents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocument()
        {
            return await _context.Documents.ToListAsync();
        }
        [HttpGet("{empid}")]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocuments(int empid)
        {
            return await _context.Documents.Where(x => x.EmpId == empid).ToListAsync();
        }
        [HttpGet]

        [Route("DownloadAction")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            if (!Directory.Exists(AppDirectory))
                Directory.CreateDirectory(AppDirectory);

            //getting file from inmemory obj
            //var file = fileDB?.Where(n => n.Id == id).FirstOrDefault(); 
            //getting file from DB
            var file = _context.Documents.Where(n => n.Id== id).FirstOrDefault();

            var path = Path.Combine(AppDirectory, file?.FilePath);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var contentType = "APPLICATION/octet-stream";
            var fileName = Path.GetFileName(path);

            return File(memory, contentType, fileName);
        }
        // GET: api/Documents/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Document>> GetDocument(int id)
        //{
        //    var document = await _context.Documents.FindAsync(id);

        //    if (document == null)
        //    {
        //        return NotFound();
        //    }

        //    return document;
        //}

        // PUT: api/Documents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocument(int id, Document document)
        {
            if (id != document.Id)
            {
                return BadRequest();
            }

            _context.Entry(document).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Documents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Document>> PostDocument(Document document)
        {
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDocument", new { id = document.Id }, document);
        }
        public class FileModel
        {
            public IFormFile MyFile { get; set; }
            public int EmpId { get; set; }
            //public string AltText { get; set; }
            //public string Description { get; set; }

        }
        public class FileRecord
        {
            public int Id { get; set; }
            public int EmpId { get; set; }
            public string FileName { get; set; }
            public string FileFormat { get; set; }
            public string FilePath { get; set; }
            public string ContentType { get; set; }
            //public string AltText { get; set; }
            //public string Description { get; set; }
        }
        [HttpPost]
        [Route("DocumentPost")]
        [Consumes("multipart/form-data")]
        public async Task<HttpResponseMessage> PostAsync([FromForm] FileModel model)
        {
            try
            {
                FileRecord file = await SaveFileAsync(model.MyFile);

                if (!string.IsNullOrEmpty(file.FilePath))
                {
                    //file.AltText = model.AltText;
                    //file.Description = model.Description;
                    file.EmpId = model.EmpId;
                    fileDB.Add(file);
                    //Save to SQL Server DB
                    SaveToDB(file);
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message),
                };
            }
        }

        private async Task<FileRecord> SaveFileAsync(IFormFile myFile)
        {
            FileRecord file = new FileRecord();
            if (myFile != null)
            {
                if (!Directory.Exists(AppDirectory))
                    Directory.CreateDirectory(AppDirectory);
                //DateTime.Now.Ticks.ToString()
                var fileName = myFile.FileName + Path.GetExtension(myFile.FileName);
                var path = Path.Combine(AppDirectory, fileName);

                file.Id = fileDB.Count() + 1;
                file.FilePath = path;
                file.FileName = fileName;
                file.FileFormat = Path.GetExtension(myFile.FileName);
                file.ContentType = myFile.ContentType;

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await myFile.CopyToAsync(stream);
                }

                return file;
            }
            return file;
        }

        private void SaveToDB(FileRecord record)
        {
            if (record == null)
                throw new ArgumentNullException($"{nameof(record)}");

            Document fileData = new Document();
            fileData.FilePath = record.FilePath;
            fileData.FileName = record.FileName;
            fileData.FileExtension = record.FileFormat;
            fileData.MimeType = record.ContentType;
            fileData.EmpId = record.EmpId;

            _context.Documents.Add(fileData);
            _context.SaveChanges();
        }

        // DELETE: api/Documents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DocumentExists(int id)
        {
            return _context.Documents.Any(e => e.Id == id);
        }
       private bool Documents(int empId)
        {
            return _context.Documents.Any(e => e.EmpId==empId);
        }
    }
}
