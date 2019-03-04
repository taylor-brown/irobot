using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRobotCreate.Script
{
    public class DriveVelocity{
    private int velocity = 100;

    /**
     * @return the velocity
     */
    public int getVelocity() {
        return velocity;
    }

    /**
     * @param velocity the velocity to set
     */
    public void setVelocity(int velocity) {
        this.velocity = velocity;
    }

    public byte[] getCommand() {
        return CommandConverter.commandWithFourBytes(145, velocity, velocity);
    }


}

}
