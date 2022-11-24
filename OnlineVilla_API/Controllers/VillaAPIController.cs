using Microsoft.AspNetCore.Mvc;
using OnlineVilla_API.Models;

namespace OnlineVilla_API.Controllers;

[ApiController]
public class VillaAPIController : ControllerBase
{
    public IEnumerable<Villa> GetVillas()
    {
        return new List<Villa>
        {
            new Villa{Id =1, Name = "Pool View"},
            new Villa{Id =2, Name = "Beach View"},
        };
    }
}
