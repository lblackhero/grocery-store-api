IF OBJECT_ID('dbo.Product', 'U') IS NOT NULL
BEGIN
	DROP TABLE dbo.Product
END
GO

CREATE TABLE [dbo].[Product] (
	[ProductId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	CONSTRAINT [PKProducts] PRIMARY KEY CLUSTERED ( [ProductId] ASC)
	WITH (PAD_INDEX = OFF,
			STATISTICS_NORECOMPUTE = OFF,
			IGNORE_DUP_KEY = OFF,
			ALLOW_ROW_LOCKS = ON,
			ALLOW_PAGE_LOCKS = ON,
			OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
