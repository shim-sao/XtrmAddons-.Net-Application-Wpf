namespace XtrmAddons.Fotootof.Lib.Base.Enums
{
    /// <summary>
    /// <para>Enumerator XtrmAddons Fotootof Libraries Base Display Mode.</para>
    /// <para>The Enumerator us used to defines the main Application Context : Client|Server</para>
    /// </summary>
    public enum DisplayMode
    {
        /// <summary>
        /// Display Mode for Server main Application Context.
        /// </summary>
        Server = 0,

        /// <summary>
        /// Display Mode for Client main Application Context.
        /// </summary>
        Client = 1
    }

    /// <summary>
    /// <para>Class XtrmAddons Fotootof Libraries Base Display Mode Extensions.</para>
    /// <para></para>
    /// </summary>
    public static class DisplayModeExtensions
    {
        /// <summary>
        /// Method to get the name string value of a Display Mode.
        /// </summary>
        /// <param name="displayMode">The Display Mode.</param>
        /// <returns>The string name of the Display Mode.</returns>
        public static string Name(this DisplayMode displayMode)
        {
            switch (displayMode)
            {
                case DisplayMode.Client:
                    return "Client";
                case DisplayMode.Server:
                    return "Server";
            }

            return null;
        }

        /// <summary>
        /// Method to get the int value of a Display Mode.
        /// </summary>
        /// <param name="displayMode">The Display Mode.</param>
        /// <returns>The string name of the Display Mode.</returns>
        public static int Value(this DisplayMode displayMode)
        {
            switch (displayMode)
            {
                case DisplayMode.Client:
                    return 1;
                case DisplayMode.Server:
                    return 0;
            }

            return -1;
        }
    }
}
