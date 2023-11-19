namespace VibeTogether.Authorization.Helpers
{
    public static class ConnectionStringHelper
    {
        private static string? _connectionString;
        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    throw new InvalidOperationException("Connection string is empty. Set connection string before get.");
                }
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }
    }
}
