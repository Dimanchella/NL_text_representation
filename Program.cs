using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NL_text_representation.ComponentMorphologicalRepresentation;

namespace NL_text_representation
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                StreamReader reader = new("input.txt", Encoding.UTF8);
                List<string> input = new(reader.ReadToEnd().Trim().Split('\n'));
                reader.Close();
                input = input.Select(str => str.Trim()).ToList();

                TermsAnalizer termsAnalizer = new();

                foreach (string str in input)
                {
                    termsAnalizer.Text = str;
                    var wordReps = termsAnalizer.GetCMR();
                    wordReps.ToList()
                        .ForEach(wordRep => wordRep.GetCMRs().ToList()
                            .ForEach(cmr => Console.WriteLine(cmr.ToString())));
                    Console.WriteLine("\n****************************************************\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
