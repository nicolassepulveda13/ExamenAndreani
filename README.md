# ExamenAndreani

Prueba tecnica -andreani

Una vez que se corra el docker-compose up

si la base de datos no esta persisitida :

Ejecutar :

/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P Pass@word

USE [master] GO CREATE DATABASE [Prueba_Andreani] GO

CREATE TABLE [dbo].[Pedido]( [Id] [int] IDENTITY(1,1) NOT NULL, [calle] nvarchar NOT NULL, [numero] nvarchar NOT NULL, [ciudad] nvarchar NOT NULL, [provincia] nvarchar NOT NULL, [pais] nvarchar NOT NULL,[latitud] nvarchar NULL, [longitud] nvarchar NULL, CONSTRAINT [PK_Pedido] PRIMARY KEY CLUSTERED
( [Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] GO
