using System.Threading;
using Microsoft.SPOT;
using System.Text;

using CTRE.Phoenix;
using static BotShotCode.DriveSub;
using static BotShotCode.ShootSub;

namespace BotShotCode {

    public class Program {

        //Communication constants
        static System.IO.Ports.SerialPort _uart = new System.IO.Ports.SerialPort(CTRE.HERO.IO.Port1.UART, 115200);
        static StringBuilder stringBuilder = new StringBuilder();

		//Controller constants
        static CTRE.Phoenix.Controller.GameController _gamepad = null;

        public static void Main() {
            /* loop forever */
            while (true) {
				/* drive robot using gamepad */
				Operate();
                /* print whatever is in our string builder */
                Debug.Print(stringBuilder.ToString());
                stringBuilder.Clear();
                /* feed watchdog to keep Talon's enabled */
                CTRE.Phoenix.Watchdog.Feed();
                /* run this task every 20ms */
                Thread.Sleep(20);
            }
        }

		

		static void Operate() {
            if (_gamepad == null) {
                _gamepad = new CTRE.Phoenix.Controller.GameController(UsbHostDevice.GetInstance());
            }
			//Drive Function
			stringBuilder.Append("--DRIVEBASE CONTROLS--");
			Drive(_gamepad,stringBuilder);

            //Shooting Function
            stringBuilder.Append("--SHOOTER CONTROLS--");
			Shoot(_gamepad,stringBuilder);

			//Tilt Function
			stringBuilder.Append("--TILT CONTROLS--");
			Tilt(_gamepad, stringBuilder);

		}

	}
}
