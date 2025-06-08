using System.Text;

namespace _2048WinFormsAppNew
{
    public class FileProvider
    {

        public static void Replace(string fileName, string value)
        {
            var writer = new StreamWriter(fileName, false, Encoding.UTF8);
            writer.WriteLine(value);
            writer.Close();
        }

        public static string GetAll(string fileName)
        {
            var reader = new StreamReader(fileName, Encoding.UTF8);
            var value = reader.ReadToEnd();
            reader.Close();
            return value;
        }

        public static bool Exists(string path)
        {
            return File.Exists(path);
        }
    }
}