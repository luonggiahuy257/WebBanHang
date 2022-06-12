
--------------------UserProfile-----------
CREATE TABLE UserProfile(
    NetId nvarchar(128),
    FirstName nvarchar(64),
    LastName nvarchar(64),
    Picture nvarchar(256),
    DescriptionUser nvarchar(500),
    PhoneNumber nvarchar(20),
    MobileNumber nvarchar(20),
    Email nvarchar(128),
    AddressInfo nvarchar(256),
    AddressExt nvarchar(256),
    ZipPostalCode nvarchar(20),
    PasswordHash nvarchar(32),
    TaxCode nvarchar(32),
    Admin int,
    Vendor int,
    Locked bit,
    Hidden bit,
    LastDateLogin datetime,
    Birthday datetime,
    CountryId int,
    ProvinceId int,
    CityId int,
    TownId int,
    GenderId int,
    CreatedAt datetime,
    CreatedBy nvarchar(32),
    UpdatedAt datetime,
    UpdatedBy nvarchar(32),
	PRIMARY KEY (NetId) 
)

-----------Language------------------
CREATE TABLE Language(
    Id int,
    Name nvarchar(50),
    TwoCode nvarchar(5),
    ThreeCode nvarchar(5),
    Published bit,
    DisplayOrder int,
	PRIMARY KEY (Id) 
)

-----------------Country--------------------
CREATE TABLE Country(
    Id int,
    Name nvarchar(128),
    LanguageId int,
    TwoLetterIsoCode nvarchar(2),
    ThreeLetterIsoCode nvarchar(3),
    NumericIsoCode int,
    CurrencyId int,
    AllowsBilling bit,
    AllowsShipping bit,
    Published bit,
    DisplayOrder int, 
	PRIMARY KEY (Id) 
)

-----------------Province--------------------
CREATE TABLE Province(
    Id int,
    CountryId int,
    Name nvarchar(128),
    Style nvarchar(20),
    Abbreviation nvarchar(20),
    Published bit,
    DisplayOrder int,
	PRIMARY KEY (Id) 
)
-----------------City--------------------
CREATE TABLE City(
    Id int,
    CountryId int,
    StateProvinceId int,
    Name nvarchar(100),
    Style nvarchar(20),
    Abbreviation nvarchar(10),
    GPSAddress nvarchar(128),
    Published bit,
    DisplayOrder int,
    PRIMARY KEY (Id) 
)

-----------------Town--------------------
CREATE TABLE Town(
    Id int,
    CountryId int,
    StateProvinceId int,
    CityId int,
    Name nvarchar(100),
    Style nvarchar(20),
    Abbreviation nvarchar(10),
    GPSAddress nvarchar(128),
    Published bit,
    DisplayOrder int,
    PRIMARY KEY (Id) 
)
-----------------category--------------------
CREATE TABLE category(
    Id int,
    ParentId int,
    CategoryName nvarchar(256),
    ShortDescription nvarchar(500),
    Description nvarchar(MAX),
    CategoryImages nvarchar(256),
    CategoryURL nvarchar(256),
    CategorySeoKeywords nvarchar(256),
    CategoryShowHome bit,
    CategoryEnable bit,
    CategoryOrder int,
    CreatedAt datetime,
    CreatedBy nvarchar(32),
    UpdatedAt datetime,
    UpdatedBy nvarchar(32),
    PRIMARY KEY (Id) 
)

-----------------Gender--------------------
CREATE TABLE Gender(
    Id int,
    Name nvarchar(20),
    PRIMARY KEY (Id) 
)

-----------------Product--------------------
CREATE TABLE Product(
    Id int,
    ProductName nvarchar(250),
    SKU nvarchar(20),
    ShortDescription nvarchar(500),
    Description nvarchar(MAX),
    ProductSeoKeywords nvarchar(256),
    ManufacturerId int,
    UnitTypeId int,
    TaxRateId int,
    SalePrice decimal(18,2),
    RetailPrice decimal(18,2),
    Point decimal(18,2),
    ShowOnHomePage bit,
    ShowOnSalePage bit,
    WarehouseId int,
    ProductTypeId int,
    IsGroup bit,
    OwenSale bit,
    QuantityInStock int,
    DisplayOrder int,
    Published bit,
    Deleted bit,
    ProductTitleURL nvarchar(256),
    CreatedAt datetime,
    CreatedBy nvarchar(32),
    UpdatedAt datetime,
    UpdatedBy nvarchar(32),
    PRIMARY KEY (Id) 
)
-----------------ProductCategory--------------------
CREATE TABLE ProductCategory(
    Id int,
    ProductId int,
    CategoryId int,
    ProductCategoryOrder int,
    ProductCategoryEnable bit,
    CreatedAt datetime,
    CreatedBy nvarchar(32),
    UpdatedAt datetime,
    UpdatedBy nvarchar(32),
    PRIMARY KEY (Id) 
)
-----------------ProductImage--------------------
CREATE TABLE ProductImage(
    Id int,
    ProductId int,
    ImageName nvarchar(64),
    FileName nvarchar(200),
    FilePath nvarchar(256),
    DisplayOrder int,
    Published bit,
    PRIMARY KEY (Id) 
)

