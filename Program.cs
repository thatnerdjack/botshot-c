using System.Threading;
using Microsoft.SPOT;
using System.Text;
using System;

using CTRE.Phoenix;
using CTRE.Phoenix.Controller;
using CTRE.Phoenix.MotorControl;
using CTRE.Phoenix.MotorControl.CAN;

namespace Drive {

    public class Program {
        //TODO
        //Need to uncomment to run on a robot
        /* talon constants*/
        static TalonSRX rightSlave = new TalonSRX(2);
        static TalonSRX right = new TalonSRX(1);
        static TalonSRX leftSlave = new TalonSRX(6);
        static TalonSRX left = new TalonSRX(5);

		//Communication constants
		static System.IO.Ports.SerialPort _uart = new System.IO.Ports.SerialPort(CTRE.HERO.IO.Port1.UART, 115200);

		//static TalonSRX shooterBM = new TalonSRX(5);
		//static TalonSRX shooterBS = new TalonSRX(5);
		//static TalonSRX shooterTM = new TalonSRX(5);
		//static TalonSRX shooterTS = new TalonSRX(5);

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
        /**
         * If value is within 10% of center, clear it.
         * @param value [out] floating point value to deadband.
         */
        static void Deadband(ref float value) {
            if (value < -0.10) {
                /* outside of deadband */
            }
            else if (value > +0.10) {
                /* outside of deadband */
            }
            else {
                /* within 10% so zero it */
                value = 0;
            }
        }
        static void Drive() {
            if (null == _gamepad)
                _gamepad = new GameController(UsbHostDevice.GetInstance());

            float x = _gamepad.GetAxis(1);
            float y = _gamepad.GetAxis(3);
            //float twist = _gamepad.GetAxis(2);

            Deadband(ref x);
            Deadband(ref y);
			//Deadband(ref twist);

			//Pow(x,2) gives finer controls over the drivebase
			//.5 for total half-speed reduction
			//sign(x) returns the sign, which is useful since the pow removes the negative sign.
			double leftThrot = (System.Math.Pow(x,2))*.5*System.Math.Sign(x);
			double rightThrot = (System.Math.Pow(y,2))*.5*System.Math.Sign(y);

            //TODO 
            //Uncomment when ready to test on a robot
            left.Set(ControlMode.PercentOutput, leftThrot);
            leftSlave.Set(ControlMode.PercentOutput, leftThrot);
            right.Set(ControlMode.PercentOutput, -rightThrot);
            rightSlave.Set(ControlMode.PercentOutput, -rightThrot);

            stringBuilder.Append("\t");
            stringBuilder.Append(x);
            stringBuilder.Append("\t");
            stringBuilder.Append(y);
            //stringBuilder.Append("\t");
            //stringBuilder.Append(twist);

        }

		static void Shoot() {
			if (null == _gamepad)
				_gamepad = new GameController(UsbHostDevice.GetInstance());

			float x = _gamepad.GetAxis(5);
//			float y = _gamepad.GetAxis(3);

			Deadband(ref x);
			//Deadband(ref y);

			double shooterSpeed = System.Math.Pow(x, 2);

			//shooterBM.Set(ControlMode.PercentOutput, shooterSpeed);
			//shooterBS.Set(ControlMode.PercentOutput, shooterSpeed);
			//shooterTM.Set(ControlMode.PercentOutput, -shooterSpeed);
			//shooterTS.Set(ControlMode.PercentOutput, -shooterSpeed);

			stringBuilder.Append("\t");
			stringBuilder.Append(x);
		}

		static void Operate() {
			//Drive Function
			stringBuilder.Append("--DRIVEBASE CONTROLS--");
			Drive();

			stringBuilder.Append("--SHOOTER CONTROLS--");
			//Shooting Function
			//Shoot();
		}

		static double uartRead() {
			//Connects to UART port 1 on the HERO, and reads 
			byte[] buffer = new byte[100];
			_uart.Read(buffer, 0, 100);
			return (double)buffer.GetValue(0);
		}

		static void uartWrite(byte[] data) {
			_uart.Write(data, 0, data.Length);
		}
    }
}
