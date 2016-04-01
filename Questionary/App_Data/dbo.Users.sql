CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Login] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(50) NOT NULL, 
    [Name] NVARCHAR(50) NULL, 
    [Surname] NVARCHAR(50) NULL, 
    [Country] NVARCHAR(50) NULL, 
    [Town] NVARCHAR(50) NULL, 
    [Photo] NVARCHAR(50) NULL, 
    [Song] NVARCHAR(50) NULL
)
