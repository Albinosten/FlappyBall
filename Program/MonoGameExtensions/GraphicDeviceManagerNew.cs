using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace newTestProj
{
    public interface IGraphicsDeviceManagerNew : IGraphicsDeviceService, IGraphicsDeviceManager
    {
        int PreferredBackBufferWidth {get;}
        int PreferredBackBufferHeight {get;}
        bool IsFullScreen {get;set;}

        void ApplyChanges();
    }
    public class GraphicsDeviceManagerNew : GraphicsDeviceManager , IGraphicsDeviceManagerNew
    {
        public GraphicsDeviceManagerNew(Game game)
            :base(game)
        {
            
        }
    }
}