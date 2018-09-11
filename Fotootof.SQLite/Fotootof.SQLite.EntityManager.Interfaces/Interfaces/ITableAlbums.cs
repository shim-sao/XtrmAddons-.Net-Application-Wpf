namespace Fotootof.SQLite.EntityManager.Interfaces
{
    /// <summary>
    /// Interface XtrmAddons Fotootof SQLite Entity Manager Table `Albums`.
    /// </summary>
    public interface ITableAlbums :
        IColumnAlbumId,
        IColumnNameAlias,
        IColumnDescription,
        IColumnOrdering,
        IColumnCreated,
        IColumnModified,
        IColumnDateStart,
        IColumnDateEnd,
        IColumnBackgroundPictureId,
        IColumnPreviewPictureId,
        IColumnThumbnailPictureId,
        IColumnComment,
        IColumnParameters
    { }
}