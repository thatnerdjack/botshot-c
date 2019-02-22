using System.Text;

using CTRE.Phoenix;
using CTRE.Phoenix.Controller;
using CTRE.Phoenix.MotorControl;
using CTRE.Phoenix.MotorControl.CAN;

using static BotShotCode.Helpers;

namespace BotShotCode{
    
    class DriveSub{
        /* talon constants*/
        static TalonSRX rightSlave = new TalonSRX(2);
        static TalonSRX right = new TalonSRX(1);
        static TalonSRX leftSlave = new TalonSRX(6);
        static TalonSRX left = new TalonSRX(5);


        public static void Drive(GameController GAMEPAD, StringBuilder stringBuilder) {
            if (null == GAMEPAD)
               GAMEPAD = new GameController(UsbHostDevice.GetInstance());

            double x = GAMEPAD.GetAxis(1);
            double y = GAMEPAD.GetAxis(3);

            Deadband(ref x);
            Deadband(ref y);

            //Pow(x,2) gives finer controls over the drivebase
            //.5 for total half-speed reduction
            //sign(x) returns the sign, which is useful since the pow removes the negative sign.
            double leftThrot = (System.Math.Pow(x, 2)) * .5 * System.Math.Sign(x);
            double rightThrot = (System.Math.Pow(y, 2)) * .5 * System.Math.Sign(y);

            //TODO 
            //Uncomment when ready to test on a robot
            left.Set(ControlMode.PercentOutput, leftThrot);
            leftSlave.Set(ControlMode.PercentOutput, leftThrot);
            right.Set(ControlMode.PercentOutput, -rightThrot);
            rightSlave.Set(ControlMode.PercentOutput, -rightThrot);

            stringBuilder.Append("\t");
            stringBuilder.Append(leftThrot);
            stringBuilder.Append("\t");
            stringBuilder.Append(rightThrot);

        }
    }
    
}
