using Newtonsoft.Json;

namespace _2048WinFormsAppNew
{
    
    public class UserManager
    {
        public string Path = "resaults.json";
        public List<User> GetALl()
        {
            var jsonData = FileProvider.GetAll(Path);
            return JsonConvert.DeserializeObject<List<User>>(jsonData);
        }

        public void Add(User newUser)
        {
            var users = GetALl();
            users.Add(newUser);

            var jsonData = JsonConvert.SerializeObject(users);
            FileProvider.Replace(Path, jsonData);
        }
    }
}
