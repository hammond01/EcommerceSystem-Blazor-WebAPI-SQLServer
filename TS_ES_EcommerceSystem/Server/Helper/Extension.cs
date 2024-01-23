namespace Server.Helper
{
    public static class Extension
    {
        public static string GetInsertQuery(string table, string idColumn, params string[] props)
        {
            string key = string.Join(", ", props);
            string value = $"@{string.Join(", @", props)}";
            string query = @$"INSERT INTO {table}({key}) OUTPUT INSERTED.{idColumn} VALUES({value});";
            return query;
        }
        public static string GetDeleteQueryInt(string table, string idColumn, int props)
        {
            string query = $"DELETE FROM {table} WHERE {idColumn} = {props};";
            return query;
        }
        public static string GetDeleteQueryString(string table, string idColumn, string props)
        {
            string query = $"DELETE FROM {table} WHERE {idColumn} = '{props}';";
            return query;
        }
    }
}
