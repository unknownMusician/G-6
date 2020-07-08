using UnityEngine;

public class PlaceForRoom {

    private short topDoor;
    private short rightDoor;
    private short bottomDoor;
    private short leftDoor;

    public PlaceForRoom(short topDoor, short rightDoor, short bottomDoor, short leftDoor) {
        this.topDoor = topDoor;
        this.rightDoor = rightDoor;
        this.bottomDoor = bottomDoor;
        this.leftDoor = leftDoor;
    }

    public PlaceForRoom() {
    }

    public void setTop(short door) {
        this.topDoor = door;
    }

    public void setRight(short door) {
        this.rightDoor = door;
    }

    public void setBottom(short door) {
        this.bottomDoor = door;
    }

    public void setLeft(short door) {
        this.leftDoor = door;
    }

    public short[] getDoorParams() {
        short[] doors = new short[4];
        doors[0] = topDoor;
        doors[1] = rightDoor;
        doors[2] = bottomDoor;
        doors[3] = leftDoor;

        return doors;
    }

    public bool anyEqualToOne () {
        short[] doors = new short[4];
        doors[0] = topDoor;
        doors[1] = rightDoor;
        doors[2] = bottomDoor;
        doors[3] = leftDoor;

        for (int i = 0; i < 4; i++) {
            if (doors[i] == 1) {
                return true;
            }
        }
        return false;
    }
}
