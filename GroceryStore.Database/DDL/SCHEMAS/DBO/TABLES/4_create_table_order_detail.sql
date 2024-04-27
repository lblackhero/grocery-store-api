IF OBJECT_ID('[dbo].[OrderDetail]', 'U') IS NOT NULL
BEGIN
	DROP TABLE [dbo].[OrderDetail]	
END
GO

CREATE TABLE [dbo].[OrderDetail] (
	[OrderId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Total] [decimal] (18, 2) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL
	CONSTRAINT [PKOrderDetail] PRIMARY KEY CLUSTERED (
		[OrderId] ASC,
		[ProductId]
	)
	WITH (PAD_INDEX = OFF,
		STATISTICS_NORECOMPUTE = OFF,
		IGNORE_DUP_KEY = OFF,
		ALLOW_ROW_LOCKS = ON,
		ALLOW_PAGE_LOCKS = ON,
		OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
) ON [PRIMARY]
GO
