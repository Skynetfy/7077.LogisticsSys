Create table SysExchange(
  Id bigint identity(1,1) primary key not null,
  ExchangeValue float default 0 not null,
  CurrentDate datetime default getdate() not null,
  CreateDate datetime default getdate() not null,
  IsDelete bit default 0 not null
)
Create table SysDbConfig(
  Id bigint identity(1,1) primary key not null,
  [Key] varchar(20) default '' not null,
  [Value] nvarchar(200) default '' not null,
  CreateDate datetime default getdate() not null,
  IsDelete bit default 0 not null
)
alter table [dbo].[SysReceiverInfo]
add ShippingStatus int default 0 not null

alter table [dbo].[SysCustomerInfo]
add Integral int default 0 not null

Create table SysIntegralLog(
  Id bigint identity(1,1) primary key not null,
  [Uid] bigint default 0 not null,
  [Type] int default 0 not null,
  Value int default 0 not null,
  [Desc] nvarchar(500) default '' not null,
  CreateDate datetime default getdate() not null,
  IsDelete bit default 0 not null
)

alter table SysOrderInfo
add [Integral] int default 0 not null