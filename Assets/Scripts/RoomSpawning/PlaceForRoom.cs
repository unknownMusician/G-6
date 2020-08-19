public class PlaceForRoom {

    #region Amount of doors parameter
    public byte AmountOfDoors { get; set; }
    #endregion

    #region Doors parameters
    private short topDoor;
    private short rightDoor;
    private short bottomDoor;
    private short leftDoor;
    #endregion

    public PlaceForRoom(short topDoor, short rightDoor, short bottomDoor, short leftDoor) {

        AmountOfDoors = 0;

        if (topDoor == 1) {
            AmountOfDoors += 1;
        } else if (rightDoor == 1) {
            AmountOfDoors += 1;
        } else if (bottomDoor == 1) {
            AmountOfDoors += 1;
        } else if (leftDoor == 1) {
            AmountOfDoors += 1;
        }
        
        this.topDoor = topDoor;
        this.rightDoor = rightDoor;
        this.bottomDoor = bottomDoor;
        this.leftDoor = leftDoor;
    }

    public PlaceForRoom() {
        AmountOfDoors = 0;
    }

    public void setTop(short door) {
        if (topDoor == 1) {
            AmountOfDoors += 1;
        }
        this.topDoor = door;
    }

    public void setRight(short door) {
        if (rightDoor == 1) {
            AmountOfDoors += 1;
        }
        this.rightDoor = door;
    }

    public void setBottom(short door) {
        if (bottomDoor == 1) {
            AmountOfDoors += 1;
        }
        this.bottomDoor = door;
    }

    public void setLeft(short door) {
        if (leftDoor == 1) {
            AmountOfDoors += 1;
        }
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
