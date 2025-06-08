using Newtonsoft.Json;

namespace _2048WinFormsAppNew
{
    
    public class UserManager
    {
        public static string Path = "game_history.json";
        public static List<User> GetALl()
        {
            if (FileProvider.Exists(Path))
            {
                var jsonData = FileProvider.GetAll(Path);
                return JsonConvert.DeserializeObject<List<User>>(jsonData);
            }
            return new List<User>();
        }

        public static void Add(User newUser)
        {
            var users = GetALl();
            users.Add(newUser);

            var jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
            FileProvider.Replace(Path, jsonData);
        }
    }
}
