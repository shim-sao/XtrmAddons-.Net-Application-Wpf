namespace XtrmAddons.Fotootof.Lib.Base.Interfaces
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Libraries Base Interface List Items.</para>
    /// <para>This Interface inplement a generic base for managing list of items.</para>
    /// </summary>
    /// <typeparam name="T">The Type of items collection.</typeparam>
    public interface IListItems<T>
    {
        /// <summary>
        /// Property to access to the items collection.
        /// </summary>
        T Items { get; set; }
    }
}