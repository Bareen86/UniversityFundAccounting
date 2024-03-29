using Audiences.Application.Validation;

namespace Audiences.Application.Results
{
    public class QueryResult<TQueryResultData> where TQueryResultData : class
    {
        public ValidationResult ValidationResult { get; set; }
        public TQueryResultData ObjResult { get; set; }

        public QueryResult( TQueryResultData objResult )
        {
            ObjResult = objResult;
            ValidationResult = ValidationResult.Ok();
        }

        public QueryResult( ValidationResult validationResult )
        {
            ValidationResult = validationResult;
        }
    }
}
