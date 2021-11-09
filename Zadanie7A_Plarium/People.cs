using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie7A_Plarium
{
    [Serializable]
    class People:DataBase
    {
        public string Nazvanie;
        public string Langue;
        
        public People(string Name, string langue)
        {
            Nazvanie = Name;
            Langue = langue;
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("People.txt", true))
            {

                file.WriteLine($"Люди:\n{Nazvanie}\nЯзык:\n{Langue}\n");

                file.Close();
            }

        }
        public People(People other)
        {
            Nazvanie = other.Nazvanie;
            Langue = other.Langue;
        }

        public void Cleener()
        {
            System.IO.File.WriteAllBytes("People.txt", new byte[0]);
        }
    }
}
