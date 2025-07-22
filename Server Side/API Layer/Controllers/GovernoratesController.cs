using Data_Access_Layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/Governorates")]
    [ApiController]
    public class GovernoratesController : ControllerBase
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("All", Name = "GetAllGovernorates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<GovernorateDTO>> GetAllGovernorates()
        {
            List<GovernorateDTO> List = Business_Logic_Layer.Governorate.GetAllGovernorates();
            if (List.Count == 0)
            {
                return NotFound("No Governorates Found!");
            }
            return Ok(List);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("ID/{id}", Name = "GetGovernorateById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<GovernorateDTO> GetGovernorateById(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            Business_Logic_Layer.Governorate governorate = Business_Logic_Layer.Governorate.Find(id);

            if (governorate == null)
            {
                return NotFound($"Governorate with ID {id} not found.");
            }

            GovernorateDTO dto = governorate.GDTO;
            return Ok(dto);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("Name/{Name}", Name = "GetGovernorateByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<GovernorateDTO> GetGovernorateByName(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return BadRequest($"Not accepted Name.");
            }

            Business_Logic_Layer.Governorate governorate = Business_Logic_Layer.Governorate.Find(Name);

            if (governorate == null)
            {
                return NotFound($"Governorate with Name {Name} not found.");
            }

            GovernorateDTO dto = governorate.GDTO;
            return Ok(dto);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////



    }
}
