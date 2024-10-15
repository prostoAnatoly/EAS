using System.Reflection.Emit;

namespace Shared.IL.Utils.ILReader
{
    /// <summary>
    /// Информация об инструкции на промежуточном языке (IL)
    /// </summary>
    public class ILInstruction
    {
        /// <summary>
        /// Описывает инструкцию на промежуточном языке (IL)
        /// </summary>
        public OpCode Code { get; set; }

        /// <summary>
        /// Операнд инструкции
        /// </summary>
        public object Operand { get; set; }

    }
}