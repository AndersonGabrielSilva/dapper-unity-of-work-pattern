using System;
using System.Collections.Generic;
using DapperUowTests.Data;
using Microsoft.AspNetCore.Mvc;

namespace DapperUowTests.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {

        [HttpGet("")]
        public IEnumerable<NotificationModel> Get(
            [FromServices] NotificationRepository repository)
        {
            return repository.Get();
        }

        [HttpPost("")]
        public IActionResult Post(
            [FromServices] IUnitOfWork unitOfWork,
            [FromServices] NotificationRepository repository)
        {
            // Como o DbSession está como Scoped a instancia da conexão é unica.
            // por isto que este begin transacion segura a conexão do repository
            unitOfWork.BeginTransaction();
            repository.Save(new NotificationModel
            {
                Date = DateTime.Now,
                Id = Guid.NewGuid(),
                Title = "Teste",
                Url = "Teste"
            });
            unitOfWork.Commit();
            return Ok();
        }
    }
}
