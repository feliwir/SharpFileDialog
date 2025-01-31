namespace SharpFileDialog
{
    /// <summary>
    /// The result of a file/directory selection dialog
    /// </summary>
    public struct DialogResult
    {
        /// <summary>
        /// The path to the file or directory chosen
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// True if a file or directory was chosen, false if the dialog was cancelled or an error was encountered.
        /// </summary>
        public bool Success { get; set; }
    }
}
