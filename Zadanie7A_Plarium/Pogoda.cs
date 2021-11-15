using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Zadanie7A_Plarium
{
    [Serializable]
    class Pogoda//класс погоды
    {
        public Region reg { get; set; }//регион по которому будет информация о погоде
        public DateTime date;//дата
        public decimal temp;//температура
        public string osad;//осадки

        public delegate void AccountHandler(string message);//делегат для события
        private event AccountHandler _notify;//событие(вывод информации если подписались на событие)
        public event AccountHandler Notify//добавление и удаление событий
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
        public Pogoda()
        {
           

        }
        public void GetPogoda(List<Pogoda> vezers, Region region)//Вывести сведения о погоде в заданном регионе
        {
            foreach (Pogoda pogoda in vezers)
                if (pogoda.reg.Nazva == region.Nazva)
                {
                    _notify?.Invoke($"{pogoda.reg.GetInfo()} {pogoda.date} числа, температура {pogoda.temp + "°C"} , осадки:{pogoda.osad}");
                }

        }
        public void GetData(List<Pogoda> vezers, Region region, string osadki, decimal zTemp)//Вывести даты, когда в заданном регионе шел снег и температура была ниже заданной отрицательной
        {
            foreach (Pogoda pogoda in vezers)
                if (pogoda.reg.Nazva == region.Nazva && pogoda.osad == osadki && zTemp > pogoda.temp)
                    _notify?.Invoke($" {pogoda.date} числа {pogoda.reg.GetInfo()}, температура {pogoda.temp + "°C"} была меньше заданной {zTemp + "°C"}, и были заданные осадки:{pogoda.osad}");
        }
        public void GetPogoda(List<Pogoda> vezers, string Lang)//Вывести информацию о погоде за прошедшую неделю в регионах, жители которых общаются на заданном языке
        {

            foreach (Pogoda pogoda in vezers)
            {
                if (Lang == pogoda.reg.people.Langue && pogoda.date.AddDays(7) >= DateTime.Today)
                {
                    _notify?.Invoke($"{pogoda.reg.GetInfo()} люди говорят на языке {Lang} {pogoda.date} числа, температура {pogoda.temp + "°C"}, осадки:{pogoda.osad}");
                }
            }
        }
        public void GetTemp(List<Pogoda> vezers, int Zplochad, List< Region> regions)//Вывести среднюю температуру за прошедшую неделю в регионах с площадью больше заданной
        {
            decimal srTemp = 0;
            foreach (Region region in regions)
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
        public override string ToString()//переопределенный метод ToString
        {
            return $"Регион:\n{reg.GetInfo()}\nДата:\n{date}\nТемпература:\n{temp}\nОсодки:\n{osad}\n";
        }
  
        public void Cleener()
        {
            System.IO.File.WriteAllBytes("Pogoda.txt", new byte[0]);
        }
    }
}
