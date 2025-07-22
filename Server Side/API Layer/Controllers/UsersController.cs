using Business_Logic_Layer;
using Data_Access_Layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("All", Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UserDTO>> GetAllUsers()
        {
            List<UserDTO> List = Business_Logic_Layer.User.GetAllUsers();
            if (List.Count == 0)
            {
                return NotFound("No Users Found!");
            }
            return Ok(List);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("ID/{id}", Name = "GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> GetUserById(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            Business_Logic_Layer.User user = Business_Logic_Layer.User.Find(id);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            UserDTO dto = user.UDTO;
            return Ok(dto);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("IsUserExists/{id}", Name = "IsUserExists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult IsUserExists(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            if (!Business_Logic_Layer.User.CheckUserExists(id))
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok($"User with ID {id} is exists.");
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("CheckLoginCredentials/{username}/{password}", Name = "CheckLoginCredentials")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CheckLoginCredentials(string username, string password)
        {
            if (!Business_Logic_Layer.User.CheckLoginCredentials(username, password))
            {
                return BadRequest($"Wrong username/password");
            }

            return Ok($"User is exists.");
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private (bool, string) IsValidUser(UserDTO user)
        {
            if (user == null)
            {
                return (false, "the object in null.");
            }

            if (string.IsNullOrEmpty(user.Username))
            {
                return (false, "the fields Username is required.");
            }

            if (Business_Logic_Layer.Person.Find(user.PersonID) == null)
            {
                return (false, $"Person with ID: {user.PersonID} not exists.");
            }

            var usersList = Business_Logic_Layer.User.GetAllUsers();
            foreach(var u in usersList)
            {
                if(u.PersonID == user.PersonID)
                {
                    return (false, $"Person with ID: {user.PersonID} already a user.");
                }
            }

            return (true, "");
        }

        [HttpPost(Name = "AddUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDTO> AddUser(UserDTO newUser, string password)
        {
            //we validate the data here
            if (!IsValidUser(newUser).Item1)
            {
                return BadRequest(IsValidUser(newUser).Item2);
            }
            if(string.IsNullOrEmpty(password))
            {
                return BadRequest("Password is requarid.");
            }

            UserDTO DTO = new UserDTO(newUser.UserID, newUser.PersonID, newUser.Username);
            Business_Logic_Layer.User user = new Business_Logic_Layer.User(DTO, password);
            if (user.Save())
            {
                DTO.UserID = user.UserID;
            }

            return CreatedAtRoute("GetUserById", new { id = DTO.UserID }, DTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPut("ChangeUsername/{id}", Name = "ChangeUsername")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> ChangeUsername(int id, string username)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            if(string.IsNullOrEmpty(username))
            {
                return BadRequest($"Not accepted username.");
            }

            Business_Logic_Layer.User user = Business_Logic_Layer.User.Find(id);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            user.Username = username;

            user.Save();

            return Ok(user.UDTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPut("ChangePassword/{id}", Name = "ChangePassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> ChangePassword(int id, string currentPassword, string newPassword)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword))
            {
                return BadRequest($"Not accepted Password.");
            }

            Business_Logic_Layer.User user = Business_Logic_Layer.User.Find(id);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            if(user.Password != currentPassword)
            {
                return BadRequest("Wrong Password.");
            }

            user.Password = newPassword;

            user.Save();

            return Ok(user.UDTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteUser(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            if (Business_Logic_Layer.User.DeleteUser(id))

                return Ok($"User with ID {id} has been deleted.");
            else
                return NotFound($"User with ID {id} not found. no rows deleted!");
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}
