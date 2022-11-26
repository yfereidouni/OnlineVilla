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
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<VillaDTO>> GetVillas()
    {
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
        if (id==0)
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
}
