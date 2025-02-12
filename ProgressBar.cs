using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortenInTheMaking
{
    internal class ProgressBar : GameObject
    {
        public ProgressBar(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {
            this.layer = 0.8f;
            this.sprite = GameWorld.sprites[type];
        }
    }
}
