USE [7077SysDB]
GO

/****** Object:  Table [dbo].[SysUser]    Script Date: 2017/2/24 0:21:50 ******/
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
 CONSTRAINT [PK_SysUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SysUser] ADD  CONSTRAINT [DF_SysUser_Status]  DEFAULT ((0)) FOR [Status]
GO

ALTER TABLE [dbo].[SysUser] ADD  CONSTRAINT [DF_SysUser_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO


