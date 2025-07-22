using Data_Access_Layer;

namespace Business_Logic_Layer
{
    public class Notary
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public NotaryDTO notaryDTO
        {
            get
            {
                return (new NotaryDTO(this.NotaryPublicID, this.Number, this.GovernorateID, this.Name));
            }
        }


        public int NotaryPublicID { get; set; }
        public int Number { get; set; }
        public int GovernorateID { get; set; }
        public Governorate Governorate { get; set; }
        public string Name { get; set; }


        public Notary(NotaryDTO notaryDTO, enMode cMode = enMode.AddNew)
        {
            this.NotaryPublicID = notaryDTO.NotaryPublicID;
            this.Number = notaryDTO.Number;

            this.GovernorateID = notaryDTO.GovernorateID;
            this.Governorate = Governorate.Find(notaryDTO.GovernorateID);

            this.Name = notaryDTO.Name;

            Mode = cMode;
        }

        private bool _AddNewNotary()
        {
            //call DataAccess Layer 

            this.NotaryPublicID = NotaryData.AddNotary(this.notaryDTO);

            return (this.NotaryPublicID > 0);
        }

        private bool _UpdateNotary()
        {
            return NotaryData.UpdateNotary(this.notaryDTO);
        }

        public static List<NotaryDTO> GetAllNotaries()
        {
            return NotaryData.GetAllNotaries();
        }

        public static Notary Find(int ID)
        {

            NotaryDTO NDTO = NotaryData.GetNotaryById(ID);

            if (NDTO != null)
            //we return new object of that student with the right data
            {

                return new Notary(NDTO, enMode.Update);
            }
            else
                return null;
        }

        public static Notary Find(string Name)
        {

            NotaryDTO NDTO = NotaryData.GetNotaryByName(Name);

            if (NDTO != null)
            //we return new object of that student with the right data
            {

                return new Notary(NDTO, enMode.Update);
            }
            else
                return null;
        }

        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (_AddNewNotary())
                    {

                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateNotary();

            }

            return false;
        }

        public static bool DeleteNotary(int ID)
        {
            return NotaryData.DeleteNotary(ID);
        }


    }
}
