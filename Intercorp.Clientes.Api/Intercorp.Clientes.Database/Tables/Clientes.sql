CREATE TABLE [dbo].[Clientes]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Nombre] NVARCHAR(MAX) NOT NULL, 
    [Apellido] NVARCHAR(MAX) NOT NULL, 
    [FechaDeNacimiento] DATETIME NULL, 
    [Created] DATETIME2 NOT NULL, 
    [Modified] DATETIME2 NULL, 
    [Deleted] DATETIME2 NULL, 
    [IsDeleted] BIT NOT NULL
)
