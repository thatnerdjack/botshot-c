using System.Text;

using CTRE.Phoenix;
using CTRE.Phoenix.Controller;
using CTRE.Phoenix.MotorControl;
using CTRE.Phoenix.MotorControl.CAN;
using CTRE.Phoenix.Sensors;

using static BotShotCode.Helpers;

namespace BotShotCode{
    
    class DriveSub{
        /* talon constants*/
        static TalonSRX rightSlave = new TalonSRX(2);
        static TalonSRX right = new TalonSRX(1);
        static TalonSRX leftSlave = new TalonSRX(6);
        static TalonSRX left = new TalonSRX(5);

        public static void Drive(GameController GAMEPAD, StringBuilder stringBuilder) {
			/*Talon and Encoder Constants*/
			right.SetNeutralMode(NeutralMode.Brake);
			rightSlave.SetNeutralMode(NeutralMode.Brake);
			left.SetNeutralMode(NeutralMode.Brake);
			leftSlave.SetNeutralMode(NeutralMode.Brake);

			/*Right side of drivetrain needs to be inverted*/
			//right.SetInverted(true);
			//rightSlave.SetInverted(true);

			

			left.ConfigSelectedFeedbackSensor(FeedbackDevice.QuadEncoder,Helpers.PID,Helpers.timeoutMs);
			leftSlave.ConfigRemoteFeedbackFilter(left.GetDeviceID(), RemoteSensorSource.RemoteSensorSource_TalonSRX_SelectedSensor, Helpers.remotePID, Helpers.timeoutMs);
			right.ConfigSelectedFeedbackSensor(FeedbackDevice.QuadEncoder, Helpers.PID, Helpers.timeoutMs);
			rightSlave.ConfigRemoteFeedbackFilter(left.GetDeviceID(), RemoteSensorSource.RemoteSensorSource_TalonSRX_SelectedSensor, Helpers.remotePID, Helpers.timeoutMs);

			/*End Constants*/

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
