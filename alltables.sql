create schema dbo
create table dbo.users
(
id integer primary key,
firstname varchar(50) not null,
lastname varchar(50) not null,
password varchar(25) not null,
email varchar(100),
createdon date null,
createdby int null,
modifiedon date null,
modifiedby int null
);

create table dbo.roles(
id integer primary key,
name varchar(50) not null
)

create table dbo.DeliveryOrder(
id bigint primary key,
createdon timestamp not null,
status int not null,
FBRNo varchar(15) not null,
IsEdiOrder boolean, 	
BillToAddrId int not null,
PickupAddrId int not null,
ConsigneeAddrId int not null,
ReturnAddrId int ,
BrokerName varchar(100) not null,
BrokerRefNo varchar(25) ,
PortofOrigin varchar(25),
ShipmentWeight decimal(5,2),
FreightCharges varchar(15),
VesselName varchar(200),
BookingNo varchar(250),
CutoffDate timestamp,
Boxes int,
CustomerNotes varchar(500),
modifiedOn timestamp,
modifiedby bigint		
)

create table dbo.ShipmentDetail(
id bigint primary key,
DOId bigint ,
isComplete boolean,
containerNo varchar(50),
size varchar(50),
seaNo varchar(50),
apptDate date,
apptTime time,
driverNotes varchar(200),
 CONSTRAINT shipmentdetail_DOid_fkey FOREIGN KEY (DOId)
      REFERENCES dbo.DeliveryOrder (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION

)
create table dbo.Address
(
id bigInt primary key,
addressee varchar(50) ,
addressLine1 varchar(200),
addressLine3 varchar(300),
city varchar(50),
state varchar(50),
zip varchar(10),
country varchar(50),
addrType int

)


create table dbo.AddressTypes
(
id int primary key,
typename varchar(25)
)


insert into dbo.AddressTypes values (1, 'BillTo')
insert into dbo.AddressTypes values(2, 'PickUp')
insert into dbo.AddressTypes values(3, 'Consignee')
insert into dbo.AddressTypes values (4, 'Return')


Create table dbo.DOAddressDetail
(
 id bigint primary key,
 addressId bigint,
 CONSTRAINT DOAddressdetail_addressid_fkey FOREIGN KEY (addressId)
      REFERENCES dbo.Address (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
)


CREATE TABLE dbo.Invoice
(
id bigint primary key,
invoiceNo varchar(50),
invoiceDate date,
DOId bigint,
customerNotes varchar(500),
exceptions varchar(500),
baseRate decimal(5,2),
xfactor int,
stopOff decimal(5,2),
demurrage decimal(5,2),
forklift decimal(5,2),
incomingFreight decimal(5,2),
outgoingFreight decimal(5,2),
dropNpull decimal(5,2),
overWt decimal(5,2),
warehouseStorage decimal(5,2),
chassisFee decimal(5,2),
overnightFee decimal(5,2),
perdiemFee decimal(5,2),
transloadFee decimal(5,2),
waitingFee decimal(5,2),
hazardousFee decimal(5,2),
extraFee1 decimal(5,2),
extraFee2 decimal(5,2),
extraFee3 decimal(5,2),
extraFee4 decimal(5,2),
CONSTRAINT Invoice_DOid_fkey FOREIGN KEY (DOId)
      REFERENCES dbo.DeliveryOrder (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION

)


CREATE TABLE dbo.DriverDetail
(
id int primary key,
driverName varchar(50),
driverNo varchar(10)
)

CREATE TABLE dbo.DODriverDetail
(
id bigint primary key,
DoId bigint,
driverNo varchar(10),
actionDate date,
loadStatus varchar(20),
driverMoney decimal(5,2),
specialNotes varchar(100),
isLocked boolean,
userLogin varchar(10),

CONSTRAINT DODriverDetail_DOid_fkey FOREIGN KEY (DOId)
      REFERENCES dbo.DeliveryOrder (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,


CONSTRAINT DODriverDetail_driverNo_fkey FOREIGN KEY (DOId)
      REFERENCES dbo.DeliveryOrder (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION


)


