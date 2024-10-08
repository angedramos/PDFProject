IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'PDFSignatureDatabase')
BEGIN
    CREATE DATABASE [PDFSignatureDatabase]
END
GO
USE [PDFSignatureDatabase]
GO
/****** Object:  Table [dbo].[PdfDocuments]    Script Date: 19/09/2024 10:54:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PdfDocuments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](255) NOT NULL,
	[FileContent] [varbinary](max) NOT NULL,
	[IsSigned] [bit] NOT NULL,
	[SignerName] [nvarchar](255) NULL,
	[SignedDate] [datetime2](7) NULL,
	[SignatureImage] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
