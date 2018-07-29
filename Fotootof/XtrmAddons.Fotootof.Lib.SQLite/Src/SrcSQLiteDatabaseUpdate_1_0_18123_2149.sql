PRAGMA foreign_keys=off;
PRAGMA temp_store = 2;

/** ********************************************************************
 * SECTIONS TABLE CHANGED
 */
DROP TABLE IF EXISTS [Sections_Temp];
 
ALTER TABLE [Sections] RENAME TO [Sections_Temp];

CREATE TABLE IF NOT EXISTS [Sections] (
	[SectionId] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,

	[Name] VARCHAR(64)  NOT NULL DEFAULT '' NOT NULL,
	[Alias] VARCHAR(64) DEFAULT '' NOT NULL,
	[Description] TEXT DEFAULT '' NOT NULL,

	[IsDefault] BOOLEAN DEFAULT '0' NOT NULL,
	[Ordering] INTEGER DEFAULT '0' NOT NULL,

	[backgroundPictureId] INTEGER DEFAULT '0' NOT NULL,
	[previewPictureId] INTEGER DEFAULT '0' NOT NULL,
	[thumbnailPictureId] INTEGER DEFAULT '0' NOT NULL,

	[Comment] TEXT DEFAULT '' NOT NULL,
	[Parameters] TEXT DEFAULT '' NOT NULL
);
 
INSERT INTO [Sections]
('SectionId', 'Name', 'Alias', 'Description', 'IsDefault', 'Ordering', 'Comment')
SELECT
[SectionId], [Name], [Alias], [Description], [IsDefault], [Ordering], [Comment]
FROM [Sections_Temp];
 
DROP TABLE [Sections_Temp];
 
PRAGMA foreign_keys=on;


/** ********************************************************************
 * VERSIONS
 */
CREATE TABLE IF NOT EXISTS `Versions` (
	`VersionId`	INTEGER NOT NULL,
	`AssemblyVersionMin`	TEXT NOT NULL DEFAULT '',
	`AssemblyVersionMax`	TEXT NOT NULL DEFAULT '',
	`Comment`	TEXT NOT NULL DEFAULT '',
	PRIMARY KEY(`VersionId`)
);

REPLACE INTO [Versions]
(VersionId, AssemblyVersionMin, AssemblyVersionMax, Comment)
VALUES
(1, '1.0.18123.2149', '', 'Database first create.');