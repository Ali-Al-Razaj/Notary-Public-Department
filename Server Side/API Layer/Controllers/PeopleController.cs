using Business_Logic_Layer;
using Data_Access_Layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/People")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("All", Name = "GetAllPeople")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PersonDTO>> GetAllPeople()
        {
            List<PersonDTO> PeopleList = Business_Logic_Layer.Person.GetAllPeople();
            if (PeopleList.Count == 0)
            {
                return NotFound("No People Found!");
            }
            return Ok(PeopleList);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("ID/{id}", Name = "GetPersonById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PersonDTO> GetPersonById(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            Business_Logic_Layer.Person person = Business_Logic_Layer.Person.Find(id);

            if (person == null)
            {
                return NotFound($"Person with ID {id} not found.");
            }

            PersonDTO dto = person.personDTO;
            return Ok(dto);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("NationalNumber/{NationalNumber}", Name = "GetPersonByNationalNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PersonDTO> GetPersonByNationalNumber(string NationalNumber)
        {
            Business_Logic_Layer.Person person = Business_Logic_Layer.Person.Find(NationalNumber);

            if (person == null)
            {
                return NotFound($"Person with National_Number {NationalNumber} not found.");
            }

            PersonDTO dto = person.personDTO;
            return Ok(dto);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("IsPersonExists/{id}", Name = "IsPersonExists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult IsPersonExists(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            if (!Business_Logic_Layer.Person.CheckPersonExists(id))
            {
                return NotFound($"Person with ID {id} not found.");
            }

            return Ok($"Person with ID {id} is exists.");
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        private bool IsAtLeast18(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            // Adjust if birthday hasn't occurred yet this year
            if (birthDate.Date > today.AddYears(-age)) age--;

            return age >= 18;
        }

        private (bool, string) IsValidPerson(PersonDTO Person)
        {
            if(Person == null)
            {
                return (false, "the object in null.");
            }

            if(string.IsNullOrEmpty(Person.NationalNumber) || string.IsNullOrEmpty(Person.FirstName) || string.IsNullOrEmpty(Person.LastName))
            {
                return (false, "the fields NationalNumber, FirstName, LastName are required.");
            }

            if(Person.BirthPlaceID != null)
            {
                int id = Person.BirthPlaceID.Value;
                if(Governorate.Find(id) == null)
                {
                    return (false, $"Governorate with id: {id} not exists.");
                }
            }

            if (Person.RegistrationAuthorityID != null)
            {
                int id = Person.RegistrationAuthorityID.Value;
                if (RegistrationAuthority.Find(id) == null)
                {
                    return (false, $"RegistrationAuthority with id: {id} not exists.");
                }
            }

            if (Person.RegistrationRecordID != null)
            {
                int id = Person.RegistrationRecordID.Value;
                if (RegistrationRecord.Find(id) == null)
                {
                    return (false, $"RegistrationRecord with id: {id} not exists.");
                }
            }

            if(Person.DateOfBirth != null)
            {
                DateTime date = Person.DateOfBirth.Value;
                if (!IsAtLeast18(date))
                {
                    return (false, $"Age must be 18 or older.");
                }
            }

            if(Business_Logic_Layer.Person.Find(Person.NationalNumber) != null)
            {
                return (false, $"National Number: {Person.NationalNumber} already exists for another person.");
            }

            return (true, "");
        }

        [HttpPost(Name = "AddPerson")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PersonDTO> AddPerson(PersonDTO newPerson)
        {
            //we validate the data here
            if (!IsValidPerson(newPerson).Item1)
            {
                return BadRequest(IsValidPerson(newPerson).Item2);
            }

            PersonDTO DTO = new PersonDTO(newPerson.PersonID, newPerson.NationalNumber, newPerson.FirstName, newPerson.LastName, newPerson.FatherName,
                                            newPerson.MotherName, newPerson.BirthPlaceID, newPerson.DateOfBirth, newPerson.RegistrationAuthorityID,
                                            newPerson.RegistrationRecordID, newPerson.Gender, newPerson.Adress, newPerson.Phone,
                                            newPerson.GrantHistory, newPerson.CardNumber);
            Business_Logic_Layer.Person person = new Business_Logic_Layer.Person(DTO);
            if(person.Save())
            {
                DTO.PersonID = person.PersonID;

            }

            return CreatedAtRoute("GetPersonById", new { id = DTO.PersonID }, DTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPut("{id}", Name = "UpdatePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PersonDTO> UpdatePerson(int id, PersonDTO updatedPerson)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            Business_Logic_Layer.Person person = Business_Logic_Layer.Person.Find(id);

            if (person == null)
            {
                return NotFound($"Person with ID {id} not found.");
            }

            if (!IsValidPerson(updatedPerson).Item1)
            {
                if(IsValidPerson(updatedPerson).Item2 == $"National Number: {updatedPerson.NationalNumber} already exists for another person.")
                {
                    if(!(person.NationalNumber == updatedPerson.NationalNumber))
                    {
                        return BadRequest(IsValidPerson(updatedPerson).Item2);
                    }
                }
                else
                {
                    return BadRequest(IsValidPerson(updatedPerson).Item2);
                }
            }

            person.NationalNumber = updatedPerson.NationalNumber;
            person.FirstName = updatedPerson.FirstName;
            person.LastName = updatedPerson.LastName;
            person.FatherName = updatedPerson.FatherName;
            person.MotherName = updatedPerson.MotherName;

            person.BirthPlaceID = updatedPerson.BirthPlaceID;
            if(updatedPerson.BirthPlaceID != null)
            {
                int birthPlaceID = updatedPerson.BirthPlaceID.Value;
                person.BirthPlace = Business_Logic_Layer.Governorate.Find(birthPlaceID);
            }
            else
            {
                person.BirthPlace = null;
            }

            person.DateOfBirth = updatedPerson.DateOfBirth;

            person.RegistrationAuthorityID = updatedPerson.RegistrationAuthorityID;
            if (updatedPerson.RegistrationAuthorityID != null)
            {
                int registrationAuthorityID = updatedPerson.RegistrationAuthorityID.Value;
                person.RegistrationAuthority = Business_Logic_Layer.RegistrationAuthority.Find(registrationAuthorityID);
            }
            else
            {
                person.RegistrationAuthority = null;
            }

            person.RegistrationRecordID = updatedPerson.RegistrationRecordID;
            if (updatedPerson.RegistrationRecordID != null)
            {
                int registrationRecordID = updatedPerson.RegistrationRecordID.Value;
                person.RegistrationRecord = Business_Logic_Layer.RegistrationRecord.Find(registrationRecordID);
            }
            else
            {
                person.RegistrationRecord = null;
            }

            person.Gender = updatedPerson.Gender;
            person.Adress = updatedPerson.Adress;
            person.Phone = updatedPerson.Phone;
            person.GrantHistory = updatedPerson.GrantHistory;
            person.CardNumber = updatedPerson.CardNumber;

            person.Save();
            
            return Ok(person.personDTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}", Name = "DeletePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeletePerson(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            if (Business_Logic_Layer.Person.DeletePerson(id))

                return Ok($"Person with ID {id} has been deleted.");
            else
                return NotFound($"Person with ID {id} not found. no rows deleted!");
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
    }
}
