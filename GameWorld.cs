using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Threading;

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
        internal static Workstation CoffeeBeanStation;
        internal static Workstation MilkStation;
        internal static Workstation WaterStation;
        internal static Workstation BrewingStation;
        internal static Workstation ComputerStation;

        private static int money;
        private static int productivity = 3; //Start productivity
        private static int winCondition = 1000000; ///Win conditions amount

        internal static Decoration startScreen;
        internal static SoundEffectInstance brewingSoundEffectInstance;
        internal static SoundEffectInstance typpingSoundEffectInstance;

        #region Assets

        public static Dictionary<Enum, Texture2D> sprites = new Dictionary<Enum, Texture2D>();
        public static Dictionary<Enum, Texture2D[]> animations = new Dictionary<Enum, Texture2D[]>();
        public static Dictionary<string, SoundEffect> soundEffects = new Dictionary<string, SoundEffect>();
        public static Dictionary<string, Song> music = new Dictionary<string, Song>();
        public static SpriteFont ressourceFont;

        #endregion
        #region Threads

        private Thread drawThread;
        private Mutex drawMutex = new Mutex();

        #endregion
        #endregion
        #region Properties

        public static bool GameRunning { get => gameRunning; }

        public static int Productivity { get => productivity; set => productivity = value; }

        public static int Money { get => money; set => money = value; }

        #endregion
        #region Constructor

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
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



            #region decoration the office
            startScreen = new Decoration(DecorationType.Start, new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2));
            gameObjects.Add(startScreen); ////////////////////////////////

            gameObjects.Add(new Decoration(DecorationType.Background, new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2)));
            //gameObjects.Add(new Workstation(WorkstationType.Computer, new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight * 3 / 5))); Note form Philip: Moved it down with other workstations
            gameObjects.Add(new Decoration(DecorationType.Sign, new Vector2(960, 80)));

            gameObjects.Add(new Ressource(RessourceType.Status, Vector2.Zero));
            gameObjects.Add(new Decoration(DecorationType.Morten, new Vector2(1400, 200))); //Undercover Morten

            int stationMove = 190; //Background to the different kind of stations 
            gameObjects.Add(new Decoration(DecorationType.Station, new Vector2(stationMove / 2, 320))); //Top left
            gameObjects.Add(new Decoration(DecorationType.Station, new Vector2(_graphics.PreferredBackBufferWidth - stationMove / 2, 320))); //Top rigth
            gameObjects.Add(new Decoration(DecorationType.Station, new Vector2(stationMove / 2, _graphics.PreferredBackBufferHeight - stationMove / 2))); //Bottom left
            gameObjects.Add(new Decoration(DecorationType.Station, new Vector2(_graphics.PreferredBackBufferWidth - stationMove / 2, _graphics.PreferredBackBufferHeight - stationMove / 2))); //Bottom rigth

            gameObjects.Add(new Decoration(DecorationType.TextBox1, new Vector2(145, 95))); //left
            gameObjects.Add(new Decoration(DecorationType.TextBox2, new Vector2(1760, 95))); //right



            #endregion
            #region overlay
            gameObjects.Add(new Decoration(OverlayGraphics.MoneySquare, new Vector2(1480, 1000)));
            gameObjects.Add(new Decoration(OverlayGraphics.BarHollow, new Vector2(780, 1000)));
            gameObjects.Add(new ProductivityManager(ProgressFilling.BarFilling, new Vector2(303, 983)));
            gameObjects.Add(new Decoration(OverlayGraphics.Lightning, new Vector2(255, 1000)));
            #endregion

            //Worker
            gameObjects.Add(new Worker(WorkerID.Irene, new Vector2(_graphics.PreferredBackBufferWidth / 2 - 150, 510)));
            gameObjects.Add(new Worker(WorkerID.Simon, new Vector2(_graphics.PreferredBackBufferWidth / 2 + 150, 670)));
            gameObjects.Add(new Worker(WorkerID.Philip, new Vector2(_graphics.PreferredBackBufferWidth / 2 + 150, 505)));
            gameObjects.Add(new Worker(WorkerID.Rikke, new Vector2(_graphics.PreferredBackBufferWidth / 2 - 150, 670)));

            //Workstations:
            CoffeeBeanStation = new Workstation(WorkstationType.CoffeeBeanStation, new Vector2(_graphics.PreferredBackBufferWidth - stationMove / 2, 320));
            gameObjects.Add(CoffeeBeanStation);
            CoffeeBeanStation.Start();

            MilkStation = new Workstation(WorkstationType.MilkStation, new Vector2(stationMove / 2, _graphics.PreferredBackBufferHeight - stationMove / 2));
            gameObjects.Add(MilkStation);
            MilkStation.Start();

            WaterStation = new Workstation(WorkstationType.WaterStation, new Vector2(_graphics.PreferredBackBufferWidth - stationMove / 2, _graphics.PreferredBackBufferHeight - stationMove / 2));
            gameObjects.Add(WaterStation);
            WaterStation.Start();

            BrewingStation = new Workstation(WorkstationType.BrewingStation, new Vector2(stationMove / 2, 320));
            gameObjects.Add(BrewingStation);
            BrewingStation.Start();

            ComputerStation = new Workstation(WorkstationType.Computer, new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight * 3 / 5));
            gameObjects.Add(ComputerStation);
            ComputerStation.Start();

            //The soundeffect instants for the coffee brewing and pc typing
            brewingSoundEffectInstance = GameWorld.soundEffects["brewingSound"].CreateInstance();
            typpingSoundEffectInstance = GameWorld.soundEffects["typingSound"].CreateInstance();




            drawThread = new Thread(RunDraw);
            drawThread.IsBackground = true;
            drawThread.Start(); //SKAL startes som det sidste

        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadSprites(Content, sprites);
            LoadAnimations(Content, animations);
            LoadSounds(Content, soundEffects);
            LoadMusic(Content, music);

            ressourceFont = Content.Load<SpriteFont>("standardFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                gameRunning = false;
                Thread.Sleep(30);
                Exit();
            }
            else if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Enter)) //Starting the game
            {
                //Removing the start screen from gameObjects
                GameWorld.gameObjects.Remove(GameWorld.startScreen);
            }
            else if (money > winCondition) //Win conditions
            {
                //Clearing gameObjects
                GameWorld.gameObjects.Clear();

                //Stopping the soundeffect instancs
                brewingSoundEffectInstance.Stop();
                typpingSoundEffectInstance.Stop();
                
                //Adding the end screen to gameObjects
                GameWorld.gameObjects.Add(new Decoration(DecorationType.End, new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2)));
            }

            //base.Update(gameTime);

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(gameTime);
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
            sprites.Add(DecorationType.Morten, Content.Load<Texture2D>("Sprites\\underCoverMortenSlingGul3"));
            sprites.Add(DecorationType.Sign, Content.Load<Texture2D>("Sprites\\sign"));
            sprites.Add(DecorationType.Start, Content.Load<Texture2D>("Sprites\\introScreen"));
            sprites.Add(DecorationType.End, Content.Load<Texture2D>("Sprites\\loseScreen"));
            sprites.Add(DecorationType.TextBox1, Content.Load<Texture2D>("Sprites\\textbox1"));
            sprites.Add(DecorationType.TextBox2, Content.Load<Texture2D>("Sprites\\textbox2"));

            //Worker
            sprites.Add(WorkerID.Irene, Content.Load<Texture2D>("Sprites\\irene"));
            sprites.Add(WorkerID.Philip, Content.Load<Texture2D>("Sprites\\philip"));
            sprites.Add(WorkerID.Rikke, Content.Load<Texture2D>("Sprites\\rikke"));
            sprites.Add(WorkerID.Simon, Content.Load<Texture2D>("Sprites\\simon"));

            ////RessourceType
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
            soundEffects.Add("brewingSound", Content.Load<SoundEffect>("Sounds\\sound-of-coffee-maker-brewing-a-fresh-pot-hd-263171-[AudioTrimmer.com]"));
            soundEffects.Add("typingSound", Content.Load<SoundEffect>("Sounds\\typing-keyboard-sound-254462-[AudioTrimmer.com]"));

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
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.FrontToBack);

                mousePointer.Draw(_spriteBatch);
                foreach (GameObject gameObject in gameObjects)
                {
                    gameObject.Draw(_spriteBatch);
                }

                _spriteBatch.End();
                drawMutex.ReleaseMutex();
            }

        }


        #endregion
    }
}
