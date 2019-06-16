using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Common.Enums;

namespace TeacherControl.Infraestructure.Logger
{
    public static class LoggerExtensions
    {
        private static Dictionary<Enum, LogLevel> LogLevelMapper = new Dictionary<Enum, LogLevel>
        {
            { LoggerEventStatus.GENERATED_ITEMS, LogLevel.Information },
            { LoggerEventStatus.LIST_ITEMS, LogLevel.Information },
            { LoggerEventStatus.GET_ITEM, LogLevel.Information },
            { LoggerEventStatus.INSERTED_ITEM, LogLevel.Information },
            { LoggerEventStatus.UPDATED_ITEM, LogLevel.Information },
            { LoggerEventStatus.DELETED_ITEM, LogLevel.Information },
            { LoggerEventStatus.GET_ITEM_NOT_FOUND, LogLevel.Information },
            { LoggerEventStatus.UPDATE_ITEM_NOT_FOUND, LogLevel.Information },
            { LoggerEventStatus.BAD_REQUEST_FORMAT, LogLevel.Information },

            { LoggerEventStatus.GENERIC_EXCEPTION_ERROR, LogLevel.Warning },
            { LoggerEventStatus.DABATABASE_ERROR, LogLevel.Warning },
            { LoggerEventStatus.NULL_EXCEPTION_ERROR, LogLevel.Warning},


        };

        public static void Emit<T>(this ILogger<T> logger, Enum eventType, string message)
        {
            int eventID = (int)Convert.ChangeType(eventType, eventType.GetTypeCode());
            string eventName = Enum.GetName(eventType.GetType(), eventType);
            string eventScope = eventType.GetType().Name;

            var level = LogLevelMapper.ContainsKey(eventType) ? LogLevelMapper[eventType] : LogLevel.None;
            logger.Log(level, $"(Event scope: { eventScope}, Name: { eventName}):\n\t{message}");
        }

    }
}
