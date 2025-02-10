using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortenInTheMaking
{

    public enum DecorationType
    {
        Table,
        Chair,
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
        WaterStation,
        MilkStation,
        CoffeeBeanStation,
        BrewingStation
    }

    public enum ProgressBarGraphics
    { 
        BarHollow,
        BarFill,
        Lightning
    }
}
