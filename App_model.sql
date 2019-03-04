CREATE SCHEMA dbo
CREATE TABLE dbo.Address(
	AddrKey UUID NOT NULL PRIMARY KEY,
	AddrName varchar(255) NOT NULL,
	Address1 varchar(255) NOT NULL,
	Address2 varchar(255) NOT NULL,
	City varchar(255) NULL,
	State varchar(255) NULL,
	ZipCode varchar(50) NULL,
	Country char(3) NULL,
	Website varchar(255) NULL,
	Phone varchar(20) NULL,
	Email varchar(255) NULL,
	Fax varchar(20) NULL)


CREATE TABLE dbo.AppRoles(
	RoleKey UUID NOT NULL PRIMARY KEY,
	Descrption varchar(255) NULL
 )

CREATE TABLE dbo.Broker(
	BrokerKey UUID NOT NULL PRIMARY KEY,
	BrokerId varchar(20) NULL,
	BrokerName varchar(255) NULL,
	AddrKey UUID NULL,
	CreateDate timestamp NULL,
	Status smallint NULL,
	StatusDate timestamp NULL)
/****** Object:  Table dbo.BrokerContacts    Script Date: 9/26/2018 11:24:06 PM ******/




CREATE TABLE dbo.BrokerContacts(
	BrokerKey UUID NOT NULL,
	ContactKey UUID NOT NULL,
 PRIMARY KEY 
(
	BrokerKey ,
	ContactKey ))
/****** Object:  Table dbo.Carrier    Script Date: 9/26/2018 11:24:06 PM ******/




CREATE TABLE dbo.Carrier(
	CarrierKey UUID NOT NULL PRIMARY KEY,
	CarrierId varchar(20) NOT NULL,
	CarrierName varchar(100) NOT NULL,
	IsSteamline bit NOT NULL,
	AddrKey UUID NOT NULL,
	SCACCode varchar(4) NOT NULL,
	LicensePlate varchar(255) NULL,
	LicensePlateExpiryDate date NULL,
	CreateDate timestamp NOT NULL,
	Status smallint NOT NULL,
	StatusDate timestamp NOT NULL
 )


CREATE TABLE dbo.Comment(
	CommentKey UUID NOT NULL PRIMARY KEY,
	Description varchar NULL,
	CreateDate timestamp NULL,
	CreateUserkey UUID NULL)

/****** Object:  Table dbo.CommentDocuments    Script Date: 9/26/2018 11:24:06 PM ******/

CREATE TABLE dbo.CommentDocuments(
	CommentKey UUID NOT NULL PRIMARY KEY,
	DocumentKey UUID NOT NULL
 )
/****** Object:  Table dbo.Contacts    Script Date: 9/26/2018 11:24:06 PM ******/




CREATE TABLE dbo.Contacts(
	ContactKey UUID NOT NULL PRIMARY KEY,
	ContactId varchar(20) NULL,
	FirstName varchar(100) NULL,
	LastName varchar(100) NULL,
	ContactType smallint NULL,
	AddrKey UUID NULL)


CREATE TABLE dbo.Customer(
	CustKey UUID NOT NULL PRIMARY KEY,
	CustId varchar(20) NOT NULL,
	CustName varchar(255) NOT NULL,
	AddrKey UUID NOT NULL,
	CreateDate timestamp NOT NULL,
	CustomerGroup smallint NULL,
	Status smallint NOT NULL,
	StatusDate timestamp NOT NULL,
	CreditCheck bit NULL,
	CreditLimit decimal(18, 2) NULL,
	CreditStatus smallint NULL)


CREATE TABLE dbo.CustomerAddress(
	CustKey UUID NOT NULL,
	AddrKey UUID NOT NULL,
	AddrType smallint NULL,
	AddrId varchar(20) NULL,
  PRIMARY KEY  
(
	CustKey ,
	AddrKey 
))

CREATE TABLE dbo.CustomerContacts(
	CustKey UUID NOT NULL,
	ContactKey UUID NOT NULL,
PRIMARY KEY  
(
	CustKey ,
	ContactKey
)
) 

/****** Object:  Table dbo.Document    Script Date: 9/26/2018 11:24:06 PM ******/




CREATE TABLE dbo.Document(
	DocumentKey UUID NOT NULL PRIMARY KEY,
	DocumentType smallint NULL,
	CreateDate timestamp NULL,
	CreateUserKey UUID NULL,
	OriginalFileName varchar(2000) NULL,
	OriginalFileType varchar(5) NULL,
	FileSizeinMB int NULL)

/****** Object:  Table dbo.DocumentType    Script Date: 9/26/2018 11:24:06 PM ******/




CREATE TABLE dbo.DocumentType(
	DocumentType smallint NOT NULL PRIMARY KEY,
	Description varchar(100) NULL,
	StoragePath varchar(1000) NULL
)

