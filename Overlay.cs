﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortenInTheMaking
{
    internal class Overlay : GameObject
    {
        #region Fields



        #endregion
        #region Properties



        #endregion
        #region Constructor


        public Overlay(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {
            Type = type;
            position = spawnPos;
        }

        #endregion
        #region Methods



        #endregion
    }
}
