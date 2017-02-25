
CREATE TABLE [dbo].[SysOrderInfo](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderNo] varchar(50) default '' not null,
	[ShipperName] [nvarchar](50) default '' NOT NULL,
	[ShipperPhone] [varchar](50) default '' NOT NULL,
	[UserId] bigint default 0 not null,
	[PickupNumber] int default 0 not null,
	[Status] [int] NOT NULL,
	[CreateDate] [datetime] default getdate() NOT NULL,
	[IsDelete] bit default 0 not null
 CONSTRAINT [PK_SysOrderInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[SysAddresserInfo](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
[OrderId] bigint default 0 not null,
LogisticsSingle varchar(100) default '' not null,
RussiaCityId bigint default 0 not null,
RussiaAddress nvarchar(500) default '' not null,
CargoNumber int default 0 not null,
PickupDate datetime default getdate() not null,
PickupWay int default 0 not null,
GoodsType int default 0 not null,
TransportationWay int default 0 not null,
ProtectPrice decimal(18, 2) default 0.00 not null,
PolicyFee decimal(18, 2) default 0.00 not null,
GoodsWeight decimal(18,2) default 0.00 not null,
BoxLong decimal(18,2) default 0.00 not null,
BoxWidth decimal(18,2) default 0.00 not null,
BoxHeight decimal(18,2) default 0.00 not null,
	[CreateDate] [datetime] default getdate() NOT NULL,
	[IsDelete] bit default 0 not null
 CONSTRAINT [PK_SysAddresserInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[SysReceiverInfo](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
[OrderId] bigint default 0 not null,
ParcelSingle varchar(100) default '' not null,
ChinaCityId bigint default 0 not null,
ChinaAddress nvarchar(500) default '' not null,
ReceiverName nvarchar(100) default '' not null,
ReceiverPhone varchar(50)  default '' not null,
PackagingWay int default 0 not null,
ExpressWay int default 0 not null,
GoodsDesc nvarchar(2000) default '' not null, 
ParcelWeight decimal(18,2) default 0.00 not null,
ChinaCourierNumber varchar(50) default '' not null,
[CreateDate] [datetime] default getdate() NOT NULL,
[IsDelete] bit default 0 not null
 CONSTRAINT [PK_SysReceiverInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[SysGoodsType](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	GoodsType nvarchar(200) default '' not null,
	GoodsTypeDesc nvarchar(1000) default '' not null,
	MinWeight  decimal(18,2) default 0.00 not null,
	PremiumAmount decimal(18,2) default 0.00 not null,
   [CreateDate] [datetime] default getdate() NOT NULL,
   [IsDelete] bit default 0 not null
 CONSTRAINT [PK_SysGoodsType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[SysRussiaCity](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
   CityName nvarchar(100) default '' not null,
   CityDesc nvarchar(1000) default '' not null,
   LandTransportTime nvarchar(50) default '' not null,
   AirTransportTime nvarchar(50) default '' not null,
   [CreateDate] [datetime] default getdate() NOT NULL,
   [IsDelete] bit default 0 not null
 CONSTRAINT [PK_SysRussiaCity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[SysChinaCity](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
   CityName nvarchar(100) default '' not null,
   UnitPrice decimal(18,2) default 0.00 not null,
   ExpressBeavyPrice  decimal(18,2) default 0.00 not null,
   SfUnitPrice decimal(18,2) default 0.00 not null,
   SflogisticsBeavyPrice decimal(18,2) default 0.00 not null,
   [CreateDate] [datetime] default getdate() NOT NULL,
   [IsDelete] bit default 0 not null
 CONSTRAINT [PK_SysChinaCity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[SysLogisticsInfo](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
    LogisticsSingle varchar(50) default '' not null,
	LogisticsDesc nvarchar(1000) default '' not null,
    [CreateDate] [datetime] default getdate() NOT NULL,
    [IsDelete] bit default 0 not null
 CONSTRAINT [PK_SysLogisticsInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[SysUnitPrice](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
    RCityId int default 0 not null,
	GoodsType varchar(1000) default '' not null,
	LandPrice1 decimal(18,2) default 0.00 not null,
	LandPrice2 decimal(18,2) default 0.00 not null,
	AirPrice1 decimal(18,2) default 0.00 not null,
    AirPrice2 decimal(18,2) default 0.00 not null,
    [CreateDate] [datetime] default getdate() NOT NULL,
    [IsDelete] bit default 0 not null
 CONSTRAINT [PK_SysUnitPrice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[SysAgentCityMapping](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
    RCityId int default 0 not null,
	UserId bigint default 0 not null,
    [CreateDate] [datetime] default getdate() NOT NULL,
    [IsDelete] bit default 0 not null
 CONSTRAINT [PK_SysAgentCityMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[SysUserAcitonLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
    UserId bigint default 0 not null,
	Context nvarchar(4000) default '' not null,
    [CreateDate] [datetime] default getdate() NOT NULL,
    [IsDelete] bit default 0 not null
 CONSTRAINT [PK_SysUserAcitonLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

