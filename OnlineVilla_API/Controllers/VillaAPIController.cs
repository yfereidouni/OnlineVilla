using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using OnlineVilla_API.Data;
using OnlineVilla_API.Models;
using OnlineVilla_API.Models.Dto;

namespace OnlineVilla_API.Controllers;

//[Route("api/[controller]")]
[Route("api/VillaAPI")]
[ApiController]
public class VillaAPIController : ControllerBase
{
    private readonly ILogger<VillaAPIController> _logger;

    public VillaAPIController(ILogger<VillaAPIController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<VillaDTO>> GetVillas()
    {
        _logger.LogInformation("Getting all Villas!");
        return Ok(VillaStore.villaList);
    }

    [HttpGet("{id:int}", Name = "GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    //[ProducesResponseType(200, Type = typeof(VillaDTO))]
    //[ProducesResponseType(400)]
    //[ProducesResponseType(404)]
    public ActionResult<VillaDTO> GetVilla(int id)
    {
        if (id == 0)
        {
            _logger.LogInformation($"Get Villa Error with Id: {id}");
            return BadRequest();
        }

        var villa = VillaStore.villaList.FirstOrDefault(c => c.Id == id);
        if (villa == null)
        {
            return NotFound();
        }

        return Ok(villa);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
    {
        ///It not works because model validation is checked by [ApiController]
        //if (!ModelState.IsValid)
        //{
        //    return BadRequest(ModelState);
        //}

        ///IF VillaName was not unique
        if (VillaStore.villaList.FirstOrDefault(c => c.Name.ToLower() == villaDTO.Name.ToLower()) != null)
        {
            ModelState.AddModelError("CustomeError", "Villa already Exsist!");
            return BadRequest(ModelState);
        }

        if (villaDTO == null)
        {
            return BadRequest(villaDTO);
        }
        if (villaDTO.Id > 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        villaDTO.Id = VillaStore.villaList.OrderByDescending(c => c.Id).FirstOrDefault().Id + 1;
        VillaStore.villaList.Add(villaDTO);

        //return Ok(villaDTO);
        return CreatedAtRoute("GetVilla", new { villaDTO.Id }, villaDTO);
    }

    [HttpDelete("{id:int}", Name = "DeleteVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }

        var villa = VillaStore.villaList.FirstOrDefault(c => c.Id == id);
        if (villa == null)
        {
            return NotFound();
        }

        VillaStore.villaList.Remove(villa);
        return NoContent();
    }

    [HttpPut("{id:int}", Name = "UpdateVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
    {
        if (villaDTO == null || id != villaDTO.Id)
        {
            return BadRequest();
        }

        var villa = VillaStore.villaList.FirstOrDefault(c => c.Id == id);
        villa.Name = villaDTO.Name;
        villa.Sqft = villaDTO.Sqft;
        villa.Occupancy = villaDTO.Occupancy;

        return NoContent();

    }

    [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
    {
        if (patchDTO == null || id == 0)
        {
            return BadRequest();
        }

        var villa = VillaStore.villaList.FirstOrDefault(c => c.Id == id);
        
        if (villa == null)
        {
            return NotFound();
        }

        patchDTO.ApplyTo(villa, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return NoContent();
    }

}
