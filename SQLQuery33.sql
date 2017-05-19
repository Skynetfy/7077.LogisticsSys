  alter table [7077SysDB].[dbo].[SysReceiverInfo]
  add DomesticCost decimal(18,2) default 0 not null
/****** Object:  View [dbo].[v_orderinfo]    Script Date: 2017/5/19 18:06:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO










ALTER view [dbo].[v_orderinfo]
as	select A.[Id]
      ,A.[OrderNo]
      ,A.[ShipperName]
      ,A.[ShipperPhone]
      ,A.[UserId]
	  ,A.OrderRealPrice
      ,A.[Status]
	  ,A.PayStatus
	   ,A.[ArrivePayStatus]
      ,A.[WorldPayStatus]
      ,A.[ChinaPayStatus]
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
	  ,B.insurancecost
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
	   ,C.[PackagingCosts]
	   ,C.DomesticCost
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


