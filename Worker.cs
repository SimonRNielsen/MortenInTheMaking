using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortenInTheMaking
{
    internal class Worker : GameObject
    {
        #region Fields



        #endregion
        #region Properties



        #endregion
        #region Constructor

        public Worker(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {
            Type = type;
            position = spawnPos;
            this.sprite = GameWorld.sprites[type];
            this.layer = 0.98f;
        }

        #endregion
        #region Methods



        #endregion
    }
}
