using Data_Access_Layer;

namespace Business_Logic_Layer
{
    public class DocumentPersonRecord
    {

        public DocumentPersonRecordDTO DP_DTO
        {
            get
            {
                return (new DocumentPersonRecordDTO(this.RecordID, this.DocumentID, this.PersonID, this.PersonRole));
            }
        }

        public int RecordID { get; set; }
        public int DocumentID { get; set; }
        public Document Document { get; set; }
        public int PersonID { get; set; }
        public Person Person { get; set; }
        public bool PersonRole { get; set; }

        public DocumentPersonRecord(DocumentPersonRecordDTO dpDTO)
        {
            this.RecordID = dpDTO.RecordID;
            this.DocumentID = dpDTO.DocumentID;
            this.Document = Document.Find(dpDTO.DocumentID);
            this.PersonID = dpDTO.PersonID;
            this.Person = Person.Find(dpDTO.PersonID);
            this.PersonRole = dpDTO.PersonRole;
        }

        private bool _AddNewRecord()
        {
            //call DataAccess Layer 

            this.RecordID = DocumentPersonRecordData.AddRecord(this.DP_DTO);

            return (this.RecordID > 0);
        }

        public bool Save()
        {
            if (_AddNewRecord())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<DocumentPersonRecordDTO> GetAllRecords()
        {
            return DocumentPersonRecordData.GetAllRecords();
        }

        public static DocumentPersonRecord Find(int ID)
        {

            DocumentPersonRecordDTO DTO = DocumentPersonRecordData.GetRecordById(ID);

            if (DTO != null)
            //we return new object of that student with the right data
            {

                return new DocumentPersonRecord(DTO);
            }
            else
                return null;
        }

        public static bool DeleteRecord(int ID)
        {
            return DocumentPersonRecordData.DeleteRecord(ID);
        }



    }
}
