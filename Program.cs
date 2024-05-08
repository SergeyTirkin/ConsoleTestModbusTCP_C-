using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestModbusTCP_C_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}

/*********************КАРТА РЕГИСТРОВ*****************************
 * HEX (DEC)
 * 0x01(1) - выход 1 
 * 0x02(2) - выход 2
 * 0x03(3) - выход 3
 * 0x04(4) - выход 4
 * 0x05(5) - выход 5
 * 0x06(6) - выход 6
 * 0x07(7) - выход 7
 * 0x08(8) - выход 8
 * 0x09(9) - выход 9 
 * 0x0A(10) - выход 10
 * 0x0B(11) - выход 11
 * 0x0C(12) - выход 12
 * 0x0D(13) - выход 13
 * 0x0E(14) - выход 14
 * 0x0F(15) - выход 15
 * 0x10(16) - выход 16 - функция для записи дискретных выходов 0x05(5) - Write Single Coils для одного или маской 0x0A(15) - Write Multiple Coils
 
 * 0x11(17) - битовая маска дискретных входов - только для чтения, функция 0x04(4) - ReadInputRegisters     
 
 * 0x12(18) - значение АЦП 1-го аналогового входа(в ответ прилетает uint16_t 2 байта)
 * 0x13(19) - значение АЦП 2-го аналогового входа(в ответ прилетает uint16_t 2 байта)
 * 0x14(20) - значение АЦП 3-го аналогового входа(в ответ прилетает uint16_t 2 байта)
 * 0x15(21) - значение АЦП 4-го аналогового входа(в ответ прилетает uint16_t 2 байта), 
    читается функцией 0x04(4) - ReadInputRegisters или 0x03(3) - Read Holding Register 
    пишется 0x06(6) - Write Single Register или 0x10(16) - Write Multiple Registers 
*/
