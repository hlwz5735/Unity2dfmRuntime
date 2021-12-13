using System;
using UnityEngine;

namespace _2dfmFile
{
    public static class DecompressUtil
    {
        public static byte[] Decompress(byte[] original, int destSize)
        {
            if (destSize <= 0)
            {
                return null;
            }

            var result = new byte[destSize];
            int inputOffset = 0;
            int resultPointer = 0;
            try
            {
                while (inputOffset < original.Length)
                {
                    int current = original[inputOffset];
                    int type = current >> 6;
                    current = current & 0x3f;
                    if (current == 0)
                    {
                        inputOffset += 1;
                        current = original[inputOffset];
                        if (current == 0)
                        {
                            inputOffset += 1;
                            current = BitConverter.ToUInt16(original, inputOffset);
                            inputOffset += 2;

                            var highBits = original[inputOffset] << 16;
                            current = current + highBits + 0x013f;
                        }
                        else
                        {
                            current += 0x3f;
                        }
                    }

                    switch (type)
                    {
                        case 0:
                            for (int i = 0; i < current; i++)
                            {
                                result[resultPointer + i] = 0;
                            }

                            resultPointer += current;
                            break;
                        case 1:
                            if (current > 0)
                            {
                                for (int i = 0; i < current; i++)
                                {
                                    result[resultPointer + i] = original[inputOffset + 1 + i];
                                }

                                resultPointer += current;
                                inputOffset += current;
                            }

                            break;
                        case 2:
                        {
                            inputOffset += 1;
                            var nextByte = original[inputOffset];
                            for (int i = 0; i < current; i++)
                            {
                                result[resultPointer + i] = nextByte;
                            }

                            resultPointer += current;
                        }
                            break;
                        case 3:
                        {
                            inputOffset += 1;
                            int backTraceCount = original[inputOffset];
                            if (backTraceCount == 0)
                            {
                                inputOffset += 1;
                                backTraceCount = (original[inputOffset] + 1) << 8;
                            }

                            var backTraceStart = resultPointer - backTraceCount;
                            for (int i = 0; i < current; i++)
                            {
                                result[resultPointer + i] = result[backTraceStart + i];
                            }

                            resultPointer += current;
                        }
                            break;
                    }

                    inputOffset++;
                }
            }
            catch (Exception e)
            {
                Debug.LogError($@"发生异常！innerOffset={inputOffset}, resultPointer={resultPointer}");
                Debug.LogError(e.StackTrace);
                throw;
            }


            return result;
        }
    }
}