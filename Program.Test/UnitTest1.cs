using Microsoft.VisualStudio.TestTools.UnitTesting;
using newTestProj;
using System; 

namespace Program.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var a = new RectangleRow(null);

            Assert.IsNotNull(a);
        }


        [TestMethod]
        public void ModulusTest()
        {
            var width = 10;
            var height = 30;
            var data = width*height;

            for(int i = 0; i < data; i++)
            {
                var mod = i%width;
                Console.WriteLine(i + " : " + mod);
            }
        }
        
        [TestMethod]
        public void calculate()
        {
            var width = 10;

            Assert.AreEqual(15, width/2*3);

            width = 32;
            Assert.AreEqual(4, width/(2*4));
        }
        
    }
}
