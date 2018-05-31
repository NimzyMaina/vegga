using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace vegga.Services
{
    public class ValidationResultModel
    {
        public string Message { get; } 

        public Dictionary<string,string> Errors { get; }

        public ValidationResultModel(ModelStateDictionary modelState)
        {   
            Message = "Validation Failed";
            Errors =  modelState.Keys
            .SelectMany(key => modelState[key].Errors.Select(x => new {Key = key, Value = x.ErrorMessage}))
            .ToDictionary(d => d.Key != string.Empty ? d.Key : "message", d => d.Value);
        }
    }
}