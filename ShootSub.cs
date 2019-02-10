using System;
using Microsoft.SPOT;

using BotShotCode;

using CTRE.Phoenix;
using CTRE.Phoenix.Controller;
using CTRE.Phoenix.MotorControl.CAN;
namespace BotShotCode {
    class ShootSub {
        static TalonSRX shooterBM = new TalonSRX(5);
        static TalonSRX shooterBS = new TalonSRX(5);
        static TalonSRX shooterTM = new TalonSRX(5);
        static TalonSRX shooterTS = new TalonSRX(5);

        static void Shoot(GameController GAMEPAD) {

            double power = GAMEPAD.GetAxis(5);

            Helpers.Deadband(ref power);

            double shooterSpeed = System.Math.Pow(power, 2);

            //shooterBM.Set(ControlMode.PercentOutput, shooterSpeed);
            //shooterBS.Set(ControlMode.PercentOutput, shooterSpeed);
            //shooterTM.Set(ControlMode.PercentOutput, -shooterSpeed);
            //shooterTS.Set(ControlMode.PercentOutput, -shooterSpeed);

            //stringBuilder.Append("\t");
            //stringBuilder.Append(power);
        }
    }
}
