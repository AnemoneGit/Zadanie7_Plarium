using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie7A_Plarium
{
    [Serializable]
    class Pogoda:DataBase
    {
        
        public Region reg { get; set; }
        public DateTime date;
        public decimal temp;
        public string osad;
       
        public Pogoda[] pogodas;


        public delegate void AccountHandler(string message);
        private event AccountHandler _notify;
        public event AccountHandler Notify
        {
            add
            {
                _notify += value;
                Console.WriteLine($"{value.Method.Name} добавлен");
            }
            remove
            {
                _notify -= value;
                Console.WriteLine($"{value.Method.Name} удален");
            }
        }

        public Pogoda(Region region, DateTime Date, decimal T, string Osad)
        {
            reg = region;
            date = Date;
            temp = T;
            osad = Osad;
        }
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < pogodas.Length; i++)
            {
                yield return pogodas[i];
            }
        }
        public void Add(Pogoda pogoda)
        {
            pogodas = pogodas.Concat(new Pogoda[] { pogoda }).ToArray();
            AddToBase(pogoda);
        }
        public Pogoda()
        {
            pogodas = new Pogoda[] { };

        }
        public void GetPogoda(Pogoda vezers, Region region)
        {
            foreach (Pogoda pogoda in vezers)
                if (pogoda.reg.Nazva == region.Nazva)
                {
                    _notify?.Invoke($"{pogoda.reg.GetInfo()} {pogoda.date} числа, температура {pogoda.temp + "°C"} , осадки:{pogoda.osad}");
                }

        }

        public void GetData(Pogoda vezers, Region region, string osadki, decimal zTemp)
        {
            foreach (Pogoda pogoda in vezers)
                if (pogoda.reg.Nazva == region.Nazva && pogoda.osad == osadki && zTemp > pogoda.temp)
                    _notify?.Invoke($" {pogoda.date} числа {pogoda.reg.GetInfo()}, температура {pogoda.temp + "°C"} была меньше заданной {zTemp + "°C"}, и были заданные осадки:{pogoda.osad}");
        }

        public void GetPogoda(Pogoda vezers, string Lang)
        {

            foreach (Pogoda pogoda in vezers)
            {
                if (Lang == pogoda.reg.people.Langue && pogoda.date.AddDays(7) >= DateTime.Today)
                {
                    _notify?.Invoke($"{pogoda.reg.GetInfo()} люди говорят на языке {Lang} {pogoda.date} числа, температура {pogoda.temp + "°C"}, осадки:{pogoda.osad}");
                }
            }
        }

        public void GetTemp(Pogoda vezers, int Zplochad, Dictionary<int, Region> regions)
        {
            decimal srTemp = 0;
            foreach (Region region in regions.Values)
            {
                try
                {
                    if (region.Plochad > Zplochad)
                        foreach (Pogoda pogoda in vezers)
                            if (pogoda.reg.Nazva == region.Nazva && pogoda.date.AddDays(7) >= DateTime.Today)
                                srTemp += pogoda.temp;

                    _notify?.Invoke($"{region.GetInfo()} средняя температура {srTemp + "°C"}");
                    srTemp = 0;
                }
                catch { _notify?.Invoke($"возникла ошибка, выход за пределы"); }

            }

        }

        public override string ToString()
        {
            return $"Регион:\n{reg.GetInfo()}\nДата:\n{date}\nТемпература: {temp + "°C"}\nОсодки: {osad}\n";
        }

        private void AddToBase(Pogoda pogoda)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("Pogoda.txt", true))
            {
               
                    file.WriteLine(pogoda.ToString()); 
                
                file.Close();
            }
        }

        public void Cleener()
        {
            System.IO.File.WriteAllBytes("Pogoda.txt", new byte[0]);
        }
    }
}
