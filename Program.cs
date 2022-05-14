﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NL_text_representation.ComponentMorphologicalRepresentation;
using NL_text_representation.SemBuilding;

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

                TermsSearcher termsAnalizer = new();

                foreach (string str in input)
                {
                    termsAnalizer.FindCMR(str);
                    var wordReps = termsAnalizer.CMR;
                    wordReps.ToList()
                        .ForEach(wordRep => wordRep.CMUs.ToList()
                            .ForEach(cmr => Console.WriteLine(cmr.ToString())));

                    SemBuilder b = new SemBuilder();

                    Console.WriteLine("\n" + b.getSemReprRequest(str));
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
