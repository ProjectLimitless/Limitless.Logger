/** 
* This file is part of Project Limitless.
* Copyright © 2016 Donovan Solms.
* Project Limitless
* https://www.projectlimitless.io
* 
* Project Limitless is free software: you can redistribute it and/or modify
* it under the terms of the Apache License Version 2.0.
* 
* You should have received a copy of the Apache License Version 2.0 with
* Project Limitless. If not, see http://www.apache.org/licenses/LICENSE-2.0.
*/

using System;

using Limitless.Runtime.Interfaces;

namespace Limitless.Logger
{
    public class LoggerConfig
    {
        public LoggerConfig()
        {

        }
    }

    public class Logger : IModule, ILogger
    {
        /// <summary>
        /// Implemented from interface 
        /// <see cref="Limitless.Runtime.Interfaces.ILogger.Trace(string, object[])"/>
        /// </summary>
        public void Trace(string format, params object[] args)
        {
            Console.WriteLine($"Trace: {format}", args);
        }

        /// <summary>
        /// Implemented from interface 
        /// <see cref="Limitless.Runtime.Interfaces.ILogger.Debug(string, object[])"/>
        /// </summary>
        public void Debug(string format, params object[] args)
        {
            Console.WriteLine($"Debug: {format}", args);
        }

        /// <summary>
        /// Implemented from interface 
        /// <see cref="Limitless.Runtime.Interfaces.ILogger.Info(string, object[])"/>
        /// </summary>
        public void Info(string format, params object[] args)
        {
            Console.WriteLine($"Info: {format}", args);
        }

        /// <summary>
        /// Implemented from interface 
        /// <see cref="Limitless.Runtime.Interfaces.ILogger.Warning(string, object[])"/>
        /// </summary>
        public void Warning(string format, params object[] args)
        {
            Console.WriteLine($"Warning: {format}", args);
        }

        /// <summary>
        /// Implemented from interface 
        /// <see cref="Limitless.Runtime.Interfaces.ILogger.Error(string, object[])"/>
        /// </summary>
        public void Error(string format, params object[] args)
        {
            Console.WriteLine($"Error: {format}", args);
        }

        /// <summary>
        /// Implemented from interface 
        /// <see cref="Limitless.Runtime.Interfaces.ILogger.Critical(string, object[])"/>
        /// </summary>
        public void Critical(string format, params object[] args)
        {
            Console.WriteLine($"Critical: {format}", args);
        }

        /// <summary>
        /// Implemented from interface
        /// <see cref="Limitless.Runtime.Interfaces.IModule.Configure(dynamic)"/>
        /// </summary>
        public void Configure(dynamic settings)
        {
            // Nothing to configure
        }

        /// <summary>
        /// Implemented from interface
        /// <see cref="Limitless.Runtime.Interfaces.IModule.GetConfigurationType"/>
        /// </summary>
        public Type GetConfigurationType()
        {
            // No configuration type
            return typeof(LoggerConfig);
        }
    }
}
