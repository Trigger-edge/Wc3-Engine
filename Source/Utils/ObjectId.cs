using System;
using System.Collections.Generic;
using System.Linq;

namespace Wc3Engine
{
    static class ObjectId
    {
        private static int ABILITY_ID_START = 1093677104;

        private static List<int> AbilityList = new List<int>();
        private static List<int> MasterList = new List<int>();

        public static int GenerateAbilityId()
        {
            int id = ABILITY_ID_START;
            while (AbilityList.Contains(id))
                id++;

            AddAbilityId(id);
            return id;
        }

        public static void AddAbilityId(int id)
        {
            if (!AbilityList.Contains(id))
            {
                AbilityList.Add(id);
                MasterList.Add(id);
            }
        }

        public static int ModuloInteger(int dividend, int divisor)
        {
            int modulus = dividend - (dividend / divisor) * divisor;

            // If the dividend was negative, the above modulus calculation will
            // be negative, but within (-divisor..0).  We can add (divisor) to
            // shift this result into the desired range of (0..divisor).
            if (modulus < 0)
                modulus = modulus + divisor;

            return modulus;
        }

        public static int ToIntObjectId(this string value)
        {
            return (value[0] << 24) + (value[1] << 16) + (value[2] << 8) + value[3];
        }

        /*public static int uintToTag(uint)
        {
            return String.fromCharCode((uint >> 24) & 0xff, (uint >> 16) & 0xff, (uint >> 8) & 0xff, uint & 0xff);
        }*/

        public static string ToObjectId(this int value)
        {
            string charMap = ".................................!.#$%&'()*+,-./0123456789:;<=>.@ABCDEFGHIJKLMNOPQRSTUVWXYZ[.]^_`abcdefghijklmnopqrstuvwxyz{|}~.................................................................................................................................";
            string result = "";
            int remainingValue = value;
            int charValue;

            for (int i = 0; i < 4; i++)
            {
                charValue = ModuloInteger(remainingValue, 256);
                remainingValue = remainingValue / 256;
                result = charMap[charValue].ToString() + result;
            }

            return result;
        }

        public static string ToObjectId(this string value)
        {
            return Convert.ToInt32(value).ToObjectId();
        }
    }
}
