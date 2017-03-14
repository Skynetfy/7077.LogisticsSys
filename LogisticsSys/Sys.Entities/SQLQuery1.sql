
alter view v_orderinfo
as	select A.[Id]
      ,A.[OrderNo]
      ,A.[ShipperName]
      ,A.[ShipperPhone]
      ,A.[UserId]
      ,A.[PickupNumber]
      ,A.[Status]
	  ,D.UserName
	  ,D.DisplayName
	  ,B.[LogisticsSingle]
      ,B.[RussiaCityId]
	  ,F.CityName as RussiaCityName
      ,B.[RussiaAddress]
      ,B.[CargoNumber]
      ,B.[PickupDate]
      ,B.[PickupWay]
      ,B.[GoodsType]
      ,B.[TransportationWay]
      ,B.[ProtectPrice]
      ,B.[PolicyFee]
      ,B.[GoodsWeight]
      ,B.[BoxLong]
      ,B.[BoxWidth]
      ,B.[BoxHeight]
	  ,C.[ParcelSingle]
      ,C.[ChinaCityId]
      ,C.[ChinaAddress]
      ,C.[ReceiverName]
      ,C.[ReceiverPhone]
      ,C.[PackagingWay]
      ,C.[ExpressWay]
      ,C.[GoodsDesc]
      ,C.[ParcelWeight]
      ,C.[ChinaCourierNumber]
	  ,C.[Desc]
      ,A.[CreateDate]
      ,A.[IsDelete]
from [dbo].[SysOrderInfo] A
inner join [dbo].[SysAddresserInfo] B on A.Id=B.OrderId
inner join [dbo].[SysReceiverInfo] C on A.Id=C.OrderId
left join [dbo].[SysUser] D on A.UserId=D.Id
left join [dbo].[SysRussiaCity] F on F.Id=B.[RussiaCityId]
where A.IsDelete=0 and C.IsDelete=0 and B.IsDelete=0 