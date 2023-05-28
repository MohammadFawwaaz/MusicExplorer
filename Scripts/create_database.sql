IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'MusicExplorer')
BEGIN
    CREATE DATABASE [MusicExplorer];
END
GO
