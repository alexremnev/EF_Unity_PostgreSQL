using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EF_Unity_PostgreSQL.Models.Business
{
    public class RC5 : ICryptoEngine
    {
        private readonly uint[] s;
        private readonly uint[] l;
        private readonly uint t;
        private readonly uint c;
        private const int rounds = 16;

        public RC5(string securityKey)
        {
            var password = securityKey;
            var key = GetKeyFromString(password);
            var b = (uint)key.Length;
            const uint u = 4;
            t = 2 * rounds + 2;
            c = Math.Max(b, 1) / u;
            s = new uint[2 * rounds + 2];
            l = new uint[key.Length];
            GenerateKey(key);
        }

        private static uint leftRotate(uint x, int offset)
        {
            var t1 = x >> (32 - offset);
            var t2 = x << offset;
            return t1 | t2;
        }

        private static uint RightRotate(uint x, int offset)
        {
            var t1 = x << (32 - offset);
            var t2 = x >> offset;
            return t1 | t2;
        }

        private void Encode(ref uint r1, ref uint r2)
        {
            r1 = r1 + s[0];
            r2 = r2 + s[1];
            for (var i = 1; i <= rounds; i++)
            {
                r1 = leftRotate(r1 ^ r2, (int)r2) + s[2 * i];
                r2 = leftRotate(r2 ^ r1, (int)r1) + s[2 * i + 1];
            }
        }

        private void Decode(ref uint r1, ref uint r2)
        {
            for (var i = rounds; i >= 1; i--)
            {
                r2 = (RightRotate(r2 - s[2 * i + 1], (int)r1)) ^ r1;
                r1 = (RightRotate(r1 - s[2 * i], (int)r2)) ^ r2;
            }
            r2 = r2 - s[1];
            r1 = r1 - s[0];
        }

        private void GenerateKey(IReadOnlyList<byte> key)
        {
            var P32 = uint.Parse("b7e15163", System.Globalization.NumberStyles.HexNumber);
            var Q32 = uint.Parse("9e3779b9", System.Globalization.NumberStyles.HexNumber);

            for (var i = key.Count - 1; i >= 0; i--)
            {
                l[i] = leftRotate((uint)i, 8) + key[i];
            }

            s[0] = P32;
            for (var i = 1; i <= t - 1; i++)
            {
                s[i] = s[i - 1] + Q32;
            }

            uint jj;
            var ii = jj = 0;
            uint y;
            var x = y = 0;
            var v = 3 * Math.Max(t, c);
            for (var counter = 0; counter <= v; counter++)
            {
                x = s[ii] = leftRotate((s[ii] + x + y), 3);
                y = l[jj] = leftRotate((l[jj] + x + y), (int)(x + y));
                ii = (ii + 1) % t;
                jj = (jj + 1) % c;
            }
        }

        private static byte[] GetKeyFromString(string str)
        {
            var mykeyinchar = str.ToCharArray();
            var mykeyinbytes = new byte[mykeyinchar.Length];
            for (var i = 0; i < mykeyinchar.Length; i++)
            {
                mykeyinbytes[i] = (byte)mykeyinchar[i];
            }
            return mykeyinbytes;
        }

        public void Encrypt(Stream streamreader, Stream streamwriter)
        {
            var br = new BinaryReader(streamreader);
            var bw = new BinaryWriter(streamwriter);
            var filelength = streamreader.Length;
            while (filelength > 0)
            {
                uint r1;
                uint r2;
                try
                {
                    r1 = br.ReadUInt32();
                    try
                    {
                        r2 = br.ReadUInt32();
                    }
                    catch
                    {
                        r2 = 0;
                    }
                }
                catch
                {
                    r1 = r2 = 0;
                }
                Encode(ref r1, ref r2);
                bw.Write(r1);
                bw.Write(r2);
                filelength -= 8;
            }
            streamreader.Close();
            streamwriter.Close();
        }

        public void Decrypt(Stream streamreader, Stream streamwriter)
        {
            var br = new BinaryReader(streamreader);
            var bw = new BinaryWriter(streamwriter);
            var filelength = streamreader.Length;
            while (filelength > 0)
            {
                var r1 = br.ReadUInt32();
                var r2 = br.ReadUInt32();
                Decode(ref r1, ref r2);
                if (!(r1 == 0 && r2 == 0 && (filelength - 8 <= 0)))
                {
                    bw.Write(r1);
                    bw.Write(r2);
                }
                if (r2 == 0 && (filelength - 8 <= 0))
                {
                    bw.Write(r1);
                }
                filelength -= 8;
            }
            streamreader.Close();
            streamwriter.Close();
        }

        public string Encrypt(string text)
        {
            byte[] buf;
            using (var mstream = new MemoryStream())
            using (var writer = new BinaryWriter(mstream))
            {
                var temp = Encoding.UTF8.GetBytes(text);
                writer.Write(temp.Length);
                writer.Write(temp);

                var padding = (int)(mstream.Length % 16);
                if (padding != 0)
                {
                    padding = 16 - padding;
                    var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
                    var paddingBuf = new byte[padding];
                    rng.GetNonZeroBytes(paddingBuf);
                    writer.Write(paddingBuf);
                }
                buf = mstream.ToArray();
            }

            var result = new byte[buf.Length];
            var input = new byte[16];
            var output = new byte[16];
            for (var i = 0; i < buf.Length / 16; i++)
            {
                Array.Copy(buf, i * 16, input, 0, 16);
                var x = new MemoryStream(input);
                var y = new MemoryStream(output);
                Encrypt(x, y);
                Array.Copy(output, 0, result, i * 16, 16);
            }
            return Convert.ToBase64String(result);
        }

        public string Decrypt(string text)
        {
            var data = Convert.FromBase64String(text);
            var result = new byte[data.Length];
            var input = new byte[16];
            var output = new byte[16];
            for (var i = 0; i < data.Length / 16; i++)
            {
                Array.Copy(data, i * 16, input, 0, 16);
                var x = new MemoryStream(input);
                var y = new MemoryStream(output);
                Decrypt(x, y);
                Array.Copy(output, 0, result, i * 16, 16);
            }
            var bytes = BitConverter.ToInt32(result, 0);
            return Encoding.UTF8.GetString(result, 4, bytes);
        }
    }
}