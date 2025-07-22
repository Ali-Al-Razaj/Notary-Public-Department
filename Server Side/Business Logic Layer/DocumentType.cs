using Data_Access_Layer;

namespace Business_Logic_Layer
{
    public class DocumentType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public DocumentTypeDTO DT_DTO
        {
            get
            {
                return (new DocumentTypeDTO(this.DocumentTypeID, this.Title, this.Body));
            }
        }

        public int DocumentTypeID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }


        public DocumentType(DocumentTypeDTO DT_DTO, enMode cMode = enMode.AddNew)
        {
            this.DocumentTypeID = DT_DTO.DocumentTypeID;
            this.Title = DT_DTO.Title;
            this.Body = DT_DTO.Body;

            Mode = cMode;
        }

        private bool _AddNewDocumentType()
        {
            //call DataAccess Layer 

            this.DocumentTypeID = DocumentTypeData.AddDocumentType(this.DT_DTO);

            return (this.DocumentTypeID > 0);
        }

        private bool _UpdateDocumentType()
        {
            return DocumentTypeData.UpdateDocumentType(this.DT_DTO);
        }

        public static List<DocumentTypeDTO> GetAllDocumentTypes()
        {
            return DocumentTypeData.GetAllDocumentTypes();
        }

        public static DocumentType Find(int ID)
        {

            DocumentTypeDTO DT_DTO = DocumentTypeData.GetDocumentTypeById(ID);

            if (DT_DTO != null)
            //we return new object of that student with the right data
            {

                return new DocumentType(DT_DTO, enMode.Update);
            }
            else
                return null;
        }

        public static DocumentType Find(string Title)
        {

            DocumentTypeDTO DT_DTO = DocumentTypeData.GetDocumentTypeByTitle(Title);

            if (DT_DTO != null)
            //we return new object of that student with the right data
            {

                return new DocumentType(DT_DTO, enMode.Update);
            }
            else
                return null;
        }

        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDocumentType())
                    {

                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateDocumentType();

            }

            return false;
        }

        public static bool DeleteDocumentType(int ID)
        {
            return DocumentTypeData.DeleteDocumentType(ID);
        }

        public static bool CheckDocumentTypeExists(int ID)
        {
            return DocumentTypeData.CheckDocumentTypeExists(ID);
        }

    }
}
