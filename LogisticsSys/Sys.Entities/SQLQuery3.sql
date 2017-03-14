USE [7077SysDB]
GO

/****** Object:  StoredProcedure [dbo].[CreateOrder_Proc]    Script Date: 2017/2/26 23:41:43 ******/
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
	@PickupNumber int,
	@Status [int],
	@LogisticsSingle varchar(100),
    @RussiaCityId bigint,
    @RussiaAddress nvarchar(500),
    @CargoNumber int,
    @PickupDate datetime,
    @PickupWay int,
    @GoodsType int,
    @TransportationWay int,
    @ProtectPrice decimal(18, 2),
    @PolicyFee decimal(18, 2),
    @GoodsWeight decimal(18,2),
    @BoxLong decimal(18,2),
    @BoxWidth decimal(18,2),
    @BoxHeight decimal(18,2),
	@ParcelSingle varchar(100),
    @ChinaCityId bigint,
    @ChinaAddress nvarchar(500),
    @ReceiverName nvarchar(100),
    @ReceiverPhone varchar(50),
    @PackagingWay int,
    @ExpressWay int,
    @GoodsDesc nvarchar(2000), 
    @ParcelWeight decimal(18,2),
    @ChinaCourierNumber varchar(50),
	@Desc nvarchar(max),
    @result nvarchar(100) output
)
as  
begin

  declare @error int
  declare @status_ int
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
	       insert into [dbo].[SysOrderInfo]([OrderNo],[ShipperName],[ShipperPhone],[UserId],[PickupNumber],[Status])
		   values(@OrderNo,@ShipperName,@ShipperPhone,@UserId,@PickupNumber,1) 
		   set @error=@error+@@ERROR

		   select @OrderId=SCOPE_IDENTITY()

		   INSERT INTO [dbo].[SysAddresserInfo]
           ([OrderId]
           ,[LogisticsSingle]
           ,[RussiaCityId]
           ,[RussiaAddress]
           ,[CargoNumber]
           ,[PickupDate]
           ,[PickupWay]
           ,[GoodsType]
           ,[TransportationWay]
           ,[ProtectPrice]
           ,[PolicyFee]
           ,[GoodsWeight]
           ,[BoxLong]
           ,[BoxWidth]
           ,[BoxHeight])
            VALUES
           (@OrderId
           ,@LogisticsSingle
           ,@RussiaCityId
           ,@RussiaAddress
           ,@CargoNumber
           ,@PickupDate
           ,@PickupWay
           ,@GoodsType
           ,@TransportationWay
           ,@ProtectPrice
           ,@PolicyFee
           ,@GoodsWeight
           ,@BoxLong
           ,@BoxWidth
           ,@BoxHeight
		    )
			set @error=@error+@@ERROR
			INSERT INTO [dbo].[SysReceiverInfo]
           ([OrderId]
           ,[ParcelSingle]
           ,[ChinaCityId]
           ,[ChinaAddress]
           ,[ReceiverName]
           ,[ReceiverPhone]
           ,[PackagingWay]
           ,[ExpressWay]
           ,[GoodsDesc]
           ,[ParcelWeight]
           ,[ChinaCourierNumber]
           ,[Desc])
            VALUES
           (@OrderId
           ,@ParcelSingle
           ,@ChinaCityId
           ,@ChinaAddress
           ,@ReceiverName
           ,@ReceiverPhone
           ,@PackagingWay
           ,@ExpressWay
           ,@GoodsDesc
           ,@ParcelWeight
           ,@ChinaCourierNumber
           ,@Desc)
		    set @error=@error+@@ERROR
		   
            set @result='Success'
            select @status_=1
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


