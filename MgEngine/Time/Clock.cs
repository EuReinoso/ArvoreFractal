using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgEngine.Time
{
    public class Clock
    {
        private Game _game;

        private float _dt;

        private float FPS_UPDATE_TIME = 0.3f;
        private int _fps;
        private float _fpsUpdateTicks;
        private int _fpsLimit = 60;
            
        public Clock(Game game)
        {
            _game = game;

            IsFpsLimited = false;
        }

        #region Property
        public float Dt
        {
            get { return _dt; }
        }

        public int Fps
        {
            get { return _fps; }
        }

        public bool IsFpsLimited
        {
            get { return _game.IsFixedTimeStep; }

            set { _game.IsFixedTimeStep = value; }
        }
        

        public int FpsLimit
        {
            get { return _fpsLimit; }

            set
            {
                _fpsLimit = value;
                long ticksPerFrame = TimeSpan.TicksPerSecond / _fpsLimit;
                _game.TargetElapsedTime = TimeSpan.FromTicks(ticksPerFrame);
            }
        }
        #endregion

        public void Update(GameTime gameTime)
        {
            UpdateFps(gameTime);
            UpdateDt(gameTime);
        }

        private void UpdateFps(GameTime gameTime)
        {
            _fpsUpdateTicks += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_fpsUpdateTicks >= FPS_UPDATE_TIME)
            {
                _fps = (int)(1 / gameTime.ElapsedGameTime.TotalSeconds);
                _fpsUpdateTicks = 0;
            }
        }

        private void UpdateDt(GameTime gameTime)
        {
            _dt = (float)(gameTime.ElapsedGameTime.TotalSeconds * 60);
        }


        public int DtInt(float amount)
        {
            return (int)(amount * Dt);
        }
    }
}
