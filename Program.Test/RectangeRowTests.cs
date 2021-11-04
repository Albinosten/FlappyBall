using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.Xna.Framework;

namespace newTestProj.Test
{
    [TestClass]
    public class RectangleRowTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var graphicsDeviceManager = new Mock<IGraphicsDeviceManagerNew>();
           
            var sut = new RectangleRow(graphicsDeviceManager.Object);
            sut.TopRectangle = Mock.Of<IPositionalTexture2D>();
            sut.BottomRectangle = Mock.Of<IPositionalTexture2D>();

            var spriteBatch = new Mock<ISpriteBatch>();
            sut.Draw(new GameTime(), spriteBatch.Object);
            spriteBatch.Verify(v => v.Draw(null, It.IsAny<Vector2>(), Color.White), Times.Exactly(2));
        }
    }
}
