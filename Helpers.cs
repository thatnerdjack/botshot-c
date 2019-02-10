using System;
using Microsoft.SPOT;

namespace BotShotCode{

    class Helpers{

        public static void Deadband(ref double value) {
            if (System.Math.Abs(value) < 0.10) { value = 0.0; }
        }
    }
}