CREATE TABLE dbo.Driver(
	DriverKey UUID NOT NULL PRIMARY KEY,
	DriverId varchar(20) NULL,
	FirstName varchar(100) NULL,
	LastName varchar(100) NULL,
	AddrKey UUID NULL,
	CarrierKey UUID NULL,
	DrivingLicenseNo varchar(50) NULL,
	DrivingLicenseExpiryDate date NULL,
	CreateDate timestamp NULL,
	Status smallint NULL,
	StatusDate timestamp NULL,
	VendKey UUID NULL)

/****** Object:  Table dbo.InvoiceDetail    Script Date: 9/26/2018 11:24:06 PM ******/




CREATE TABLE dbo.InvoiceDetail(
	InvoiceLineKey UUID NOT NULL PRIMARY KEY,
	InvoiceKey UUID NULL,
	ItemKey UUID NULL,
	Description varchar(255) NULL,
	UnitPrice decimal(9, 5) NULL,
	Qty decimal(18, 2) NULL,
	ExtAmt decimal(18, 2) NULL)

/****** Object:  Table dbo.InvoiceHeader    Script Date: 9/26/2018 11:24:06 PM ******/

CREATE TABLE dbo.InvoiceHeader(
	InvoiceKey UUID NOT NULL PRIMARY KEY,
	InvoiceNo int NULL,
	InvoiceDate date NULL,
	CustKey UUID NULL,
	BillToAddrKey UUID NULL,
	BillToCopyAddrKey UUID NULL,
	InvoiceAmount numeric(18, 2) NULL,
	DueDate date NULL,
	InvoiceType smallint NULL)

/****** Object:  Table dbo.Item    Script Date: 9/26/2018 11:24:06 PM ******/


CREATE TABLE dbo.Item(
	ItemKey UUID NOT NULL PRIMARY KEY,
	ItemId varchar(30) NULL,
	Description varchar(255) NULL,
	ItemType smallint NULL,
	UnitPrice decimal(18, 5) NULL,
	UnitCost decimal(18, 5) NULL
)

/****** Object:  Table dbo.ShippingPort    Script Date: 9/26/2018 11:24:06 PM ******/




CREATE TABLE dbo.ShippingPort(
	ShippingPortKey UUID NOT NULL PRIMARY KEY,
	ShippingPortId varchar(20) NOT NULL,
	AddrKey UUID NOT NULL,
	Status timestamp NULL
 )

/****** Object:  Table dbo.ShippingPortTerminals    Script Date: 9/26/2018 11:24:06 PM ******/




CREATE TABLE dbo.ShippingPortTerminals(
	TerminalKey UUID NOT NULL PRIMARY KEY,
	PortKey UUID NOT NULL,
	AddrKey UUID NULL,
	Status smallint NULL)

/****** Object:  Table dbo.TMS_ContainerSize    Script Date: 9/26/2018 11:24:06 PM ******/




CREATE TABLE dbo.TMS_ContainerSize(
	ContainerSize smallint NOT NULL PRIMARY KEY,
	Description varchar(20) NULL)

/****** Object:  Table dbo.TMS_HoldReason    Script Date: 9/26/2018 11:24:06 PM ******/




CREATE TABLE dbo.TMS_HoldReason(
	HoldReason smallint NOT NULL PRIMARY KEY,
	Description varchar(255) NULL
 )

/****** Object:  Table dbo.TMS_OrderDetail    Script Date: 9/26/2018 11:24:06 PM ******/




CREATE TABLE dbo.TMS_OrderDetail(
	OrderDetailKey UUID NOT NULL PRIMARY KEY,
	OrderKey UUID NOT NULL,
	ContainerNo varchar(20) NOT NULL,
	ContainerSize smallint NOT NULL,
	Chassis varchar(20) NULL,
	SealNo varchar(20) NOT NULL,
	Weight decimal(18, 2) NULL,
	ApptDateFrom timestamp NULL,
	ApptDateTo timestamp NULL,
	Status smallint NULL,
	StatusDate timestamp NULL,
	HoldReason smallint NULL,
	HoldDate timestamp NULL)
/****** Object:  Table dbo.TMS_OrderDetailComments    Script Date: 9/26/2018 11:24:06 PM ******/




CREATE TABLE dbo.TMS_OrderDetailComments(
	OrderDetailKey UUID NOT NULL PRIMARY KEY,
	CommentKey UUID NOT NULL,
	CommentType smallint NULL
 )

/****** Object:  Table dbo.TMS_OrderDetailDocuments    Script Date: 9/26/2018 11:24:06 PM ******/




CREATE TABLE dbo.TMS_OrderDetailDocuments(
	OrderDetailKey UUID NOT NULL,
	DocumentKey UUID NOT NULL,
 PRIMARY KEY  
(
	OrderDetailKey ,
	DocumentKey
))

/****** Object:  Table dbo.TMS_OrderHeader    Script Date: 9/26/2018 11:24:06 PM ******/




