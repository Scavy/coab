namespace Classes
{
    // 0x400
    public class Struct_1B2CA
    {
        byte[] m_data = new byte[0x400];

        public Struct_1B2CA()
        {
            Clear();
        }

        public Struct_1B2CA(byte[] data, int offset)
        {
            System.Array.Copy(data, offset, m_data, 0, 0x400);
        }

        public void Clear()
        {
            System.Array.Clear(m_data, 0, 0x400);
        }

        public byte[] ToByteArray()
        {
            return (byte[])m_data.Clone();
        }

        public ushort this[int index]
        {
            get
            {
                // simulate the 16 bit memory space.
                index &= 0xFFFF;

                return Sys.ArrayToUshort(m_data, index);
            }
            set
            {
                index &= 0xFFFF;
                Sys.ShortToArray((short)value, m_data, index);
            }
        }
    }
}
