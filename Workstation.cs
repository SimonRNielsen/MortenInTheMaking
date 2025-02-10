using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortenInTheMaking
{
    internal class Workstation : GameObject, ISelectable
    {
        #region Fields



        #endregion
        #region Properties



        #endregion
        #region Constructor


        public Workstation(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {
            Type = type;
            position = spawnPos;

            //Selection the sprite
            this.sprite = GameWorld.sprites[type];

            GameWorld.locations.Add(type, spawnPos);
        }

        #endregion
        #region Methods



        #endregion
    }
}
