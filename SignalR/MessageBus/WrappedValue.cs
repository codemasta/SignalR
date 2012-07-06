namespace SignalR
{
    /// <summary>
    /// All values saved to the messages store are wrapped by this type.
    /// If a store needs to save values in a serializable way then it just needs to call
    /// ToString() and we'll unwrap it when it comes back (if needed).
    /// </summary>
    internal class WrappedValue
    {
        private readonly IJsonSerializer _serializer;
        private readonly object _value;
        private readonly IJsonString _valueJson;

        public WrappedValue(object value, IJsonSerializer serializer)
        {
            _value = value;
            _valueJson = new JsonString(serializer.Stringify(value));
            _serializer = serializer;
        }

        public object Value
        {
            get
            {
                return _value;
            }
        }

        public IJsonString ValueJson
        {
            get
            {
                return _valueJson;
            }
        }

        public static T Unwrap<T>(object value, IJsonSerializer serializer)
        {
            var wrappedValue = value as WrappedValue;
            if (wrappedValue != null)
            {
                return (T)wrappedValue.Value;
            }

            return serializer.Parse<T>((string)value);
        }

        public static object Unwrap(object value, IJsonSerializer serializer)
        {
            var wrappedValue = value as WrappedValue;
            if (wrappedValue != null)
            {
                return wrappedValue.Value;
            }

            return serializer.Parse((string)value);
        }

        public static string UnwrapJson(object value)
        {
            var wrappedValue = value as WrappedValue;
            if (wrappedValue != null)
            {
                return wrappedValue.ValueJson.Value;
            }
            return null;
        }

        public override string ToString()
        {
            return _serializer.Stringify(_value);
        }
    }
}
