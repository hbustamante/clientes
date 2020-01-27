/*
Script de implementación para InterCorp

Una herramienta generó este código.
Los cambios realizados en este archivo podrían generar un comportamiento incorrecto y se perderán si
se vuelve a generar el código.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "InterCorp"
:setvar DefaultFilePrefix "InterCorp"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detectar el modo SQLCMD y deshabilitar la ejecución del script si no se admite el modo SQLCMD.
Para volver a habilitar el script después de habilitar el modo SQLCMD, ejecute lo siguiente:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'El modo SQLCMD debe estar habilitado para ejecutar correctamente este script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Iniciando recompilación de la tabla [dbo].[Clientes]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Clientes] (
    [Id]                BIGINT         IDENTITY (1, 1) NOT NULL,
    [Nombre]            NVARCHAR (MAX) NOT NULL,
    [Apellido]          NVARCHAR (MAX) NOT NULL,
    [FechaDeNacimiento] DATETIME       NULL,
    [Created]           DATETIME2 (7)  NOT NULL,
    [Modified]          DATETIME2 (7)  NULL,
    [Deleted]           DATETIME2 (7)  NULL,
    [IsDeleted]         BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Clientes])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Clientes] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Clientes] ([Id], [Nombre], [Apellido], [FechaDeNacimiento], [Created], [Modified], [Deleted], [IsDeleted])
        SELECT   [Id],
                 [Nombre],
                 [Apellido],
                 [FechaDeNacimiento],
                 [Created],
                 [Modified],
                 [Deleted],
                 [IsDeleted]
        FROM     [dbo].[Clientes]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Clientes] OFF;
    END

DROP TABLE [dbo].[Clientes];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Clientes]', N'Clientes';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Actualización completada.';


GO
