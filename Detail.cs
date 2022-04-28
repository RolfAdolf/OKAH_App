using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace OKAH_App
{
    class Detail
    {
        //Счётчик Id для класса
        private static int ID = 0;
        //Количество деталей. Проверить, сменяется ли значение.
        public static int num_of_details = 0;


        //Id
        public int id { get; set; }
        //Наименование
        public string name { get; set; }
        //Количество сопряжений детали
        public int links { get; set; }

        //Поверхности детали
        public BindingList<Surface> surfaces = new BindingList<Surface>();
        //Количество поверхностей
        public int surfaces_num { get; set; }

        //Добавить поверхность
        public void add_surface(Surface surf)
        {
            this.surfaces.Add(surf);
            this.surfaces_num++;
        }

        //Конструктор
        public Detail(string name)
        {
            this.name = name;
            id = ID;
            links = 0;
            ID++;
            num_of_details++;
            this.surfaces_num = 0;
        }

        //Индекс по Id:
        static public Detail ind_ID(BindingList<Detail> details, int ID)
        {
            return details.SingleOrDefault(p => p.id == ID);
        }
    }




    //Поверхность
    class Surface
    {
        //Деталь
        public Detail detail;
        //Название поверхности
        public string name { get; set; }
        //Тип поверхности
        public Surface_Type type;
        //Размеры поверхностей и допуски
        public Tolerance tolerance = new Tolerance();
        //Свойство типа поверхности
        public string surface_type
        {
            get
            {
                string result = type switch
                {
                    Surface_Type.Cylinder => "Цилиндр",
                    Surface_Type.Plane => "Плоскость",
                    _ => "Null"
                };
                return result;
            }
        }

        //Конструктор
        public Surface(string name, Surface_Type type, Detail detail)
        {
            this.name = name;
            this.type = type;
            this.tolerance.name = name;
            this.detail = detail;
        }
    }
}
