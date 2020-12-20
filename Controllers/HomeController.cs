using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace fota.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {

        private ApplicationContext _context;
        public static string ADMIN_PASSWORD = "15304560";

        public HomeController(ApplicationContext context)
        {
            _context = context;
        }
        private async Task<Consumer> GetConsumerByUserName(string userName) => _context.Consumers.FirstOrDefault(f => f.UserName == userName);

        private async Task<Consumer> GetConsumer(string userName, string apiKey) => _context.Consumers.FirstOrDefault(f => f.UserName == userName && f.Password == apiKey);

        //tested
        [HttpGet("~/api/hasUpdate/{userName}/{apiKey}")]
        public async Task<IActionResult> HasUpdate([FromRoute]string userName, [FromRoute]string apiKey)
        {
            var u = await GetConsumer(userName, apiKey);
            if (!IsActive(u)) return BadRequest("Please renew your licesnce !");
            if (u == null) return BadRequest("you are not allowed to access this api ,please get credential from our company.");
            var l = await GetLastFileData();
            return Ok(_context.Readings.Where(d => d.ConsumerId == u.Id).Any(c => c.FileDataId == l.Id) ? "n" : "y");
        }

        [HttpGet("~/api/CountNewFileLines/{userName}/{apiKey}")]
        public async Task<IActionResult> CountNewFileLines([FromRoute]string userName, [FromRoute]string apiKey)
        {
            var u = await GetConsumer(userName, apiKey);
            if (!IsActive(u)) return BadRequest("Please renew your licesnce !");
            if (u == null) return BadRequest("you are not allowed to access this api ,please get credential from our company.");
            var l = await GetLastFileData();
            return Ok(l.LineCount);
        }

        private async Task<FileData> GetLastFileData()
        {
            var data = await _context.FileDatas.ToListAsync();
            return data.LastOrDefault();
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }


        //tested

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm]IFormFile file)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var result = Regex.Split(await reader.ReadToEndAsync(), "\r\n|\r|\n").ToList();
                result = result.Where(x => ! String.IsNullOrWhiteSpace(x)).ToList();
                var lineCounter = 0;
                bool found = false;//found *
                await _context.FileDatas.AddAsync(new FileData
                {
                    Lines = result.Select(c =>
                    {
                        if (!found && c.Equals("*"))
                            found = true;
                        return new Line
                        {
                            Data = c,
                            LineNum = ++lineCounter,
                            Type = found ? 1 : 0
                        };
                    }).Where(z => !z.Data.Equals("*")&&!string.IsNullOrWhiteSpace(z.Data)).ToList(),
                    UploadingDate = DateTime.Now,
                    LineCount = result.Count - 1
                });
                await _context.SaveChangesAsync();
                ViewData["Size"] = result.Count;
                return View("UploadedSuccess");
            }
        }

        private string GenerateKey()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNabcdegfyuxcOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [HttpPost("~/api/purchase/{userName}")]
        public async Task<IActionResult> Purchase([FromRoute]string userName,[FromHeader]string adminPassword,[FromQuery]int months,[FromQuery]int days)
        {
            if (!adminPassword.Equals(ADMIN_PASSWORD))
            {
                return BadRequest("yoy are not the admin !");
            }
            var user = await  GetConsumerByUserName(userName);
            if (user == null) return BadRequest("No userName found!");
            if(user.ActiveTill<DateTime.UtcNow)
                user.ActiveTill=user.ActiveTill.AddMonths(months).AddDays(days);
            user.ActiveTill = DateTime.UtcNow.AddMonths(months).AddDays(days);
            await  _context.SaveChangesAsync();
            return Ok("Done!");
        }

        private bool  IsActive(Consumer consumer)
        {
            return consumer.ActiveTill >= DateTime.UtcNow;
        }

        [HttpGet("~/api/Consumers")]
        public async Task<IActionResult> Consumers()
        {
            var last = await GetLastFileData();
            var data = _context.Consumers.Include(c => c.Readings)
                .Select(c => new UserVersions
                {
                    UserId=c.Id,
                    UserName=c.UserName,
                    VersionsCount=c.Readings.Count(),
                    ReadLastVersion=c.Readings.Any(c=>c.FileDataId==last.Id)
                });
            return Ok(data);
       }
    

        [HttpPost("~/api/AddConsumer/{userName}/{adminPassword}")]
        public async Task<IActionResult> AddConsumer([FromRoute]string userName, [FromRoute]string adminPassword)
        {
            if (!adminPassword.Equals(ADMIN_PASSWORD))
            {
                return BadRequest();
            }
            var user = new Consumer
            {
                Password = GenerateKey(),
                UpdateCount = 0,
                UserName = userName
            };
            _context.Consumers.Add(user);
            try
            {
                if (await _context.SaveChangesAsync() > 0)
                    return Ok(user);
                return BadRequest("user is not added!");
            }
            catch (Exception e)
            {
                return BadRequest($"it seems username is repeated :{e.Message}");
            }

        }

        private async Task<bool> Reader(Consumer c,FileData fileData)=>await
             _context.Readings.AnyAsync(x => x.FileDataId == fileData.Id && x.ConsumerId == c.Id);

        [HttpGet("~/api/receive/{type}/{packageNumber}/{packageSize}/{userName}/{apiKey}")]
        public async Task<IActionResult> RecieveCode([FromRoute]int type, [FromRoute]int packageNumber,[FromRoute]int packageSize ,[FromRoute]string userName, [FromRoute]string apiKey)
        {
            var user = await GetConsumer(userName, apiKey);
            if (!IsActive(user)) return BadRequest("Please renew your licesnce !");
            if (user == null) return BadRequest("you are not allowed to access this api ,please get credential from our company.");
            var l = await GetLastFileData();
            var list2 = _context.Lines
                .Where(x => x.FileDataId == l.Id &&x.Type==type)
                .ToList();

            var list = list2.Skip((packageNumber - 1) * packageSize)
                .Take(packageSize);
           
            if ( ! await Reader(user,l)&&list2.Count <= (packageNumber) * packageSize)
            {
                _context.Readings.Add(new Reading
                {
                    FileDataId = l.Id,
                    ConsumerId = user.Id,
                    ReadingDateTime = DateTime.Now
                });
                await _context.SaveChangesAsync();
            }
            return Ok(list.Select(l => l.Data));
        }

    }

}