-----------------ProductType--------------------
CREATE TABLE ProductType(
    Id int,
    Name nvarchar(100),
    Description nvarchar(400),
	PRIMARY KEY (Id) 
)
-----------------ProductWarehouse--------------------
CREATE TABLE ProductWarehouse(
    Id int,
    Name nvarchar(200),
    Code nvarchar(20),
    OfficeId int,
    Email nvarchar(50),
    PhoneNumber nvarchar(20),
    MobileNumber nvarchar(20),
    Fax nvarchar(20),
    About nvarchar(400),
    CreatedDate datetime,
    DisplayOrder int,
    IsActive bit,
    Deleted bit,
    PRIMARY KEY (Id) 
)
-----------------Office--------------------
CREATE TABLE Office(
    Id int,
    Name nvarchar(400),
    ShortName nvarchar(50),
    SloganTitle nvarchar(200),
    Email nvarchar(100),
    Picture nvarchar(256),
    About nvarchar(1000),
    TownId int,
    Address nvarchar(200),
    AddressExt nvarchar(200),
    ZipPostalCode nvarchar(20),
    PhoneNumber nvarchar(20),
    MobileNumber nvarchar(20),
    FaxNumber nvarchar(20),
    OfficeTypeId int,
    ParentId int,
    LanguageId int,
    CurrencyId int,
    CreatedDate datetime,
    DisplayOrder int,
    IsActive bit,
    Deleted bit,
    PRIMARY KEY (Id) 
)

-----------------OfficeType--------------------
CREATE TABLE OfficeType(
    Id int,
    Name nvarchar(256),
    Description nvarchar(1000),
    PRIMARY KEY (Id) 
)
-----------------TaxRate--------------------
CREATE TABLE TaxRate(
    Id int,
    Name nvarchar(200),
    Value decimal(18,2),
    IsPercent bit,
    Discription nvarchar(400),
    PRIMARY KEY (Id) 
)
-----------------Manufacturer--------------------
CREATE TABLE Manufacturer(
    Id int,
    Name nvarchar(200),
    ShortName nvarchar(50),
    Logo nvarchar(256),
    Picture nvarchar(256),
    Email nvarchar(100),
    TownId int,
    Address nvarchar(200),
    AddressExt nvarchar(100),
    ZipPostalCode nvarchar(20),
    PhoneNumber nvarchar(20),
    MobileNumber nvarchar(20),
    FaxNumber nvarchar(20),
    BankAccount nvarchar(50),
    BankName nvarchar(400),
    TaxCode nvarchar(50),
    About nvarchar(400),
    DisplayOrder int,
    CoperativeDate datetime,
    IsActive bit,
    Deleted bit,
    PRIMARY KEY (Id) 
)
-----------------ProductTag--------------------
CREATE TABLE ProductTag(
    Id int,
    ProductId int,
    TagId int,
    PRIMARY KEY (Id) 
)

-----------------Tag--------------------
CREATE TABLE Tag(
    Id int,
    ParentTagId int,
    TitleTag nvarchar(256),
    TagSeoKeywords nvarchar(256),
    Description nvarchar(500),
    TagURL nvarchar(256),
    CreateAt datetime,
    CreateBy nvarchar(32),
    UpdateAt datetime,
    UpdateBy nvarchar(32),
    PRIMARY KEY (Id) 
)

