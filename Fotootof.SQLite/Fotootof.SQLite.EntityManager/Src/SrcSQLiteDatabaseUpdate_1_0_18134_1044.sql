PRAGMA foreign_keys=off;
PRAGMA temp_store = 2;

/** ********************************************************************
 * ALBUMS TABLE CHANGED
 */
DROP TABLE IF EXISTS [Albums_Temp];
 
ALTER TABLE [Albums] RENAME TO [Albums_Temp];

CREATE TABLE IF NOT EXISTS [Albums] (
	[AlbumId] integer PRIMARY KEY AUTOINCREMENT NOT NULL,

	[Name] VARCHAR(64) DEFAULT '' NOT NULL,
	[Alias] VARCHAR(64) DEFAULT '' NOT NULL,
	[Description] TEXT DEFAULT '' NOT NULL,

	[Ordering] INTEGER DEFAULT '0' NOT NULL,

	[Created] TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	[Modified] TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,

	[DateStart] TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	[DateEnd] TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,

	[backgroundPictureId] INTEGER DEFAULT '0' NOT NULL,
	[previewPictureId] INTEGER DEFAULT '0' NOT NULL,
	[thumbnailPictureId] INTEGER DEFAULT '0' NOT NULL,

	[Comment] TEXT DEFAULT '' NOT NULL,
	[Parameters] TEXT DEFAULT '' NOT NULL
);


INSERT INTO [Albums]
('AlbumId', 'Name', 'Alias', 'Description', 'Ordering', 'Created', 'Modified', 'DateStart', 'DateEnd', 'Comment')
SELECT
[AlbumId], [Name], [Alias], [Description], [Ordering], [Created], [Modified], [DateStart], [DateEnd], [Comment]
FROM [Albums_Temp];
 
DROP TABLE [Albums_Temp];
 
PRAGMA foreign_keys=on;

REPLACE INTO [Versions]
(VersionId, AssemblyVersionMin, AssemblyVersionMax, Comment)
VALUES
(1, '1.0.18123.0000', '', 'Database first creation.'),
(2, '1.0.18123.2149', '', 'Database Sections shema changed. Sections pictures are associated by Primary Keys.'),
(3, '1.0.18134.1044', '', 'Database Albums shema changed. Albums pictures are associated by Primary Keys.');