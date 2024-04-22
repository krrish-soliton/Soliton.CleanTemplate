using System;

namespace Soliton.Shared
{
    public class MessageHolder
    {
        public MessageHolder() { }

        public MessageHolder(bool isSuccess) => IsSuccess = isSuccess;

        public MessageHolder(string? message)
        {
            Message = message;
            IsSuccess = false;
        }

        public MessageHolder(int errorCode, string? message, bool isSuccess = false)
        {
            ErrorCode = errorCode;
            Message = message;
            IsSuccess = isSuccess;
        }

        public int ErrorCode { get; set; }

        public bool IsSuccess { get; set; }

        public string? Message { get; set; }

        public MessageHolder OnFailure(Action<MessageHolder> failureAction)
        {
            if (!IsSuccess) failureAction?.Invoke(this);
            return this;
        }

        public MessageHolder OnSuccess(Action<MessageHolder> successAction)
        {
            if (IsSuccess) successAction?.Invoke(this);
            return this;
        }

        public static implicit operator MessageHolder(string message) => new(message);

        public static implicit operator bool(MessageHolder value) => value?.IsSuccess ?? false;

        public static implicit operator string(MessageHolder value) => value?.Message ?? string.Empty;

        public static implicit operator int(MessageHolder value) => value?.ErrorCode ?? -1;

        public static implicit operator MessageHolder(bool value) => new() { IsSuccess = value };
    }
}
