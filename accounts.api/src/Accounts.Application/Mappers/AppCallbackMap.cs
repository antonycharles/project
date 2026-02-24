using Accounts.Core.Entities;
using Accounts.Core.Requests;
using Accounts.Core.Responses;

namespace Accounts.Application.Mappers
{
    public static class AppCallbackMap
    {
        public static AppCallback ToAppCallback(this AppCallbackRequest request)
        {
            return new AppCallback
            {
                Url = request.Url?.Trim(),
                Environment = request.Environment,
                AppId = request.AppId,
                IsDefault = request.IsDefault
            };
        }

        public static AppCallbackResponse ToAppCallbackResponse(this AppCallback entity)
        {
            return new AppCallbackResponse
            {
                Id = entity.Id,
                Url = entity.Url,
                Environment = entity.Environment,
                AppId = entity.AppId,
                IsDefault = entity.IsDefault
            };
        }

        public static void Update(this AppCallback callback, AppCallbackRequest request)
        {
            callback.Url = request.Url != null ? request.Url.Trim() : string.Empty;
            callback.Environment = request.Environment;
            callback.AppId = request.AppId;
            callback.IsDefault = request.IsDefault;
        }
    }
}
