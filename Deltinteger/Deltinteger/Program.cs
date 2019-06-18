using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Deltin.Deltinteger.Elements;
using Deltin.Deltinteger.Parse;
using Deltin.Deltinteger.LanguageServer;

namespace Deltin.Deltinteger
{
    public class Program
    {
        static Log Log = new Log(":");
        static Log ParseLog = new Log("Parse");

        static void Main(string[] args)
        {
            Log.LogLevel = LogLevel.Normal;
            if (args.Contains("-verbose"))
                Log.LogLevel = LogLevel.Verbose;
            if (args.Contains("-quiet"))
                Log.LogLevel = LogLevel.Quiet;

            if (args.Contains("-langserver"))
            {
                string[] portArgs = args.FirstOrDefault(v => v.Split(' ')[0] == "-port")?.Split(' ');
                int.TryParse(portArgs.ElementAtOrDefault(1), out int serverPort);
                int.TryParse(portArgs.ElementAtOrDefault(2), out int clientPort);
                Check.RequestLoop(serverPort, clientPort);
            }
            else
            {
                string script = args.ElementAtOrDefault(0);

                if (File.Exists(script))
                {
                    # if DEBUG == false
                    try
                    {
                        Script(script);
                    }
                    catch (Exception ex)
                    {
                        Log.Write(LogLevel.Normal, "Internal exception.");
                        Log.Write(LogLevel.Normal, ex.ToString());
                    }
                    #else
                    Script(script);
                    #endif
                }
                else if (script != null)
                {
                    Log.Write(LogLevel.Normal, $"Could not find the file \"{script}\"");
                }
                else
                {
                    Log.Write(LogLevel.Normal, $"Drag and drop a script over the executable to parse.");
                    ConsoleLoop.Start();
                }

                Log.Write(LogLevel.Normal, "Done. Press enter to exit.");
                Console.ReadLine();
            }
        }

        static void Script(string parseFile)
        {
            string text = File.ReadAllText(parseFile);

            ParserData result = ParserData.GetParser(text, null);

            ParseLog.Write(LogLevel.Normal, new ColorMod("Build succeeded.", ConsoleColor.Green));

            // List all variables
            ParseLog.Write(LogLevel.Normal, new ColorMod("Variable Guide:", ConsoleColor.Blue));

            if (result.Root?.VarCollection().Count > 0)
            {
                int nameLength = result.Root.VarCollection().Max(v => v.Name.Length);

                bool other = false;
                foreach (DefinedVar var in result.Root.VarCollection())
                {
                    ConsoleColor textcolor = other ? ConsoleColor.White : ConsoleColor.DarkGray;
                    other = !other;

                    ParseLog.Write(LogLevel.Normal,
                        // Names
                        new ColorMod(var.Name + new string(' ', nameLength - var.Name.Length) + "  ", textcolor),
                        // Variable
                        new ColorMod(
                            (var.IsGlobal ? "global" : "player") 
                            + " " + 
                            var.Variable.ToString() +
                            (var.Index != -1 ? $"[{var.Index}]" : "")
                            , textcolor)
                    );
                }
            }

            string final = RuleArrayToWorkshop(result.Rules, result.VarCollection);

            Log.Write(LogLevel.Normal, "Press enter to copy code to clipboard, then in Overwatch click \"Paste Rule\".");
            Console.ReadLine();

            InputSim.SetClipboard(final);
        }

        public static string RuleArrayToWorkshop(Rule[] rules, VarCollection varCollection)
        {
            var builder = new StringBuilder();

            builder.AppendLine("// --- Variable Guide ---");

            foreach(var var in varCollection.AllVars)
                builder.AppendLine("// " + (var.IsGlobal ? "global" : "player") + " " + var.Variable + "[" + var.Index + "] " + var.Name);
            
            builder.AppendLine();

            Log debugPrintLog = new Log("Tree");
            foreach (var rule in rules)
            {
                rule.DebugPrint(debugPrintLog);
                builder.AppendLine(rule.ToWorkshop());
                builder.AppendLine();
            }
            return builder.ToString();
        }
    }
}
