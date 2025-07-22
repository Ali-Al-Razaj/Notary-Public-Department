using Business_Logic_Layer;
using Data_Access_Layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/Records")]
    [ApiController]
    public class RecordsController : ControllerBase
    {

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("All", Name = "GetAllRecords")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<DocumentPersonRecordDTO>> GetAllRecords()
        {
            List<DocumentPersonRecordDTO> List = Business_Logic_Layer.DocumentPersonRecord.GetAllRecords();
            if (List.Count == 0)
            {
                return NotFound("No Records Found!");
            }
            return Ok(List);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("ID/{id}", Name = "GetRecordById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DocumentPersonRecordDTO> GetRecordById(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            Business_Logic_Layer.DocumentPersonRecord r = Business_Logic_Layer.DocumentPersonRecord.Find(id);

            if (r == null)
            {
                return NotFound($"Record with ID {id} not found.");
            }

            DocumentPersonRecordDTO dto = r.DP_DTO;
            return Ok(dto);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private (bool, string) IsValidRecord(DocumentPersonRecordDTO record)
        {
            if (record == null)
            {
                return (false, "the object in null.");
            }

            if (Business_Logic_Layer.Document.Find(record.DocumentID) == null)
            {
                return (false, $"Document with id: {record.DocumentID} not exists.");
            }

            if (Business_Logic_Layer.Person.Find(record.PersonID) == null)
            {
                return (false, $"Person with id: {record.PersonID} not exists.");
            }

            return (true, "");
        }

        [HttpPost(Name = "AddRecord")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DocumentPersonRecordDTO> AddRecord(DocumentPersonRecordDTO newRecord)
        {
            //we validate the data here
            if (!IsValidRecord(newRecord).Item1)
            {
                return BadRequest(IsValidRecord(newRecord).Item2);
            }

            DocumentPersonRecordDTO DTO = new DocumentPersonRecordDTO(newRecord.RecordID, newRecord.DocumentID, newRecord.PersonID, newRecord.PersonRole);
            Business_Logic_Layer.DocumentPersonRecord record = new Business_Logic_Layer.DocumentPersonRecord(DTO);
            if (record.Save())
            {
                DTO.RecordID = record.RecordID;

            }

            return CreatedAtRoute("GetRecordById", new { id = DTO.RecordID }, DTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}", Name = "DeleteRecord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteRecord(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            if (Business_Logic_Layer.DocumentPersonRecord.DeleteRecord(id))

                return Ok($"Record with ID {id} has been deleted.");
            else
                return NotFound($"Record with ID {id} not found. no rows deleted!");
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////




    }
}
