using UnityEngine;

namespace G6.RoomSpawning {
    public class PlaceForRoom {
        public bool? topDoor = null;
        public bool? rightDoor = null;
        public bool? bottomDoor = null;
        public bool? leftDoor = null;

        public byte DoorsAmount => (byte)(0 + (topDoor == true ? 1 : 0) + (rightDoor == true ? 1 : 0) + (bottomDoor == true ? 1 : 0) + (leftDoor == true ? 1 : 0));

        public (bool? topDoor, bool? rightDoor, bool? bottomDoor, bool? leftDoor) DoorParams {
            get => (topDoor, rightDoor, bottomDoor, leftDoor);
            set {
                topDoor = value.topDoor;
                rightDoor = value.rightDoor;
                bottomDoor = value.bottomDoor;
                leftDoor = value.leftDoor;
            }
        }

        public bool AtLeastOneDoor => topDoor == true || rightDoor == true || bottomDoor == true || leftDoor == true;

        public PlaceForRoom(bool topDoor, bool rightDoor, bool bottomDoor, bool leftDoor) {
            this.topDoor = topDoor;
            this.rightDoor = rightDoor;
            this.bottomDoor = bottomDoor;
            this.leftDoor = leftDoor;
        }

        public PlaceForRoom(bool? all) => topDoor = rightDoor = bottomDoor = leftDoor = all;

        public PlaceForRoom() => new PlaceForRoom(null);

        public override string ToString() {
            return (topDoor == true ? "2" : "0") + (rightDoor == true ? "2" : "0") + (bottomDoor == true ? "2" : "0") + (leftDoor == true ? "2" : "0");
        }

        public static class PlaceForDoor {
            public static bool? True => true;
            public static bool? False => false;
            public static bool? Unknown => null;
        } // todo: remove
    }
}