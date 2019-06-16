using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using TeacherControl.Common.Enums;
using TeacherControl.Common.Extensors;
using TeacherControl.Infraestructure.Logger;

namespace TeacherControl.API.Extensors
{
    public static class HttpResponseExtensors
    {

        public static IActionResult Created<T>(this Controller controller, Func<T> callback, ILogger<Controller> logger) where T : class
        {
            return HandlingExceptions(controller, logger, () =>
            {
                T obj = callback.Invoke();

                if (obj.Equals(null))
                {
                    Type type = obj.GetType();
                    throw new NullReferenceException(
                        $"The Object {type.FullName} cannot be Null or Nullable\nFinds in:\t{type.Assembly}");
                }

                JObject json = obj.ToJson();

                if (json.HasValues)
                {
                    logger.Emit(LoggerEventStatus.INSERTED_ITEM,
                        $"{controller.Url.Action()} Is called, Creating The {typeof(T).Name} Entity");

                    return controller.Created(controller.Url.Action(), json);
                }

                return controller.BadRequest(controller.ModelState.GetErrors().ToJson());
            });
        }

        public static IActionResult Ok<T>(this Controller controller, Func<T> callback, ILogger<Controller> logger) where T : class
        {
            return HandlingExceptions(controller, logger, () =>
            {
                T obj = callback.Invoke();

                if (obj.Equals(null))
                {
                    Type type = obj.GetType();
                    throw new NullReferenceException(
                        $"The Object {type.FullName} cannot be Null or Nullable" +
                        $"\t\nFinds in:\t{type.Assembly}");
                }

                JObject json = obj.ToJson();
                if (json.HasValues)
                {
                    logger.Emit(LoggerEventStatus.GET_ITEM,
                        $"{controller.Url.Action()} Is called, Getting The {typeof(T).Name} Entities");

                    return controller.Json(json);
                }

                return controller.BadRequest(controller.ModelState);
            });

        }

        public static IActionResult Ok<T>(this Controller controller, Func<IEnumerable<T>> callback, ILogger<Controller> logger) where T : class
        {
            return HandlingExceptions(controller, logger, () =>
            {
                JArray json = callback.Invoke().ToJsonArray();
                if (json.Count >= 0)
                {
                    return controller.Json(json);
                }

                return controller.BadRequest(controller.ModelState);
            });
        }

        public static IActionResult NoContent(this Controller controller, Func<bool> callback, ILogger<Controller> logger)
        {
            return HandlingExceptions(controller, logger, () =>
            {
                bool isSuccess = callback.Invoke();
                if (isSuccess)
                {
                    return controller.NoContent();
                }

                return controller.BadRequest(controller.ModelState);// TBD

            });

        }

        private static IActionResult HandlingExceptions(Controller controller, ILogger<Controller> logger, Func<IActionResult> callback)
        {
            if (callback != null && controller.ModelState.IsValid)
            {
                try
                {
                    return callback.Invoke();
                }
                catch (NullReferenceException ex)
                {
                    JsonResult json = new JsonResult(ex)
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        Value = HttpStatusCode.InternalServerError.ToString()
                    };

                    string details = ex.InnerException != null ? ex.InnerException.Message : string.Empty;
                    logger.Emit(LoggerEventStatus.NULL_EXCEPTION_ERROR,
                        $"Error: {ex.Message}\n" +
                        $"Details: {details}\n" +
                        $"Where: {controller.Url.Action()}");

                    return json;
                }
                catch (SqlException ex)
                {

                    JsonResult json = new JsonResult(ex)
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        Value = HttpStatusCode.InternalServerError.ToString()
                    };

                    string details = ex.InnerException != null ? ex.InnerException.Message : string.Empty;
                    logger.Emit(LoggerEventStatus.DABATABASE_ERROR,
                        $"Error: {ex.Message}\n\t" +
                        $"Where: {controller.Url.Action()}\n\t" +
                        $"Details: {details}"
                        );

                    return json;
                }
                catch (Exception ex)
                {
                    JsonResult json = new JsonResult(ex)
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        Value = HttpStatusCode.InternalServerError.ToString()
                    };

                    string details = ex.InnerException != null ? ex.InnerException.Message : string.Empty;
                    logger.Emit(LoggerEventStatus.GENERIC_EXCEPTION_ERROR,
                        $"Error: {ex.Message}\n\t" +
                        $"Where: {controller.Url.Action()}\n\t" +
                        $"Details: {details}"
                        );

                    return json;
                }
            }

            logger.Emit(LoggerEventStatus.BAD_REQUEST_FORMAT,
                $"Action Method: {controller.Url.Action()}\n\t" +
                $"Errors: {string.Join("\n\t", controller.ModelState.GetErrors())}");


            return controller.BadRequest(controller.ModelState.GetErrors().ToJson());
        }

    }
}
