using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StringToInt
{
    static public class IntConvert
    {

        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Convert string to Int32
        /// </summary>
        /// <param name="number">The string which need to convert</param>
        /// <returns>The int number</returns>
        public static int Convert(string number)
        {
            // Set the max digit value to avoiding overflow exception when number == int32.maxValue
            const int MaxDigit = 1000000000;
            // Set initial value to -1 to avoiding overflow exception when number == int32.minValue
            int finalNumber = -1;
            int digit = 1;
            bool isNegative = false;
            StringBuilder stringToConvert = new StringBuilder(number);

            _logger.Debug($"Get {number} to convert");
            _logger.Info($"Start convert {number} to Int");
            try
            {
                for (int numeral = stringToConvert.Length - 1; numeral >= 0; numeral--)
                {
                    checked
                    {
                        _logger.Debug($"Try convert {stringToConvert[numeral]} to number");
                        switch (stringToConvert[numeral])
                        {
                            case '-':
                                if (!isNegative)
                                {
                                    isNegative = true;
                                }
                                else
                                {
                                    throw new FormatException("Too many '-'");
                                }
                                break;
                            case '0':
                                finalNumber += 0 * digit;
                                if (digit < MaxDigit)
                                    digit *= 10;
                                break;
                            case '1':
                                finalNumber += 1 * digit;
                                if (digit < MaxDigit)
                                    digit *= 10;
                                break;
                            case '2':
                                finalNumber += 2 * digit;
                                if (digit < MaxDigit)
                                    digit *= 10;
                                break;
                            case '3':
                                finalNumber += 3 * digit;
                                if (digit < MaxDigit)
                                    digit *= 10;
                                break;
                            case '4':
                                finalNumber += 4 * digit;
                                if (digit < MaxDigit)
                                    digit *= 10;
                                break;
                            case '5':
                                finalNumber += 5 * digit;
                                if (digit < MaxDigit)
                                    digit *= 10;
                                break;
                            case '6':
                                finalNumber += 6 * digit;
                                if (digit < MaxDigit)
                                    digit *= 10;
                                break;
                            case '7':
                                finalNumber += 7 * digit;
                                if (digit < MaxDigit)
                                    digit *= 10;
                                break;
                            case '8':
                                finalNumber += 8 * digit;
                                if (digit < MaxDigit)
                                    digit *= 10;
                                break;
                            case '9':
                                finalNumber += 9 * digit;
                                if (digit < MaxDigit)
                                    digit *= 10;
                                break;
                            default: throw new FormatException("Input string isn't number, may be your input string contains symbols");
                        }
                        _logger.Debug($"Finish convert {stringToConvert[numeral]} to number");
                    }
                }
                // Don't remember return value to normal form
                if (isNegative)
                {
                    finalNumber *= -1;
                    finalNumber -= 1;
                }
                else
                {
                    finalNumber += 1;
                }
                _logger.Debug($"Finish convert");
                return finalNumber;
            }
            catch (FormatException ex)
            {
                _logger.Error(ex, ex.Message);
                throw ex;
            }
            catch (OverflowException ex)
            {
                _logger.Error(ex, "Overflow Int32");
                throw ex;
            }
        }
    }
}
