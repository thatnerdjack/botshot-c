using System;
using Microsoft.SPOT.Hardware;

namespace BotShotCode{

    class Helpers{

        public static void Deadband(ref double value) {
            if (System.Math.Abs(value) < 0.10) { value = 0.0; }
        }

        static double uartRead(System.IO.Ports.SerialPort _uart) {
            //Connects to UART port 1 on the HERO, and reads 
            byte[] buffer = new byte[100];
            _uart.Read(buffer, 0, 100);
            return (double)buffer.GetValue(0);
        }

        static void uartWrite(System.IO.Ports.SerialPort _uart, byte[] data) {
            _uart.Write(data, 0, data.Length);
        }
    }
}
