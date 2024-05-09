using FluentModbus;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTestModbusTCP_C_
{
    class Program
    {
        static async Task Main(string[] args)
        {
            /* create logger */
            var loggerFactory = LoggerFactory.Create(loggingBuilder =>
            {
                loggingBuilder.SetMinimumLevel(LogLevel.Debug);
                //loggingBuilder.AddConsole(); // не добавлется вывод в консоль, выведем пока по другому
            });
            
            var clientLogger = loggerFactory.CreateLogger("Client");

            /* create Modbus TCP client */
            var client = new ModbusTcpClient();            

            /* run Modbus TCP client */
            var task_client = Task.Run(() =>
            {
                client.Connect(new IPEndPoint(IPAddress.Parse("192.168.1.2"), 502));

                try
                {
                    DoClientWork(client, clientLogger);
                    Console.WriteLine("работает!!!!!");
                }
                catch (Exception ex)
                {
                    clientLogger.LogError(ex.Message);
                    Console.WriteLine("не работает*********");
                    Console.WriteLine(ex.Message);
                }

                client.Disconnect();

                Console.WriteLine("Tests finished. Press any key to continue.");
                Console.ReadKey(intercept: true);
            });

            // wait for client task to finish
            await task_client;            
        }

        static void DoClientWork(ModbusTcpClient client, ILogger logger)
        {
            Span<byte> data;

            var sleepTime = TimeSpan.FromMilliseconds(100);
            var unitIdentifier = 0x01;
            var startingAddress = 0x11;
            var amountRegisters = 0x04;

            // ReadInputRegisters = 0x04,          // FC04
            data = client.ReadInputRegisters<byte>(unitIdentifier, startingAddress, amountRegisters);
            logger.LogInformation("FC04 - ReadInputRegisters: Done");
            Console.WriteLine(data.ToArray().GetValue(3));//так не показывается, как посмотреть данные пока х\з, еще и таймер надо зафигачить            
            Thread.Sleep(sleepTime);            
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
