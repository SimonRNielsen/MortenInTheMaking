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
        internal static MousePointer mousePointer;
        private static bool gameRunning = true;
        public static List<GameObject> gameObjects = new List<GameObject>();
        public static Dictionary<Enum, Vector2> locations = new Dictionary<Enum, Vector2>();

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

        public static bool GameRunning { get => gameRunning; }
        public SpriteBatch SpriteBatch { get => _spriteBatch; set => _spriteBatch = value; }

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



            #region decoration the office
            gameObjects.Add(new Decoration(DecorationType.Background, new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2)));
            gameObjects.Add(new Workstation(WorkstationType.Computer, new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight * 3 / 5)));
            gameObjects.Add(new Decoration(DecorationType.Sign, new Vector2(960, 80)));


            int stationMove = 190; //Background to the different kind of stations 
            gameObjects.Add(new Decoration(DecorationType.Station, new Vector2(stationMove / 2, 320))); //Top left
            gameObjects.Add(new Decoration(DecorationType.Station, new Vector2(_graphics.PreferredBackBufferWidth - stationMove / 2, 320))); //Top rigth
            gameObjects.Add(new Decoration(DecorationType.Station, new Vector2(stationMove / 2, _graphics.PreferredBackBufferHeight - stationMove / 2))); //Bottom left
            gameObjects.Add(new Decoration(DecorationType.Station, new Vector2(_graphics.PreferredBackBufferWidth - stationMove / 2, _graphics.PreferredBackBufferHeight - stationMove / 2))); //Bottom rigth

            #endregion
            #region overlay
            gameObjects.Add(new ProductivityManager(OverlayGraphics.BarHollow, new Vector2(299, 975)));
            gameObjects.Add(new ProductivityManager(ProgressFilling.BarFilling, new Vector2(303, 980)));
            gameObjects.Add(new ProductivityManager(OverlayGraphics.Lightning, new Vector2(280, 950)));
            gameObjects.Add(new ProductivityManager(OverlayGraphics.MoneySquare, new Vector2(1300, 950)));
            #endregion

            gameObjects.Add(new Worker(WorkerID.Irene, new Vector2(500, 500)));

            drawThread = new Thread(RunDraw);
            drawThread.IsBackground = true;
            drawThread.Start();

        }

        protected override void LoadContent()
        {

            SpriteBatch = new SpriteBatch(GraphicsDevice);

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
            //ProgressBar
            sprites.Add(OverlayGraphics.BarHollow, Content.Load<Texture2D>("Sprites\\barHollow"));
            sprites.Add(ProgressFilling.BarFilling, Content.Load<Texture2D>("Sprites\\barFill"));
            sprites.Add(OverlayGraphics.Lightning, Content.Load<Texture2D>("Sprites\\lyn"));
            sprites.Add(OverlayGraphics.MoneySquare, Content.Load<Texture2D>("Sprites\\money"));

            //Decoration
            sprites.Add(DecorationType.Background, Content.Load<Texture2D>("Sprites\\office_background"));
            sprites.Add(DecorationType.Station, Content.Load<Texture2D>("Sprites\\station"));
            sprites.Add(DecorationType.Cursor, Content.Load<Texture2D>("Sprites\\mousePointer"));
            sprites.Add(DecorationType.SelectionBox, Content.Load<Texture2D>("Sprites\\selection"));
            sprites.Add(DecorationType.Sign, Content.Load<Texture2D>("Sprites\\sign"));

            //Worker
            sprites.Add(WorkerID.Irene, Content.Load<Texture2D>("Sprites\\irene"));
            sprites.Add(WorkerID.Philip, Content.Load<Texture2D>("Sprites\\philip"));
            sprites.Add(WorkerID.Rikke, Content.Load<Texture2D>("Sprites\\rikke"));
            sprites.Add(WorkerID.Simon, Content.Load<Texture2D>("Sprites\\simon"));

            //RessourceType
            sprites.Add(RessourceType.CoffeeBeans, Content.Load<Texture2D>("Sprites\\coffeebean"));
            sprites.Add(RessourceType.Milk, Content.Load<Texture2D>("Sprites\\milk"));
            sprites.Add(RessourceType.Water, Content.Load<Texture2D>("Sprites\\water"));
            sprites.Add(RessourceType.Coffee, Content.Load<Texture2D>("Sprites\\cup"));

            //WorkStation
            sprites.Add(WorkstationType.Computer, Content.Load<Texture2D>("Sprites\\pcStation"));
            sprites.Add(WorkstationType.CoffeeBeanStation, Content.Load<Texture2D>("Sprites\\coffeebean"));
            sprites.Add(WorkstationType.MilkStation, Content.Load<Texture2D>("Sprites\\milk"));
            sprites.Add(WorkstationType.WaterStation, Content.Load<Texture2D>("Sprites\\water"));
            sprites.Add(WorkstationType.BrewingStation, Content.Load<Texture2D>("Sprites\\cup"));
        }

        private void LoadAnimations(ContentManager content, Dictionary<Enum, Texture2D[]> animations)
        {

        }

        private void LoadSounds(ContentManager content, Dictionary<string, SoundEffect> sounds)
        {

        }

        private void LoadMusic(ContentManager content, Dictionary<string, Song> songs)
        {
            music.Add("backgroundMusic", Content.Load<Song>("Sounds\\game-music-loop-19-153393"));
            MediaPlayer.Play(music["backgroundMusic"]);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.15f;
        }

        #endregion


        private void RunDraw()
        {

            while (gameRunning)
            {
                drawMutex.WaitOne();
                Thread.Sleep(1);
                GraphicsDevice.Clear(Color.CornflowerBlue);
                SpriteBatch.Begin(samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.FrontToBack);

                mousePointer.Draw(SpriteBatch);
                try
                {
                    foreach (GameObject gameObject in gameObjects)
                    {
                        gameObject.Draw(SpriteBatch);
                    }
                }
                catch { }

                SpriteBatch.End();
                drawMutex.ReleaseMutex();
            }

        }

        #endregion
    }
}
