using Data_Access_Layer;

namespace Business_Logic_Layer
{
    public class Person
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public PersonDTO personDTO
        {
            get
            {
                return (new PersonDTO(this.PersonID, this.NationalNumber, this.FirstName, this.LastName, this.FatherName,
                                        this.MotherName, this.BirthPlaceID, this.DateOfBirth, this.RegistrationAuthorityID,
                                        this.RegistrationRecordID, this.Gender, this.Adress, this.Phone, this.GrantHistory, this.CardNumber));
            }
        }

        public int PersonID { get; set; }
        public string NationalNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public int? BirthPlaceID { get; set; }
        public Governorate? BirthPlace { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? RegistrationAuthorityID { get; set; }
        public RegistrationAuthority? RegistrationAuthority { get; set; }
        public int? RegistrationRecordID { get; set; }
        public RegistrationRecord? RegistrationRecord { get; set; }
        public bool Gender { get; set; }
        public string? Adress { get; set; }
        public string? Phone { get; set; }
        public DateTime? GrantHistory { get; set; }
        public string? CardNumber { get; set; }


        public Person(PersonDTO personDTO, enMode cMode = enMode.AddNew)
        {
            this.PersonID = personDTO.PersonID;
            this.NationalNumber = personDTO.NationalNumber;
            this.FirstName = personDTO.FirstName;
            this.LastName = personDTO.LastName;
            this.FatherName = personDTO.FatherName;
            this.MotherName = personDTO.MotherName;

            this.BirthPlaceID = personDTO.BirthPlaceID;
            if(personDTO.BirthPlaceID != null)
            {
                int id = personDTO.BirthPlaceID.Value;
                this.BirthPlace = Governorate.Find(id);
            }
            else
            {
                this.BirthPlace = null;
            }

            this.DateOfBirth = personDTO.DateOfBirth;

            this.RegistrationAuthorityID = personDTO.RegistrationAuthorityID;
            if (personDTO.RegistrationAuthorityID != null)
            {
                int id = personDTO.RegistrationAuthorityID.Value;
                this.RegistrationAuthority = RegistrationAuthority.Find(id);
            }
            else
            {
                this.RegistrationAuthority = null;
            }

            this.RegistrationRecordID = personDTO.RegistrationRecordID;
            if (personDTO.RegistrationRecordID != null)
            {
                int id = personDTO.RegistrationRecordID.Value;
                this.RegistrationRecord = RegistrationRecord.Find(id);
            }
            else
            {
                this.RegistrationRecord = null;
            }

            this.Gender = personDTO.Gender;
            this.Adress = personDTO.Adress;
            this.Phone = personDTO.Phone;
            this.GrantHistory = personDTO.GrantHistory;
            this.CardNumber = personDTO.CardNumber;

            Mode = cMode;
        }

        private bool _AddNewPerson()
        {
            //call DataAccess Layer 

            this.PersonID = PersonData.AddPerson(this.personDTO);

            return (this.PersonID > 0);
        }

        private bool _UpdatePerson()
        {
            return PersonData.UpdatePerson(this.personDTO);
        }

        public static List<PersonDTO> GetAllPeople()
        {
            return PersonData.GetAllPeople();
        }

        public static Person Find(int ID)
        {

            PersonDTO PDTO = PersonData.GetPersontById(ID);

            if (PDTO != null)
            //we return new object of that student with the right data
            {

                return new Person(PDTO, enMode.Update);
            }
            else
                return null;
        }

        public static Person Find(string NationalNumber)
        {

            PersonDTO PDTO = PersonData.GetPersontByNationalNumber(NationalNumber);

            if (PDTO != null)
            //we return new object of that student with the right data
            {

                return new Person(PDTO, enMode.Update);
            }
            else
                return null;
        }

        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {

                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdatePerson();

            }

            return false;
        }

        public static bool DeletePerson(int ID)
        {
            return PersonData.DeletePerson(ID);
        }

        public static bool CheckPersonExists(int ID)
        {
            return PersonData.CheckPersonExists(ID);
        }

    }
}