CREATE TABLE dbo.TMS_OrderHeader(
	OrderKey UUID NOT NULL PRIMARY KEY,
	OrderNo varchar(20) NOT NULL,
	OrderDate timestamp NOT NULL,
	CustKey UUID NOT NULL,
	BillToAddrKey UUID NULL,
	BillToCopyAddrKey UUID NULL,
	SourceAddrKey UUID NULL,
	SourceCopyAddrKey UUID NULL,
	DestinationAddrKey UUID NULL,
	DestinationCopyAddrKey UUID NULL,
	ReturnAddrKey UUID NULL,
	ReturnCopyAddrKey UUID NULL,
	Source smallint NULL,
	OrderType smallint NULL,
	Status smallint NULL,
	StatusDate timestamp NULL,
	HoldReason smallint NULL,
	HoldDate timestamp NULL,
	BrokerKey UUID NULL,
	BrokerRefNo varchar(50) NULL,
	PortOfOriginKey UUID NULL,
	CarrierKey UUID NULL,
	VesselName varchar(50) NULL,
	BillOfLading varchar(50) NULL,
	BookingNo varchar(50) NULL,
	CutOffDate timestamp NULL,
	IsHazardous bit NULL,
	Priority smallint NULL,
	CreateDate timestamp NULL,
	CreateUserKey UUID NULL,
	LastUpdateDate timestamp NULL,
	LastUpdateUserkey UUID NULL
)/****** Object:  Table dbo.TMS_OrderHeaderComments    Script Date: 9/26/2018 11:24:06 PM ******/




CREATE TABLE dbo.TMS_OrderHeaderComments(
	OrderKey UUID NOT NULL,
	CommentKey UUID NOT NULL,
	CommentType smallint NULL,
  PRIMARY KEY  
(
	OrderKey ,
	CommentKey
)
) 

/****** Object:  Table dbo.TMS_OrderHeaderDocuments    Script Date: 9/26/2018 11:24:06 PM ******/




CREATE TABLE dbo.TMS_OrderHeaderDocuments(
	OrderKey UUID NOT NULL,
	DocumentKey UUID NOT NULL,
PRIMARY KEY  
(
	OrderKey,
	DocumentKey 
)
) 
/****** Object:  Table dbo.TMS_OrderInvoices    Script Date: 9/26/2018 11:24:06 PM ******/




CREATE TABLE dbo.TMS_OrderInvoices(
	OrderDetailKey UUID NOT NULL,
	InvoiceKey UUID NOT NULL,
  PRIMARY KEY  
(
	OrderDetailKey ,
	InvoiceKey 
)
) 

/****** Object:  Table dbo.TMS_OrderSource    Script Date: 9/26/2018 11:24:06 PM ******/




CREATE TABLE dbo.TMS_OrderSource(
	Source smallint NOT NULL,
	Description varchar(100) NOT NULL,
 PRIMARY KEY  
(
	Source 
)
) 
/****** Object:  Table dbo.TMS_OrderStatus    Script Date: 9/26/2018 11:24:06 PM ******/

CREATE TABLE dbo.TMS_OrderStatus(
	Status smallint NOT NULL,
	Description varchar(100) NOT NULL,
	OrderBy smallint NULL,
 PRIMARY KEY  
(
	Status 
)
) 

/****** Object:  Table dbo.TMS_OrderType    Script Date: 9/26/2018 11:24:06 PM ******/




CREATE TABLE dbo.TMS_OrderType(
	OrderType smallint NOT NULL,
	Description varchar(100) NOT NULL,
 PRIMARY KEY  
(
	OrderType ))

/****** Object:  Table dbo.TMS_Priority    Script Date: 9/26/2018 11:24:06 PM ******/




CREATE TABLE dbo.TMS_Priority(
	Priority smallint NOT NULL,
	Description varchar(20) NULL,
	ColorCode varchar(10) NULL,
 PRIMARY KEY  
(
	Priority
)
) 

/****** Object:  Table dbo.TMS_Routes    Script Date: 9/26/2018 11:24:06 PM ******/




CREATE TABLE dbo.TMS_Routes(
	RouteKey UUID NOT NULL,
	OrderDetailKey UUID NULL,
	OrderKey UUID NULL,
	LegNo smallint NULL,
	LegType smallint NULL,
	SourceAddrKey UUID NULL,
	DestinationAddrKey UUID NULL,
	EstimatedDistanceInMiles decimal(18, 2) NULL,
	EstimatedTravelTime decimal(5, 2) NULL,
	Status smallint NULL,
	DriverKey UUID NULL,
	ScheduledArrival timestamp NULL,
	ScheduledDeparture timestamp NULL,
	OdometerAtSource smallint NULL,
	ActualArrival timestamp NULL,
	ActualDeparture timestamp NULL,
	OdometerAtDestination smallint NULL,
 PRIMARY KEY  
(
	RouteKey 
)
) 
/****** Object:  Table dbo.TMS_RouteVouchers    Script Date: 9/26/2018 11:24:06 PM ******/




