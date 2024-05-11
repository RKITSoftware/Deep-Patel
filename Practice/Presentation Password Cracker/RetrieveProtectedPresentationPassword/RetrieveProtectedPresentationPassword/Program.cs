using Spire.Presentation;

namespace RetrieveProtectedPresentationPassword
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Deep.P\Downloads\demo.pptx";
            string password = GetPresentationPassword(filePath);

            if (!string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Presentation password: " + password);
            }
            else
            {
                Console.WriteLine("The presentation is not protected or the password couldn't be retrieved.");
            }

            Console.ReadLine();
        }

        static string GetPresentationPassword(string filePath)
        {
            string password = "";

            try
            {
                Presentation presentation = new Presentation();
                presentation.LoadFromFile(filePath);

                // Check if the presentation is protected
                presentation.RemoveEncryption();

                presentation.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return password;
        }
    }
}
