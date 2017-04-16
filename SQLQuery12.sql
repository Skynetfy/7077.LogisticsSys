USE [master]
GO
/****** Object:  Database [7077SysDB1]    Script Date: 2017/4/16 21:34:29 ******/
CREATE DATABASE [7077SysDB1]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'7077SysDB1', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\7077SysDB1.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'7077SysDB1_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\7077SysDB1_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [7077SysDB1] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [7077SysDB1].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [7077SysDB1] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [7077SysDB1] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [7077SysDB1] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [7077SysDB1] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [7077SysDB1] SET ARITHABORT OFF 
GO
ALTER DATABASE [7077SysDB1] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [7077SysDB1] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [7077SysDB1] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [7077SysDB1] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [7077SysDB1] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [7077SysDB1] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [7077SysDB1] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [7077SysDB1] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [7077SysDB1] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [7077SysDB1] SET  DISABLE_BROKER 
GO
ALTER DATABASE [7077SysDB1] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [7077SysDB1] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [7077SysDB1] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [7077SysDB1] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [7077SysDB1] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [7077SysDB1] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [7077SysDB1] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [7077SysDB1] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [7077SysDB1] SET  MULTI_USER 
GO
ALTER DATABASE [7077SysDB1] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [7077SysDB1] SET DB_CHAINING OFF 
GO
ALTER DATABASE [7077SysDB1] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [7077SysDB1] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [7077SysDB1] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [7077SysDB1] SET QUERY_STORE = OFF
GO
USE [7077SysDB1]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [7077SysDB1]
GO
/****** Object:  UserDefinedTableType [dbo].[CUT_LAY_SysReceiverInfoType]    Script Date: 2017/4/16 21:34:30 ******/
CREATE TYPE [dbo].[CUT_LAY_SysReceiverInfoType] AS TABLE(
	[Id] [bigint] NOT NULL,
	[OrderId] [bigint] NOT NULL,
	[ParcelSingle] [varchar](100) NULL,
	[ChinaCityId] [bigint] NULL,
	[ChinaAddress] [nvarchar](500) NOT NULL,
	[ReceiverName] [nvarchar](100) NOT NULL,
	[ReceiverPhone] [varchar](50) NULL,
	[PackagingWay] [int] NULL,
	[ExpressWay] [int] NULL,
	[GoodsDesc] [nvarchar](2000) NOT NULL,
	[ParcelWeight] [decimal](18, 2) NOT NULL,
	[CourierComId] [bigint] NOT NULL,
	[ChinaCourierNumber] [varchar](50) NOT NULL,
	[Desc] [nvarchar](max) NULL,
	[RealWeight] [decimal](18, 2) NOT NULL,
	[RealPrice] [decimal](18, 2) NOT NULL,
	[BudgetPrice] [decimal](18, 2) NOT NULL
)
GO
/****** Object:  Table [dbo].[SysAgentInfo]    Script Date: 2017/4/16 21:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysAgentInfo](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[AgentCityId] [bigint] NOT NULL,
	[QQNumber] [varchar](20) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysRussiaCity]    Script Date: 2017/4/16 21:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysRussiaCity](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CityName] [nvarchar](100) NOT NULL,
	[CityDesc] [nvarchar](1000) NOT NULL,
	[LandTransportTime] [nvarchar](50) NOT NULL,
	[AirTransportTime] [nvarchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_SysRussiaCity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysUser]    Script Date: 2017/4/16 21:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysUser](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Email] [varchar](100) NULL,
	[Status] [int] NOT NULL,
	[Phone] [varchar](50) NULL,
	[CreateDate] [datetime] NOT NULL,
	[DisplayName] [nvarchar](150) NULL,
	[RuleType] [varchar](50) NULL,
	[isdelete] [bit] NOT NULL,
 CONSTRAINT [PK_SysUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[v_AgentInfo]    Script Date: 2017/4/16 21:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[v_AgentInfo]
as 
select A.Id
       ,A.[UserName]
      ,A.[Password]
      ,A.[Email]
      ,A.[Status]
      ,A.[Phone]
      ,A.[CreateDate]
      ,A.[DisplayName]
      ,A.[RuleType]
      ,A.[isdelete]
	  ,B.AgentCityId
	  ,(select top 1 [CityName] from [dbo].[SysRussiaCity] C where C.Id=B.AgentCityId and C.IsDelete=0) as CityName
	  ,B.QQNumber
 from [dbo].[SysUser] A
 inner join [dbo].[SysAgentInfo] B on A.Id=B.UserId
 where A.isdelete=0 and B.IsDelete=0 and A.[RuleType]='Agents'


GO
/****** Object:  Table [dbo].[SysLogisticsInfo]    Script Date: 2017/4/16 21:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysLogisticsInfo](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[LogisticsSingle] [varchar](50) NOT NULL,
	[LogisticsDesc] [nvarchar](1000) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[Status] [bit] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[OrderNos] [varchar](2000) NOT NULL,
	[UserName] [varchar](20) NOT NULL,
 CONSTRAINT [PK_SysLogisticsInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[v_LogisticsInfo]    Script Date: 2017/4/16 21:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE view [dbo].[v_LogisticsInfo]
as 
select 
      [LogisticsSingle]
      ,[LogisticsDesc]
      ,[IsDelete]
      ,[Status]
      ,[UpdateDate]
	  ,UserName from [SysLogisticsInfo] (nolock)
                             where [Id] in(select Max(Id) from [SysLogisticsInfo] (nolock)  GROUP BY [LogisticsSingle]) 
group by [LogisticsSingle]
      ,[LogisticsDesc]
      ,[IsDelete]
      ,[Status]
      ,[UpdateDate] 
	  ,UserName




GO
/****** Object:  Table [dbo].[SysAddresserInfo]    Script Date: 2017/4/16 21:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysAddresserInfo](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderId] [bigint] NOT NULL,
	[LogisticsSingle] [varchar](100) NOT NULL,
	[RussiaCityId] [bigint] NOT NULL,
	[RussiaAddress] [nvarchar](500) NOT NULL,
	[PickupDate] [datetime] NOT NULL,
	[GoodsType] [int] NOT NULL,
	[TransportationWay] [int] NOT NULL,
	[ProtectPrice] [decimal](18, 2) NOT NULL,
	[PolicyFee] [decimal](18, 2) NOT NULL,
	[GoodsWeight] [decimal](18, 2) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[OrderFrees] [decimal](18, 2) NOT NULL,
	[IsArrivePay] [bit] NOT NULL,
	[ArrivePayValue] [decimal](18, 2) NOT NULL,
	[IsOutPhoto] [bit] NOT NULL,
	[ExchangeRate] [decimal](18, 2) NOT NULL,
	[WebUrl] [varchar](200) NOT NULL,
 CONSTRAINT [PK_SysAddresserInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysChinaCity]    Script Date: 2017/4/16 21:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysChinaCity](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CityName] [nvarchar](100) NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[ExpressBeavyPrice] [decimal](18, 2) NOT NULL,
	[SfUnitPrice] [decimal](18, 2) NOT NULL,
	[SflogisticsBeavyPrice] [decimal](18, 2) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_SysChinaCity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysGoodsType]    Script Date: 2017/4/16 21:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysGoodsType](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[GoodsType] [nvarchar](200) NOT NULL,
	[GoodsTypeDesc] [nvarchar](1000) NOT NULL,
	[MinWeight] [decimal](18, 2) NOT NULL,
	[PremiumAmount] [decimal](18, 2) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_SysGoodsType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysKuaiDiCom]    Script Date: 2017/4/16 21:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysKuaiDiCom](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ComSn] [varchar](50) NOT NULL,
	[ComName] [nvarchar](100) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysOrderInfo]    Script Date: 2017/4/16 21:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysOrderInfo](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderNo] [varchar](50) NOT NULL,
	[ShipperName] [nvarchar](50) NOT NULL,
	[ShipperPhone] [varchar](50) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[PickupNumber] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[PayStatus] [int] NOT NULL,
	[OrderRealPrice] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_SysOrderInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysReceiverInfo]    Script Date: 2017/4/16 21:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysReceiverInfo](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderId] [bigint] NOT NULL,
	[ChinaCityId] [bigint] NOT NULL,
	[ChinaAddress] [nvarchar](500) NOT NULL,
	[ReceiverName] [nvarchar](100) NOT NULL,
	[ReceiverPhone] [varchar](50) NOT NULL,
	[PackagingWay] [int] NOT NULL,
	[ExpressWay] [int] NOT NULL,
	[GoodsDesc] [nvarchar](2000) NOT NULL,
	[ParcelWeight] [decimal](18, 2) NOT NULL,
	[ChinaCourierNumber] [varchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[Desc] [nvarchar](max) NULL,
	[CourierFees] [decimal](18, 2) NOT NULL,
	[CourierComId] [bigint] NOT NULL,
 CONSTRAINT [PK_SysReceiverInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  View [dbo].[v_orderinfo]    Script Date: 2017/4/16 21:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE view [dbo].[v_orderinfo]
as	select A.[Id]
      ,A.[OrderNo]
      ,A.[ShipperName]
      ,A.[ShipperPhone]
      ,A.[UserId]
	  ,A.OrderRealPrice
      ,A.[Status]
	  ,A.PayStatus
	  ,D.UserName
	  ,D.DisplayName
	  ,B.[Id] as Bid
      ,B.[LogisticsSingle]
      ,B.[RussiaCityId]
      ,B.[RussiaAddress]
      ,B.[PickupDate]
      ,B.[GoodsType]
	  ,H.[GoodsType] as GoodsTypeName
      ,B.[TransportationWay]
      ,B.[ProtectPrice]
      ,B.[PolicyFee]
      ,B.[GoodsWeight]
      ,B.[OrderFrees]
      ,B.[IsArrivePay]
      ,B.[ArrivePayValue]
      ,B.[IsOutPhoto]
      ,B.[ExchangeRate]
      ,B.[WebUrl]
	  ,F.CityName as RussiaCityName
      ,C.[Id] as Cid
      ,C.[ChinaCityId]
	  ,G.CityName as ChinaCityName
      ,C.[ChinaAddress]
      ,C.[ReceiverName]
      ,C.[ReceiverPhone]
      ,C.[PackagingWay]
      ,C.[ExpressWay]
      ,C.[GoodsDesc]
      ,C.[ParcelWeight]
      ,C.[ChinaCourierNumber]
      ,C.[Desc]
      ,C.[CourierFees]
      ,C.[CourierComId]
	  ,I.ComName as CourierComName
      ,A.[CreateDate]
      ,A.[IsDelete]
from [dbo].[SysOrderInfo] A
left join [dbo].[SysAddresserInfo] B on A.Id=B.OrderId
left join [dbo].[SysReceiverInfo] C on A.Id=C.OrderId
left join [dbo].[SysChinaCity] G on G.Id=C.[ChinaCityId]
left join [dbo].[SysGoodsType] H on H.Id=B.[GoodsType]
left join [dbo].[SysUser] D on A.UserId=D.Id
left join [dbo].[SysRussiaCity] F on F.Id=B.[RussiaCityId]
left join [dbo].[SysKuaiDiCom] I on I.Id=C.[CourierComId]
where A.IsDelete=0 and  B.IsDelete=0  and  C.IsDelete=0  and  D.IsDelete=0  and  F.IsDelete=0  and  H.IsDelete=0 








GO
/****** Object:  Table [dbo].[SysUnitPrice]    Script Date: 2017/4/16 21:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysUnitPrice](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RCityId] [int] NOT NULL,
	[LandPrice1] [decimal](18, 2) NOT NULL,
	[LandPrice2] [decimal](18, 2) NOT NULL,
	[AirPrice1] [decimal](18, 2) NOT NULL,
	[AirPrice2] [decimal](18, 2) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[GoodsTypeId] [bigint] NOT NULL,
 CONSTRAINT [PK_SysUnitPrice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[v_UnitPrice]    Script Date: 2017/4/16 21:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[v_UnitPrice]
as
select A.[CityName],A.Id as Cid,B.GoodsType as GoodsTypeName,B.Id as Gid,C.*
from [dbo].[SysRussiaCity] A CROSS JOIN [dbo].[SysGoodsType] B
left join SysUnitPrice C on A.Id=C.RCityId and B.Id=C.GoodsTypeId and C.IsDelete=0
where A.IsDelete=0 and B.IsDelete=0 


GO
/****** Object:  Table [dbo].[SysActionLog]    Script Date: 2017/4/16 21:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysActionLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[OrderId] [bigint] NOT NULL,
	[LogType] [int] NOT NULL,
	[ActionDate] [datetime] NOT NULL,
	[ActionDesc] [nvarchar](4000) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysAgentCityMapping]    Script Date: 2017/4/16 21:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysAgentCityMapping](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RCityId] [int] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_SysAgentCityMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysCustomerInfo]    Script Date: 2017/4/16 21:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysCustomerInfo](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[CustomerID] [varchar](50) NOT NULL,
	[CityId] [bigint] NOT NULL,
	[Phone] [varchar](50) NOT NULL,
	[Address] [nvarchar](1000) NOT NULL,
	[WebChatNo] [varchar](50) NOT NULL,
	[QQNumber] [varchar](20) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysOrderNumber]    Script Date: 2017/4/16 21:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysOrderNumber](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderId] [bigint] NOT NULL,
	[Number] [varchar](50) NOT NULL,
	[Status] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysOrderPayInfo]    Script Date: 2017/4/16 21:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysOrderPayInfo](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderId] [bigint] NOT NULL,
	[CardNumber] [varchar](50) NOT NULL,
	[PayUserName] [nvarchar](50) NOT NULL,
	[PayAmount] [decimal](18, 2) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Type] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysUserAcitonLog]    Script Date: 2017/4/16 21:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysUserAcitonLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Context] [nvarchar](4000) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_SysUserAcitonLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[SysActionLog] ADD  DEFAULT ((0)) FOR [UserId]
GO
ALTER TABLE [dbo].[SysActionLog] ADD  DEFAULT ((0)) FOR [OrderId]
GO
ALTER TABLE [dbo].[SysActionLog] ADD  DEFAULT ((0)) FOR [LogType]
GO
ALTER TABLE [dbo].[SysActionLog] ADD  DEFAULT (getdate()) FOR [ActionDate]
GO
ALTER TABLE [dbo].[SysActionLog] ADD  DEFAULT ('') FOR [ActionDesc]
GO
ALTER TABLE [dbo].[SysActionLog] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[SysActionLog] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[SysAddresserInfo] ADD  DEFAULT ((0)) FOR [OrderId]
GO
ALTER TABLE [dbo].[SysAddresserInfo] ADD  DEFAULT ('') FOR [LogisticsSingle]
GO
ALTER TABLE [dbo].[SysAddresserInfo] ADD  DEFAULT ((0)) FOR [RussiaCityId]
GO
ALTER TABLE [dbo].[SysAddresserInfo] ADD  DEFAULT ('') FOR [RussiaAddress]
GO
ALTER TABLE [dbo].[SysAddresserInfo] ADD  DEFAULT (getdate()) FOR [PickupDate]
GO
ALTER TABLE [dbo].[SysAddresserInfo] ADD  DEFAULT ((0)) FOR [GoodsType]
GO
ALTER TABLE [dbo].[SysAddresserInfo] ADD  DEFAULT ((0)) FOR [TransportationWay]
GO
ALTER TABLE [dbo].[SysAddresserInfo] ADD  DEFAULT ((0.00)) FOR [ProtectPrice]
GO
ALTER TABLE [dbo].[SysAddresserInfo] ADD  DEFAULT ((0.00)) FOR [PolicyFee]
GO
ALTER TABLE [dbo].[SysAddresserInfo] ADD  DEFAULT ((0.00)) FOR [GoodsWeight]
GO
ALTER TABLE [dbo].[SysAddresserInfo] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[SysAddresserInfo] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[SysAddresserInfo] ADD  CONSTRAINT [DF_SysAddresserInfo_BudgetPrice]  DEFAULT ((0)) FOR [OrderFrees]
GO
ALTER TABLE [dbo].[SysAddresserInfo] ADD  CONSTRAINT [DF_SysAddresserInfo_IsArrivePay]  DEFAULT ((0)) FOR [IsArrivePay]
GO
ALTER TABLE [dbo].[SysAddresserInfo] ADD  CONSTRAINT [DF_SysAddresserInfo_ArrivePayValue]  DEFAULT ((0)) FOR [ArrivePayValue]
GO
ALTER TABLE [dbo].[SysAddresserInfo] ADD  CONSTRAINT [DF_SysAddresserInfo_IsOutPhoto]  DEFAULT ((0)) FOR [IsOutPhoto]
GO
ALTER TABLE [dbo].[SysAddresserInfo] ADD  CONSTRAINT [DF_SysAddresserInfo_ExchangeRate]  DEFAULT ((0)) FOR [ExchangeRate]
GO
ALTER TABLE [dbo].[SysAddresserInfo] ADD  DEFAULT ('') FOR [WebUrl]
GO
ALTER TABLE [dbo].[SysAgentCityMapping] ADD  DEFAULT ((0)) FOR [RCityId]
GO
ALTER TABLE [dbo].[SysAgentCityMapping] ADD  DEFAULT ((0)) FOR [UserId]
GO
ALTER TABLE [dbo].[SysAgentCityMapping] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[SysAgentCityMapping] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[SysAgentInfo] ADD  DEFAULT ((0)) FOR [UserId]
GO
ALTER TABLE [dbo].[SysAgentInfo] ADD  DEFAULT ((0)) FOR [AgentCityId]
GO
ALTER TABLE [dbo].[SysAgentInfo] ADD  DEFAULT ('') FOR [QQNumber]
GO
ALTER TABLE [dbo].[SysAgentInfo] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[SysAgentInfo] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[SysChinaCity] ADD  DEFAULT ('') FOR [CityName]
GO
ALTER TABLE [dbo].[SysChinaCity] ADD  DEFAULT ((0.00)) FOR [UnitPrice]
GO
ALTER TABLE [dbo].[SysChinaCity] ADD  DEFAULT ((0.00)) FOR [ExpressBeavyPrice]
GO
ALTER TABLE [dbo].[SysChinaCity] ADD  DEFAULT ((0.00)) FOR [SfUnitPrice]
GO
ALTER TABLE [dbo].[SysChinaCity] ADD  DEFAULT ((0.00)) FOR [SflogisticsBeavyPrice]
GO
ALTER TABLE [dbo].[SysChinaCity] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[SysChinaCity] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[SysCustomerInfo] ADD  DEFAULT ((0)) FOR [UserId]
GO
ALTER TABLE [dbo].[SysCustomerInfo] ADD  DEFAULT ('') FOR [CustomerID]
GO
ALTER TABLE [dbo].[SysCustomerInfo] ADD  DEFAULT ((0)) FOR [CityId]
GO
ALTER TABLE [dbo].[SysCustomerInfo] ADD  DEFAULT ('') FOR [Phone]
GO
ALTER TABLE [dbo].[SysCustomerInfo] ADD  DEFAULT ('') FOR [Address]
GO
ALTER TABLE [dbo].[SysCustomerInfo] ADD  DEFAULT ('') FOR [WebChatNo]
GO
ALTER TABLE [dbo].[SysCustomerInfo] ADD  DEFAULT ('') FOR [QQNumber]
GO
ALTER TABLE [dbo].[SysCustomerInfo] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[SysCustomerInfo] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[SysGoodsType] ADD  DEFAULT ('') FOR [GoodsType]
GO
ALTER TABLE [dbo].[SysGoodsType] ADD  DEFAULT ('') FOR [GoodsTypeDesc]
GO
ALTER TABLE [dbo].[SysGoodsType] ADD  DEFAULT ((0.00)) FOR [MinWeight]
GO
ALTER TABLE [dbo].[SysGoodsType] ADD  DEFAULT ((0.00)) FOR [PremiumAmount]
GO
ALTER TABLE [dbo].[SysGoodsType] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[SysGoodsType] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[SysKuaiDiCom] ADD  DEFAULT ('') FOR [ComSn]
GO
ALTER TABLE [dbo].[SysKuaiDiCom] ADD  DEFAULT ('') FOR [ComName]
GO
ALTER TABLE [dbo].[SysKuaiDiCom] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[SysKuaiDiCom] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[SysLogisticsInfo] ADD  DEFAULT ('') FOR [LogisticsSingle]
GO
ALTER TABLE [dbo].[SysLogisticsInfo] ADD  DEFAULT ('') FOR [LogisticsDesc]
GO
ALTER TABLE [dbo].[SysLogisticsInfo] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[SysLogisticsInfo] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[SysLogisticsInfo] ADD  CONSTRAINT [DF_SysLogisticsInfo_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[SysLogisticsInfo] ADD  CONSTRAINT [DF_SysLogisticsInfo_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
ALTER TABLE [dbo].[SysLogisticsInfo] ADD  DEFAULT ('') FOR [OrderNos]
GO
ALTER TABLE [dbo].[SysLogisticsInfo] ADD  DEFAULT ('') FOR [UserName]
GO
ALTER TABLE [dbo].[SysOrderInfo] ADD  DEFAULT ('') FOR [OrderNo]
GO
ALTER TABLE [dbo].[SysOrderInfo] ADD  DEFAULT ('') FOR [ShipperName]
GO
ALTER TABLE [dbo].[SysOrderInfo] ADD  DEFAULT ('') FOR [ShipperPhone]
GO
ALTER TABLE [dbo].[SysOrderInfo] ADD  DEFAULT ((0)) FOR [UserId]
GO
ALTER TABLE [dbo].[SysOrderInfo] ADD  DEFAULT ((0)) FOR [PickupNumber]
GO
ALTER TABLE [dbo].[SysOrderInfo] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[SysOrderInfo] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[SysOrderInfo] ADD  DEFAULT ((0)) FOR [PayStatus]
GO
ALTER TABLE [dbo].[SysOrderInfo] ADD  DEFAULT ((0)) FOR [OrderRealPrice]
GO
ALTER TABLE [dbo].[SysOrderNumber] ADD  DEFAULT ((0)) FOR [OrderId]
GO
ALTER TABLE [dbo].[SysOrderNumber] ADD  DEFAULT ('') FOR [Number]
GO
ALTER TABLE [dbo].[SysOrderNumber] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[SysOrderNumber] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[SysOrderNumber] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[SysOrderPayInfo] ADD  DEFAULT ((0)) FOR [OrderId]
GO
ALTER TABLE [dbo].[SysOrderPayInfo] ADD  DEFAULT ('') FOR [CardNumber]
GO
ALTER TABLE [dbo].[SysOrderPayInfo] ADD  DEFAULT ('') FOR [PayUserName]
GO
ALTER TABLE [dbo].[SysOrderPayInfo] ADD  DEFAULT ((0)) FOR [PayAmount]
GO
ALTER TABLE [dbo].[SysOrderPayInfo] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[SysOrderPayInfo] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[SysOrderPayInfo] ADD  DEFAULT ((0)) FOR [Type]
GO
ALTER TABLE [dbo].[SysReceiverInfo] ADD  DEFAULT ((0)) FOR [OrderId]
GO
ALTER TABLE [dbo].[SysReceiverInfo] ADD  DEFAULT ((0)) FOR [ChinaCityId]
GO
ALTER TABLE [dbo].[SysReceiverInfo] ADD  DEFAULT ('') FOR [ChinaAddress]
GO
ALTER TABLE [dbo].[SysReceiverInfo] ADD  DEFAULT ('') FOR [ReceiverName]
GO
ALTER TABLE [dbo].[SysReceiverInfo] ADD  DEFAULT ('') FOR [ReceiverPhone]
GO
ALTER TABLE [dbo].[SysReceiverInfo] ADD  DEFAULT ((0)) FOR [PackagingWay]
GO
ALTER TABLE [dbo].[SysReceiverInfo] ADD  DEFAULT ((0)) FOR [ExpressWay]
GO
ALTER TABLE [dbo].[SysReceiverInfo] ADD  DEFAULT ('') FOR [GoodsDesc]
GO
ALTER TABLE [dbo].[SysReceiverInfo] ADD  DEFAULT ((0.00)) FOR [ParcelWeight]
GO
ALTER TABLE [dbo].[SysReceiverInfo] ADD  DEFAULT ('') FOR [ChinaCourierNumber]
GO
ALTER TABLE [dbo].[SysReceiverInfo] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[SysReceiverInfo] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[SysReceiverInfo] ADD  DEFAULT ((0)) FOR [CourierFees]
GO
ALTER TABLE [dbo].[SysReceiverInfo] ADD  DEFAULT ((0)) FOR [CourierComId]
GO
ALTER TABLE [dbo].[SysRussiaCity] ADD  DEFAULT ('') FOR [CityName]
GO
ALTER TABLE [dbo].[SysRussiaCity] ADD  DEFAULT ('') FOR [CityDesc]
GO
ALTER TABLE [dbo].[SysRussiaCity] ADD  DEFAULT ('') FOR [LandTransportTime]
GO
ALTER TABLE [dbo].[SysRussiaCity] ADD  DEFAULT ('') FOR [AirTransportTime]
GO
ALTER TABLE [dbo].[SysRussiaCity] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[SysRussiaCity] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[SysUnitPrice] ADD  DEFAULT ((0)) FOR [RCityId]
GO
ALTER TABLE [dbo].[SysUnitPrice] ADD  DEFAULT ((0.00)) FOR [LandPrice1]
GO
ALTER TABLE [dbo].[SysUnitPrice] ADD  DEFAULT ((0.00)) FOR [LandPrice2]
GO
ALTER TABLE [dbo].[SysUnitPrice] ADD  DEFAULT ((0.00)) FOR [AirPrice1]
GO
ALTER TABLE [dbo].[SysUnitPrice] ADD  DEFAULT ((0.00)) FOR [AirPrice2]
GO
ALTER TABLE [dbo].[SysUnitPrice] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[SysUnitPrice] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[SysUnitPrice] ADD  DEFAULT ((0)) FOR [GoodsTypeId]
GO
ALTER TABLE [dbo].[SysUser] ADD  CONSTRAINT [DF_SysUser_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[SysUser] ADD  CONSTRAINT [DF_SysUser_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[SysUser] ADD  DEFAULT ((0)) FOR [isdelete]
GO
ALTER TABLE [dbo].[SysUserAcitonLog] ADD  DEFAULT ((0)) FOR [UserId]
GO
ALTER TABLE [dbo].[SysUserAcitonLog] ADD  DEFAULT ('') FOR [Context]
GO
ALTER TABLE [dbo].[SysUserAcitonLog] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[SysUserAcitonLog] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  StoredProcedure [dbo].[CreateOrder_Proc]    Script Date: 2017/4/16 21:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO










--创建下单存储过程

CREATE proc [dbo].[CreateOrder_Proc](
    @OrderNo varchar(50),
    @ShipperName [nvarchar](50),
	@ShipperPhone [varchar](50),
	@UserName varchar(50),
	@OrderRealPrice decimal(18, 2),
	@Status [int],
	@LogisticsSingle varchar(100),
    @RussiaCityId bigint,
    @RussiaAddress nvarchar(500),
    @PickupDate datetime,
    @GoodsType int,
    @TransportationWay int,
    @ProtectPrice decimal(18, 2),
    @PolicyFee decimal(18, 2),
    @GoodsWeight decimal(18,2),
	@OrderFrees decimal(18,2),
	@IsArrivePay bit,
	@ArrivePayValue decimal(18,2),
	@IsOutPhoto bit,
	@ExchangeRate decimal(18,2),
	@WebUrl varchar(200),
	@ChinaCityId bigint,
	@ChinaAddress nvarchar(500),
	@ReceiverName nvarchar(100),
	@ReceiverPhone varchar(50),
	@PackagingWay int,
	@ExpressWay int,
	@GoodsDesc nvarchar(2000),
	@ParcelWeight decimal(18, 2),
	@ChinaCourierNumber varchar(50),
	@Desc nvarchar(MAX),
	@CourierFees decimal(18, 2),
	@CourierComId bigint,
    @result nvarchar(100) output
)
as  
begin

  declare @error int
  declare @status_ bigint
  begin tran T
  begin try
    declare @OrderId bigint 
    declare @UserId bigint
	
	 set @status_=0
	--查询用户是否存在
	if exists(select * from [dbo].[SysUser] with(nolock) where [UserName]=@UserName and ([RuleType]='Admin' or [RuleType]='Customer') and [Status]=1)
	begin 

	   select @UserId=[Id] from [dbo].[SysUser] with(nolock) where [UserName]=@UserName and ([RuleType]='Admin' or [RuleType]='Customer') and [Status]=1
	   
	   if exists(select * from [dbo].[SysOrderInfo] with(nolock) where [OrderNo]=@OrderNo)
	   begin 
	       set @result='订单编号：'+@OrderNo+'已存在'
	       set @status_= 0
	   end
	   else
	   begin
	       --插入主订单表
	       insert into [dbo].[SysOrderInfo]([OrderNo],[ShipperName],[ShipperPhone],[UserId],[Status],[OrderRealPrice])
		   values(@OrderNo,@ShipperName,@ShipperPhone,@UserId,1,@OrderRealPrice) 
		   set @error=@error+@@ERROR

		   select @OrderId=SCOPE_IDENTITY()
		   

		   INSERT INTO [dbo].[SysAddresserInfo]
           ([OrderId]
           ,[LogisticsSingle]
           ,[RussiaCityId]
           ,[RussiaAddress]
           ,[PickupDate]
           ,[GoodsType]
           ,[TransportationWay]
           ,[ProtectPrice]
           ,[PolicyFee]
           ,[GoodsWeight]
           ,[OrderFrees]
           ,[IsArrivePay]
           ,[ArrivePayValue]
           ,[IsOutPhoto]
           ,[ExchangeRate]
		   ,WebUrl)
            VALUES
           (@OrderId
           ,@LogisticsSingle
           ,@RussiaCityId
           ,@RussiaAddress
           ,@PickupDate
           ,@GoodsType
           ,@TransportationWay
           ,@ProtectPrice
           ,@PolicyFee
           ,@GoodsWeight
		   ,@OrderFrees
		   ,@IsArrivePay
		   ,@ArrivePayValue
		   ,@IsOutPhoto
		   ,@ExchangeRate
		   ,@WebUrl
            )
			set @error=@error+@@ERROR
            -- 操作
            insert into [dbo].[SysReceiverInfo](
			 [OrderId]
           ,[ChinaCityId]
           ,[ChinaAddress]
           ,[ReceiverName]
           ,[ReceiverPhone]
           ,[PackagingWay]
           ,[ExpressWay]
           ,[GoodsDesc]
           ,[ParcelWeight]
           ,[ChinaCourierNumber]
           ,[Desc]
           ,[CourierFees]
           ,[CourierComId]) values(
		    @OrderId
           ,@ChinaCityId
           ,@ChinaAddress
           ,@ReceiverName
           ,@ReceiverPhone
           ,@PackagingWay
           ,@ExpressWay
           ,@GoodsDesc
           ,@ParcelWeight
           ,@ChinaCourierNumber
           ,@Desc
		   ,@CourierFees
		    ,@CourierComId)

			set @error=@error+@@ERROR
          
		   
            set @result='Success'
            select  @status_=@OrderId
	   end
	end
	else
	begin
	     set @result='你没有下单权限'
	     set @status_=0
	end
  end try
  begin catch
    set @result= Error_message()
	select @status_=0
  end catch

  if @error<>0
  begin
     rollback tran T --如果成功Lives表中，将会有3条数据。
  end
  else
  begin
     commit tran T
  end

  print @status_
  return @status_
end











GO
USE [master]
GO
ALTER DATABASE [7077SysDB1] SET  READ_WRITE 
GO
