USE [DatongDatabase]
GO
/****** Object:  Table [dbo].[ClassInfo]    Script Date: 2018/6/11 12:08:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Remark] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_ClassInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[ClassInfo] ON 

GO
INSERT [dbo].[ClassInfo] ([Id], [Name], [Remark]) VALUES (1, N'һ��', N'wqe')
GO
INSERT [dbo].[ClassInfo] ([Id], [Name], [Remark]) VALUES (2, N'����', N'wef')
GO
INSERT [dbo].[ClassInfo] ([Id], [Name], [Remark]) VALUES (3, N'����', N'we')
GO
INSERT [dbo].[ClassInfo] ([Id], [Name], [Remark]) VALUES (4, N'�İ�', N'jhgf')
GO
INSERT [dbo].[ClassInfo] ([Id], [Name], [Remark]) VALUES (5, N'����', N'��Ȥζ��')
GO
INSERT [dbo].[ClassInfo] ([Id], [Name], [Remark]) VALUES (6, N'����', N'��Ȥζ��')
GO
SET IDENTITY_INSERT [dbo].[ClassInfo] OFF
GO
