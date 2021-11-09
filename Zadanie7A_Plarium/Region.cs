using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie7A_Plarium
{
    [Serializable]
    abstract class Region:DataBase
    {
        public string Nazva;
        public int Plochad;
        public People people;

        public Region(string Name, int plochad, People person)
        {
            Nazva = Name;
            Plochad = plochad;
            people = person;
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("Region.txt", true))
            {

                file.WriteLine($"Регион:\n{Nazva}\nПлощадь:\n{Plochad}\nЛюди: {people.Nazvanie}\n");

                file.Close();
            }


        }

        public void Cleener()
        {
            System.IO.File.WriteAllBytes("Region.txt", new byte[0]);
        }

        public abstract string GetInfo();

    }

    class Oblast : Region
    {
        public Oblast(string Name, int plochad, People person) : base(Name, plochad, person)
        {
        }
        public override string GetInfo()
        {
            return $"В областе {Nazva} ";
        }
    }

    class Country : Region
    {
        public Country(string Name, int plochad, People person) : base(Name, plochad, person)
        {
        

        }
        
        public override string GetInfo()
        {
            return $"В стране {Nazva} ";
        }
    }

    class City : Region
    {
        public City(string Name, int plochad, People person) : base(Name, plochad, person)
        {
        }

        public override string GetInfo()
        {
            return $"В городе {Nazva} ";
        }
    }
}
