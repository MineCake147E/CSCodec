using CSCodec.Binary.Checksum;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSCodec.UnitTests.Binary.Checksum
{
	[TestFixture]
	public class CrcTests
	{
		/// <summary>
		/// A Quotation from Wikipedia.
		/// </summary>
		public readonly string data = "A cyclic redundancy check (CRC) is an error-detecting code commonly used in digital networks and storage devices to detect accidental changes to raw data.";

		[TestCase("CRC-8", (byte)0x07u, (byte)0x00u, (byte)0x00u, false, false, (byte)0x08u)]
		[TestCase("CRC-8/CDMA2000", (byte)0x9Bu, (byte)0xFFu, (byte)0x00u, false, false, (byte)0xCBu)]
		[TestCase("CRC-8/DARC", (byte)0x39u, (byte)0x00u, (byte)0x00u, true, true, (byte)0xBEu)]
		[TestCase("CRC-8/DVB-S2", (byte)0xD5u, (byte)0x00u, (byte)0x00u, false, false, (byte)0x19u)]
		[TestCase("CRC-8/EBU", (byte)0x1Du, (byte)0xFFu, (byte)0x00u, true, true, (byte)0xF4u)]
		[TestCase("CRC-8/I-CODE", (byte)0x1Du, (byte)0xFDu, (byte)0x00u, false, false, (byte)0xE6u)]
		[TestCase("CRC-8/ITU", (byte)0x07u, (byte)0x00u, (byte)0x55u, false, false, (byte)0x5Du)]
		[TestCase("CRC-8/MAXIM", (byte)0x31u, (byte)0x00u, (byte)0x00u, true, true, (byte)0x1Fu)]
		[TestCase("CRC-8/ROHC", (byte)0x07u, (byte)0xFFu, (byte)0x00u, true, true, (byte)0x6Du)]
		[TestCase("CRC-8/WCDMA", (byte)0x9Bu, (byte)0x00u, (byte)0x00u, true, true, (byte)0xADu)]
		public void TestCrc8(string name, byte polynomial, byte initialState, byte xorOutput, bool reverseInput, bool reverseOutput, byte check)
		{
			using (Crc8 crc = new Crc8(polynomial, initialState, xorOutput, reverseInput, reverseOutput))
			{
				crc.Initialize();
				var bytes = Encoding.UTF8.GetBytes(data);
				for (int i = 0; i < bytes.Length; i++)
				{
					crc.Append(bytes[i]);
				}
				byte output = crc.CurrentOutput;
				Assert.AreEqual(check, output, 0);
			}
		}

		[TestCase("CRC-16/CCITT-FALSE", (ushort)0x1021u, (ushort)0xFFFFu, (ushort)0x0000u, false, false, (ushort)0x7635u)]
		[TestCase("CRC-16/ARC", (ushort)0x8005u, (ushort)0x0000u, (ushort)0x0000u, true, true, (ushort)0x1AC3u)]
		[TestCase("CRC-16/AUG-CCITT", (ushort)0x1021u, (ushort)0x1D0Fu, (ushort)0x0000u, false, false, (ushort)0x280Bu)]
		[TestCase("CRC-16/BUYPASS", (ushort)0x8005u, (ushort)0x0000u, (ushort)0x0000u, false, false, (ushort)0x3538u)]
		[TestCase("CRC-16/CDMA2000", (ushort)0xC867u, (ushort)0xFFFFu, (ushort)0x0000u, false, false, (ushort)0x21CBu)]
		[TestCase("CRC-16/DDS-110", (ushort)0x8005u, (ushort)0x800Du, (ushort)0x0000u, false, false, (ushort)0x9FBFu)]
		[TestCase("CRC-16/DECT-R", (ushort)0x0589u, (ushort)0x0000u, (ushort)0x0001u, false, false, (ushort)0x9FD7u)]
		[TestCase("CRC-16/DECT-X", (ushort)0x0589u, (ushort)0x0000u, (ushort)0x0000u, false, false, (ushort)0x9FD6u)]
		[TestCase("CRC-16/DNP", (ushort)0x3D65u, (ushort)0x0000u, (ushort)0xFFFFu, true, true, (ushort)0x0FA8u)]
		[TestCase("CRC-16/EN-13757", (ushort)0x3D65u, (ushort)0x0000u, (ushort)0xFFFFu, false, false, (ushort)0x3CB6u)]
		[TestCase("CRC-16/GENIBUS", (ushort)0x1021u, (ushort)0xFFFFu, (ushort)0xFFFFu, false, false, (ushort)0x89CAu)]
		[TestCase("CRC-16/MAXIM", (ushort)0x8005u, (ushort)0x0000u, (ushort)0xFFFFu, true, true, (ushort)0xE53Cu)]
		[TestCase("CRC-16/MCRF4XX", (ushort)0x1021u, (ushort)0xFFFFu, (ushort)0x0000u, true, true, (ushort)0xF9C8u)]
		[TestCase("CRC-16/RIELLO", (ushort)0x1021u, (ushort)0xB2AAu, (ushort)0x0000u, true, true, (ushort)0x4845u)]
		[TestCase("CRC-16/T10-DIF", (ushort)0x8BB7u, (ushort)0x0000u, (ushort)0x0000u, false, false, (ushort)0xBDD4u)]
		[TestCase("CRC-16/TELEDISK", (ushort)0xA097u, (ushort)0x0000u, (ushort)0x0000u, false, false, (ushort)0xA345u)]
		[TestCase("CRC-16/TMS37157", (ushort)0x1021u, (ushort)0x89ECu, (ushort)0x0000u, true, true, (ushort)0x098Fu)]
		[TestCase("CRC-16/USB", (ushort)0x8005u, (ushort)0xFFFFu, (ushort)0xFFFFu, true, true, (ushort)0xD9F3u)]
		[TestCase("CRC-A", (ushort)0x1021u, (ushort)0xC6C6u, (ushort)0x0000u, true, true, (ushort)0xF4B8u)]
		[TestCase("CRC-16/KERMIT", (ushort)0x1021u, (ushort)0x0000u, (ushort)0x0000u, true, true, (ushort)0x80DDu)]
		[TestCase("CRC-16/MODBUS", (ushort)0x8005u, (ushort)0xFFFFu, (ushort)0x0000u, true, true, (ushort)0x260Cu)]
		[TestCase("CRC-16/X-25", (ushort)0x1021u, (ushort)0xFFFFu, (ushort)0xFFFFu, true, true, (ushort)0x0637u)]
		[TestCase("CRC-16/XMODEM", (ushort)0x1021u, (ushort)0x0000u, (ushort)0x0000u, false, false, (ushort)0xDEABu)]
		public void TestCrc16(string name, ushort polynomial, ushort initialState, ushort xorOutput, bool reverseInput, bool reverseOutput, ushort check)
		{
			using (Crc16 crc = new Crc16(polynomial, initialState, xorOutput, reverseInput, reverseOutput))
			{
				crc.Initialize();
				var bytes = Encoding.UTF8.GetBytes(data);
				for (int i = 0; i < bytes.Length; i++)
				{
					crc.Append(bytes[i]);
				}
				ushort output = crc.CurrentOutput;
				Assert.AreEqual(check, output, 0);
			}
		}

		[TestCase("CRC-32", 0x04C11DB7u, 0xFFFFFFFFu, 0xFFFFFFFFu, true, true, 0x6393E68Au)]
		[TestCase("CRC-32/BZIP2", 0x04C11DB7u, 0xFFFFFFFFu, 0xFFFFFFFFu, false, false, 0x9DEE0FD5u)]
		[TestCase("CRC-32C", 0x1EDC6F41u, 0xFFFFFFFFu, 0xFFFFFFFFu, true, true, 0xFB2FEA63u)]
		[TestCase("CRC-32D", 0xA833982Bu, 0xFFFFFFFFu, 0xFFFFFFFFu, true, true, 0x50EC7ABAu)]
		[TestCase("CRC-32/MPEG-2", 0x04C11DB7u, 0xFFFFFFFFu, 0x00000000u, false, false, 0x6211F02Au)]
		[TestCase("CRC-32/POSIX", 0x04C11DB7u, 0x00000000u, 0xFFFFFFFFu, false, false, 0xDF80B4DAu)]
		[TestCase("CRC-32Q", 0x814141ABu, 0x00000000u, 0x00000000u, false, false, 0x9CFE3164u)]
		[TestCase("CRC-32/JAMCRC", 0x04C11DB7u, 0xFFFFFFFFu, 0x00000000u, true, true, 0x9C6C1975u)]
		[TestCase("CRC-32/XFER", 0x000000AFu, 0x00000000u, 0x00000000u, false, false, 0x17D3C65Cu)]
		public void TestCrc32(string name, uint polynomial, uint initialState, uint xorOutput, bool reverseInput, bool reverseOutput, uint check)
		{
			using (Crc32 crc = new Crc32(polynomial, initialState, xorOutput, reverseInput, reverseOutput))
			{
				crc.Initialize();
				var bytes = Encoding.UTF8.GetBytes(data);
				for (int i = 0; i < bytes.Length; i++)
				{
					crc.Append(bytes[i]);
				}
				uint output = crc.CurrentOutput;
				Assert.AreEqual(check, output, 0);
			}
		}
	}
}
