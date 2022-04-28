using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKAH_App
{
    //Сопряжения
    class Link
    {
        //Счётчик Id для класса
        private static int ID = 0;
        //Id
        public int id { get; set; }
        //Детали
        public Surface surface_1;
        public Surface surface_2;
        public Link_Type? link_type;

        //Расстояние
        public float dist = 0;

        /*Свойства*/
        //Наименование первой детали
        public string det_name_1
        {
            get
            {
                return surface_1.detail.name;
            }
        }
        public string Name_surf_1
        {
            get
            {
                return surface_1.name;
            }
        }
        //Второй
        public string det_name_2
        {
            get
            {
                return surface_2.detail.name;
            }
        }
        public string Name_surf_2
        {
            get
            {
                return surface_2.name;
            }
        }
        //Тип сопряжения
        public string type_of_link
        {
            get
            {
                string result = link_type switch
                {
                    Link_Type.Tight_Contact => "Плотный контакт",
                    Link_Type.Distance => "Расстояние ("+this.dist.ToString().Replace(",", ".")+")",
                    Link_Type.Gap_or_Conctact => "Зазор или контакт",
                    Link_Type.Gap => "Зазор без контакта",
                    Link_Type.Misalignment => "Cоосность",
                    Link_Type.Touch => "Касание",
                    _ => "Null"
                };
                return result;
            }
        }

        //Конструктор
        public Link(Surface surface_1, Surface surface_2, Link_Type? link_type, float dist = 0)
        {
            this.surface_1 = surface_1;
            this.surface_2 = surface_2;
            this.link_type = link_type;
            id = ID;
            ID++;
            this.dist = dist;
        }

    }
}
