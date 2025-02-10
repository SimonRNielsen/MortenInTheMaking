using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Threading;

namespace MortenInTheMaking
{
    internal class MousePointer
    {

        #region Fields

        private Texture2D sprite;
        private GameObject tempObject;
        private static Vector2 position;
        private static bool leftClick;
        private static bool rightClick;
        private bool ranLeftClick = false;
        private bool ranRightClick = false;
        private float selectionBoxScale;
        private float selectionBoxLayer = 0.99f;
        private Thread inputThread;

        #endregion
        #region Properties

        public Rectangle CollisionBox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, 1, 1); }
        }
        public Vector2 Position { get => position; }
        public bool LeftClick
        {
            get => leftClick;
            private set
            {
                leftClick = value;
                if (value == true)
                    LeftClickEvent();
                else
                    ranLeftClick = false;
            }
        }
        public bool RightClick 
        { 
            get => rightClick;
            private set
            {
                rightClick = value;
                if (value == true)
                    RightClickEvent();
                else
                    ranRightClick = false;
            }
        }

        #endregion
        #region Constructor

        public MousePointer(Enum type)
        {

            try
            {
                sprite = GameWorld.sprites[type];
            }
            catch { }
            inputThread = new Thread(HandleInput);
            inputThread.IsBackground = true;
            inputThread.Start();

        }

        #endregion
        #region Methods

        /// <summary>
        /// Draws a custom mousecursor at the location its detected to be in
        /// </summary>
        /// <param name="spriteBatch">GameWorld logic</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (sprite != null)
                spriteBatch.Draw(sprite, position, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 1f);
            if (tempObject != null)
                spriteBatch.Draw(GameWorld.sprites[DecorationType.SelectionBox], tempObject.Position, null, Color.White, 0f, Vector2.Zero, selectionBoxScale, SpriteEffects.None, selectionBoxLayer);
        }


        private void SetSelectionBoxSize()
        {
            selectionBoxScale = Math.Max((float)(tempObject.Sprite.Width / GameWorld.sprites[DecorationType.SelectionBox].Width), (float)(tempObject.Sprite.Height / GameWorld.sprites[DecorationType.SelectionBox].Height));
            selectionBoxLayer = tempObject.Layer + 0.001f;
        }


        private void LeftClickEvent()
        {
            if (!ranLeftClick)
                foreach (GameObject gameObject in GameWorld.gameObjects)
                {
                    if (gameObject is ISelectable)
                    {
                        if (gameObject.CollisionBox.Intersects(CollisionBox))
                        {
                            tempObject = gameObject;
                            SetSelectionBoxSize();
                            break;
                        }
                        else
                            tempObject = null;
                    }
                }
            ranLeftClick = true;
        }


        private void RightClickEvent()
        {
            if (tempObject != null && !ranRightClick)
                foreach (GameObject gameObject in GameWorld.gameObjects)
                {
                    if (gameObject is ISelectable && tempObject is Worker && gameObject.CollisionBox.Intersects(CollisionBox) && !(tempObject as Worker).Busy)
                        if (gameObject is Workstation)
                        {
                            (gameObject as ISelectable).AssignToWorkstation(tempObject as Worker, gameObject as Workstation);
                            (tempObject as Worker).Busy = true;
                        }
                }
            ranRightClick = true;
        }


        private void HandleInput()
        {

            while (GameWorld.GameRunning)
            {
                var mouseState = Mouse.GetState();
                position = mouseState.Position.ToVector2();
                LeftClick = mouseState.LeftButton == ButtonState.Pressed;
                RightClick = mouseState.RightButton == ButtonState.Pressed;
            }

        }

        #endregion

    }
}
