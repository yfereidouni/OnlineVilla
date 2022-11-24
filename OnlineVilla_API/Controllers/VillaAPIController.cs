using Microsoft.AspNetCore.Mvc;
using OnlineVilla_API.Models;

namespace OnlineVilla_API.Controllers;

//[Route("api/[controller]")]
[Route("api/VillaAPI")]
[ApiController]
public class VillaAPIController : ControllerBase
{
    [HttpGet]
    public IEnumerable<Villa> GetVillas()
    {
        return new List<Villa>
        {
            new Villa{Id =1, Name = "Pool View"},
            new Villa{Id =2, Name = "Beach View"},
        };
    }
}
