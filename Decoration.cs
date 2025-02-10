using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortenInTheMaking
{
    internal class Decoration : GameObject
    {
        #region Fields



        #endregion
        #region Properties



        #endregion
        #region Constructor


        public Decoration(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {
            Type = type;
            position = spawnPos;

            this.sprite = GameWorld.sprites[DecorationType.Background];
            this.layer = 0.0f;
        }

        #endregion
        #region Methods

        //public override void LoadContent(ContentManager content)
        //{
        //    //base.LoadContent(content);

        //}


        #endregion
    }
}
