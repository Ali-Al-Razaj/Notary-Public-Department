using Data_Access_Layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/RegistrationAuthorities")]
    [ApiController]
    public class RegistrationAuthoritiesController : ControllerBase
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("All", Name = "GetAllRegistrationAuthorities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<RegistrationAuthorityDTO>> GetAllRegistrationAuthorities()
        {
            List<RegistrationAuthorityDTO> List = Business_Logic_Layer.RegistrationAuthority.GetAllRegistrationAuthorities();
            if (List.Count == 0)
            {
                return NotFound("No Registration Authorities Found!");
            }
            return Ok(List);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("ID/{id}", Name = "GetRegistrationAuthorityById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RegistrationAuthorityDTO> GetRegistrationAuthorityById(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            Business_Logic_Layer.RegistrationAuthority registrationAuthority = Business_Logic_Layer.RegistrationAuthority.Find(id);

            if (registrationAuthority == null)
            {
                return NotFound($"Registration Authority with ID {id} not found.");
            }

            RegistrationAuthorityDTO dto = registrationAuthority.RDTO;
            return Ok(dto);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("Name/{Name}", Name = "GetRegistrationAuthorityByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RegistrationAuthorityDTO> GetRegistrationAuthorityByName(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return BadRequest($"Not accepted Name");
            }

            Business_Logic_Layer.RegistrationAuthority registrationAuthority = Business_Logic_Layer.RegistrationAuthority.Find(Name);

            if (registrationAuthority == null)
            {
                return NotFound($"Registration Authority with Name {Name} not found.");
            }

            RegistrationAuthorityDTO dto = registrationAuthority.RDTO;
            return Ok(dto);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost("{Name}", Name = "AddRegistrationAuthority")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<RegistrationAuthorityDTO> AddRegistrationAuthority(string Name)
        {
            //we validate the data here
            if (string.IsNullOrEmpty(Name))
            {
                return BadRequest("Not accepted Name.");
            }

            RegistrationAuthorityDTO DTO = new RegistrationAuthorityDTO(0, Name);
            Business_Logic_Layer.RegistrationAuthority registrationAuthority = new Business_Logic_Layer.RegistrationAuthority(DTO);
            if (registrationAuthority.Save())
            {
                DTO.RegistrationAuthorityID = registrationAuthority.RegistrationAuthorityID;

            }

            return CreatedAtRoute("GetRegistrationAuthorityById", new { id = DTO.RegistrationAuthorityID }, DTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        

    }
}
