using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKAH_App
{
    //Тип сопряжения
    enum Link_Type
    {
        Tight_Contact,                                  //Плотный контакт
        Distance,                                       //Расстояние
        Gap_or_Conctact,                                //Зазор или контакт
        Gap,                                            //Зазор (контакт не допустим)
        Misalignment,                                   //Cоосность
        Touch                                           //Касание
    }

    //Тип допуска
    enum Tolerance_Type
    {
        Flatness_tolerance,
        Perpendicularity_tolerance,
        Concentricity_tolerance,
        Tilt_tolerance,
        Run_Out_tolerance,
        Positional_tolerance
    }

    //Тип поверхности
    enum Surface_Type
    {
        Cylinder,
        Plane
    }
}
