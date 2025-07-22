using Data_Access_Layer;

namespace Business_Logic_Layer
{
    public class Document
    {

        public DocumentDTO documentDTO
        {
            get
            {
                return (new DocumentDTO(this.DocumentID, this.DocumentTypeID, this.Date, this.NotaryPublicID));
            }
        }


        public int DocumentID { get; set; }
        public int DocumentTypeID { get; set; }
        public DocumentType DocumentType { get; set; }
        public DateTime Date { get; set; }
        public int NotaryPublicID { get; set; }
        public Notary NotaryPublic { get; set; }


        public Document(DocumentDTO documentDTO)
        {
            this.DocumentID = documentDTO.DocumentID;

            this.DocumentTypeID = documentDTO.DocumentTypeID;
            this.DocumentType = DocumentType.Find(documentDTO.DocumentTypeID);

            this.Date = documentDTO.Date;

            this.NotaryPublicID = documentDTO.NotaryPublicID;
            this.NotaryPublic = Notary.Find(documentDTO.NotaryPublicID);
        }

        private bool _AddNewDocument()
        {
            //call DataAccess Layer 

            this.DocumentID = DocumentData.AddDocument(this.documentDTO);

            return (this.DocumentID > 0);
        }

        public bool Save()
        {
            if (_AddNewDocument())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<DocumentDTO> GetAllDocuments()
        {
            return DocumentData.GetAllDocuments();
        }

        public static Document Find(int ID)
        {

            DocumentDTO DDTO = DocumentData.GetDocumentById(ID);

            if (DDTO != null)
            //we return new object of that student with the right data
            {

                return new Document(DDTO);
            }
            else
                return null;
        }

        public static bool DeleteDocument(int ID)
        {
            return DocumentData.DeleteDocument(ID);
        }

        public static bool CheckDocumentExists(int ID)
        {
            return DocumentData.CheckDocumentExists(ID);
        }



    }
}
