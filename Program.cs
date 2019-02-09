﻿using System;
using System.Threading;
using Microsoft.SPOT;
using System.Text;


using CTRE.Phoenix;
using CTRE.Phoenix.Controller;
using CTRE.Phoenix.MotorControl;
using CTRE.Phoenix.MotorControl.CAN;

namespace Hero_Arcade_Drive_Example1
{
    public class Program
    {
        //TODO
        //Need to uncomment to run on a robot
        /* talon constants*/
        //static TalonSRX rightSlave = new TalonSRX(2);
        //static TalonSRX right = new TalonSRX(3);
        //static TalonSRX leftSlave = new TalonSRX(6);
        //static TalonSRX left = new TalonSRX(5);
        //static TalonSRX shooterBM = new TalonSRX(5);
        //static TalonSRX shooterBS = new TalonSRX(5);
        //static TalonSRX shooterTM = new TalonSRX(5);
        //static TalonSRX shooterTS = new TalonSRX(5);

        static StringBuilder stringBuilder = new StringBuilder();

        static CTRE.Phoenix.Controller.GameController _gamepad = null;

        public static void Main()
        {
            /* loop forever */
            while (true)
            {
                /* drive robot using gamepad */
                Drive();
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
        static void Deadband(ref float value)
        {
            if (value < -0.10)
            {
                /* outside of deadband */
            }
            else if (value > +0.10)
            {
                /* outside of deadband */
            }
            else
            {
                /* within 10% so zero it */
                value = 0;
            }
        }
        static void Drive()
        {
            if (null == _gamepad)
                _gamepad = new GameController(UsbHostDevice.GetInstance());

            float x = _gamepad.GetAxis(1);
            float y = _gamepad.GetAxis(3);
            //float twist = _gamepad.GetAxis(2);

            Deadband(ref x);
            Deadband(ref y);
            //Deadband(ref twist);

            float leftThrot = x;
            float rightThrot = y;

            //TODO 
            //Uncomment when ready to test on a robot
            //left.Set(ControlMode.PercentOutput, leftThrot);
            //leftSlave.Set(ControlMode.PercentOutput, leftThrot);
            //right.Set(ControlMode.PercentOutput, -rightThrot);
            //rightSlave.Set(ControlMode.PercentOutput, -rightThrot);

            stringBuilder.Append("\t");
            stringBuilder.Append(x);
            stringBuilder.Append("\t");
            stringBuilder.Append(y);
            //stringBuilder.Append("\t");
            //stringBuilder.Append(twist);

        }
    }
}