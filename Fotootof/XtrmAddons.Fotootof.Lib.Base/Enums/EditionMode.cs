namespace XtrmAddons.Fotootof.Lib.Base.Enums
{
    /// <summary>
    /// <para>Enumerator XtrmAddons Fotootof Libraries Base Edition Mode.</para>
    /// <para>This Enumerator take the list of available Edition Mode for custom user control.</para>
    /// </summary>
    public enum EditionMode
    {
        /// <summary>
        /// 
        /// </summary>
        Add = 0,

        /// <summary>
        /// 
        /// </summary>
        Delete = 1,

        /// <summary>
        /// 
        /// </summary>
        Edit = 2,

        /// <summary>
        /// 
        /// </summary>
        Order = 3,

        /// <summary>
        /// 
        /// </summary>
        None = 4
    }

    /// <summary>
    /// <para>Class XtrmAddons Fotootof Libraries Base Edition Mode Extensions.</para>
    /// <para>This Class provides extensions methods for EditionMode.</para>
    /// </summary>
    public static class EditionModeExtensions
    {
        /// <summary>
        /// Method to get the name string value of a Edition Mode.
        /// </summary>
        /// <param name="EditionMode">The Edition Mode.</param>
        /// <returns>The string name of the Edition Mode.</returns>
        public static string Name(this EditionMode editionMode)
        {
            switch (editionMode)
            {
                case EditionMode.Add:
                    return "Add";
                case EditionMode.Delete:
                    return "Delete";
                case EditionMode.Edit:
                    return "Edit";
                case EditionMode.Order:
                    return "Order";
                case EditionMode.None:
                    return "None";
            }

            return null;
        }

        /// <summary>
        /// Method to get the int value of a Edition Mode.
        /// </summary>
        /// <param name="EditionMode">The Edition Mode.</param>
        /// <returns>The int value of the Edition Mode.</returns>
        public static int Value(this EditionMode editionMode)
        {
            switch (editionMode)
            {
                case EditionMode.Add:
                    return 0;
                case EditionMode.Delete:
                    return 1;
                case EditionMode.Edit:
                    return 2;
                case EditionMode.Order:
                    return 3;
                case EditionMode.None:
                    return 4;
            }

            return -1;
        }
    }
}