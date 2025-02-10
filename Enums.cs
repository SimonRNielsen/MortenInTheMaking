using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortenInTheMaking
{

    public enum DecorationType
    {
        Station,
        //PCStation,
        Background,
        Cursor
    }

    public enum WorkerID
    {
        Irene,
        Philip,
        Rikke,
        Simon
    }

    public enum RessourceType
    {
        Coffee,
        Milk,
        Water,
        CoffeeBeans,
        Money,
        Productivity
    }

    public enum WorkstationType
    {
        Computer,
        Station
        //WaterStation,
        //MilkStation,
        //CoffeeBeanStation,
        //BrewingStation
    }

    public enum ProgressBarGraphics
    { 
        BarHollow,
        BarFill,
        Lightning
    }
}
