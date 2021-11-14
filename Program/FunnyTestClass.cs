using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace newTestProj
{
    public  class FunnyTestClass2
    {
        public string GetMySecret()
        {
            return "Funny test class 2";
        }
    }
    public class FunnyTestClass
    {
        private FunnyTestClass2 internalClass;
        public FunnyTestClass(FunnyTestClass2 sdf)
        {
            this.internalClass = sdf;
        }

        public string GetMySecret()
        {
            return "Funny Test class.           " + this.internalClass.GetMySecret();;
        }
    }
}
