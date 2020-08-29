using Microsoft.AspNetCore.Mvc;
using Shared.Common;
using System.Collections.Generic;
using System.Linq;

namespace PresentationServer.Extensions
{
    public static class ActionContextExtensions
    {
        public static ServerResponse FormatModelResponse(ActionContext context)
        {
            var errors = new List<KeyValuePair<string, string>>();
            var erroredFields = context.ModelState.Values.Where(v => v.Errors.Count > 0);
            foreach (var property in context.ModelState.Keys)
            {
                var value = context.ModelState[property];
                foreach (var error in value.Errors)
                {
                    errors.Add(new KeyValuePair<string, string>(property, error.ErrorMessage));
                }
            }

            return new ServerResponse
            {
                Errors = errors,
            };
        }
    }
}
