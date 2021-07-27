using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _06.ReadingAndWritingClass
{

    public class  Reader : IO
    {
        /// <summary>
        /// Create a reader to read a path
        /// </summary>
        /// <param name="path">The path to the fail to read</param>
        public Reader(string path)
        {
            br = new BinaryReader(File.OpenRead(path));
            this.byteOrder = ByteOrder.BigEndian;
        }

        /// <summary>
        /// Create a reader to read a path
        /// </summary>
        /// <param name="path">The path to the fail to read</param>
        /// <param name="bo">The order of the bytes to read</param>
        public Reader(string path,ByteOrder  bo)
        {
            br = new BinaryReader(File.OpenRead(path));
            this.byteOrder = bo;
        }

        BinaryReader br;

        /// <summary>
        /// The position to read at
        /// </summary>
        public long Position
        {
            get { return br.BaseStream.Position; }
            set { br.BaseStream.Position = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte ReadByte()
        {
            return br.ReadByte();
        }

        public sbyte ReadSByte()
        {
            return (sbyte)br.ReadByte();
        }

        public short ReadInt16()
        {
            short myShort = br.ReadInt16();

            if (byteOrder == ByteOrder.BigEndian)
            {
                byte[] buff = BitConverter.GetBytes(myShort);
                Array.Reverse(buff);
                myShort = BitConverter.ToInt16(buff, 0);
            }

            return myShort;
        }

        public ushort ReadUInt16()
        {
            ushort myUShort = br.ReadUInt16();

            if (byteOrder == ByteOrder.BigEndian)
            {
                byte[] buff = BitConverter.GetBytes(myUShort);
                Array.Reverse(buff);
                myUShort = BitConverter.ToUInt16(buff, 0);
            }

            return myUShort;
        }

        public int ReadInt32()
        {
            int myShort = br.ReadInt32();

            if (byteOrder == ByteOrder.BigEndian)
            {
                byte[] buff = BitConverter.GetBytes(myShort);
                Array.Reverse(buff);
                myShort = BitConverter.ToInt32(buff, 0);
            }

            return myShort;
        }

        public uint ReadUInt32()
        {
            uint myUShort = br.ReadUInt32();

            if (byteOrder == ByteOrder.BigEndian)
            {
                byte[] buff = BitConverter.GetBytes(myUShort);
                Array.Reverse(buff);
                myUShort = BitConverter.ToUInt32(buff, 0);
            }

            return myUShort;
        }

        public long ReadInt64()
        {
            long myShort = br.ReadInt32();

            if (byteOrder == ByteOrder.BigEndian)
            {
                byte[] buff = BitConverter.GetBytes(myShort);
                Array.Reverse(buff);
                myShort = BitConverter.ToInt64(buff, 0);
            }

            return myShort;
        }

        public ulong ReadUInt64()
        {
            ulong myUShort = br.ReadUInt32();

            if (byteOrder == ByteOrder.BigEndian)
            {
                byte[] buff = BitConverter.GetBytes(myUShort);
                Array.Reverse(buff);
                myUShort = BitConverter.ToUInt64(buff, 0);
            }

            return myUShort;
        }

        public byte[] ReadByte(int amount)
        {
            byte[] buffer = br.ReadBytes(amount);

            if (byteOrder == ByteOrder.LittleEndian)
            {
                Array.Reverse(buffer); 
            }

            return buffer;
        }

        public void Close()
        {
            br.Close();
        }


        public string ReadString(int lenght)
        {
           return new string( br.ReadChars(lenght));
        }

        public string ReadUnicodeString(int lenght)
        {
            if (byteOrder == ByteOrder.BigEndian)
            {
               return  Encoding.BigEndianUnicode.GetString(br.ReadBytes(lenght));
            }
            else
            {
                return Encoding.Unicode.GetString(br.ReadBytes(lenght));
            }

            
        }

        public char ReadChar()
        {
           return br.ReadChar();
        }

        public char[] ReadChar(int amount)
        {
            return br.ReadChars(amount);
        }

    }
    public class Writer: IO
    {
        BinaryWriter bw;
        public Writer (string path)
        {
            bw = new BinaryWriter(File.OpenWrite(path));
            this.byteOrder = ByteOrder.BigEndian;
        }
        public Writer (string path, ByteOrder bo)
        {
            bw = new BinaryWriter(File.OpenWrite(path));
            this.byteOrder = bo;
        }
        public long Position
        {
            get { return bw.BaseStream.Position; }
            set { bw.BaseStream.Position = value; }
        }

        public void WriteByte(byte toWrite)
        {
          bw.Write(toWrite);
        }

        public void WriteBytes(byte[] bytesToWrite)
        {
            if (byteOrder == ByteOrder.BigEndian)
            {
                Array.Reverse(bytesToWrite);
                bw.Write(bytesToWrite);
            }
        }

        public void WriteToInt16(short toWrite)
        {
            byte[] buff = BitConverter.GetBytes(toWrite);
            if (byteOrder == ByteOrder.BigEndian)
                Array.Reverse(buff);
                bw.Write(buff);  
        }

        public void WriteToUInt16(ushort toWrite)
        {
            byte[] buff = BitConverter.GetBytes(toWrite);
            if (byteOrder == ByteOrder.BigEndian)
                Array.Reverse(buff);
            bw.Write(buff);
        }

        public void WriteToInt32(int toWrite)
        {
            byte[] buff = BitConverter.GetBytes(toWrite);
            if (byteOrder == ByteOrder.BigEndian)
                Array.Reverse(buff);
            bw.Write(buff);
        }
        
        public void WriteToUInt32(uint toWrite)
        {
            byte[] buff = BitConverter.GetBytes(toWrite);
            if (byteOrder == ByteOrder.BigEndian)
                Array.Reverse(buff);
            bw.Write(buff);
        }

        public void WriteToInt64(long toWrite)
        {
            byte[] buff = BitConverter.GetBytes(toWrite);
            if (byteOrder == ByteOrder.BigEndian)
                Array.Reverse(buff);
            bw.Write(buff);
        }

        public void WriteToUInt64(ulong toWrite)
        {
            byte[] buff = BitConverter.GetBytes(toWrite);
            if (byteOrder == ByteOrder.BigEndian)
                Array.Reverse(buff);
            bw.Write(buff);
        }

        public void WriteString(string toWrite)
        {
            bw.Write(toWrite.ToCharArray());
        }
  
        public void WriteUnicodeString(string toWrite)
        {
            
           byte [] buffer = (byteOrder == ByteOrder.BigEndian) 
                ? Encoding.BigEndianUnicode.GetBytes(toWrite)
                :Encoding.Unicode.GetBytes(toWrite);
            bw.Write(buffer);


        }

        public void WriteCharacter(char toWrite)
        {
            bw.Write(toWrite);
        }

        public void WriteCharacters(char[] toWrite)
        {
            bw.Write(toWrite);
        }

        public void Close()
        {
            bw.Close();
        }

        
    }
   public abstract class IO
    {
        /// <summary>
        /// The order of the bytes both read and written
        /// </summary>
        public enum ByteOrder
        {
            BigEndian,
            LittleEndian
        }
        protected ByteOrder byteOrder;

        public void ChangeByteOrder(ByteOrder bo)
        {
            this.byteOrder = bo;
        }
    }
}
