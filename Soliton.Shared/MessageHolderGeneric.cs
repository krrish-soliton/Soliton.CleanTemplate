using System;

namespace Soliton.Shared
{
    public class MessageHolder<T> : MessageHolder, IDisposable
    {
        private bool disposedValue;

        public MessageHolder() { }

        public MessageHolder(T data, bool isSuccess = true)
        {
            Data = data;
            IsSuccess = isSuccess;
        }

        public MessageHolder(string message) : base(message) { }

        public MessageHolder(int errorCode, string? message, bool isSuccess = false) : base(errorCode, message, isSuccess) { }

        public MessageHolder(int errorCode, string? message, bool isSuccess, T? data = default) : base(errorCode, message, isSuccess)
        {
            Data = data;
        }

        public T? Data { get; set; }

        public bool IsFailOrNull => !IsSuccess || Data == null;

        public MessageHolder<T> OnDataNull(Action<MessageHolder<T>> action)
        {
            if (Data == null) action?.Invoke(this);
            return this;
        }

        public MessageHolder<T> OnDataNotNull(Action<MessageHolder<T>> action)
        {
            if (Data != null) action?.Invoke(this);
            return this;
        }

        public static implicit operator MessageHolder<T>(string message) => new(message);

        public static implicit operator MessageHolder<T>(T value) => new(value);

        public static implicit operator bool(MessageHolder<T> value) => value?.IsSuccess ?? false;

        public static implicit operator string(MessageHolder<T> value) => value?.Message ?? string.Empty;

        public static implicit operator int(MessageHolder<T> value) => value?.ErrorCode ?? -1;

        public static implicit operator T?(MessageHolder<T> value) => value == null ? default : value.Data;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (Data is IDisposable disposable) disposable?.Dispose();
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
