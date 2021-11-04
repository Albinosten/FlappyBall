using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace newTestProj
{
    public interface ISpriteBatch
    {
        SpriteBatch SpriteBatch {get;}
        void Begin(SpriteSortMode sortMode = SpriteSortMode.Deferred
            , BlendState blendState = null
            , SamplerState samplerState = null
            , DepthStencilState depthStencilState = null
            , RasterizerState rasterizerState = null
            , Effect effect = null
            , Matrix? transformMatrix = null
            );
        void Draw(Texture2D texture
            , Vector2 position
            , Color color
            );
        void Draw(Texture2D texture
            , Vector2 position
            , Rectangle? sourceRectangle
            , Color color
            , float rotation
            , Vector2 origin
            , float scale
            , SpriteEffects effects
            , float layerDepth
            );
        void End();
    }
    public class SpriteBatchNew : SpriteBatch, ISpriteBatch
    {
        public SpriteBatchNew(GraphicsDevice graphicsDevice)
            : base(graphicsDevice)
        {
            
        }
        public SpriteBatch SpriteBatch => this;
    }
}
