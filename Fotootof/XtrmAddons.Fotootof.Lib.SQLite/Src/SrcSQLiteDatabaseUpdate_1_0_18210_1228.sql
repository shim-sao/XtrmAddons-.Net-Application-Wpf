PRAGMA foreign_keys=off;
PRAGMA temp_store = 2;

CREATE TABLE IF NOT EXISTS `Storages` (
	`StorageId`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Name`	TEXT,
	`FullName`	TEXT
);

CREATE TABLE IF NOT EXISTS `StoragesInAlbums` (
	`StorageId`	INTEGER NOT NULL,
	`AlbumId`	INTEGER NOT NULL,
	CONSTRAINT `FK_StoragesInAlbums_Storages_StorageId` FOREIGN KEY(`StorageId`) REFERENCES `Storages`(`StorageId`) ON DELETE CASCADE,
	CONSTRAINT `PK_StoragesInAlbums` PRIMARY KEY(`StorageId`,`AlBumId`),
	CONSTRAINT `FK_StoragesInAlbums_AlBums_AlBumId` FOREIGN KEY(`AlBumId`) REFERENCES `AlBums`(`AlBumId`) ON DELETE CASCADE
);
 
PRAGMA foreign_keys=on;