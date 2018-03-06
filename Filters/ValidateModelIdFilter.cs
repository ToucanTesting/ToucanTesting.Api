using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TucanTesting.Filters
{
    public class ValidateModelIdFilter : ActionFilterAttribute
    {
        private readonly string _idKey;
        private readonly string _modelKey;

        public ValidateModelIdFilter(string idKey, string modelKey)
        {
            this._modelKey = modelKey;
            this._idKey = idKey;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var model = context.ActionArguments[_modelKey];
            var modelId = GetPropValue(model, "Id") as long?;

            var id = context.ActionArguments[_idKey] as long?;
            if (id != modelId)
            {
                context.Result = new BadRequestObjectResult("Invalid ID");
            }

        }

        private object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}