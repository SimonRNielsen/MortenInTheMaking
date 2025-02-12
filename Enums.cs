﻿namespace MortenInTheMaking
{
    //Enum which doesn't have a sprites to
    public enum DecorationType
    {
        Station,
        //PCStation, 
        Background,
        Cursor,
        SelectionBox,
        Sign,
        Morten, 
        BarBlack,
        Start,
        End,
        TextBox1,
        TextBox2
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
        Productivity,
        Status
    }

    public enum WorkstationType
    {
        Computer,
        //Station,
        WaterStation,
        MilkStation,
        CoffeeBeanStation,
        BrewingStation
    }

    public enum OverlayGraphics
    {
        BarHollow,
        //BarFill,
        Lightning,
        MoneySquare
    }

    public enum ProgressFilling
    { 
        BarFilling
    }

    public enum SoundMusic
    {
        BackgroundMusic,
        DoneSoundEffect,
        BadSoundEffect
    }

}
