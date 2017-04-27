using MyoNet.Myo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHandMobile
{
    class Program
    {
        private static int roll_w = 0;
        private static int pitch_w = 0;
        private static int yaw_w = 0;
        private static bool onArm = false;
        private static Pose currentPose;
        private static Arm whichArm;


        private static MotorController motorController;
        


        static void Main(string[] args)
        {
            using (var hub = new Hub())
            {
                Console.WriteLine("Attempting to find a Myo...");
                IMyo myo = hub.WaitForMyo(TimeSpan.FromSeconds(10));
                if (myo == null)
                    throw new TimeoutException("Unable to find a Myo!");

                Console.WriteLine("Connected to a Myo armband!\n");

                hub.MyoUnpaired += OnUnpair;
                hub.RecognizedArm += OnRecognizedArm;
                hub.LostArm += OnLostArm;

                myo.PoseChanged += OnPoseChanged;
                myo.OrientationDataAcquired += OnOrientationData;
            }
        }
        static void OnPoseChanged(object sender, PoseChangedEventArgs e)
        {
            Pose oldPose = currentPose;
            currentPose = e.Pose;

            // Vibrate the Myo whenever we've detected that the user has made a fist.
            switch (e.Pose)
            {
                case Pose.Fist:
                    motorController.moveForward();
                    break;
                case Pose.WaveIn:
                    motorController.turnLeft();
                    break;
                case Pose.WaveOut:
                    motorController.turnRight();
                    break;
                case Pose.ThumbToPinky:
                    motorController.reverse();
                    break;
                case Pose.FingersSpread:
                    motorController.stop();
                    break;
                default:
                    motorController.stop();
                    break;
            }
            if (currentPose != oldPose)
                e.Myo.Vibrate(VibrationType.Short);
        }

        static void OnUnpair(object sender, MyoEventArgs e)
        {
            // We've lost a Myo.
            // Let's clean up some leftover state.
            roll_w = 0;
            pitch_w = 0;
            yaw_w = 0;
            onArm = false;
        }
        static void OnRecognizedArm(object sender, RecognizedArmEventArgs e)
        {
            onArm = true;
            whichArm = e.Arm;
        }

        static void OnLostArm(object sender, MyoEventArgs e)
        {
            onArm = false;
        }
        static void OnOrientationData(object sender, OrientationDataEventArgs e)
        {
            // Calculate Euler angles (roll, pitch, and yaw) from the unit quaternion.        
            double roll = Quaternion.Roll(e.Orientation);
            double pitch = Quaternion.Pitch(e.Orientation);
            double yaw = Quaternion.Yaw(e.Orientation);

            // Convert the floating point angles in radians to a scale from 0 to 18.
            roll_w = (int)((roll + (double)Math.PI) / (Math.PI * 2.0) * 18);
            pitch_w = (int)((pitch + (float)Math.PI / 2.0) / Math.PI * 18);
            yaw_w = (int)((yaw + (float)Math.PI) / (Math.PI * 2.0) * 18);
        }
    }
}
