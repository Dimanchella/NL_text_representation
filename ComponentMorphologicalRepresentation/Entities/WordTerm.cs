using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL_text_representation.ComponentMorphologicalRepresentation.Entities
{
    public class WordTerm
    {
        private readonly Term term;
        private readonly WordForm[] forms;
        private readonly string lexeme;
        private int mainFormIndex;

        public WordTerm(Term term, IEnumerable<WordForm> forms)
        {
            this.term = term;
            this.forms = forms.ToArray();
            lexeme = InitLexeme();
            mainFormIndex = SearchMainFormIndex();
        }

        private string InitLexeme()
        {
            StringBuilder sb = new();
            if (HasClass)
            {
                for (int i = 0; i < forms.Length && i < term.Components.Count(); i++)
                {
                    if (term.Components.ToList()[i].Lexeme.IndexOf('#') == 0
                        && term.Components.ToList()[i].Lexeme.LastIndexOf('#') == term.Components.ToList()[i].Lexeme.Length - 1)
                    {
                        sb.Append($"{forms[i].Lemma} ");
                    }
                    else
                    {
                        sb.Append($"{term.Components.ToList()[i].Lexeme} ");
                    }
                }
            }
            else
            {
                forms.ToList().ForEach(form => sb.Append($"{form.Lemma} "));
            }
            return sb.ToString().Trim();
        }
        private int SearchMainFormIndex()
        {
            if (HasClass)
            {
                for (int i = 0; i < forms.Length && i < term.Components.Count(); i++)
                {
                    if (term.Components.ToList()[i].IsMain)
                    {
                        return i;
                    }
                }
            }
            return forms.Length - 1;
        }

        public string Unit
        {
            get
            {
                StringBuilder sb = new();
                forms.ToList().ForEach(form => sb.Append($"{form.Word} "));
                return sb.ToString().Trim();
            }
        }
        public string Lexeme { get => lexeme; }
        public int Length { get => forms.Length; }
        public Term Term { get => term; }
        public WordForm Form { get => forms[mainFormIndex]; }
        public bool HasClass { get => term != null; }
        

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.Append($"{Unit} | {Lexeme} |");
            if (HasClass)
            {
                sb.Append($" {Term.PartOfSpeech} : {Term.SubClass} |");
            }
            else
            {
                sb.Append($" ? :  |");
            }
            Form.Traits.Grams.ToList().ForEach(gram => sb.Append($" {gram}"));
            return sb.ToString();
        }
    }
}
