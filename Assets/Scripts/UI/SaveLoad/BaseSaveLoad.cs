using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Scripts.UI.SaveLoad
{

    public class BaseSaveLoad<TData> where TData : class
    {
        protected string Path;
        public TData Data;

        public BaseSaveLoad(string name)
        {
            Path = System.IO.Path.Combine(Application.streamingAssetsPath, name);
        }

        public void Serialize()
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream file = File.Create(Path))
                {
                    bf.Serialize(file, this.Data);
                    file.Close();
                } 
            }
            catch (Exception ex)
            {
                Logger.LogW(ex, $"Error with putting data in {typeof(TData)} save");
            }

        }
        public void Deserialize()
        {
            try
            {

                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream file = File.OpenRead(Path))
                {
                    this.Data = (TData)bf.Deserialize(file);
                    file.Close();
                }
                
            }
            catch (Exception ex)
            {
                Logger.LogW(ex, $"Error with getting data in {typeof(TData)} load");
            }

        }


    }

}