CREATE TABLE dbo.TMS_RouteVouchers(
	RouteKey UUID NOT NULL,
	VoucherKey UUID NOT NULL,
 PRIMARY KEY  
(RouteKey ,VoucherKey 
)
) /****** Object:  Table dbo.UserInfo    Script Date: 9/26/2018 11:24:06 PM ******/

CREATE TABLE dbo.UserInfo(
	UserKey UUID NOT NULL,
	FirstName varchar(255) NULL,
	LastName varchar(255) NULL,
	UserType smallint NULL,
	UserTypeRefKey UUID NULL,
 PRIMARY KEY  
(
	UserKey 
)
)
CREATE TABLE dbo.UserRoles(
	UserKey UUID NOT NULL,
	RoleKey UUID NOT NULL,
PRIMARY KEY  
(
	UserKey ,
	RoleKey 
)) 

/****** Object:  Table dbo.Vendor    Script Date: 9/26/2018 11:24:07 PM ******/




CREATE TABLE dbo.Vendor(
	VendKey UUID NOT NULL,
	VendId varchar(20) NULL,
	VendName varchar(255) NULL,
	AddrKey UUID NULL,
	Status smallint NULL,
	StatusDate timestamp NULL,
 PRIMARY KEY  
(
	VendKey 
)
) 
/****** Object:  Table dbo.VoucherDetail    Script Date: 9/26/2018 11:24:07 PM ******/

CREATE TABLE dbo.VoucherDetail(
	VoucherLineKey UUID NOT NULL,
	VoucherKey UUID NULL,
	ItemKey UUID NULL,
	Description varchar(255) NULL,
	UnitCost decimal(9, 5) NULL,
	Qty decimal(18, 2) NULL,
	ExtCost decimal(18, 2) NULL,
PRIMARY KEY  
(
	VoucherLineKey 
)
) 

CREATE TABLE dbo.VoucherHeader(
	VoucherKey UUID NOT NULL,
	VoucherNo int NULL,
	VoucherDate date NULL,
	VendKey UUID NULL,
	BillToAddrKey UUID NULL,
	BillToCopyAddrKey UUID NULL,
	VoucherAmount numeric(18, 2) NULL,
	DueDate date NULL,
	IsPaymentApproved bit NULL,
  PRIMARY KEY  
(
	VoucherKey 
)
) 

CREATE TABLE dbo.Warehouse(
	WarehouseKey UUID NOT NULL,
	WarehouseId varchar(10) NULL,
	AddrKey UUID NULL,
	Status smallint NULL,
 PRIMARY KEY  
(
	WarehouseKey 
)
)

ALTER TABLE dbo.Broker  ADD  CONSTRAINT FK_Broker_Address FOREIGN KEY(AddrKey)
REFERENCES dbo.Address (AddrKey)

--ALTER TABLE dbo.Broker CHECK CONSTRAINT FK_Broker_Address

ALTER TABLE dbo.BrokerContacts ADD  CONSTRAINT FK_BrokerContacts_Broker FOREIGN KEY(BrokerKey)
REFERENCES dbo.Broker (BrokerKey)

--ALTER TABLE dbo.BrokerContacts CHECK CONSTRAINT FK_BrokerContacts_Broker

ALTER TABLE dbo.BrokerContacts   ADD  CONSTRAINT FK_BrokerContacts_Contacts FOREIGN KEY(ContactKey)
REFERENCES dbo.Contacts (ContactKey)

--ALTER TABLE dbo.BrokerContacts CHECK CONSTRAINT FK_BrokerContacts_Contacts

ALTER TABLE dbo.Carrier  ADD  CONSTRAINT FK_Carrier_Address FOREIGN KEY(AddrKey)
REFERENCES dbo.Address (AddrKey)

--ALTER TABLE dbo.Carrier CHECK CONSTRAINT FK_Carrier_Address

ALTER TABLE dbo.CommentDocuments   ADD  CONSTRAINT FK_CommentDocuments_Comment FOREIGN KEY(CommentKey)
REFERENCES dbo.Comment (CommentKey)

--ALTER TABLE dbo.CommentDocuments CHECK CONSTRAINT FK_CommentDocuments_Comment

ALTER TABLE dbo.CommentDocuments  ADD  CONSTRAINT FK_CommentDocuments_Document FOREIGN KEY(DocumentKey)
REFERENCES dbo.Document (DocumentKey)

--ALTER TABLE dbo.CommentDocuments CHECK CONSTRAINT FK_CommentDocuments_Document

ALTER TABLE dbo.Contacts   ADD  CONSTRAINT FK_Contacts_Address FOREIGN KEY(AddrKey)
REFERENCES dbo.Address (AddrKey)

--ALTER TABLE dbo.Contacts CHECK CONSTRAINT FK_Contacts_Address

ALTER TABLE dbo.Customer   ADD  CONSTRAINT FK_Customer_Address FOREIGN KEY(AddrKey)
REFERENCES dbo.Address (AddrKey)

--ALTER TABLE dbo.Customer CHECK CONSTRAINT FK_Customer_Address

