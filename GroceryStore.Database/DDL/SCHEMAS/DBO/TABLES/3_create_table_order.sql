IF OBJECT_ID('[dbo].[Order]', 'U') IS NOT NULL
BEGIN
	DROP TABLE [dbo].[Order]	
END
GO

CREATE TABLE [dbo].[Order] (
	[OrderId] [uniqueidentifier] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[OrderNumber] [int] IDENTITY(1, 1) NOT NULL,
	[TotalToPay] [decimal](18, 2) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL
	CONSTRAINT [PKOrder] PRIMARY KEY CLUSTERED ([OrderId] ASC)
	WITH (PAD_INDEX = OFF,
		  STATISTICS_NORECOMPUTE = OFF,
		  IGNORE_DUP_KEY = OFF,
		  ALLOW_ROW_LOCKS = ON,
		  ALLOW_PAGE_LOCKS = ON,
		  OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
	CONSTRAINT [FKOrderUser] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
) ON [PRIMARY]
GO
