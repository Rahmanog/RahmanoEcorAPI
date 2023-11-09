# Create Table For RahmanoDB
USE [RahmanoDB]
GO

/****** Object:  Table [dbo].[ApplicationType]    Script Date: 09/11/2023 21:53:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ApplicationType](
	[TypeId] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [varchar](50) NOT NULL,
	[TypeDescripton] [varchar](max) NULL,
	[Is_Active] [char](1) NOT NULL,
 CONSTRAINT [PK_ApplicationType] PRIMARY KEY CLUSTERED 
(
	[TypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Category]    Script Date: 09/11/2023 21:53:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](150) NOT NULL,
	[CategoryDescription] [nvarchar](max) NULL,
	[DisplayOrder] [int] NOT NULL,
	[Is_Active] [char](1) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Product]    Script Date: 09/11/2023 21:53:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[ShortDesc] [varchar](150) NULL,
	[Description] [varchar](max) NULL,
	[Price] [float] NULL,
	[ImageUrl] [varchar](max) NULL,
	[CategoryId] [int] NULL,
	[ApplicationTypeId] [int] NULL,
	[Is_Active] [char](1) NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ProductRequest]    Script Date: 09/11/2023 21:53:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProductRequest](
	[RequestId] [int] IDENTITY(1,1) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[Phone] [varchar](50) NULL,
	[RequestDate] [smalldatetime] NOT NULL,
	[UserId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Is_Active] [char](1) NOT NULL,
 CONSTRAINT [PK_Requestation] PRIMARY KEY CLUSTERED 
(
	[RequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[User]    Script Date: 09/11/2023 21:53:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](150) NOT NULL,
	[UserEmail] [varchar](150) NOT NULL,
	[UserPassword] [varchar](75) NOT NULL,
	[UserRole] [varchar](10) NULL,
	[Is_Active] [char](1) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ApplicationType] ADD  CONSTRAINT [DF_ApplicationType_Name]  DEFAULT ('') FOR [TypeName]
GO

ALTER TABLE [dbo].[ApplicationType] ADD  CONSTRAINT [DF_ApplicationType_Is_Active]  DEFAULT ((1)) FOR [Is_Active]
GO

ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_CategoryName]  DEFAULT ('') FOR [CategoryName]
GO

ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_CategoryDescription]  DEFAULT ('') FOR [CategoryDescription]
GO

ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_DisplayOrder]  DEFAULT ((0)) FOR [DisplayOrder]
GO

ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_IsActive]  DEFAULT ((1)) FOR [Is_Active]
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Name]  DEFAULT ('') FOR [Name]
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_ShortDesc]  DEFAULT ('') FOR [ShortDesc]
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Description]  DEFAULT ('') FOR [Description]
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Price]  DEFAULT ((0)) FOR [Price]
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_CategoryId]  DEFAULT ((0)) FOR [CategoryId]
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_ApplicationId]  DEFAULT ((0)) FOR [ApplicationTypeId]
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Is_Active]  DEFAULT ((1)) FOR [Is_Active]
GO

ALTER TABLE [dbo].[ProductRequest] ADD  CONSTRAINT [DF_ProductRequest_Quantity]  DEFAULT ((0)) FOR [Quantity]
GO

ALTER TABLE [dbo].[ProductRequest] ADD  CONSTRAINT [DF_ProductRequest_Price]  DEFAULT ((0)) FOR [Price]
GO

ALTER TABLE [dbo].[ProductRequest] ADD  CONSTRAINT [DF_ProductRequest_RequestDate]  DEFAULT (getdate()) FOR [RequestDate]
GO

ALTER TABLE [dbo].[ProductRequest] ADD  CONSTRAINT [DF_ProductRequest_UserId]  DEFAULT ((0)) FOR [UserId]
GO

ALTER TABLE [dbo].[ProductRequest] ADD  CONSTRAINT [DF_ProductRequest_ProductId]  DEFAULT ((0)) FOR [ProductId]
GO

ALTER TABLE [dbo].[ProductRequest] ADD  CONSTRAINT [DF_ProductRequest_Is_Active]  DEFAULT ((1)) FOR [Is_Active]
GO

ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Is_Active]  DEFAULT ((1)) FOR [Is_Active]
GO


