namespace Back_end_API.Handle.Number
{
    public class Validate_Number
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
