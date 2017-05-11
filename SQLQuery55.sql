Create table SysExchange(
  Id bigint identity(1,1) primary key not null,
  ExchangeValue float default 0 not null,
  CurrentDate datetime default getdate() not null,
  CreateDate datetime default getdate() not null,
  IsDelete bit default 0 not null
)

alter table [dbo].[SysReceiverInfo]
add ShippingStatus int default 0 not null