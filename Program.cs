using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NL_text_representation.ComponentMorphologicalRepresentation;
using NL_text_representation.MatrixSemanticSyntacticRepresentation;

namespace NL_text_representation
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                StreamReader reader = new("E:\\Dimanchella_save\\МАИ\\Диплом\\Проекты\\NL_text_representation\\resources\\input3.txt", Encoding.UTF8);
                List<string> input = new(reader.ReadToEnd().Trim().Split('\n'));
                reader.Close();
                input = input.Select(str => str.Trim()).ToList();

                TermsSearcher termsSearcher = new();
                LexicalBasis lexicalBasis = new();

                foreach (string str in input)
                {
                    Console.WriteLine("\n################################\n"
                        + str
                        + "\n################################");
                    try
                    {
                        termsSearcher.FindCMR(str);
                        lexicalBasis.ProjectLinguisticBasis(termsSearcher.CMR);

                        var lsp = lexicalBasis.LSP;
                        var qrfp = lexicalBasis.QRFP;
                        var pfp = lexicalBasis.PFP;
                        var vpfp = lexicalBasis.VPFP;

                        Console.WriteLine("\n--------LexicalSemanticProjection--------");
                        lsp.ToList()
                            .ForEach(complect => complect.LSUs.ToList()
                                .ForEach(unit => Console.WriteLine(unit.ToString())));
                        Console.WriteLine("\n--------QuestionRoleFrameProjection--------");
                        qrfp.ToList()
                            .ForEach(complect => complect.QRFs.ToList()
                                .ForEach(frame => Console.WriteLine(frame.ToString())));
                        Console.WriteLine("\n--------PrepositionFrameProjection--------");
                        pfp.ToList()
                            .ForEach(complect => complect.PFs.ToList()
                                .ForEach(frame => Console.WriteLine(frame.ToString())));
                        Console.WriteLine("\n--------VerbPrepositionFrameProjection--------");
                        vpfp.ToList()
                            .ForEach(complect => complect.VPFs.ToList()
                                .ForEach(frame => Console.WriteLine(frame.ToString())));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
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
