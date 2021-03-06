﻿namespace Com.Ericmas001.Windows.ViewCommands
{
    /// <summary>
    /// Defines a property to access the ViewCommandManager instance of an object.
    /// </summary>
    public interface IViewCommandSource
    {
        /// <summary>
        /// Gets the ViewCommandManager instance of the object.
        /// </summary>
        ViewCommandManager ViewCommandManager { get; }
    }
}
