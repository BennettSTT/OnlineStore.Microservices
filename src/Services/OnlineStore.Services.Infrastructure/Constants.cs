namespace OnlineStore.Services.Infrastructure
{
    public static class Constants
    {
        public static class EventBus
        {
            public static class Exchange
            {
                public static class Type
                {
                    public const string Default = "";
                    public const string Direct = "direct";
                    public const string Topic = "topic";
                    public const string Fanout = "fanout";
                    public const string Header = "headers";
                }

                public static class Name
                {
                    public const string Default = "OnlineStore.Exchange.Default";
                }
            }
        }
    }
}