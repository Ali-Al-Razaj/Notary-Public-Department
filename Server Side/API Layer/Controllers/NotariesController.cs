using Business_Logic_Layer;
using Data_Access_Layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/Notaries")]
    [ApiController]
    public class NotariesController : ControllerBase
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("All", Name = "GetAllNotaries")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<NotaryDTO>> GetAllNotaries()
        {
            List<NotaryDTO> List = Business_Logic_Layer.Notary.GetAllNotaries();
            if (List.Count == 0)
            {
                return NotFound("No Notaries Found!");
            }
            return Ok(List);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("ID/{id}", Name = "GetNotaryById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<NotaryDTO> GetNotaryById(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            Business_Logic_Layer.Notary notary = Business_Logic_Layer.Notary.Find(id);

            if (notary == null)
            {
                return NotFound($"Notary with ID {id} not found.");
            }

            NotaryDTO dto = notary.notaryDTO;
            return Ok(dto);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("Name/{name}", Name = "GetNotaryByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<NotaryDTO> GetNotaryByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest($"Not accepted Name");
            }

            Business_Logic_Layer.Notary notary = Business_Logic_Layer.Notary.Find(name);

            if (notary == null)
            {
                return NotFound($"Notary with name {name} not found.");
            }

            NotaryDTO dto = notary.notaryDTO;
            return Ok(dto);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private (bool, string) IsValidNotary(NotaryDTO notary)
        {
            if (notary == null)
            {
                return (false, "the object in null.");
            }

            if (string.IsNullOrEmpty(notary.Name))
            {
                return (false, "the field Name is required.");
            }

            if (Business_Logic_Layer.Governorate.Find(notary.GovernorateID) == null)
            {
                return (false, $"Governorate with id: {notary.GovernorateID} not found.");
            }

            return (true, "");
        }

        [HttpPost(Name = "AddNotary")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<NotaryDTO> AddNotary(NotaryDTO newNotary)
        {
            //we validate the data here
            if (!IsValidNotary(newNotary).Item1)
            {
                return BadRequest(IsValidNotary(newNotary).Item2);
            }

            NotaryDTO DTO = new NotaryDTO(newNotary.NotaryPublicID, newNotary.Number, newNotary.GovernorateID, newNotary.Name);
            Business_Logic_Layer.Notary notary = new Business_Logic_Layer.Notary(DTO);
            if (notary.Save())
            {
                DTO.NotaryPublicID = notary.NotaryPublicID;

            }

            return CreatedAtRoute("GetNotaryById", new { id = DTO.NotaryPublicID }, DTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPut("{id}", Name = "UpdateNotary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<NotaryDTO> UpdateNotary(int id, NotaryDTO updatedNotary)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            Business_Logic_Layer.Notary notary = Business_Logic_Layer.Notary.Find(id);

            if (notary == null)
            {
                return NotFound($"Notary with ID {id} not found.");
            }

            if (!IsValidNotary(updatedNotary).Item1)
            {
               
                return BadRequest(IsValidNotary(updatedNotary).Item2);
                
            }

            notary.Number = updatedNotary.Number;
            notary.GovernorateID = updatedNotary.GovernorateID;
            notary.Governorate = Business_Logic_Layer.Governorate.Find(updatedNotary.GovernorateID);
            notary.Name = updatedNotary.Name;


            notary.Save();

            return Ok(notary.notaryDTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}", Name = "DeleteNotary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteNotary(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            if (Business_Logic_Layer.Notary.DeleteNotary(id))

                return Ok($"Notary with ID {id} has been deleted.");
            else
                return NotFound($"Notary with ID {id} not found. no rows deleted!");
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}
