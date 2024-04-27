IF OBJECT_ID('[dbo].[Stock]', 'U') IS NOT NULL
BEGIN
	DROP TABLE [dbo].[Stock]
END
GO

CREATE TABLE [dbo].[Stock](
	[ProductId] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NOT NULL,
	[IsAvailable] [bit] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL
	CONSTRAINT [FKStock] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([ProductId])
) ON [PRIMARY]
GO
