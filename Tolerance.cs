using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace OKAH_App
{
    /*Допуск*/
    class Tolerance
    {
        //НАименование поверхности
        public string name { get; set; }
        //Тип допуска
        public Tolerance_Type tolerance_type;
        //Номинальное значение
        public float nominal_value { get; set; }
        //Границы
        public float bottom_value { get; set; }
        public float upper_value { get; set; }
        //Допуск
        public float tolerance { get; set; }

        //Свойство типа допуска
        public string type_tolerance
        {
            get
            {
                string result = tolerance_type switch
                {
                    Tolerance_Type.Concentricity_tolerance => "Допуск соосности",
                    Tolerance_Type.Flatness_tolerance => "Допуск плоскостности",
                    Tolerance_Type.Perpendicularity_tolerance => "Допуск перпендикулярности",
                    Tolerance_Type.Positional_tolerance => "Позиционный допуск",
                    Tolerance_Type.Run_Out_tolerance => "Допуск радиального биения",
                    Tolerance_Type.Tilt_tolerance => "Допуск наклона",
                    _ => "Null"
                };
                return result;
            }
        }

        //Конструктор
        public Tolerance(float nominal_value=0,  float bottom_value=0, float upper_value = 0, Tolerance_Type tolerance_type = Tolerance_Type.Concentricity_tolerance)
        {
            this.tolerance_type = tolerance_type;
            this.nominal_value = nominal_value;
            this.bottom_value = bottom_value;
            this.upper_value = upper_value;
        }
    }
}
