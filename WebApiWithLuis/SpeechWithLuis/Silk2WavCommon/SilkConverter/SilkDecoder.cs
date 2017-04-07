﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Silk2WavCommon.SilkConverter
{
    public class SilkDecoder
    {
        [DllImport(@"\SilkConverter\decoderdll.dll", CallingConvention = CallingConvention.Cdecl)]
        private unsafe static extern int GetResult(
        int an_int,
        byte** buffer
        );

        [DllImport(@"\SilkConverter\decoderdll.dll", CallingConvention = CallingConvention.Cdecl)]
        private unsafe static extern void CleanBytePointer(byte *ptr);

        [DllImport(@"\SilkConverter\decoderdll.dll", CallingConvention = CallingConvention.Cdecl)]
        private unsafe static extern void CleanShortPointer(short* ptr);

        [DllImport(@"\SilkConverter\decoderdll.dll", CallingConvention = CallingConvention.Cdecl)]
        private unsafe static extern void CleanIntPointer(int* ptr);

        [DllImport(@"\SilkConverter\decoderdll.dll", CallingConvention = CallingConvention.Cdecl)]
        private unsafe static extern int SilkDecoderToPcm(
            byte[] jBuffers,
            int size,
            short** outBuffer,
            int* length
         );

        public static int RunConvert(byte[] inputs, int inputLen, out byte[] outpus, out int outputLen)
        {
            byte[] buffers;
            int count = 0;
            unsafe
            {
                short* buffer_y;
                int length;
                var returnValue = SilkDecoderToPcm(inputs, inputLen, &buffer_y, &length);
                buffers = new byte[length * 2];
                //Marshal.Copy(Marshal.AllocHGlobal(b
                for (int i = 0; i < length; i++)
                {
                    var temps = BitConverter.GetBytes(buffer_y[i]);
                    buffers[i * 2] = temps[0];
                    buffers[i * 2 + 1] = temps[1];
                }
                // do free task
                CleanShortPointer(buffer_y);

                count = length * 2;
                outpus = buffers;
                outputLen = count;

                return returnValue;
            }
        }

        private byte[] _silkBytes;

        private int _silkBytesLen;

        private byte[] _pcmBytes;

        private int _pcmBytesLen;

        bool _decoded = false;

        public SilkDecoder(byte[] inputs, int inputLen)
        {
            _silkBytes = inputs;
            _silkBytesLen = inputLen;
        }

        public int RunDecode()
        {
            int value =  RunConvert(this._silkBytes, this._silkBytesLen, out this._pcmBytes, out this._pcmBytesLen);
            this._decoded = true;
            return value;
        }

        public byte[] PcmOuts
        {
            get
            {
                if (!_decoded)
                {
                    RunDecode();
                }

                return _pcmBytes;
            }
            set { }
        }

        public int PcmOutsLen
        { get
            {
                if (!_decoded)
                {
                    RunDecode();
                }

                return _pcmBytesLen;
            }
            set { }
        }
    }
}
