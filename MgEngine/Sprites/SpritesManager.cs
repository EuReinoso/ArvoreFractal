using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MgEngine.Shape;
using MgEngine.Window;

namespace MgEngine.Sprites
{
    public class SpritesManager
    {
        private GraphicsDevice _graphicsDevice;
        private int[] rectIndexes;
        private BasicEffect _effects;
        private RenderTarget2D _mainRenderTarget;

        public SpritesManager(GraphicsDevice graphicsDevice, WindowManager window) 
        {
            _graphicsDevice = graphicsDevice;
            _mainRenderTarget = window.Canvas.RenderTarget;
            LoadEffects();
            LoadRectIndexes();
        }

        #region Properties
        public GraphicsDevice GraphicsDevice { get { return _graphicsDevice; } }
        #endregion

        #region Loading
        private void LoadEffects()
        {
            _effects = new(_graphicsDevice);
            _effects.Alpha = 1.0f;
            _effects.VertexColorEnabled = true;
            _effects.TextureEnabled = false;
            _effects.LightingEnabled = false;
            _effects.World = Matrix.Identity;
            _effects.View = Matrix.Identity;
            _effects.Projection = Matrix.CreateOrthographicOffCenter(0, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height, 0, 0f, 1f);

        }

        private void LoadRectIndexes()
        {
            rectIndexes = new int[6];

            rectIndexes[0] = 0;
            rectIndexes[1] = 1;
            rectIndexes[2] = 2;
            rectIndexes[3] = 0;
            rectIndexes[4] = 2;
            rectIndexes[5] = 3;
        }
        #endregion

        #region Draw
        public void DrawRect(Rect rect, Color color)
        {
            VertexPositionColor[] vertices = new VertexPositionColor[4];

            vertices[0] = new VertexPositionColor(new Vector3(rect.Vertices[0], 0f), color);
            vertices[1] = new VertexPositionColor(new Vector3(rect.Vertices[1], 0f), color);
            vertices[2] = new VertexPositionColor(new Vector3(rect.Vertices[2], 0f), color);
            vertices[3] = new VertexPositionColor(new Vector3(rect.Vertices[3], 0f), color);

            foreach (EffectPass pass in _effects.CurrentTechnique.Passes)
            {
                pass.Apply();

                _graphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(
                    PrimitiveType.TriangleList, 
                    vertices, 
                    0,
                    4,
                    rectIndexes,
                    0,
                    2);
            }
        }

        public void DrawLine(Line line, Color color)
        {
            VertexPositionColor[] vertices = new VertexPositionColor[4];

            vertices[0] = new VertexPositionColor(new Vector3(line.Vertices[0], 0f), color);
            vertices[1] = new VertexPositionColor(new Vector3(line.Vertices[1], 0f), color);
            vertices[2] = new VertexPositionColor(new Vector3(line.Vertices[2], 0f), color);
            vertices[3] = new VertexPositionColor(new Vector3(line.Vertices[3], 0f), color);

            foreach (EffectPass pass in _effects.CurrentTechnique.Passes)
            {
                pass.Apply();

                _graphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(
                    PrimitiveType.TriangleList,
                    vertices,
                    0,
                    4,
                    rectIndexes,
                    0,
                    2);
            }
        }
        #endregion

        public void SetMainRenderTarget()
        {
            _graphicsDevice.SetRenderTarget(_mainRenderTarget);
        }
    }
}
