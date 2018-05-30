using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Valdators
{
    public static class ValidationExceptioExtensions
    {
        public static void AddToModelState(this ValidationException exception, ModelStateDictionary modelState, string prefix = "")
        {
            foreach (var error in exception.Errors)
            {
                string key = string.IsNullOrEmpty(prefix) ? error.PropertyName : prefix + "." + error.PropertyName;
                modelState.AddModelError(key, error.ErrorMessage);
            }
        }
    }
}
