using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRobotCreate.Script
{
    public class DriveAngleVelocity
    {

        private int radius;
        private int velocity;

        /**
         * @return the radius
         */
        public int getRadius()
        {
            return radius;
        }

        /**
         * @param radius the radius to set
         */
        public void setRadius(int radius)
        {
            this.radius = radius;
        }

        /**
         * @return the velocity
         */
        public int getVelocity()
        {
            return velocity;
        }

        /**
         * @param velocity the velocity to set
         */
        public void setVelocity(int velocity)
        {
            this.velocity = velocity;
        }

        public byte[] getCommand()
        {
            return CommandConverter.commandWithFourBytes(137, velocity, radius);
        }
    }
}
