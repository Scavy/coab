namespace Classes
{
    /// <summary>
    /// Summary description for Action.
    /// </summary>
    public class Action
    {
        public int spell_id; // 0x00
        public bool can_cast; // 0x01
        public bool can_use;  // 0x02
        public int delay;   // 0x03
        public int attackIdx;  // 0x04
        public byte maxSweapTargets;  // 0x05
        public int move;     // 0x06
        public bool guarding; // 0x07
        public bool field_8;  // 0x08
        public int direction;  // 0x09 field_9
        public Player target; // 0x0A 4 bytes
        public int bleeding; // 0x0E
        public byte AttacksReceived;  // 0x0F
        public bool fleeing; // 0x10
        public bool hasTurnedUndead; // 0x11
        public int directionChanges; // 0x12
        public bool nonTeamMember; // 0x13 field_13
        public bool moral_failure; // 0x14
        public int field_15; // 0x15

        public Action()
        {
            target = null;
        }

        public void Clear()
        {
            delay = 0;
            spell_id = 0;
            guarding = false;
            move = 0;
        }
    }
}
