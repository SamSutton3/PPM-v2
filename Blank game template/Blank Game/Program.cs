﻿using System;

namespace Blank_Game
{
#if WINDOWS || LINUX
    public static class Program
    {

        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }
    }
#endif
}
