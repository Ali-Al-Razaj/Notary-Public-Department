using Data_Access_Layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/DocumentTypes")]
    [ApiController]
    public class DocumentTypesController : ControllerBase
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("All", Name = "GetAllDocumentTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<DocumentTypeDTO>> GetAllDocumentTypes()
        {
            List<DocumentTypeDTO> List = Business_Logic_Layer.DocumentType.GetAllDocumentTypes();
            if (List.Count == 0)
            {
                return NotFound("No Document Types Found!");
            }
            return Ok(List);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        [HttpGet("ID/{id}", Name = "GetDocumentTypeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DocumentTypeDTO> GetDocumentTypeById(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            Business_Logic_Layer.DocumentType documentType = Business_Logic_Layer.DocumentType.Find(id);

            if (documentType == null)
            {
                return NotFound($"Document Type with ID {id} not found.");
            }

            DocumentTypeDTO dto = documentType.DT_DTO;
            return Ok(dto);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("Title/{title}", Name = "GetDocumentTypeByTitle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DocumentTypeDTO> GetDocumentTypeByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest($"Not accepted title.");
            }

            Business_Logic_Layer.DocumentType documentType = Business_Logic_Layer.DocumentType.Find(title);

            if (documentType == null)
            {
                return NotFound($"Document Type with title {title} not found.");
            }

            DocumentTypeDTO dto = documentType.DT_DTO;
            return Ok(dto);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("IsDocumentTypeExists/{id}", Name = "IsDocumentTypeExists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult IsDocumentTypeExists(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            if (!Business_Logic_Layer.DocumentType.CheckDocumentTypeExists(id))
            {
                return NotFound($"Document Type with ID {id} not found.");
            }

            return Ok($"Document Type with ID {id} is exists.");
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost(Name = "AddDocumentType")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DocumentTypeDTO> AddDocumentType(DocumentTypeDTO newDocumentType)
        {
            //we validate the data here
            if (newDocumentType == null || string.IsNullOrEmpty(newDocumentType.Title) || string.IsNullOrEmpty(newDocumentType.Body))
            {
                return BadRequest("Invalid Data.");
            }

            DocumentTypeDTO DTO = new DocumentTypeDTO(newDocumentType.DocumentTypeID, newDocumentType.Title, newDocumentType.Body);
            Business_Logic_Layer.DocumentType documentType = new Business_Logic_Layer.DocumentType(DTO);
            if (documentType.Save())
            {
                DTO.DocumentTypeID = documentType.DocumentTypeID;

            }

            return CreatedAtRoute("GetDocumentTypeById", new { id = DTO.DocumentTypeID }, DTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPut("{id}", Name = "UpdateDocumentType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DocumentTypeDTO> UpdateDocumentType(int id, DocumentTypeDTO updatedDocumentType)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            if(string.IsNullOrEmpty(updatedDocumentType.Title) || string.IsNullOrEmpty(updatedDocumentType.Body))
            {
                return BadRequest("Invalid Data.");
            }

            Business_Logic_Layer.DocumentType documentType = Business_Logic_Layer.DocumentType.Find(id);

            if (documentType == null)
            {
                return NotFound($"Document Type with ID {id} not found.");
            }


            documentType.Title = updatedDocumentType.Title;
            documentType.Body = updatedDocumentType.Body;


            documentType.Save();

            return Ok(documentType.DT_DTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}", Name = "DeleteDocumentType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteDocumentType(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            if (Business_Logic_Layer.DocumentType.DeleteDocumentType(id))

                return Ok($"Document Type with ID {id} has been deleted.");
            else
                return NotFound($"Document Type with ID {id} not found. no rows deleted!");
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////


    }
}