ALTER TABLE dbo.CustomerAddress  ADD  CONSTRAINT FK_CustomerAddress_Address FOREIGN KEY(AddrKey)
REFERENCES dbo.Address (AddrKey)

--ALTER TABLE dbo.CustomerAddress CHECK CONSTRAINT FK_CustomerAddress_Address

ALTER TABLE dbo.CustomerAddress  ADD  CONSTRAINT FK_CustomerAddress_Customer FOREIGN KEY(CustKey)
REFERENCES dbo.Customer (CustKey)

--ALTER TABLE dbo.CustomerAddress CHECK CONSTRAINT FK_CustomerAddress_Customer

ALTER TABLE dbo.CustomerContacts  ADD  CONSTRAINT FK_CustomerContacts_Contacts FOREIGN KEY(ContactKey)
REFERENCES dbo.Contacts (ContactKey)

--ALTER TABLE dbo.CustomerContacts CHECK CONSTRAINT FK_CustomerContacts_Contacts

ALTER TABLE dbo.CustomerContacts  ADD  CONSTRAINT FK_CustomerContacts_Customer FOREIGN KEY(CustKey)
REFERENCES dbo.Customer (CustKey)

--ALTER TABLE dbo.CustomerContacts CHECK CONSTRAINT FK_CustomerContacts_Customer

ALTER TABLE dbo.Document  ADD  CONSTRAINT FK_Document_DocumentType FOREIGN KEY(DocumentType)
REFERENCES dbo.DocumentType (DocumentType)

--ALTER TABLE dbo.Document CHECK CONSTRAINT FK_Document_DocumentType

ALTER TABLE dbo.Driver ADD  CONSTRAINT FK_Driver_Address FOREIGN KEY(AddrKey)
REFERENCES dbo.Address (AddrKey)

--ALTER TABLE dbo.Driver CHECK CONSTRAINT FK_Driver_Address

ALTER TABLE dbo.Driver   ADD  CONSTRAINT FK_Driver_Carrier FOREIGN KEY(CarrierKey)
REFERENCES dbo.Carrier (CarrierKey)

--ALTER TABLE dbo.Driver CHECK CONSTRAINT FK_Driver_Carrier

ALTER TABLE dbo.Driver   ADD  CONSTRAINT FK_Driver_Vendor FOREIGN KEY(VendKey)
REFERENCES dbo.Vendor (VendKey)

--ALTER TABLE dbo.Driver CHECK CONSTRAINT FK_Driver_Vendor

ALTER TABLE dbo.InvoiceDetail   ADD  CONSTRAINT FK_InvoiceDetail_InvoiceHeader FOREIGN KEY(InvoiceKey)
REFERENCES dbo.InvoiceHeader (InvoiceKey)

--ALTER TABLE dbo.InvoiceDetail CHECK CONSTRAINT FK_InvoiceDetail_InvoiceHeader

ALTER TABLE dbo.InvoiceDetail   ADD  CONSTRAINT FK_InvoiceDetail_Item FOREIGN KEY(ItemKey)
REFERENCES dbo.Item (ItemKey)

--ALTER TABLE dbo.InvoiceDetail CHECK CONSTRAINT FK_InvoiceDetail_Item

ALTER TABLE dbo.ShippingPort   ADD  CONSTRAINT FK_ShippingPort_Address FOREIGN KEY(AddrKey)
REFERENCES dbo.Address (AddrKey)

--ALTER TABLE dbo.ShippingPort CHECK CONSTRAINT FK_ShippingPort_Address

ALTER TABLE dbo.ShippingPortTerminals   ADD  CONSTRAINT FK_ShippingPortTerminals_Address FOREIGN KEY(AddrKey)
REFERENCES dbo.Address (AddrKey)

--ALTER TABLE dbo.ShippingPortTerminals CHECK CONSTRAINT FK_ShippingPortTerminals_Address

ALTER TABLE dbo.ShippingPortTerminals   ADD  CONSTRAINT FK_ShippingPortTerminals_ShippingPort FOREIGN KEY(PortKey)
REFERENCES dbo.ShippingPort (ShippingPortKey)

--ALTER TABLE dbo.ShippingPortTerminals CHECK CONSTRAINT FK_ShippingPortTerminals_ShippingPort

ALTER TABLE dbo.TMS_OrderDetail   ADD  CONSTRAINT FK_TMS_OrderDetail_TMS_ContainerSize1 FOREIGN KEY(ContainerSize)
REFERENCES dbo.TMS_ContainerSize (ContainerSize)

--ALTER TABLE dbo.TMS_OrderDetail CHECK CONSTRAINT FK_TMS_OrderDetail_TMS_ContainerSize1

ALTER TABLE dbo.TMS_OrderDetail   ADD  CONSTRAINT FK_TMS_OrderDetail_TMS_HoldReason FOREIGN KEY(HoldReason)
REFERENCES dbo.TMS_HoldReason (HoldReason)

--ALTER TABLE dbo.TMS_OrderDetail CHECK CONSTRAINT FK_TMS_OrderDetail_TMS_HoldReason

