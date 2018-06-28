using OSIsoft.AF;
using OSIsoft.AF.Search;
using OSIsoft.AF.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EventFrames
{
    class Program
    {
        static void Main(string[] args)
        {

            NetworkCredential credential = new NetworkCredential(connectionInfo.user, connectionInfo.password);
            var piSystem = (new PISystems())[connectionInfo.AFServerName];
            piSystem.Connect(credential);
            var afdb = piSystem.Databases[connectionInfo.AFDatabaseName];


            //using tokens
            List<AFSearchToken> tokenList = new List<AFSearchToken>();
            tokenList.Add(new AFSearchToken(AFSearchFilter.Template, AFSearchOperator.Equal, "Low Contaiment Strength"));
            tokenList.Add(new AFSearchToken(AFSearchFilter.Name, AFSearchOperator.Equal, "T001*"));
            var tokenSearch = new AFEventFrameSearch(afdb, "Template Search", tokenList);


            //using search string
            //var query = "Template:'Low Contaiment Strength' Name:T001*";
            //var stringSearch = new AFEventFrameSearch(afdb, "Template Search", query);


            var results = tokenSearch.FindEventFrames(0, false, 10);
            var counter = 0;
            foreach (var item in results)
            {
                Console.WriteLine($"Event {item.Name}, time: {item.StartTime} - {item.EndTime} Duration: {item.Duration}");
                counter++;
                if (counter > 9) break;
            }


            Console.ReadKey();
        }
    }
}
