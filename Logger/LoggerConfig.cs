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

namespace Limitless.Logger
{
    /// <summary>
    /// Basic configuration for the logger.
    /// </summary>
    public class LoggerConfig
    {
        /// <summary>
        /// Get and sets if the original caller should be included
        /// in the log output. Defaults to true.
        /// </summary>
        public bool IncludeCallSite { get; set; } = true;
        /// <summary>
        /// Sets and gets the log level to log.
        /// </summary>
        public string Level { get; set; } = "info";
    }
}
