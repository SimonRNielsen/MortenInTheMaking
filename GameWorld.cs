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

        public static Dictionary<string, Texture2D> sprites = new Dictionary<string, Texture2D>();
        public static Dictionary<string, Texture2D[]> animations = new Dictionary<string, Texture2D[]>();
        public static Dictionary<string, SoundEffect> soundEffects = new Dictionary<string, SoundEffect>();
        public static Dictionary<string, Song> music = new Dictionary<string, Song>();
        public static SpriteFont gameFont;

        #endregion

        #endregion
        #region Properties

        #region Mouse
        public static Vector2 MousePosition { get => mousePosition; }
        public static bool LeftMouseClick { get => leftMouseClick; }
        public static bool RightMouseClick { get => rightMouseClick; }
        #endregion

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

        private void LoadSprites(ContentManager content, Dictionary<string, Texture2D> sprites)
        {

        }

        private void LoadAnimations(ContentManager content, Dictionary<string, Texture2D[]> animations)
        {

        }

        private void LoadSounds(ContentManager content, Dictionary<string, SoundEffect> sounds)
        {

        }

        private void LoadMusic(ContentManager content, Dictionary<string, Song> songs)
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
