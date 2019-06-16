using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace TeacherControl.API.Extensors
{
    public static class ControllerExtensor
    {

        public static string GetUsername(this ControllerBase controller) => controller.User.Identity.Name;

        public static bool IsAuthenticated(this ControllerBase controller) => controller.User.Identity.IsAuthenticated;

        public static string GetParam (this IQueryCollection query, string paramName) => 
            query.Where(i => i.Key.ToLower().Equals(paramName)).FirstOrDefault().Value.ToString();

        public static IEnumerable<string> GetErrors(this ModelStateDictionary ModelState) =>
            ModelState.Values.SelectMany(i => i.Errors.Select(m => m.ErrorMessage));
    }
}
