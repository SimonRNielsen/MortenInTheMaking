using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MortenInTheMaking
{
    public class GameWorld : Game
    {
        #region Fields

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private static Vector2 mousePosition;
        private static bool leftMouseClick;
        private static bool rightMouseClick;
        internal static MousePointer mousePointer;
        private volatile GameTime gameTime;
        private bool gameRunning = true;

        #region Lists

        private List<GameObject> gameObjects = new List<GameObject>();
        public static List<GameObject> newGameObjects = new List<GameObject>();

        #endregion
        #region Threads

        private Thread drawThread;

        #endregion
        #region Assets

        public static Dictionary<Texture2D, string> sprites = new Dictionary<Texture2D, string>();
        public static Dictionary<Texture2D[], string> animations = new Dictionary<Texture2D[], string>();
        public static Dictionary<SoundEffect, string> soundEffects = new Dictionary<SoundEffect, string>();
        public static Dictionary<Song, string> music = new Dictionary<Song, string>();
        public static SpriteFont gameFont;

        #endregion

        #endregion
        #region Properties

        public static Vector2 MousePosition { get => MousePosition; }
        public static bool LeftMouseClick { get => leftMouseClick; }
        public static bool RightMouseClick { get => rightMouseClick; }


        #endregion
        #region Constructor

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            drawThread = new Thread(RunDraw);
            drawThread.IsBackground = true;
        }

        #endregion
        #region Methods

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();

            base.Initialize();

            LoadSprites(Content, sprites);
            LoadAnimations(Content, animations);
            LoadSounds(Content, soundEffects);
            LoadMusic(Content, music);
            gameFont = Content.Load<SpriteFont>("standardFont");
            mousePointer = new MousePointer();
            drawThread.Start();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                gameRunning = false;
                Thread.Sleep(30);
                Exit();
            }

            this.gameTime = gameTime;

            // TODO: Add your update logic here

            base.Update(gameTime);

            foreach (GameObject gameObject in gameObjects)
            {

            }

            //Add lock here (Critical region) -> Mutex?
            gameObjects.RemoveAll(obj => obj.IsAlive == false);
            gameObjects.AddRange(newGameObjects);
            //

            newGameObjects.Clear();

        }

        protected override void Draw(GameTime gameTime)
        {
        }

        #region LoadAssets

        private void LoadSprites(ContentManager content, Dictionary<Texture2D, string> sprites)
        {

        }

        private void LoadAnimations(ContentManager content, Dictionary<Texture2D[], string> animations)
        {

        }

        private void LoadSounds(ContentManager content, Dictionary<SoundEffect, string> sounds)
        {

        }

        private void LoadMusic(ContentManager content, Dictionary<Song, string> songs)
        {

        }

        #endregion


        private void RunDraw()
        {

            while (gameRunning)
            {
                
                GraphicsDevice.Clear(Color.CornflowerBlue);

                _spriteBatch.Begin();

                mousePointer.Draw(_spriteBatch);
                if (gameObjects.Count > 0)
                    foreach (GameObject gameObject in gameObjects)
                    {
                        gameObject.Draw(_spriteBatch);
                    }

                base.Draw(gameTime);

                _spriteBatch.End();

            }

        }

        #endregion
    }
}