-----------------CartItems--------------------
CREATE TABLE CartItems(
    Id int,
    ProductId int,
    CartId int,
    SKU nvarchar(20),
    Price int,
    Discount float,
    Contents nvarchar(500),
    Quantity int,
    Active bit,
    CreatedAt datetime,
    UpdatedAt datetime,
    PRIMARY KEY (Id) 
)
-----------------Cart--------------------
CREATE TABLE Cart(
    Id int,
    UserId nvarchar(128),
    SessionId nvarchar(128),
    Token nvarchar(128),
    Status bit,
    Contents nvarchar(500),
    FirstName nvarchar(64),
    LastName nvarchar(64),
    MobileNumber nvarchar(20),
    PhoneNumber nvarchar(20),
    Email nvarchar(128),
    Address nvarchar(256),
    AddressExt nvarchar(256),
    CountryId int,
    ProvinceId int,
    CityId int,
    TownId int,
    GenderId int,
    CreatedAt datetime,
    UpdatedAt datetime,
    PRIMARY KEY (Id) 
)
-----------------OrderItems--------------------
CREATE TABLE OrderItems(
    Id nvarchar(128),
    PurchaseOrderId nvarchar(128),
    ProductId int,
    Quantity int,
    Point decimal(18,2),
    UnitPrice decimal(18,2),
    TotalPrice decimal(18,2),
    PRIMARY KEY (Id) 
)
-----------------PurchaseOrder--------------------
CREATE TABLE PurchaseOrder(
    Id nvarchar(128),
    OrderCode nvarchar(10),
    SessionId nvarchar(128),
    Token nvarchar(128),
    WarehouseId int,
    BuyerId nvarchar(128),
    ApprovedById nvarchar(128),
    CountProduct int,
    CountProductType int,
    TotalPoint decimal(18,2),
    TotalPrice decimal(18,2),
    TaxRate decimal(18,2),
    FinalTotalPrice decimal(18,2),
    PaymentTypeId int,
    AlreadyPaid bit,
    PaidDate datetime,
    Notes nvarchar(400),
    ShipToCustomer nvarchar(64),
    ShipToPhone nvarchar(20),
    ShipOnDate datetime,
    ShippingTime int,
    ShippingExpress nvarchar(20),
    ShipToAddress nvarchar(100),
    TownId int,
    ZipPostalCode nvarchar(20),
    ShippingCost decimal(18,2),
    OrderStatusId int,
    CurrencyId int,
    GuestEmail nvarchar(300),
    GuestName nvarchar(300),
    GuestPhone nvarchar(50),
    GuestAddress nvarchar(500),
    CreatedAt datetime,
    CreatedBy nvarchar(32),
    UpdatedAt datetime,
    UpdatedBy nvarchar(32),
    PRIMARY KEY (Id) 
)
-----------------PaymentType--------------------
CREATE TABLE PaymentType(
    Id int,
    Name nvarchar(50),
    Description nvarchar(400),
    PRIMARY KEY (Id) 
)
-----------------StatusOrder--------------------
CREATE TABLE StatusOrder(
    Id int,
    Name nvarchar(50),
    Description nvarchar(200),
    PRIMARY KEY (Id) 
)
-----------------Currency--------------------
CREATE TABLE Currency(
    Id int,
    Name nvarchar(50),
    Code nvarchar(10),
    ShortCode nvarchar(5),
    Description nvarchar(64),
    Published bit,
    DisplayOrder int,
    PRIMARY KEY (Id) 
)

-----------------WebBannerGroup--------------------
CREATE TABLE WebBannerGroup(
    BannerGroupID int,
    BannerGroupName nvarchar(200),
    BannerGroupEnable bit,
    PRIMARY KEY (BannerGroupID) 
)

----------------WebBanner--------------------
CREATE TABLE WebBanner(
    BannerId int,
    BannerImage nvarchar(300),
    BannerTitle nvarchar(300),
    BannerDescription nvarchar(300),
    BannerAlt nvarchar(300),
    BannerURL nvarchar(400),
    BannerEnable bit,
    BannerGroupId int,
    PostParentId int,
    PRIMARY KEY (BannerId) 
)
-----------------WebInfo--------------------
CREATE TABLE WebInfo(
    Id int,
    ImageLogo nvarchar(128),
    TitleLogo nvarchar(128),
    Slogone nvarchar(256),
    CompanyTitle nvarchar(256),
    ShortDescription nvarchar(256),
    Description nvarchar(500),
    Address nvarchar(128),
    PhoneNumber nvarchar(20),
    Hotline nvarchar(20),
    Hotline2 nvarchar(20),
    Email nvarchar(20),
    Website nvarchar(128),
    TitleWeb nvarchar(128),
    MainKeywordH1 nvarchar(128),
    Seokeyword nvarchar(1000),
    Seodescription nvarchar(128),
    SeoGooglesiteverification nvarchar(128),
    SeoAuthor nvarchar(128),
    SeoRevisitafter nvarchar(128),
    Seorobots nvarchar(128),
    Seogeoregion nvarchar(128),
    Seogeoplacename nvarchar(128),
    Seogeoposition nvarchar(128),
    SeoICBM nvarchar(128),
    Seomsvalidate01 nvarchar(128),
    Seocontentlanguage nvarchar(128),
    SeoCOPYRIGHT nvarchar(128),
    Seogoogleanalytics nvarchar(128),
    Fax nvarchar(256),
    Facebook nvarchar(256),
    Twitter nvarchar(256),
    Googleplus nvarchar(256),
    Telegram nvarchar(256),
    Skype nvarchar(256),
    BaiViet1 ntext,
    BaiViet2 ntext,
    So1 int,
    So2 int,
    PRIMARY KEY (Id) 
)

