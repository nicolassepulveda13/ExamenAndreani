USE [master]
GO
CREATE DATABASE [Prueba_Andreani]
GO

USE [Prueba_Andreani]
GO

/****** Object:  Table [dbo].[Pedido]    Script Date: 14/7/2020 19:12:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Pedido](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[calle] [nvarchar](50) NOT NULL,
	[numero] [nvarchar](50) NOT NULL,
	[ciudad] [nvarchar](50) NOT NULL,
	[provincia] [nvarchar](50) NOT NULL,
	[pais] [nvarchar](50) NOT NULL,
	[latitud] [nvarchar](100) NULL,
	[longitud] [nvarchar](100) NULL,
 CONSTRAINT [PK_Pedido] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


