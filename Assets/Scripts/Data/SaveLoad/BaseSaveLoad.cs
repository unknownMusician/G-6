using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using JetBrains.Annotations;
using UnityEngine;

namespace G6.Data.SaveLoad
{

    public class BaseSaveLoad
    {
        protected string Path;

        public BaseSaveLoad(string name)
        {
            Path = Application.streamingAssetsPath + "/Savings/" + name;
            if (!Directory.Exists(Application.streamingAssetsPath + "/Savings/"))
                Directory.CreateDirectory(Application.streamingAssetsPath + "/Savings/");
        }

        public void Serialize(SaveLoadData data)
        {
            Debug.Log(Path);
           // try
           // {
                if (data != null)
                {
                    Stream stream = File.Open(Path, FileMode.Create);
                    BinaryFormatter bformatter = new BinaryFormatter();
                    bformatter.Serialize(stream, data);
                    stream.Close();

                }
                else
                {
                    Debug.Log("Data is null");
                }
           // }
           // catch (Exception ex)
          //  {

             //   Debug.Log($"Error with putting data in  save");
            ///    Logger.LogW(ex, $"Error with putting data in  save");

           // }

        }

        [CanBeNull]
        public SaveLoadData Deserialize()
        {
            SaveLoadData data = null;
            try
            {
                if(!File.Exists(Path)) { return null; }

                using (FileStream file = File.OpenRead(Path))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    data = (SaveLoadData)bf.Deserialize(file);
                    file.Close();
                }

                Debug.Log(" Load Path " + Path);
            }
            catch
            {
                Debug.LogWarning($"Error with getting data in load");
            }

            return data;
        }


    }
}
