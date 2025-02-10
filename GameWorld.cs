﻿using Microsoft.Xna.Framework;
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
        internal static MousePointer mousePointer;
        private bool gameRunning = true;

        #region Lists

        private List<GameObject> gameObjects = new List<GameObject>();
        public static List<GameObject> newGameObjects = new List<GameObject>();

        #endregion
        #region Assets

        public static Dictionary<Enum, Texture2D> sprites = new Dictionary<Enum, Texture2D>();
        public static Dictionary<Enum, Texture2D[]> animations = new Dictionary<Enum, Texture2D[]>();
        public static Dictionary<string, SoundEffect> soundEffects = new Dictionary<string, SoundEffect>();
        public static Dictionary<string, Song> music = new Dictionary<string, Song>();
        public static SpriteFont gameFont;

        #endregion
        #region Threads

        private Thread drawThread;
        private Mutex drawMutex = new Mutex();

        #endregion
        #endregion
        #region Properties



        #endregion
        #region Constructor

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        #endregion
        #region Methods

        protected override void Initialize()
        {
            base.Initialize();
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();

            mousePointer = new MousePointer(DecorationType.Cursor);
            //gameObjects.Add(new Worker(WorkerID.Simon, Vector2.Zero));

            gameObjects.Add(new ProgressBar(ProgressBarGraphics.BarHollow, new Vector2(100, 100)));
            gameObjects.Add(new ProgressBar(ProgressBarGraphics.Lightning, new Vector2(100, 100)));
            gameObjects.Add(new ProgressBar(ProgressBarGraphics.BarFill, new Vector2(100, 100)));


            drawThread = new Thread(RunDraw);
            drawThread.IsBackground = true;
            drawThread.Start();

        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadSprites(Content, sprites);
            LoadAnimations(Content, animations);
            LoadSounds(Content, soundEffects);
            LoadMusic(Content, music);
            gameFont = Content.Load<SpriteFont>("standardFont");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                gameRunning = false;
                Thread.Sleep(30);
                Exit();
            }

            base.Update(gameTime);

            foreach (GameObject gameObject in gameObjects)
            {

            }


        }

        protected override void Draw(GameTime gameTime)
        {
            drawMutex.WaitOne();
            base.Draw(gameTime);
            drawMutex.ReleaseMutex();
        }

        #region LoadAssets

        private void LoadSprites(ContentManager content, Dictionary<Enum, Texture2D> sprites)
        {
            sprites.Add(ProgressBarGraphics.BarHollow, Content.Load<Texture2D>("Sprites\\barHollow"));
            sprites.Add(ProgressBarGraphics.BarFill, Content.Load<Texture2D>("Sprites\\barFill"));
            sprites.Add(ProgressBarGraphics.Lightning, Content.Load<Texture2D>("Sprites\\lyn"));
        }

        private void LoadAnimations(ContentManager content, Dictionary<Enum, Texture2D[]> animations)
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
                drawMutex.WaitOne();
                GraphicsDevice.Clear(Color.CornflowerBlue);
                _spriteBatch.Begin();

                mousePointer.Draw(_spriteBatch);
                try
                {
                    foreach (GameObject gameObject in gameObjects)
                    {
                        gameObject.Draw(_spriteBatch);
                    }
                }
                catch { }

                _spriteBatch.End();
                drawMutex.ReleaseMutex();
            }

        }

        #endregion
    }
}
