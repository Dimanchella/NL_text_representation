using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NL_text_representation.ComponentMorphologicalRepresentation.Entities;
using NL_text_representation.MatrixSemanticSyntacticRepresentation.Entities;

namespace NL_text_representation.MatrixSemanticSyntacticRepresentation
{
    // Basic-unit - возвращает базовую форму base из КМП по позиции этой лексемы в тексте
    // Entry-line - наименьший номер строки, лексема lec соответствует base
    // Change-form - ПРИМЕР: Change-form(Вес(z, 3/тонна)) => (Вес, 3/тонна), Change-form(Целевое-место-использования (z, нек кухня)) => Change-form((Целевое-место-использования, нек кухня))
    // Build-description - возвращает список семантических единиц прилагательных между позициями j и m
    // Find-sem-relation - сочетание (Сущ1, prep, Сущ2)

    public class SemanticParser
    {
        private List<Concept> concepts = new();

        public IEnumerable<Concept> Concepts { get => concepts; }

        public void OneNounParsing(IEnumerable<LSUComplect> lsp)
        {
            concepts.Clear();
            foreach (var lsuc in lsp)
            {
                List<LexicalSemanticUnit> adjectiveLSUs = new();
                foreach (var lsu in lsuc.LSUs)
                {
                    
                }
            }
        }
    }
}
