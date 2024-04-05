namespace ExceptionHandlingDemo.Business_Logic
{
    /// <summary>
    /// Business Logic class for Home Controller
    /// </summary>
    public class BLHome
    {
        /// <summary>
        /// Produce DivideByZeroException
        /// </summary>
        public static void Get()
        {
            int a = 0;
            int b = 1 / a;
        }
    }
}
