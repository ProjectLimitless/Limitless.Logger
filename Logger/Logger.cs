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
using System.Reflection;
using System.Diagnostics;

using Limitless.Runtime.Interfaces;
using Limitless.Runtime.Enums;

namespace Limitless.Logger
{
    /// <summary>
    /// An NLog-based default logger for Project Limitless.
    /// </summary>
    public class Logger : IModule, ILogger
    {
        /// <summary>
        /// The NLog logger.
        /// </summary>
        private NLog.Logger _log;
        /// <summary>
        /// Module configuration.
        /// </summary>
        private LoggerConfig _config;

        /// <summary>
        /// Standard constructor.
        /// </summary>
        public Logger()
        {
            _config = new LoggerConfig();
            _log = NLog.LogManager.GetCurrentClassLogger();
        }
        
        /// <summary>
        /// Implemented from interface 
        /// <see cref="Limitless.Runtime.Interfaces.ILogger.Trace(string, object[])"/>
        /// </summary>
        public void Trace(string format, params object[] args)
        {
            string fullMethodName = "";
            if (_config.IncludeCallSite)
            {
                fullMethodName = $"{Assembly.GetCallingAssembly().GetName().Name}.{GetCallingMethod()}|";
            }
            _log.Trace($"{fullMethodName}{format}", args);
        }

        /// <summary>
        /// Implemented from interface 
        /// <see cref="Limitless.Runtime.Interfaces.ILogger.Debug(string, object[])"/>
        /// </summary>
        public void Debug(string format, params object[] args)
        {
            string fullMethodName = "";
            if (_config.IncludeCallSite)
            {
                fullMethodName = $"{Assembly.GetCallingAssembly().GetName().Name}.{GetCallingMethod()}|";
            }
            _log.Debug($"{fullMethodName}{format}", args);
        }

        /// <summary>
        /// Implemented from interface 
        /// <see cref="Limitless.Runtime.Interfaces.ILogger.Info(string, object[])"/>
        /// </summary>
        public void Info(string format, params object[] args)
        {
            string fullMethodName = "";
            if (_config.IncludeCallSite)
            {
                fullMethodName = $"{Assembly.GetCallingAssembly().GetName().Name}.{GetCallingMethod()}|";
            }
            _log.Info($"{fullMethodName}{format}", args);
        }

        /// <summary>
        /// Implemented from interface 
        /// <see cref="Limitless.Runtime.Interfaces.ILogger.Warning(string, object[])"/>
        /// </summary>
        public void Warning(string format, params object[] args)
        {
            string fullMethodName = "";
            if (_config.IncludeCallSite)
            {
                fullMethodName = $"{Assembly.GetCallingAssembly().GetName().Name}.{GetCallingMethod()}|";
            }
            _log.Warn($"{fullMethodName}{format}", args);
        }

        /// <summary>
        /// Implemented from interface 
        /// <see cref="Limitless.Runtime.Interfaces.ILogger.Error(string, object[])"/>
        /// </summary>
        public void Error(string format, params object[] args)
        {
            string fullMethodName = "";
            if (_config.IncludeCallSite)
            {
                fullMethodName = $"{Assembly.GetCallingAssembly().GetName().Name}.{GetCallingMethod()}|";
            }
            _log.Error($"{fullMethodName}{format}", args);
        }

        /// <summary>
        /// Implemented from interface 
        /// <see cref="Limitless.Runtime.Interfaces.ILogger.Critical(string, object[])"/>
        /// </summary>
        public void Critical(string format, params object[] args)
        {
            string fullMethodName = "";
            if (_config.IncludeCallSite)
            {
                fullMethodName = $"{Assembly.GetCallingAssembly().GetName().Name}.{GetCallingMethod()}|";
            }
            _log.Fatal($"{fullMethodName}{format}", args);
        }

        /// <summary>
        /// Implemented from interface
        /// <see cref="Limitless.Runtime.Interfaces.IModule.Configure(dynamic)"/>
        /// </summary>
        public void Configure(dynamic settings)
        {
            if (settings == null)
            {
                throw new NullReferenceException("Settings can not be null");
            }
            _config = (LoggerConfig)settings;
            
            switch (_config.Level.ToLower())
            {
                case "trace":
                    UpdateLevel(LogLevel.Trace);
                    break;
                case "debug":
                    UpdateLevel(LogLevel.Debug);
                    break;
                case "info":
                    UpdateLevel(LogLevel.Info);
                    break;
                case "warning":
                    UpdateLevel(LogLevel.Warning);
                    break;
                case "error":
                    UpdateLevel(LogLevel.Error);
                    break;
                case "critical":
                    UpdateLevel(LogLevel.Critical);
                    break;
            }
        }

