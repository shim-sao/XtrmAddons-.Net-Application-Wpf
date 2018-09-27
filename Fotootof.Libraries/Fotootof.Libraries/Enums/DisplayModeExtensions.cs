namespace Fotootof.Libraries.Enums
{
    /// <summary>
    /// <para>Class Fotootof Libraries Display Mode Extensions.</para>
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
