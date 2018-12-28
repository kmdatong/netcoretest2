

CREATE TABLE [dbo].[FileMgmt](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Url] [nvarchar](500) NOT NULL,
	[Length] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Guid] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_FileMgmt] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


