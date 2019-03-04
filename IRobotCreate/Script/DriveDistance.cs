using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRobotCreate.Script
{
public class DriveDistance{

    private int velocity = 100;
    private int distance;

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

    /**
     * @return the distance
     */
    public int getDistance() {
        return distance;
    }

    /**
     * @param distance the distance to set
     */
    public void setDistance(int distance) {
        this.distance = distance;
    }

    public byte[] getCommand() {
        return CommandConverter.commandWithTwoBytes(156, distance);
    }
    
    public byte[] getScript(){
        CommandScript cs = new CommandScript();
        DriveVelocity dv = new DriveVelocity();
        dv.setVelocity(velocity);
        cs.addBytes(dv.getCommand());
        cs.addBytes(this.getCommand());
        dv.setVelocity(0);
        cs.addBytes(dv.getCommand());
        return cs.getScript();
    }

    public byte[] executeScript() {
        return new CommandScript().executeScript();
    }

}
}
