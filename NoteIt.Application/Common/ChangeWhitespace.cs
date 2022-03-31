namespace NoteIt.Application.Common
{
    public static class ChangeWhitespace
    {
        public static string ChangeToDash(string data)
        {
            string[] groups = data.Split(new[] { '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string connected = String.Join("-", groups);
            return connected.ToLower();
        }
    }
}
