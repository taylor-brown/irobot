using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRobotCreate.Script
{
    public class TurnAngle
    {

        private int angle;

        /**
         * @return the angle
         */
        public int getAngle()
        {
            return angle;
        }

        /**
         * @param angle the angle to set
         */
        public void setAngle(int angle)
        {
            this.angle = angle;
        }

        private byte[] getWaitAngle()
        {
            return CommandConverter.commandWithTwoBytes(157, angle);
        }

        public byte[] getTurnAngle()
        {
            DriveAngleVelocity dav = new DriveAngleVelocity();
            dav.setVelocity(80);
            if (angle < 0)
            {
                dav.setRadius(65535);
            }
            else
            {
                dav.setRadius(1);
            }
            return dav.getCommand();
        }

        public byte[] getScript()
        {
            CommandScript cs = new CommandScript();
            cs.addBytes(getTurnAngle());
            cs.addBytes(getWaitAngle());
            DriveVelocity dv = new DriveVelocity();
            dv.setVelocity(0);
            cs.addBytes(dv.getCommand());
            return cs.getScript();
        }

        public byte[] executeScript()
        {
            return new CommandScript().executeScript();
        }

    }
}