-----------------WebPost--------------------
CREATE TABLE WebPost(
    PostID int,
    PostParentID int,
    PostTypeID int,
    PostTitle nvarchar(400),
    PostLowerTitle nvarchar(400),
    PostReadmore nvarchar(1000),
    PostContent ntext,
    PostUserName nvarchar(100),
    PostImage nvarchar(300),
    PostEnable bit,
    PostSeoKeyword nvarchar(1000),
    PostTitleURL nvarchar(500),
    PostTitleGoogle nvarchar(500),
    PostTitleImage nvarchar(500),
    PostTitleAlt nvarchar(500),
    PostLastModified datetime,
    CreatedAt datetime,
    CreatedBy nvarchar(32),
    UpdatedAt datetime,
    UpdatedBy nvarchar(32),
    PRIMARY KEY (PostID) 
)

-----------------WebPostParent--------------------
CREATE TABLE WebPostParent(
    PostParentID int,
    PostParentTitle nvarchar(400),
    PostParentLowerTitle nvarchar(400),
    PostParentReadmore nvarchar(1000),
    PostParentContent ntext,
    PostParentImage nvarchar(300),
    PostParentEnable bit,
    PostParentSeoKeyword nvarchar(500),
    PostParentTitleURL nvarchar(500),
    PostParentTitleGoogle nvarchar(500),
    PostParentTitleImage nvarchar(500),
    PostParentTitleAlt nvarchar(500),
    CreatedAt datetime,
    CreatedBy nvarchar(32),
    UpdatedAt datetime,
    UpdatedBy nvarchar(32),
     
    PRIMARY KEY (PostParentID) 
)

-----------------WebPostBanner--------------------
CREATE TABLE WebPostBanner(
    BannerID int,
    BannerImage nvarchar(300),
    BannerTitle nvarchar(300),
    BannerAlt nvarchar(300),
    BannerUrl nvarchar(400),
    BannerEnable bit,
    BannerGroupID int,
    BannerDescription nvarchar(300),
     
    PRIMARY KEY (BannerID) 
)

-----------------WebPostBannerGroup--------------------
CREATE TABLE WebPostBannerGroup(
    BannerGroupID int,
    BannerGroupName nvarchar(256),
    BannerGroupEnable bit,
     
    PRIMARY KEY (BannerGroupID) 
)

-----------------WebPostInTag--------------------
CREATE TABLE WebPostInTag(
    PostInTagID int,
    PostID int,
    PostTagID int,
     
    PRIMARY KEY (PostInTagID) 
)
-----------------WebPostTag--------------------
CREATE TABLE WebPostTag(
    PostTagID int,
    PostTagTitle nvarchar(500),
    PostTagLowerTitle nvarchar(500),
    PostTagReadmore nvarchar(1000),
    PostTagContent ntext,
    PostTagImage nvarchar(128),
    PostTagTitleImage nvarchar(500),
    PostTagtTitleAlt nvarchar(500),
    PostTagSeoKeyword nvarchar(500),
    PostTagTitleURL nvarchar(500),
    PostTagTitleGoogle nvarchar(500),
    PostTagEnable bit,
    CreatedAt datetime,
    CreatedBy nvarchar(32),
    UpdatedAt datetime,
    UpdatedBy nvarchar(32),
     
    PRIMARY KEY (PostTagID) 
)

-----------------WebPostType--------------------
CREATE TABLE WebPostType(
    PostTypeID int,
    PostTypeTitle nvarchar(500),
    PostTypeReadmore nvarchar(1000),
    PostTypeImage nvarchar(256),
    PostTypeEnable bit,
    PRIMARY KEY (PostTypeID) 
)