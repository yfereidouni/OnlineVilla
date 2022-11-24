using Microsoft.AspNetCore.Mvc;
using OnlineVilla_API.Data;
using OnlineVilla_API.Models;
using OnlineVilla_API.Models.Dto;

namespace OnlineVilla_API.Controllers;

//[Route("api/[controller]")]
[Route("api/VillaAPI")]
[ApiController]
public class VillaAPIController : ControllerBase
{
    [HttpGet]
    public IEnumerable<VillaDTO> GetVillas()
    {
        return VillaStore.villaList;
    }

    [HttpGet("{id:int}")]
    public VillaDTO GetVilla(int id)
    {
        return VillaStore.villaList.FirstOrDefault(c => c.Id == id);
    }

}
