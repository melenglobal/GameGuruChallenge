using Case2Folders.Scripts.Interfaces;

namespace Case2Folders.Scripts.Commands
{
    public class SaveCommand
    {
        public void Execute<T>(T dataToSave, int uniqueID) where T : ISaveableEntity
        {   
            string path =dataToSave.GetKey() + uniqueID.ToString() + ".es3";

            string dataKey = dataToSave.GetKey();
            
            if (!ES3.FileExists(path))
            {
                ES3.Save(dataKey,dataToSave,path);
            }

            ES3.Save(dataKey,dataToSave,path);
        }
    }
}