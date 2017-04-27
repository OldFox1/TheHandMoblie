using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;


namespace TheHandMobile
{
    class MotorController
    {

        private readonly RestClient client;

        public MotorController()
        {
            client = new RestClient("http://10.10.0.252/");

        }

        public void moveForward()
        {
            sendCommand("forward");
        }

        public void reverse()
        {
            sendCommand("reverse");
        }

        public void turnRight()
        {
            sendCommand("right");
        }

        public void turnLeft()
        {
            sendCommand("left");
        }

        public void stop()
        {
            sendCommand("stop");
        }

        private void sendCommand(string resourceName)
        {
            var request = new RestRequest(resourceName, Method.POST);
            client.Execute(request);

        }

    }
}