ALTER TABLE dbo.TMS_OrderDetail   ADD  CONSTRAINT FK_TMS_OrderDetail_TMS_OrderHeader FOREIGN KEY(OrderKey)
REFERENCES dbo.TMS_OrderHeader (OrderKey)

--ALTER TABLE dbo.TMS_OrderDetail CHECK CONSTRAINT FK_TMS_OrderDetail_TMS_OrderHeader

ALTER TABLE dbo.TMS_OrderDetailComments   ADD  CONSTRAINT FK_TMS_OrderDetailComments_Comment FOREIGN KEY(CommentKey)
REFERENCES dbo.Comment (CommentKey)

--ALTER TABLE dbo.TMS_OrderDetailComments CHECK CONSTRAINT FK_TMS_OrderDetailComments_Comment

ALTER TABLE dbo.TMS_OrderDetailComments   ADD  CONSTRAINT FK_TMS_OrderDetailComments_TMS_OrderDetail FOREIGN KEY(OrderDetailKey)
REFERENCES dbo.TMS_OrderDetail (OrderDetailKey)

--ALTER TABLE dbo.TMS_OrderDetailComments CHECK CONSTRAINT FK_TMS_OrderDetailComments_TMS_OrderDetail

ALTER TABLE dbo.TMS_OrderDetailDocuments   ADD  CONSTRAINT FK_TMS_OrderDetailDocuments_Document FOREIGN KEY(DocumentKey)
REFERENCES dbo.Document (DocumentKey)

--ALTER TABLE dbo.TMS_OrderDetailDocuments CHECK CONSTRAINT FK_TMS_OrderDetailDocuments_Document

ALTER TABLE dbo.TMS_OrderDetailDocuments   ADD  CONSTRAINT FK_TMS_OrderDetailDocuments_TMS_OrderDetail FOREIGN KEY(OrderDetailKey)
REFERENCES dbo.TMS_OrderDetail (OrderDetailKey)

---ALTER TABLE dbo.TMS_OrderDetailDocuments CHECK CONSTRAINT FK_TMS_OrderDetailDocuments_TMS_OrderDetail

ALTER TABLE dbo.TMS_OrderHeader   ADD  CONSTRAINT FK_TMS_OrderHeader_Address FOREIGN KEY(BillToAddrKey)
REFERENCES dbo.Address (AddrKey)

--ALTER TABLE dbo.TMS_OrderHeader CHECK CONSTRAINT FK_TMS_OrderHeader_Address

ALTER TABLE dbo.TMS_OrderHeader   ADD  CONSTRAINT FK_TMS_OrderHeader_Address1 FOREIGN KEY(BillToCopyAddrKey)
REFERENCES dbo.Address (AddrKey)

--ALTER TABLE dbo.TMS_OrderHeader CHECK CONSTRAINT FK_TMS_OrderHeader_Address1

ALTER TABLE dbo.TMS_OrderHeader   ADD  CONSTRAINT FK_TMS_OrderHeader_Address2 FOREIGN KEY(SourceAddrKey)
REFERENCES dbo.Address (AddrKey)

--ALTER TABLE dbo.TMS_OrderHeader CHECK CONSTRAINT FK_TMS_OrderHeader_Address2

ALTER TABLE dbo.TMS_OrderHeader   ADD  CONSTRAINT FK_TMS_OrderHeader_Address3 FOREIGN KEY(SourceCopyAddrKey)
REFERENCES dbo.Address (AddrKey)

--ALTER TABLE dbo.TMS_OrderHeader CHECK CONSTRAINT FK_TMS_OrderHeader_Address3

ALTER TABLE dbo.TMS_OrderHeader   ADD  CONSTRAINT FK_TMS_OrderHeader_Address4 FOREIGN KEY(DestinationAddrKey)
REFERENCES dbo.Address (AddrKey)

--ALTER TABLE dbo.TMS_OrderHeader CHECK CONSTRAINT FK_TMS_OrderHeader_Address4

ALTER TABLE dbo.TMS_OrderHeader   ADD  CONSTRAINT FK_TMS_OrderHeader_Address5 FOREIGN KEY(DestinationCopyAddrKey)
REFERENCES dbo.Address (AddrKey)

--ALTER TABLE dbo.TMS_OrderHeader CHECK CONSTRAINT FK_TMS_OrderHeader_Address5

ALTER TABLE dbo.TMS_OrderHeader   ADD  CONSTRAINT FK_TMS_OrderHeader_Address6 FOREIGN KEY(ReturnAddrKey)
REFERENCES dbo.Address (AddrKey)

--ALTER TABLE dbo.TMS_OrderHeader CHECK CONSTRAINT FK_TMS_OrderHeader_Address6

ALTER TABLE dbo.TMS_OrderHeader   ADD  CONSTRAINT FK_TMS_OrderHeader_Address7 FOREIGN KEY(ReturnCopyAddrKey)
REFERENCES dbo.Address (AddrKey)

