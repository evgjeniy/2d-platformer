namespace Utils
{
    public static class NullCheckExtensions
    {
        public static T IfNotNull<T>(this T component, System.Action ifNotNull)
        {
            if (component != null) ifNotNull?.Invoke();
            return component;
        }
        
        public static T IfNotNull<T>(this T component, System.Action<T> ifNotNull)
        {
            if (component != null) ifNotNull?.Invoke(component);
            return component;
        }
        
        public static T2 IfNotNull<T1, T2>(this T1 component, System.Func<T2> ifNotNull) where T2 : class
        {
            return component != null && ifNotNull != null ? ifNotNull.Invoke() : component as T2;
        }
        
        public static T IfNull<T>(this T component, System.Action ifNull)
        {
            if (component == null) ifNull?.Invoke();
            return component;
        }
        
        public static T IfNull<T>(this T component, System.Action<T> ifNull)
        {
            if (component == null) ifNull?.Invoke(component);
            return component;
        }
        
        public static T2 IfNull<T1, T2>(this T1 component, System.Func<T2> ifNull) where T2 : class
        {
            return component == null && ifNull != null ? ifNull.Invoke() : component as T2;
        }
    }
}