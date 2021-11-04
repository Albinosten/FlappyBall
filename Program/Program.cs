using System;

namespace newTestProj
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            // Function();

            using (var game = new Game1())
            {
                game.DebugOutput = false;
                game.StandStill = false;
                game.Run();
            }
        }
        public static void Function()
        {
            for(float i = 0; i<2; i+= (float)0.02)
            {
                //ax^2 + bx + c = y;

                var a =  2*i - 1*i*i ;   

                Console.WriteLine(a) ;
            }
        }
    }
   
}
