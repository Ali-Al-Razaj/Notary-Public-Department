USE NotaryPublicDepartment;

--------------------------------------------------------------------------------------------- SP_AddNewDocumentType

CREATE PROCEDURE SP_AddNewDocumentType
	@Title NVARCHAR(200),
	@Body NVARCHAR(MAX),
    @NewDocumentTypeID INT OUTPUT
AS
BEGIN
    INSERT INTO DocumentsTypes(Title, Body)
    VALUES (@Title, @Body);


    SET @NewDocumentTypeID = SCOPE_IDENTITY();
END
-------------------------------------
DECLARE @NewDocumentTypeID INT;

EXEC SP_AddNewDocumentType
    @Title = 'عامة',
    @Body = 'الهندسة االمدنية هي فن توجيه الموارد الطبيعية والصناعية والقوى الطبيعية التي تمتلكها الأرض لخدمة الانسان.
ومن اهم هذه الموارد الطبيعية هي الماء الهواء ,ومن هنا يأتي دور الهندسة البيئية والتي هي من اقسام الهندسة المدنية التي تعنى بدراسة تلوث كل من الماء والهواء والتربة .
فيما يلي سنهتم بدراسة التمديدات الداخلية للمباني سواء اكانت تمديدات صحية ام صرف صحي ام صرف مطري ...
والتي على الرغم من انها تشكل 10% من تكلفة المبنى إلا انها لا زالت غير محط للأنظار.
إلى اي مدى كان البشر يدركون أهمية الصرف الصحي وتمديدات المياه "او بشكل اعم :التمديدات الصحية" ؟
ان مواقع المياه العذبة الصالحة للشرب شكلت السبب الرئيسي لاختيار مكان إقامة الحضارات القديمة فبدأ البشر حينها بإستخدام ميا الأنهار للشرب والاستحمام .
وبدأ التطور في حفر واستخدام آبار منزلية واستخدامها نظرا لما تحتويه من مخزون مياه جيد قابل للاستخدام والمعالجة من اجل الشرب.
وتطور الامر تدريجيا الى استخدام خزانات لتخزين المياه واستدراجها بواسطة نظام انابيب الى أماكن استهلاكها .
حيث استخدمت الانابيب من الحديد المغلفن في الولايات المتحدة الأميركية منذ أواخر القرن التاسع عشر حتى منتصف القرن العشرين وبعد ذلك ساد استخدام انابيب نحاسية استخدم فيها نحاس طري مع تجهيزات مقوسة ومن ثم النحاس الصلب مع تركيبات (تجهيزات ) ملحومة .
ومن وقت اسبق استخدم اليونان انابيب من الرصاص لمنع سرقة المياه الا ان استخدامها بدأ ينقص بسبب زيادة الوعي بمخاطر التسمم بالرصاص.
حاليا يتم استخدام انابيب PVC  و PPR ونظام انابيب البيكس لما لها من اثار جيدة .
وفي مجال الصرف وجد ان نشأة التمديدات من عصر الحضارات الرومانية والفارسية والصينية جيث طوروا الحمامات واحتاجوا الى مياه شرب فقد ظهرت انابيب السباكة الخزفية ذات الفتحات الواسعة واستخدم الاسفلت لمنع سرقة المياه في حضارة وادي السند ...
ولكن في مجال الصرف كانت مخلفات هذه المياه تصرف الى مصارف مفتوحة ولكن هذه الأساليب قلت بسبب تفشي الامراض ومن هنا بدأت نظام الصرف تحت الأرض وإلغاء الانابيب البلاليع المفتوحة ..
حاليا تنقل النفايات الصلبة الى محطات معالجة بعيدة عن أماكن التجمعات السكنية لما لها من تلوث واوبئة تضر بصحة الانسان حيث يتم تنقية هذه المياه قبل صبها في التيارات المائية .
وأخيرا لما نشهده اليوم من زيادة في الوعي والصحي زاد اهتمام الانسان في ضرورة الاعتناء بأعمال الهندسة الصحية والبيئية وتطوريها بشكل دائم
وهنا تأتي مهمتنا كمهندسين بيئة وصحة عامة بمسؤولية تطوير هذه الأنظمة لأن صحة المواطن وسلامته هي من اولوياتنا وهي ضرورة قصوى لدينا ..
',
    @NewDocumentTypeID = @NewDocumentTypeID OUTPUT;

-- Check the new person ID
SELECT @NewDocumentTypeID AS NewDocumentID;

--------------------------------------------------------------------------------------------- SP_GetAllDocumentTypes

CREATE PROCEDURE SP_GetAllDocumentTypes
AS
BEGIN
    SELECT * FROM DocumentsTypes
END
-------------------------------------
EXEC SP_GetAllDocumentTypes;


--------------------------------------------------------------------------------------------- SP_GetDocumentTypeByID

CREATE PROCEDURE SP_GetDocumentTypeByID
    @DocumentTypeID INT
AS
BEGIN
    SELECT * FROM DocumentsTypes WHERE DocumentTypeID = @DocumentTypeID
END
-------------------------------------
EXEC SP_GetDocumentTypeByID 
		@DocumentTypeID = 1;


--------------------------------------------------------------------------------------------- SP_GetDocumentTypeByTitle

CREATE PROCEDURE SP_GetDocumentTypeByTitle
    @Title NVARCHAR(200)
AS
BEGIN
    SELECT * FROM DocumentsTypes WHERE Title = @Title
END
-------------------------------------
EXEC SP_GetDocumentTypeByTitle 
		@Title = 'عامة';


--------------------------------------------------------------------------------------------- SP_UpdateDocumentType

CREATE PROCEDURE SP_UpdateDocumentType
		@DocumentTypeID INT,
		@Title NVARCHAR(200),
		@Body NVARCHAR(MAX)
AS
BEGIN
    UPDATE DocumentsTypes
    SET Title = @Title, Body = @Body
    WHERE DocumentTypeID = @DocumentTypeID
END
-------------------------------------
EXEC SP_UpdateDocumentType
		@DocumentTypeID = 1,
		@Title = 'عامة',
		@Body = 'معدلة';


--------------------------------------------------------------------------------------------- SP_DeleteDocumentType
CREATE PROCEDURE SP_DeleteDocumentType
    @DocumentTypeID INT
AS
BEGIN
    DELETE FROM DocumentsTypes WHERE DocumentTypeID = @DocumentTypeID

	RETURN @@ROWCOUNT;
END
-------------------------------------
EXEC SP_DeleteDocumentType
	@DocumentTypeID = 2;

--------------------------------------------------------------------------------------------- SP_CheckDocumentTypeExists

CREATE PROCEDURE SP_CheckDocumentTypeExists
    @DocumentTypeID INT
AS
BEGIN
    IF EXISTS(SELECT * FROM DocumentsTypes WHERE DocumentTypeID = @DocumentTypeID)
        RETURN 1;  -- exists
    ELSE
        RETURN 0;  -- does not exist
END
-------------------------------------
DECLARE @IsExists INT;	
EXEC @IsExists = SP_CheckDocumentTypeExists @DocumentTypeID = 1;
IF @IsExists = 1
    PRINT 'exists.';
ELSE
    PRINT 'does not exist.';