--ALTER TABLE dbo.TMS_OrderHeader CHECK CONSTRAINT FK_TMS_OrderHeader_Address7

ALTER TABLE dbo.TMS_OrderHeader   ADD  CONSTRAINT FK_TMS_OrderHeader_Customer FOREIGN KEY(CustKey)
REFERENCES dbo.Customer (CustKey)

--ALTER TABLE dbo.TMS_OrderHeader CHECK CONSTRAINT FK_TMS_OrderHeader_Customer

ALTER TABLE dbo.TMS_OrderHeader   ADD  CONSTRAINT FK_TMS_OrderHeader_TMS_HoldReason FOREIGN KEY(HoldReason)
REFERENCES dbo.TMS_HoldReason (HoldReason)

--ALTER TABLE dbo.TMS_OrderHeader CHECK CONSTRAINT FK_TMS_OrderHeader_TMS_HoldReason

ALTER TABLE dbo.TMS_OrderHeader   ADD  CONSTRAINT FK_TMS_OrderHeader_TMS_OrderSource FOREIGN KEY(Source)
REFERENCES dbo.TMS_OrderSource (Source)

--ALTER TABLE dbo.TMS_OrderHeader CHECK CONSTRAINT FK_TMS_OrderHeader_TMS_OrderSource

ALTER TABLE dbo.TMS_OrderHeader   ADD  CONSTRAINT FK_TMS_OrderHeader_TMS_OrderStatus FOREIGN KEY(Status)
REFERENCES dbo.TMS_OrderStatus (Status)

--ALTER TABLE dbo.TMS_OrderHeader CHECK CONSTRAINT FK_TMS_OrderHeader_TMS_OrderStatus

ALTER TABLE dbo.TMS_OrderHeader   ADD  CONSTRAINT FK_TMS_OrderHeader_TMS_OrderType FOREIGN KEY(OrderType)
REFERENCES dbo.TMS_OrderType (OrderType)

--ALTER TABLE dbo.TMS_OrderHeader CHECK CONSTRAINT FK_TMS_OrderHeader_TMS_OrderType

ALTER TABLE dbo.TMS_OrderHeader   ADD  CONSTRAINT FK_TMS_OrderHeader_TMS_Priority FOREIGN KEY(Priority)
REFERENCES dbo.TMS_Priority (Priority)

--ALTER TABLE dbo.TMS_OrderHeader CHECK CONSTRAINT FK_TMS_OrderHeader_TMS_Priority

ALTER TABLE dbo.TMS_OrderHeader   ADD  CONSTRAINT FK_TMS_OrderHeader_TMS_Priority1 FOREIGN KEY(Priority)
REFERENCES dbo.TMS_Priority (Priority)

--ALTER TABLE dbo.TMS_OrderHeader CHECK CONSTRAINT FK_TMS_OrderHeader_TMS_Priority1

ALTER TABLE dbo.TMS_OrderHeaderComments   ADD  CONSTRAINT FK_TMS_OrderHeaderComments_Comment FOREIGN KEY(CommentKey)
REFERENCES dbo.Comment (CommentKey)

--ALTER TABLE dbo.TMS_OrderHeaderComments CHECK CONSTRAINT FK_TMS_OrderHeaderComments_Comment

ALTER TABLE dbo.TMS_OrderHeaderComments   ADD  CONSTRAINT FK_TMS_OrderHeaderComments_TMS_OrderHeader FOREIGN KEY(OrderKey)
REFERENCES dbo.TMS_OrderHeader (OrderKey)

--ALTER TABLE dbo.TMS_OrderHeaderComments CHECK CONSTRAINT FK_TMS_OrderHeaderComments_TMS_OrderHeader

ALTER TABLE dbo.TMS_OrderHeaderDocuments   ADD  CONSTRAINT FK_TMS_OrderHeaderDocuments_Document FOREIGN KEY(DocumentKey)
REFERENCES dbo.Document (DocumentKey)

--ALTER TABLE dbo.TMS_OrderHeaderDocuments CHECK CONSTRAINT FK_TMS_OrderHeaderDocuments_Document

ALTER TABLE dbo.TMS_OrderHeaderDocuments   ADD  CONSTRAINT FK_TMS_OrderHeaderDocuments_TMS_OrderHeader FOREIGN KEY(OrderKey)
REFERENCES dbo.TMS_OrderHeader (OrderKey)

--ALTER TABLE dbo.TMS_OrderHeaderDocuments CHECK CONSTRAINT FK_TMS_OrderHeaderDocuments_TMS_OrderHeader

ALTER TABLE dbo.TMS_OrderInvoices   ADD  CONSTRAINT FK_TMS_OrderInvoices_InvoiceHeader FOREIGN KEY(InvoiceKey)
REFERENCES dbo.InvoiceHeader (InvoiceKey)

--ALTER TABLE dbo.TMS_OrderInvoices CHECK CONSTRAINT FK_TMS_OrderInvoices_InvoiceHeader

