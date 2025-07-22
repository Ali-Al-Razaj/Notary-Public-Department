using Data_Access_Layer;

namespace Business_Logic_Layer
{
    public class User
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public UserDTO UDTO
        {
            get
            {
                return (new UserDTO(this.UserID, this.PersonID, this.Username));
            }
        }


        public int UserID { get; set; }

        public int PersonID { get; set; }
        public Person Person { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }


        public User(UserDTO UDTO, string password, enMode cMode = enMode.AddNew)
        {
            this.UserID = UDTO.UserID;
            this.Username = UDTO.Username;
            this.Password = password;

            this.PersonID = UDTO.PersonID;
            this.Person = Person.Find(UDTO.PersonID);

            Mode = cMode;
        }

        private bool _AddNewUser()
        {
            //call DataAccess Layer 

            this.UserID = UserData.AddUser(this.UDTO, this.Password);

            return (this.UserID > 0);
        }

        private bool _UpdateUser()
        {
            return UserData.UpdateUser(this.UDTO, this.Password);
        }

        public static List<UserDTO> GetAllUsers()
        {
            return UserData.GetAllUser();
        }

        public static User Find(int ID)
        {

            UserDTO UDTO = UserData.GetUserById(ID);

            if (UDTO != null)
            //we return new object of that student with the right data
            {

                return new User(UDTO, UserData.GetPasswordById(ID), enMode.Update);
            }
            else
                return null;
        }

        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {

                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateUser();

            }

            return false;
        }

        public static bool DeleteUser(int ID)
        {
            return UserData.DeleteUser(ID);
        }

        public static bool CheckUserExists(int ID)
        {
            return UserData.CheckUserExists(ID);
        }

        public static bool CheckLoginCredentials(string username, string password)
        {
            return UserData.CheckLoginCredentials(username, password);
        }


    }
}
