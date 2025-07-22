using Data_Access_Layer;
using static Business_Logic_Layer.Person;

namespace Business_Logic_Layer
{
    public class RegistrationAuthority
    {

        public RegistrationAuthorityDTO RDTO
        {
            get
            {
                return (new RegistrationAuthorityDTO(this.RegistrationAuthorityID, this.RegistrationAuthorityName));
            }
        }

        public int RegistrationAuthorityID { get; set; }
        public string RegistrationAuthorityName { get; set; }

        public RegistrationAuthority(RegistrationAuthorityDTO RDTO)
        {
            this.RegistrationAuthorityID = RDTO.RegistrationAuthorityID;
            this.RegistrationAuthorityName = RDTO.RegistrationAuthorityName;
        }

        private bool _AddNewRegistrationAuthority()
        {
            //call DataAccess Layer 

            this.RegistrationAuthorityID = RegistrationAuthorityData.AddRegistrationAuthority(this.RDTO);

            return (this.RegistrationAuthorityID > 0);
        }

        public static List<RegistrationAuthorityDTO> GetAllRegistrationAuthorities()
        {
            return RegistrationAuthorityData.GetAllRegistrationAuthorities();
        }

        public static RegistrationAuthority Find(int ID)
        {

            RegistrationAuthorityDTO RDTO = RegistrationAuthorityData.GetRegistrationAuthorityById(ID);

            if (RDTO != null)
            //we return new object of that student with the right data
            {

                return new RegistrationAuthority(RDTO);
            }
            else
                return null;
        }

        public static RegistrationAuthority Find(string Name)
        {

            RegistrationAuthorityDTO RDTO = RegistrationAuthorityData.GetRegistrationAuthorityByName(Name);

            if (RDTO != null)
            //we return new object of that student with the right data
            {

                return new RegistrationAuthority(RDTO);
            }
            else
                return null;
        }

        public bool Save()
        {
            
            if (_AddNewRegistrationAuthority())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
