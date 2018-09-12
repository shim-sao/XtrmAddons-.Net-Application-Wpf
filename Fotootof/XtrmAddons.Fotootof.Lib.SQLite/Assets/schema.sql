BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS `Versions` (
	`VersionId`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`AssemblyVersionMin`	TEXT,
	`AssemblyVersionMax`	TEXT,
	`Comment`	TEXT
);
CREATE TABLE IF NOT EXISTS `UsersInAclGroups` (
	`UserId`	INTEGER NOT NULL,
	`AclGroupId`	INTEGER NOT NULL,
	CONSTRAINT `PK_UsersInAclGroups` PRIMARY KEY(`UserId`,`AclGroupId`),
	CONSTRAINT `FK_UsersInAclGroups_AclGroups_AclGroupId` FOREIGN KEY(`AclGroupId`) REFERENCES `AclGroups`(`AclGroupId`) ON DELETE CASCADE,
	CONSTRAINT `FK_UsersInAclGroups_Users_UserId` FOREIGN KEY(`UserId`) REFERENCES `Users`(`UserId`) ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS `Users` (
	`UserId`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Name`	TEXT,
	`Password`	TEXT,
	`Email`	TEXT,
	`Server`	TEXT,
	`Created`	TEXT NOT NULL,
	`Modified`	TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS `SectionsInAclGroups` (
	`SectionId`	INTEGER NOT NULL,
	`AclGroupId`	INTEGER NOT NULL,
	CONSTRAINT `FK_SectionsInAclGroups_AclGroups_AclGroupId` FOREIGN KEY(`AclGroupId`) REFERENCES `AclGroups`(`AclGroupId`) ON DELETE CASCADE,
	CONSTRAINT `PK_SectionsInAclGroups` PRIMARY KEY(`SectionId`,`AclGroupId`),
	CONSTRAINT `FK_SectionsInAclGroups_Sections_SectionId` FOREIGN KEY(`SectionId`) REFERENCES `Sections`(`SectionId`) ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS `Sections` (
	`SectionId`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Name`	TEXT,
	`Alias`	TEXT,
	`Description`	TEXT,
	`IsDefault`	INTEGER NOT NULL,
	`Ordering`	INTEGER NOT NULL,
	`BackgroundPictureId`	INTEGER NOT NULL,
	`PreviewPictureId`	INTEGER NOT NULL,
	`ThumbnailPictureId`	INTEGER NOT NULL,
	`Comment`	TEXT,
	`Parameters`	TEXT
);
CREATE TABLE IF NOT EXISTS `PicturesInAlbums` (
	`PictureId`	INTEGER NOT NULL,
	`AlbumId`	INTEGER NOT NULL,
	`Ordering`	INTEGER NOT NULL,
	CONSTRAINT `PK_PicturesInAlbums` PRIMARY KEY(`PictureId`,`AlbumId`),
	CONSTRAINT `FK_PicturesInAlbums_Albums_AlbumId` FOREIGN KEY(`AlbumId`) REFERENCES `Albums`(`AlbumId`) ON DELETE CASCADE,
	CONSTRAINT `FK_PicturesInAlbums_Pictures_PictureId` FOREIGN KEY(`PictureId`) REFERENCES `Pictures`(`PictureId`) ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS `Pictures` (
	`PictureId`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Name`	TEXT,
	`Alias`	TEXT,
	`Description`	TEXT,
	`Ordering`	INTEGER NOT NULL,
	`Captured`	TEXT NOT NULL,
	`Created`	TEXT NOT NULL,
	`Modified`	TEXT NOT NULL,
	`OriginalPath`	TEXT,
	`OriginalWidth`	INTEGER NOT NULL,
	`OriginalHeight`	INTEGER NOT NULL,
	`OriginalLength`	INTEGER NOT NULL,
	`PicturePath`	TEXT,
	`PictureWidth`	INTEGER NOT NULL,
	`PictureHeight`	INTEGER NOT NULL,
	`PictureLength`	INTEGER NOT NULL,
	`ThumbnailPath`	TEXT,
	`ThumbnailWidth`	INTEGER NOT NULL,
	`ThumbnailHeight`	INTEGER NOT NULL,
	`ThumbnailLength`	INTEGER NOT NULL,
	`Comment`	TEXT
);
CREATE TABLE IF NOT EXISTS `InfosTypes` (
	`InfoTypeId`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Name`	TEXT,
	`Alias`	TEXT,
	`Description`	TEXT,
	`IsDefault`	INTEGER NOT NULL,
	`Ordering`	INTEGER NOT NULL
);
CREATE TABLE IF NOT EXISTS `InfosInPictures` (
	`InfoId`	INTEGER NOT NULL,
	`PictureId`	INTEGER NOT NULL,
	CONSTRAINT `PK_InfosInPictures` PRIMARY KEY(`InfoId`,`PictureId`),
	CONSTRAINT `FK_InfosInPictures_Infos_InfoId` FOREIGN KEY(`InfoId`) REFERENCES `Infos`(`InfoId`) ON DELETE CASCADE,
	CONSTRAINT `FK_InfosInPictures_Pictures_PictureId` FOREIGN KEY(`PictureId`) REFERENCES `Pictures`(`PictureId`) ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS `InfosInAlbums` (
	`InfoId`	INTEGER NOT NULL,
	`AlbumId`	INTEGER NOT NULL,
	CONSTRAINT `FK_InfosInAlbums_Infos_InfoId` FOREIGN KEY(`InfoId`) REFERENCES `Infos`(`InfoId`) ON DELETE CASCADE,
	CONSTRAINT `PK_InfosInAlbums` PRIMARY KEY(`InfoId`,`AlbumId`),
	CONSTRAINT `FK_InfosInAlbums_Albums_AlbumId` FOREIGN KEY(`AlbumId`) REFERENCES `Albums`(`AlbumId`) ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS `Infos` (
	`InfoId`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`InfoTypeId`	INTEGER NOT NULL,
	`Name`	TEXT,
	`Alias`	TEXT,
	`Description`	TEXT,
	`IsDefault`	INTEGER NOT NULL,
	`Ordering`	INTEGER NOT NULL
);
CREATE TABLE IF NOT EXISTS `AlbumsInSections` (
	`AlbumId`	INTEGER NOT NULL,
	`SectionId`	INTEGER NOT NULL,
	`Ordering`	INTEGER NOT NULL,
	CONSTRAINT `FK_AlbumsInSections_Albums_AlbumId` FOREIGN KEY(`AlbumId`) REFERENCES `Albums`(`AlbumId`) ON DELETE CASCADE,
	CONSTRAINT `FK_AlbumsInSections_Sections_SectionId` FOREIGN KEY(`SectionId`) REFERENCES `Sections`(`SectionId`) ON DELETE CASCADE,
	CONSTRAINT `PK_AlbumsInSections` PRIMARY KEY(`AlbumId`,`SectionId`)
);
CREATE TABLE IF NOT EXISTS `Albums` (
	`AlbumId`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Name`	TEXT,
	`Alias`	TEXT,
	`Description`	TEXT,
	`Ordering`	INTEGER NOT NULL,
	`Created`	TEXT NOT NULL,
	`Modified`	TEXT NOT NULL,
	`DateStart`	TEXT NOT NULL,
	`DateEnd`	TEXT NOT NULL,
	`BackgroundPictureId`	INTEGER NOT NULL,
	`PreviewPictureId`	INTEGER NOT NULL,
	`ThumbnailPictureId`	INTEGER NOT NULL,
	`Comment`	TEXT,
	`Parameters`	TEXT
);
CREATE TABLE IF NOT EXISTS `AclGroupsInAclActions` (
	`AclGroupId`	INTEGER NOT NULL,
	`AclActionId`	INTEGER NOT NULL,
	CONSTRAINT `FK_AclGroupsInAclActions_AclGroups_AclGroupId` FOREIGN KEY(`AclGroupId`) REFERENCES `AclGroups`(`AclGroupId`) ON DELETE CASCADE,
	CONSTRAINT `PK_AclGroupsInAclActions` PRIMARY KEY(`AclGroupId`,`AclActionId`),
	CONSTRAINT `FK_AclGroupsInAclActions_AclActions_AclActionId` FOREIGN KEY(`AclActionId`) REFERENCES `AclActions`(`AclActionId`) ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS `AclGroups` (
	`AclGroupId`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Name`	TEXT,
	`Alias`	TEXT,
	`ParentId`	INTEGER NOT NULL,
	`IsDefault`	INTEGER NOT NULL,
	`Ordering`	INTEGER NOT NULL,
	`Comment`	TEXT
);
CREATE TABLE IF NOT EXISTS `AclActions` (
	`AclActionId`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Action`	TEXT,
	`Parameters`	TEXT
);
CREATE INDEX IF NOT EXISTS `IX_UsersInAclGroups_AclGroupId` ON `UsersInAclGroups` (
	`AclGroupId`
);
CREATE INDEX IF NOT EXISTS `IX_SectionsInAclGroups_AclGroupId` ON `SectionsInAclGroups` (
	`AclGroupId`
);
CREATE INDEX IF NOT EXISTS `IX_PicturesInAlbums_AlbumId` ON `PicturesInAlbums` (
	`AlbumId`
);
CREATE INDEX IF NOT EXISTS `IX_InfosInPictures_PictureId` ON `InfosInPictures` (
	`PictureId`
);
CREATE INDEX IF NOT EXISTS `IX_InfosInAlbums_AlbumId` ON `InfosInAlbums` (
	`AlbumId`
);
CREATE INDEX IF NOT EXISTS `IX_AlbumsInSections_SectionId` ON `AlbumsInSections` (
	`SectionId`
);
CREATE INDEX IF NOT EXISTS `IX_AclGroupsInAclActions_AclActionId` ON `AclGroupsInAclActions` (
	`AclActionId`
);
COMMIT;
