using Data_Access_Layer;

namespace Business_Logic_Layer
{
    public class RegistrationRecord
    {
        public RegistrationRecordDTO RDTO
        {
            get
            {
                return (new RegistrationRecordDTO(this.RegistrationRecordID, this.RegistrationRecordName));
            }
        }

        public int RegistrationRecordID { get; set; }
        public string RegistrationRecordName { get; set; }

        public RegistrationRecord(RegistrationRecordDTO RDTO)
        {
            this.RegistrationRecordID = RDTO.RegistrationRecordID;
            this.RegistrationRecordName = RDTO.RegistrationRecordName;
        }

        private bool _AddNewRegistrationRecord()
        {
            //call DataAccess Layer 

            this.RegistrationRecordID = RegistrationRecordData.AddRegistrationRecord(this.RDTO);

            return (this.RegistrationRecordID > 0);
        }

        public static List<RegistrationRecordDTO> GetAllRegistrationRecords()
        {
            return RegistrationRecordData.GetAllRegistrationRecords();
        }

        public static RegistrationRecord Find(int ID)
        {

            RegistrationRecordDTO RDTO = RegistrationRecordData.GetRegistrationRecordById(ID);

            if (RDTO != null)
            //we return new object of that student with the right data
            {

                return new RegistrationRecord(RDTO);
            }
            else
                return null;
        }

        public static RegistrationRecord Find(string Name)
        {

            RegistrationRecordDTO RDTO = RegistrationRecordData.GetRegistrationRecordByName(Name);

            if (RDTO != null)
            //we return new object of that student with the right data
            {

                return new RegistrationRecord(RDTO);
            }
            else
                return null;
        }



        public bool Save()
        {

            if (_AddNewRegistrationRecord())
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
