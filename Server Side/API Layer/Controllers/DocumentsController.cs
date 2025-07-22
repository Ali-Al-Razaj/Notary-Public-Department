using Business_Logic_Layer;
using Data_Access_Layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/Documents")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("All", Name = "GetAllDocuments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<DocumentDTO>> GetAllDocuments()
        {
            List<DocumentDTO> List = Business_Logic_Layer.Document.GetAllDocuments();
            if (List.Count == 0)
            {
                return NotFound("No Documents Found!");
            }
            return Ok(List);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("ID/{id}", Name = "GetDocumentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DocumentDTO> GetDocumentById(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            Business_Logic_Layer.Document d = Business_Logic_Layer.Document.Find(id);

            if (d == null)
            {
                return NotFound($"Document with ID {id} not found.");
            }

            DocumentDTO dto = d.documentDTO;
            return Ok(dto);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("IsDocumentExists/{id}", Name = "IsDocumentExists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult IsDocumentExists(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            if (!Business_Logic_Layer.Document.CheckDocumentExists(id))
            {
                return NotFound($"Document with ID {id} not found.");
            }

            return Ok($"Document with ID {id} is exists.");
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private (bool, string) IsValidDocument(DocumentDTO document)
        {
            if (document == null)
            {
                return (false, "the object in null.");
            }

            if (Business_Logic_Layer.DocumentType.Find(document.DocumentTypeID) == null)
            {
                return (false, $"Document Type Not Found.");
            }

            if (Business_Logic_Layer.Notary.Find(document.NotaryPublicID) == null)
            {
                return (false, $"Notary Public Not Found.");
            }

            if(document.Date.Date < DateTime.Now.Date)
            {
                return (false, $"Not Accepted Date.");
            }

            return (true, "");
        }

        [HttpPost(Name = "AddDocument")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DocumentDTO> AddDocument(DocumentDTO newDocument)
        {
            //we validate the data here
            if (!IsValidDocument(newDocument).Item1)
            {
                return BadRequest(IsValidDocument(newDocument).Item2);
            }

            DocumentDTO DTO = new DocumentDTO(newDocument.DocumentID, newDocument.DocumentTypeID, newDocument.Date, newDocument.NotaryPublicID);
            Business_Logic_Layer.Document d = new Business_Logic_Layer.Document(DTO);
            if (d.Save())
            {
                DTO.DocumentID = d.DocumentID;

            }

            return CreatedAtRoute("GetDocumentById", new { id = DTO.DocumentID }, DTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}", Name = "DeleteDocument")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteDocument(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            if (Business_Logic_Layer.Document.DeleteDocument(id))

                return Ok($"Document with ID {id} has been deleted.");
            else
                return NotFound($"Document with ID {id} not found. no rows deleted!");
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}
