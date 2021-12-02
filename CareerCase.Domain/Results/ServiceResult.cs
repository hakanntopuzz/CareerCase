namespace CareerCase.Domain.Results
{
    public sealed class ServiceResult: BaseServiceResult
    {
        #region ctor

        ServiceResult(bool isSuccess, string message)
          : base(isSuccess, message)
        {
        }

        #endregion

        #region factory methods

        public static ServiceResult Success()
        {
            return new ServiceResult(true, null);
        }

        public static ServiceResult Success(string message)
        {
            return new ServiceResult(true, message);
        }

        public static ServiceResult Error()
        {
            return new ServiceResult(false, null);
        }

        public static ServiceResult Error(string message)
        {
            return new ServiceResult(false, message);
        }

        #endregion
    }
}