ALTER TABLE dbo.TMS_OrderInvoices   ADD  CONSTRAINT FK_TMS_OrderInvoices_TMS_OrderDetail FOREIGN KEY(OrderDetailKey)
REFERENCES dbo.TMS_OrderDetail (OrderDetailKey)

--ALTER TABLE dbo.TMS_OrderInvoices CHECK CONSTRAINT FK_TMS_OrderInvoices_TMS_OrderDetail

ALTER TABLE dbo.TMS_Routes   ADD  CONSTRAINT FK_TMS_Routes_Address FOREIGN KEY(SourceAddrKey)
REFERENCES dbo.Address (AddrKey)

--ALTER TABLE dbo.TMS_Routes CHECK CONSTRAINT FK_TMS_Routes_Address

ALTER TABLE dbo.TMS_Routes   ADD  CONSTRAINT FK_TMS_Routes_Address1 FOREIGN KEY(DestinationAddrKey)
REFERENCES dbo.Address (AddrKey)

--ALTER TABLE dbo.TMS_Routes CHECK CONSTRAINT FK_TMS_Routes_Address1

ALTER TABLE dbo.TMS_Routes   ADD  CONSTRAINT FK_TMS_Routes_Driver FOREIGN KEY(DriverKey)
REFERENCES dbo.Driver (DriverKey)

--ALTER TABLE dbo.TMS_Routes CHECK CONSTRAINT FK_TMS_Routes_Driver

ALTER TABLE dbo.TMS_Routes   ADD  CONSTRAINT FK_TMS_Routes_TMS_OrderDetail FOREIGN KEY(OrderDetailKey)
REFERENCES dbo.TMS_OrderDetail (OrderDetailKey)

--ALTER TABLE dbo.TMS_Routes CHECK CONSTRAINT FK_TMS_Routes_TMS_OrderDetail

ALTER TABLE dbo.TMS_Routes   ADD  CONSTRAINT FK_TMS_Routes_TMS_OrderHeader FOREIGN KEY(OrderKey)
REFERENCES dbo.TMS_OrderHeader (OrderKey)

--ALTER TABLE dbo.TMS_Routes CHECK CONSTRAINT FK_TMS_Routes_TMS_OrderHeader

ALTER TABLE dbo.TMS_RouteVouchers   ADD  CONSTRAINT FK_TMS_RouteVouchers_TMS_Routes FOREIGN KEY(RouteKey)
REFERENCES dbo.TMS_Routes (RouteKey)

--ALTER TABLE dbo.TMS_RouteVouchers CHECK CONSTRAINT FK_TMS_RouteVouchers_TMS_Routes

ALTER TABLE dbo.TMS_RouteVouchers   ADD  CONSTRAINT FK_TMS_RouteVouchers_VoucherHeader FOREIGN KEY(VoucherKey)
REFERENCES dbo.VoucherHeader (VoucherKey)

--ALTER TABLE dbo.TMS_RouteVouchers CHECK CONSTRAINT FK_TMS_RouteVouchers_VoucherHeader

ALTER TABLE dbo.UserRoles   ADD  CONSTRAINT FK_UserRoles_AppRoles FOREIGN KEY(RoleKey)
REFERENCES dbo.AppRoles (RoleKey)

--ALTER TABLE dbo.UserRoles CHECK CONSTRAINT FK_UserRoles_AppRoles

ALTER TABLE dbo.UserRoles   ADD  CONSTRAINT FK_UserRoles_UserInfo FOREIGN KEY(UserKey)
REFERENCES dbo.UserInfo (UserKey)

--ALTER TABLE dbo.UserRoles CHECK CONSTRAINT FK_UserRoles_UserInfo

ALTER TABLE dbo.VoucherDetail   ADD  CONSTRAINT FK_VoucherDetail_Item FOREIGN KEY(ItemKey)
REFERENCES dbo.Item (ItemKey)

--ALTER TABLE dbo.VoucherDetail CHECK CONSTRAINT FK_VoucherDetail_Item

ALTER TABLE dbo.VoucherDetail   ADD  CONSTRAINT FK_VoucherDetail_VoucherHeader FOREIGN KEY(VoucherKey)
REFERENCES dbo.VoucherHeader (VoucherKey)

--ALTER TABLE dbo.VoucherDetail CHECK CONSTRAINT FK_VoucherDetail_VoucherHeader

ALTER TABLE dbo.VoucherHeader   ADD  CONSTRAINT FK_VoucherHeader_Vendor FOREIGN KEY(VendKey)
REFERENCES dbo.Vendor (VendKey)

--ALTER TABLE dbo.VoucherHeader CHECK CONSTRAINT FK_VoucherHeader_Vendor

ALTER TABLE dbo.Warehouse   ADD  CONSTRAINT FK_Warehouse_Address FOREIGN KEY(AddrKey)
REFERENCES dbo.Address (AddrKey)

--ALTER TABLE dbo.Warehouse CHECK CONSTRAINT FK_Warehouse_Address


