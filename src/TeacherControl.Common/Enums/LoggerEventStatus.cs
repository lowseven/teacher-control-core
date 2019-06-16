using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Common.Enums
{
    public enum LoggerEventStatus
    {
        GENERATED_ITEMS = 1000,
        LIST_ITEMS,
        GET_ITEM,
        INSERTED_ITEM,
        UPDATED_ITEM,
        DELETED_ITEM,
        BAD_REQUEST_FORMAT,

        GET_ITEM_NOT_FOUND = 4000,
        UPDATE_ITEM_NOT_FOUND,

        GENERIC_EXCEPTION_ERROR,
        DABATABASE_ERROR,
        NULL_EXCEPTION_ERROR,
    }
}
