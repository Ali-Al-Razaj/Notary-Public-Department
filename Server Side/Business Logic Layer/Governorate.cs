using Data_Access_Layer;
using static Business_Logic_Layer.Person;

namespace Business_Logic_Layer
{
    public class Governorate
    {

        public GovernorateDTO GDTO
        {
            get
            {
                return (new GovernorateDTO(this.GovernorateID, this.GovernorateName));
            }
        }


        public int GovernorateID { get; set; }
        public string GovernorateName { get; set; }

        public Governorate(GovernorateDTO gDTO)
        {
            this.GovernorateID = gDTO.GovernorateID;
            this.GovernorateName = gDTO.GovernorateName;
        }

        public static List<GovernorateDTO> GetAllGovernorates()
        {
            return GovernorateData.GetAllGovernorates();
        }

        public static Governorate Find(int ID)
        {

            GovernorateDTO GDTO = GovernorateData.GetGovernorateById(ID);

            if (GDTO != null)
            //we return new object of that student with the right data
            {

                return new Governorate(GDTO);
            }
            else
                return null;
        }

        public static Governorate Find(string Name)
        {

            GovernorateDTO GDTO = GovernorateData.GetGovernorateByName(Name);

            if (GDTO != null)
            //we return new object of that student with the right data
            {

                return new Governorate(GDTO);
            }
            else
                return null;
        }


    }
}
