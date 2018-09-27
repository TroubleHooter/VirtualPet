namespace VirtualPet.Application.HandlerResponse
{
    public class HandlerResponse<TResult>
    {
        public HandlerResponse(ResultType resultType, TResult result)
        {
            ResultType = resultType;
            Result = result;
        }

        public HandlerResponse(ResultType resultType)
        {
            ResultType = resultType;
        }

        public ResultType ResultType { get; set; }
        public TResult Result { get; set; }
    }
}
