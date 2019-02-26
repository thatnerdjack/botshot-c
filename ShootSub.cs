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
        static VictorSPX shooterTM = new VictorSPX(7);
        static VictorSPX shooterTS = new VictorSPX(8);

		static VictorSPX tiltMoter = new VictorSPX(9);
		static VictorSPX intake = new VictorSPX(10);
		static VictorSPX loader = new VictorSPX(11);

        public static void Shoot(GameController GAMEPAD, StringBuilder stringBuilder) {

            double power = GAMEPAD.GetAxis(4);

            Helpers.Deadband(ref power);

            double shooterSpeed = System.Math.Pow(power, 2)*System.Math.Sign(power)*.5;

            shooterBM.Set(ControlMode.PercentOutput, shooterSpeed);
            shooterBS.Set(ControlMode.PercentOutput, -shooterSpeed);
            shooterTM.Set(ControlMode.PercentOutput, -shooterSpeed);
            shooterTS.Set(ControlMode.PercentOutput, -shooterSpeed);

            stringBuilder.Append("\t");
            stringBuilder.Append(shooterSpeed);
        }

		public static void Tilt(GameController GAMEPAD, StringBuilder stringBuilder) {

			if (GAMEPAD.GetButton(1)) {
				tiltMoter.Set(ControlMode.PercentOutput, 1);
				stringBuilder.Append("\t");
				stringBuilder.Append("Tilt Up");
			}


			if (GAMEPAD.GetButton(2)){
				tiltMoter.Set(ControlMode.PercentOutput, -1);
				stringBuilder.Append("\t");
				stringBuilder.Append("Tilt Down");
			}

			if (!GAMEPAD.GetButton(1) && !GAMEPAD.GetButton(2)) {
				tiltMoter.Set(ControlMode.PercentOutput, 0);
			}
		}

		public static void Intake(GameController GAMEPAD, StringBuilder stringBuilder) {
			if (GAMEPAD.GetButton(3))
			{
				//intake.Set(ControlMode.PercentOutput, 1);
				//stringBuilder.Append("\t");
				//stringBuilder.Append("Tilt Up");
			}


			if (GAMEPAD.GetButton(4))
			{
				intake.Set(ControlMode.PercentOutput, -1);
				stringBuilder.Append("\t");
				stringBuilder.Append("Intake");
			}

			if (!GAMEPAD.GetButton(3) && !GAMEPAD.GetButton(4))
			{
				intake.Set(ControlMode.PercentOutput, 0);
			}
		}

		public static void Load(GameController GAMEPAD, StringBuilder stringBuilder) {
			if (GAMEPAD.GetButton(5))
			{
				loader.Set(ControlMode.PercentOutput, 1);
				stringBuilder.Append("\t");
				stringBuilder.Append("Load Up");
			}


			if (GAMEPAD.GetButton(6))
			{
				loader.Set(ControlMode.PercentOutput, -1);
				stringBuilder.Append("\t");
				stringBuilder.Append("Load Down");
			}

			if (!GAMEPAD.GetButton(5) && !GAMEPAD.GetButton(6))
			{
				loader.Set(ControlMode.PercentOutput, 0);
			}
		}
    }
}
