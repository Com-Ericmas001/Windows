﻿using System;

namespace Com.Ericmas001.Windows.ViewCommands
{
    /// <summary>
    /// Indicates that a method is a ViewCommand handler.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ViewCommandAttribute : Attribute
    {
        /// <summary>
        /// Gets the command name.
        /// </summary>
        public string CommandName { get; private set; }

        /// <summary>
        /// Initialises a new instance of the ViewCommandAttribute class.
        /// </summary>
        public ViewCommandAttribute()
        {
        }

        /// <summary>
        /// Initialises a new instance of the ViewCommandAttribute class.
        /// </summary>
        public ViewCommandAttribute(string commandName)
        {
            CommandName = commandName;
        }
    }
}
