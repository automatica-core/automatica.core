﻿using System.Collections.Concurrent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Automatica.Core.Logging
{
    public class CoreLoggerFactory : ICoreLoggerFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private static readonly object Lock = new();
        private static readonly IDictionary<string, ICoreLogger> LoggerInstances = new ConcurrentDictionary<string, ICoreLogger>();
        
        public IConfiguration Configuration { get; }

        public CoreLoggerFactory(IConfiguration config, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            Configuration = config;
        }

        public static ILogger GetLogger(IConfiguration? config, IServiceProvider? serviceProvider, string name)
        {
            lock (Lock)
            {
                if (!LoggerInstances.ContainsKey(name))
                {
                    LoggerInstances.Add(name, new CoreLogger(config, serviceProvider, name));
                }

                return LoggerInstances[name];

            }
        }
        public static ILogger GetLogger(IConfiguration config, IServiceProvider serviceProvider, string name, LogLevel? level, bool isFrameworkLog)
        {
            lock (Lock)
            {
                if (!LoggerInstances.ContainsKey(name))
                {
                    LoggerInstances.Add(name, new CoreLogger(config, serviceProvider, name, level, isFrameworkLog));
                }

                return LoggerInstances[name];
            }
        }

        public void Dispose()
        {
            
        }

        public ILogger CreateLogger(string categoryName)
        {
            return GetLogger(Configuration, _serviceProvider, categoryName, null, categoryName.ToLowerInvariant().Contains("microsoft") || categoryName.ToLowerInvariant().Contains("system"));
        }

        public IEnumerable<ICoreLogger> GetLoggers()
        {
            lock (Lock)
            {
                return LoggerInstances.Values;
            }
        }

        public void SetLogLevel(string name, LogLevel level)
        {
            lock (Lock)
            {
                if (LoggerInstances.TryGetValue(name, out var instance))
                    instance.LogLevel = level;
            }
        }

        public void SetLogLevel(LogLevel level)
        {
            lock (Lock)
            {
                foreach (var logger in LoggerInstances)
                {
                    logger.Value.LogLevel = level;
                }
            }
        }

        public void AddProvider(ILoggerProvider provider)
        {
            
        }
    }
}
