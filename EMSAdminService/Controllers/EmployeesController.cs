using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EMSAdminService.Data;
using EMSAdminService.Models;
using System.Net.Mail;
using System.Net;

namespace EMSAdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmsContext _context;

        public EmployeesController(EmsContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            return await _context.Employees.ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }
        [HttpGet]
        [Route("SearchEmployee")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee(string searchString)
        {
            var employee = await _context.Employees.Where(x=>x.EmpId.ToString().Contains(searchString)||x.FirstName.Contains(searchString)||x.LastName.Contains(searchString)).ToListAsync();

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.EmpId)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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
        public class EncryptionDecryption
        {
            public static string EncodePasswordToBase64(string password)
            {
                try
                {
                    byte[] encData_byte = new byte[password.Length];
                    encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                    string encodedData = Convert.ToBase64String(encData_byte);
                    return encodedData;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in base64Encode" + ex.Message);
                }
            }
            public string DecodeFrom64(string encodedData)
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(encodedData);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
        }
        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            var originalPassword = employee.Password;
            employee.Password = EncryptionDecryption.EncodePasswordToBase64(employee.Password);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("9b7fc6347d253a", "398bed26a288b7"),
                EnableSsl = true
            };
            client.Send("emsportal841@gmail.com", employee.Email, "New User Notification", "Hii The new user is"+" "+employee.Email+" "+"has beed created."+" "+"The Password for the user is"+"  "+"["+ originalPassword+"]");
            return CreatedAtAction("GetEmployee", new { id = employee.EmpId }, employee);
        }

        public class UserValidationRequestModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
            [HttpPut]
        [Route("ForgotPassword")]
        public async Task<IActionResult> PutEmployeePassword(UserValidationRequestModel user)
        {
           
            var employee = _context.Employees.Where(x => x.Email == user.Email).FirstOrDefault();
            employee.Password = EncryptionDecryption.EncodePasswordToBase64(user.Password);
            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
              
            }

            return NoContent();
        }
        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmpId == id);
        }
    }
}
