using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Scripts.UI.SaveLoad
{

    public class BaseSaveLoad<TData> where TData : class
    {
        protected string Path;
        protected TData Data;

        public BaseSaveLoad(string name)
        {
            Path = System.IO.Path.Combine(Application.streamingAssetsPath, name);
        }

        public void Serialize()
        {
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(TData));

                using (FileStream fs = new FileStream(Path, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, (TData)this.Data);
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
                XmlSerializer formatter = new XmlSerializer(typeof(TData));

                using (FileStream fs = new FileStream(Path, FileMode.Open))
                {
                    this.Data = (TData)formatter.Deserialize(fs);
                }

            }
            catch (Exception ex)
            {
                Logger.LogW(ex, $"Error with getting data in {typeof(TData)} load");
            }

        }


    }

}
