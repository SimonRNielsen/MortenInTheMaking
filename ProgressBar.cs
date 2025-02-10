using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortenInTheMaking
{
    internal class ProgressBar : Overlay
    {
        public ProgressBar(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {
            this.scale = 1000f;

        }
    }
}
