using System.Text;
using Microsoft.SPOT;

using BotShotCode;

using CTRE.Phoenix;
using CTRE.Phoenix.Controller;
using CTRE.Phoenix.MotorControl;
using CTRE.Phoenix.MotorControl.CAN;
namespace BotShotCode {
    class ShootSub {
        static TalonSRX shooterBM = new TalonSRX(3);
        static TalonSRX shooterBS = new TalonSRX(4);
        static TalonSRX shooterTM = new TalonSRX(7);
        static TalonSRX shooterTS = new TalonSRX(8);

		static TalonSRX tiltMoter = new TalonSRX(9);

        public static void Shoot(GameController GAMEPAD, StringBuilder stringBuilder) {

            double power = GAMEPAD.GetAxis(5);

            Helpers.Deadband(ref power);

            double shooterSpeed = System.Math.Pow(power, 2);

            shooterBM.Set(ControlMode.PercentOutput, shooterSpeed);
            shooterBS.Set(ControlMode.PercentOutput, shooterSpeed);
            shooterTM.Set(ControlMode.PercentOutput, -shooterSpeed);
            shooterTS.Set(ControlMode.PercentOutput, -shooterSpeed);

            stringBuilder.Append("\t");
            stringBuilder.Append(shooterSpeed);
        }

		public static void Tilt(GameController GAMEPAD, StringBuilder stringBuilder) {

			if (GAMEPAD.GetButton(12)) {
				tiltMoter.Set(ControlMode.PercentOutput, 1);
				stringBuilder.Append("\t");
				stringBuilder.Append("Tilt Up");
			}


			if (GAMEPAD.GetButton(13)){
				tiltMoter.Set(ControlMode.PercentOutput, -1);
				stringBuilder.Append("\t");
				stringBuilder.Append("Tilt Down");
			}
		}
    }
}
