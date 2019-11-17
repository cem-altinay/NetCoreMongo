using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDbTest.Services.Model;
using MongoDbTest.Services.Repository;
using MongoDbTest.Services.UoW;

namespace MongoDbTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _uow;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUnitOfWork uow, IUserRepository userRepository)
        {
            _logger = logger;
            this._userRepository = userRepository;
            this._uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<Users>> Get()
        {

            var users = new Users()
            {
                IsDeleted = false,
                LastName = "Altınay2",
                Name = "Cem2",
                Password = "123456",
                Title = new Title()
                {
                    Name = "Yazılım ve Veritabanı Uzmanı"
                },
                UserName = "cem.altinay"
               
            };
            await _userRepository.Add(users);

       

            // If everything is ok then:
            _uow.Commit();
            //var insertedUser = _userRepository.GetById(users.Id);
            // it will be null
            var testUser = await _userRepository.GetAll();

            return Ok(testUser);
        }
    }
}
