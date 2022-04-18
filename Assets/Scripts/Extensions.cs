public static class Extensions 
{
    public static string ToHeader(this string str)
    {
        char[] chars = str.ToUpper().ToCharArray();
        return string.Join(" ", chars);
    }
}
