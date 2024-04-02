namespace Back_end_API.Handle.CCCD
{
    public class Validate_CCCD
    {
        public static bool IsNumber(string str)
        {
            foreach (var item in str) 
            {
                if (!char.IsDigit(item))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