        /// <summary>
        /// Implemented from interface
        /// <see cref="Limitless.Runtime.Interfaces.ILogger.UpdateLevel(string)"/>
        /// </summary>
        public void UpdateLevel(LogLevel level)
        {
            // Update each rule set in the initial config
            foreach (var rule in NLog.LogManager.Configuration.LoggingRules)
            {
                // Disable all rules before enabling new ones
                rule.DisableLoggingForLevel(NLog.LogLevel.Trace);
                rule.DisableLoggingForLevel(NLog.LogLevel.Debug);
                rule.DisableLoggingForLevel(NLog.LogLevel.Info);
                rule.DisableLoggingForLevel(NLog.LogLevel.Warn);
                rule.DisableLoggingForLevel(NLog.LogLevel.Error);
                rule.DisableLoggingForLevel(NLog.LogLevel.Fatal);

                switch (level)
                {
                    case LogLevel.Trace:
                        rule.EnableLoggingForLevels(
                            NLog.LogLevel.Trace,
                            NLog.LogLevel.Fatal
                        );
                        break;
                    case LogLevel.Debug:
                        rule.EnableLoggingForLevels(
                            NLog.LogLevel.Debug,
                            NLog.LogLevel.Fatal
                        );
                        break;
                    case LogLevel.Info:
                        rule.EnableLoggingForLevels(
                            NLog.LogLevel.Info,
                            NLog.LogLevel.Fatal
                        );
                        break;
                    case LogLevel.Warning:
                        rule.EnableLoggingForLevels(
                            NLog.LogLevel.Warn,
                            NLog.LogLevel.Fatal
                        );
                        break;
                    case LogLevel.Error:
                        rule.EnableLoggingForLevels(
                            NLog.LogLevel.Error,
                            NLog.LogLevel.Fatal
                        );
                        break;
                    case LogLevel.Critical:
                        rule.EnableLoggingForLevels(
                            NLog.LogLevel.Fatal,
                            NLog.LogLevel.Fatal
                        );
                        break;
                }
            }
            // Trigger update
            NLog.LogManager.Flush();
            NLog.LogManager.ReconfigExistingLoggers();
        }

        /// <summary>
        /// Implemented from interface
        /// <see cref="Limitless.Runtime.Interfaces.IModule.GetConfigurationType"/>
        /// </summary>
        public Type GetConfigurationType()
        {
            return typeof(LoggerConfig);
        }

        /// <summary>
        /// Get the calling method for logging.
        /// </summary>
        /// <returns>The name of the calling method</returns>
        private string GetCallingMethod()
        {
            // Skip 2 frames as they are this call and the interface call
            var stackFrame = new StackFrame(2);
            return $"{stackFrame.GetMethod().ReflectedType.Name}.{stackFrame.GetMethod().Name}";
        }

        /// <summary>
        /// Implemented from interface 
        /// <see cref="Limitless.Runtime.Interface.IModule.GetTitle"/>
        /// </summary>
        public string GetTitle()
        {
            var assembly = typeof(Logger).Assembly;
            var attribute = assembly.GetCustomAttribute<AssemblyTitleAttribute>();
            if (attribute != null)
            {
                return attribute.Title;
            }
            return "Unknown";
        }

        /// <summary>
        /// Implemented from interface 
        /// <see cref="Limitless.Runtime.Interface.IModule.GetAuthor"/>
        /// </summary>
        public string GetAuthor()
        {
            var assembly = typeof(Logger).Assembly;
            var attribute = assembly.GetCustomAttribute<AssemblyCompanyAttribute>();
            if (attribute != null)
            {
                return attribute.Company;
            }
            return "Unknown";
        }

        /// <summary>
        /// Implemented from interface 
        /// <see cref="Limitless.Runtime.Interface.IModule.GetVersion"/>
        /// </summary>
        public string GetVersion()
        {
            var assembly = typeof(Logger).Assembly;
            return assembly.GetName().Version.ToString();
        }

        /// <summary>
        /// Implemented from interface 
        /// <see cref="Limitless.Runtime.Interface.IModule.GetDescription"/>
        /// </summary>
        public string GetDescription()
        {
            var assembly = typeof(Logger).Assembly;
            var attribute = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>();
            if (attribute != null)
            {
                return attribute.Description;
            }
            return "Unknown";
        }
    }
}
