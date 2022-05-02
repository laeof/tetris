using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace tetrixd
{
    public class Files
    {
        private string _filepath;
        private int _score = 0;
        public int Tier1, Tier2, Tier3;
        public Files(string filepath)
        {
            _filepath = filepath;
        }
        /// <summary>
        /// импортируем из файла
        /// </summary>
        /// <param название файла="_filepath"></param>
        public void ImportFromFile()
        {
            //экземпляр сериализации
            XmlSerializer serializer = new XmlSerializer(typeof(Leaders));
            //экземпляр для хмл файла
            Leaders f = new Leaders();
            //десериализация
            using (var reader = new StreamReader(_filepath))
            {
                f = (Leaders)serializer.Deserialize(reader);
            }
            Tier1 = f.Tier1;
            Tier2 = f.Tier2;
            Tier3 = f.Tier3;
            
        }
        public void ExportToFile(int score)
        {
            //экземпляр для сериализации
            XmlSerializer serializer = new XmlSerializer(typeof(Leaders));
            //экземпляр для хмл файла
            Leaders f = new Leaders();

            int[] A = new int[4];
            A[0] = Tier1;
            A[1] = Tier2;
            A[2] = Tier3;
            A[3] = score;

            for (int i = 0; i < A.Length; i++)
            {
                for (int j = 0; j < A.Length - 1; j++)
                {
                    if (A[j] < A[j + 1])
                    {
                        int z = A[j];
                        A[j] = A[j + 1];
                        A[j + 1] = z;
                    }
                }
            }

            Tier1 = A[0];
            Tier2 = A[1];
            Tier3 = A[2];

            f.Tier1 = Tier1;
            f.Tier2 = Tier2;
            f.Tier3 = Tier3;
            //сохраняем файлы
            using (FileStream fs = new FileStream(_filepath, FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, f);
            }

        }
    }
    [Serializable]
    [XmlType("Leaders")]
    public class Leaders
    {
        [XmlAttribute("Tier1")]
        public int Tier1 { get; set; }
        [XmlAttribute("Tier2")]
        public int Tier2 { get; set; }
        [XmlAttribute("Tier3")]
        public int Tier3 { get; set; }
    }
}
