namespace Fotootof.SQLite.EntityManager.Interfaces
{
    /// <summary>
    /// Interface XtrmAddons Fotootof SQLite Entity Manager Table `Pictures`.
    /// </summary>
    public interface ITablePictures :
        IColumnPictureId,
        IColumnNameAlias,
        IColumnDescription,
        IColumnOrdering,
        IColumnCaptured,
        IColumnCreated,
        IColumnModified,
        IColumnComment
    { }
}