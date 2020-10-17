using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.UI.SaveLoad
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
                Debug.Log(" Load Patch " + Path);

                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream file = File.OpenRead(Path))
                {
                    data = (SaveLoadData)bf.Deserialize(file);
                    file.Close();
                }

            }
            catch (Exception ex)
            {
                Debug.Log($"Error with getting data in load");
                Logger.LogW(ex, $"Error with getting data in load");
            }

            return data;
        }


    }
}
