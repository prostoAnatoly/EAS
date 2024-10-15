using System.Reflection;
using System.Reflection.Emit;

namespace Shared.IL.Utils.ILReader
{
    /// <summary>
    /// Читатель тела метода
    /// </summary>
    public class MethodBodyReader
    {
        private static readonly OpCode[] multiByteOpCodes = new OpCode[0x100];
        private static readonly OpCode[] singleByteOpCodes = new OpCode[0x100];

        private readonly byte[]? ilCode = null;
        private readonly MethodInfo? methodInfoi = null;
        private readonly Module? module = null;

        static MethodBodyReader()
        {
            LoadOpCodes();
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MethodBodyReader"/>
        /// </summary>
        /// <param name="mi">Информация о методе</param>
        public MethodBodyReader(MethodInfo mi)
        {
            methodInfoi = mi;
            module = mi.Module;
            var body = mi.GetMethodBody();
            if (body != null)
            {
                ilCode = body.GetILAsByteArray();
            }
            else
            {
                ilCode = Array.Empty<byte>();
            }
        }

        private static void LoadOpCodes()
        {
            FieldInfo[] infoArray1 = typeof(OpCodes).GetFields();
            for (int num1 = 0; num1 < infoArray1.Length; num1++)
            {
                FieldInfo info1 = infoArray1[num1];
                if (info1.FieldType == typeof(OpCode))
                {
                    OpCode code1 = (OpCode)info1.GetValue(null);
                    ushort num2 = (ushort)code1.Value;
                    if (num2 < 0x100)
                    {
                        singleByteOpCodes[(int)num2] = code1;
                    }
                    else
                    {
                        if ((num2 & 0xff00) != 0xfe00)
                        {
                            throw new Exception("Invalid OpCode.");
                        }
                        multiByteOpCodes[num2 & 0xff] = code1;
                    }
                }
            }
        }

        private static ushort ReadUInt16(byte[] il, ref int position)
        {
            return (ushort)(il[position++] | (il[position++] << 8));
        }
        private static int ReadInt32(byte[] il, ref int position)
        {
            return il[position++] | (il[position++] << 8) | (il[position++] << 0x10) | (il[position++] << 0x18);
        }
        private static ulong ReadInt64(byte[] il, ref int position)
        {
            return (ulong)((il[position++] | (il[position++] << 8)) | (il[position++] << 0x10) | (il[position++] << 0x18) | (il[position++] << 0x20) | (il[position++] << 0x28) | (il[position++] << 0x30) | (il[position++] << 0x38));
        }
        private static double ReadDouble(byte[] il, ref int position)
        {
            return (il[position++] | (il[position++] << 8)) | (il[position++] << 0x10) | (il[position++] << 0x18) | (il[position++] << 0x20) | (il[position++] << 0x28) | (il[position++] << 0x30) | (il[position++] << 0x38);
        }
        private static sbyte ReadSByte(byte[] il, ref int position)
        {
            return (sbyte)il[position++];
        }
        private static byte ReadByte(byte[] il, ref int position)
        {
            return il[position++];
        }
        private static float ReadSingle(byte[] il, ref int position)
        {
            return (il[position++] | (il[position++] << 8)) | (il[position++] << 0x10) | (il[position++] << 0x18);
        }

        /// <summary>
        /// Возвращает массив IL-инструкций
        /// </summary>
        public ILInstruction[] GetILInstructions()
        {
            return ConstructInstructions().ToArray();
        }

        private List<ILInstruction> ConstructInstructions()
        {
            int position = 0;
            var instructions = new List<ILInstruction>();
            if (module == null || ilCode == null)
            {
                return instructions;
            }

            while (position < ilCode.Length)
            {
                var instruction = new ILInstruction();
                ushort value = ilCode[position++];

                // получить код операции для текущей инструкции
                OpCode code;
                if (value != 0xfe)
                {
                    code = singleByteOpCodes[value];
                }
                else
                {
                    value = ilCode[position++];
                    code = multiByteOpCodes[value];
                }
                instruction.Code = code;
                int metadataToken;
                // Получить operand для текущей операции
                switch (code.OperandType)
                {
                    case OperandType.InlineBrTarget:
                        metadataToken = ReadInt32(ilCode, ref position);
                        metadataToken += position;
                        instruction.Operand = metadataToken;
                        break;
                    case OperandType.InlineField:
                        metadataToken = ReadInt32(ilCode, ref position);
                        try
                        {
                            instruction.Operand = module.ResolveField(metadataToken);
                        }
                        catch
                        {
                            instruction.Operand = module.ResolveSignature(metadataToken);
                        }
                        break;
                    case OperandType.InlineMethod:
                        metadataToken = ReadInt32(ilCode, ref position);
                        try
                        {
                            instruction.Operand = module.ResolveMethod(metadataToken);
                        }
                        catch
                        {
                            try
                            {
                                instruction.Operand = module.ResolveMember(metadataToken);
                            }
                            catch
                            {
                                instruction.Operand = module.ResolveSignature(metadataToken);
                            }
                        }
                        break;
                    case OperandType.InlineSig:
                        metadataToken = ReadInt32(ilCode, ref position);
                        instruction.Operand = module.ResolveSignature(metadataToken);
                        break;
                    case OperandType.InlineTok:
                        metadataToken = ReadInt32(ilCode, ref position);
                        try
                        {
                            instruction.Operand = module.ResolveType(metadataToken);
                        }
                        catch
                        {

                        }
                        break;
                    case OperandType.InlineType:
                        metadataToken = ReadInt32(ilCode, ref position);
                        instruction.Operand = module.ResolveType(metadataToken, 
                            methodInfoi.DeclaringType.GetGenericArguments(), methodInfoi.GetGenericArguments());
                        break;
                    case OperandType.InlineI:
                        {
                            instruction.Operand = ReadInt32(ilCode, ref position);
                            break;
                        }
                    case OperandType.InlineI8:
                        {
                            instruction.Operand = ReadInt64(ilCode, ref position);
                            break;
                        }
                    case OperandType.InlineNone:
                        {
                            instruction.Operand = null;
                            break;
                        }
                    case OperandType.InlineR:
                        {
                            instruction.Operand = ReadDouble(ilCode, ref position);
                            break;
                        }
                    case OperandType.InlineString:
                        {
                            metadataToken = ReadInt32(ilCode, ref position);
                            instruction.Operand = module.ResolveString(metadataToken);
                            break;
                        }
                    case OperandType.InlineSwitch:
                        {
                            int count = ReadInt32(ilCode, ref position);
                            var casesAddresses = new int[count];
                            for (int i = 0; i < count; i++)
                            {
                                casesAddresses[i] = ReadInt32(ilCode, ref position);
                            }
                            int[] cases = new int[count];
                            for (int i = 0; i < count; i++)
                            {
                                cases[i] = position + casesAddresses[i];
                            }
                            break;
                        }
                    case OperandType.InlineVar:
                        {
                            instruction.Operand = ReadUInt16(ilCode, ref position);
                            break;
                        }
                    case OperandType.ShortInlineBrTarget:
                        {
                            instruction.Operand = ReadSByte(ilCode, ref position) + position;
                            break;
                        }
                    case OperandType.ShortInlineI:
                        {
                            instruction.Operand = ReadSByte(ilCode, ref position);
                            break;
                        }
                    case OperandType.ShortInlineR:
                        {
                            instruction.Operand = ReadSingle(ilCode, ref position);
                            break;
                        }
                    case OperandType.ShortInlineVar:
                        {
                            instruction.Operand = ReadByte(ilCode, ref position);
                            break;
                        }
                    default:
                        {
                            throw new Exception("Unknown operand type.");
                        }
                }
                instructions.Add(instruction);
            }
            return instructions;
        }

    }
}