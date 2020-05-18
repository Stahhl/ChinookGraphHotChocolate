using HotChocolate.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chinook.Api.Diagnostics
{
    public class Request
    {
        public Request(IReadOnlyQueryRequest request)
        {
            Id = request.QueryName;
            Query = request.Query?.ToString();
            OperationName = request.OperationName;
            VariableValues = request.VariableValues;
        }

        public string Id { get; }
        public string Query { get; }
        public string OperationName { get; }
        public IReadOnlyDictionary<string, object> VariableValues { get; }
    }
}
