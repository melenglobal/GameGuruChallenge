using Case2Folders.Scripts.Interfaces;

namespace Case2Folders.Scripts.Commands
{
    public class LoadCommand
    {
        public T Execute<T>(string key, int uniqueId) where T : ISaveableEntity
        {
            string path = key + uniqueId.ToString() + ".es3";

            if (!ES3.FileExists(path)) return default(T);
            
            if (ES3.KeyExists(key,path))
            {
                T objectToReturn = ES3.Load<T>(key,path);
                    
                return objectToReturn;
            }
            else
            {
                return default(T); 
            }
        }

    }
}