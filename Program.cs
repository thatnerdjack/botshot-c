using System.Threading;
using Microsoft.SPOT;
using System.Text;
using CTRE.Phoenix;

//using static BotSho  tCode.DriveSub;

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
                //Debug.Print(stringBuilder.ToString());
                //stringBuilder.Clear();
                /* feed watchdog to keep Talon's enabled */
                CTRE.Phoenix.Watchdog.Feed();
                Debug.Print("yoooo");
                /* run this task every 20ms */
                Thread.Sleep(20);
            }
        }

		

		static void Operate() {
            if (_gamepad == null) {
                _gamepad = new CTRE.Phoenix.Controller.GameController(UsbHostDevice.GetInstance(0));
            }
			//Drive Function
			stringBuilder.Append("--DRIVEBASE CONTROLS--");
			DriveSub.Drive(_gamepad,stringBuilder);

            //Shooting Function
            stringBuilder.Append("\n--SHOOTER CONTROLS--");
			ShootSub.Shoot(_gamepad,stringBuilder);

			//Tilt Function
			stringBuilder.Append("\n--TILT CONTROLS--");
			ShootSub.Tilt(_gamepad, stringBuilder);

		}

	}
}
