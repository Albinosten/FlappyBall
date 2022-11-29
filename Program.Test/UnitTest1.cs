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

        [TestMethod]
        public void Yolo()
        {
            var width = 5;

            this.AAA(1,width,0);
            this.AAA(2,width,0);
            this.AAA(3,width,0);
            this.AAA(4,width,0);
            this.AAA(5,width,1);
            this.AAA(6,width,1);
            this.AAA(7,width,1);
            this.AAA(8,width,1);
            this.AAA(9,width,1);
            this.AAA(10,width,2);
        }

        private void AAA(int a, int b, int expected)
        {
            var c = a/b;

            Assert.AreEqual(expected, c);
        }
        
    }
}
