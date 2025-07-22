using Data_Access_Layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/RegistrationRecords")]
    [ApiController]
    public class RegistrationRecordsController : ControllerBase
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("All", Name = "GetAllRegistrationRecords")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<RegistrationRecordDTO>> GetAllRegistrationRecords()
        {
            List<RegistrationRecordDTO> List = Business_Logic_Layer.RegistrationRecord.GetAllRegistrationRecords();
            if (List.Count == 0)
            {
                return NotFound("No Registration Records Found!");
            }
            return Ok(List);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("ID/{id}", Name = "GetRegistrationRecordById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RegistrationRecordDTO> GetRegistrationRecordById(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            Business_Logic_Layer.RegistrationRecord registrationRecord = Business_Logic_Layer.RegistrationRecord.Find(id);

            if (registrationRecord == null)
            {
                return NotFound($"Registration Record with ID {id} not found.");
            }

            RegistrationRecordDTO dto = registrationRecord.RDTO;
            return Ok(dto);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("Name/{Name}", Name = "GetRegistrationRecordByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RegistrationRecordDTO> GetRegistrationRecordByName(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return BadRequest($"Not accepted Name.");
            }

            Business_Logic_Layer.RegistrationRecord registrationRecord = Business_Logic_Layer.RegistrationRecord.Find(Name);

            if (registrationRecord == null)
            {
                return NotFound($"Registration Record with Name {Name} not found.");
            }

            RegistrationRecordDTO dto = registrationRecord.RDTO;
            return Ok(dto);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost("{Name}", Name = "AddRegistrationRecord")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<RegistrationRecordDTO> AddRegistrationRecord(string Name)
        {
            //we validate the data here
            if (string.IsNullOrEmpty(Name))
            {
                return BadRequest("Not accepted Name.");
            }

            RegistrationRecordDTO DTO = new RegistrationRecordDTO(0, Name);
            Business_Logic_Layer.RegistrationRecord registrationRecord = new Business_Logic_Layer.RegistrationRecord(DTO);
            if (registrationRecord.Save())
            {
                DTO.RegistrationRecordID = registrationRecord.RegistrationRecordID;

            }

            return CreatedAtRoute("GetRegistrationRecordById", new { id = DTO.RegistrationRecordID }, DTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}
