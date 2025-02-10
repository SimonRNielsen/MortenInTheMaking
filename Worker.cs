using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortenInTheMaking
{
    internal class Worker : GameObject, ISelectable
    {
        #region Fields

        private bool busy;

        #endregion
        #region Properties

        public bool Busy { get => busy; set => busy = value; }

        #endregion
        #region Constructor

        public Worker(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {
            Type = type;
            position = spawnPos;
        }

        #endregion
        #region Methods



        #endregion
    }
}
