using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LinguisticDatabase
{
    public partial class Linguistic_DatabaseContext : DbContext
    {
        public Linguistic_DatabaseContext()
        {
        }

        public Linguistic_DatabaseContext(DbContextOptions<Linguistic_DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Lexeme> Lexemes { get; set; }
        public virtual DbSet<LinksToKnowledgeBase> LinksToKnowledgeBases { get; set; }
        public virtual DbSet<Meaning> Meanings { get; set; }
        public virtual DbSet<MeaningLimit> MeaningLimits { get; set; }
        public virtual DbSet<MeaningType> MeaningTypes { get; set; }
        public virtual DbSet<MorphologicalTrait> MorphologicalTraits { get; set; }
        public virtual DbSet<MorphologicalTraitSet> MorphologicalTraitSets { get; set; }
        public virtual DbSet<MorphologicalTraitType> MorphologicalTraitTypes { get; set; }
        public virtual DbSet<ParamsKrepr> ParamsKreprs { get; set; }
        public virtual DbSet<ParamsKreprToOntology> ParamsKreprToOntologies { get; set; }
        public virtual DbSet<ParamsOntology> ParamsOntologies { get; set; }
        public virtual DbSet<PrepositionFrame> PrepositionFrames { get; set; }
        public virtual DbSet<QuestionRoleFrame> QuestionRoleFrames { get; set; }
        public virtual DbSet<Term> Terms { get; set; }
        public virtual DbSet<TermAddMeaning> TermAddMeanings { get; set; }
        public virtual DbSet<TermComponent> TermComponents { get; set; }
        public virtual DbSet<TermMainMeaning> TermMainMeanings { get; set; }
        public virtual DbSet<TermMeaning> TermMeanings { get; set; }
        public virtual DbSet<VerbPrepositionFrame> VerbPrepositionFrames { get; set; }
        public virtual DbSet<Word> Words { get; set; }
        public virtual DbSet<WordForm> WordForms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=10.243.57.89;Port=5432;Database=Linguistic_Database;Username=dima;Password=dima");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "ru_RU.UTF-8");

            modelBuilder.Entity<Lexeme>(entity =>
            {
                entity.HasKey(e => e.IdLexeme)
                    .HasName("Lexemes_pkey");

                entity.HasIndex(e => e.Lexeme1, "Lexemes_Lexeme_key")
                    .IsUnique();

                entity.Property(e => e.IdLexeme)
                    .HasColumnName("ID_lexeme")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Lexeme1)
                    .IsRequired()
                    .HasColumnName("Lexeme");
            });

            modelBuilder.Entity<LinksToKnowledgeBase>(entity =>
            {
                entity.HasKey(e => e.IdLink)
                    .HasName("Links_to_KnowledgeBase_pkey");

                entity.ToTable("Links_to_KnowledgeBase");

                entity.Property(e => e.IdLink)
                    .HasColumnName("ID_link")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdTerm).HasColumnName("ID_term");

                entity.Property(e => e.KbReference).HasColumnName("KB_reference");

                entity.HasOne(d => d.IdTermNavigation)
                    .WithMany(p => p.LinksToKnowledgeBases)
                    .HasForeignKey(d => d.IdTerm)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Term");
            });

            modelBuilder.Entity<Meaning>(entity =>
            {
                entity.HasKey(e => e.IdMeaning)
                    .HasName("Meanings_pkey");

                entity.Property(e => e.IdMeaning)
                    .HasColumnName("ID_meaning")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdType).HasColumnName("ID_type");

                entity.Property(e => e.Meaning1)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("Meaning");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.Meanings)
                    .HasForeignKey(d => d.IdType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Type");
            });

            modelBuilder.Entity<MeaningLimit>(entity =>
            {
                entity.HasKey(e => e.IdLimit)
                    .HasName("Meaning_limits_pkey");

                entity.ToTable("Meaning_limits");

                entity.Property(e => e.IdLimit)
                    .HasColumnName("ID_limit")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdFrame).HasColumnName("ID_frame");

                entity.Property(e => e.IdMeaningAdd).HasColumnName("ID_meaning_add");

                entity.HasOne(d => d.IdFrameNavigation)
                    .WithMany(p => p.MeaningLimits)
                    .HasForeignKey(d => d.IdFrame)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Frame");

                entity.HasOne(d => d.IdMeaningAddNavigation)
                    .WithMany(p => p.MeaningLimits)
                    .HasForeignKey(d => d.IdMeaningAdd)
                    .HasConstraintName("Meaning_add");
            });

            modelBuilder.Entity<MeaningType>(entity =>
            {
                entity.HasKey(e => e.IdType)
                    .HasName("Meaning_types_pkey");

                entity.ToTable("Meaning_types");

                entity.Property(e => e.IdType)
                    .HasColumnName("ID_type")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<MorphologicalTrait>(entity =>
            {
                entity.HasKey(e => e.IdTrait)
                    .HasName("Morphological_traits_pkey");

                entity.ToTable("Morphological_traits");

                entity.Property(e => e.IdTrait)
                    .HasColumnName("ID_trait")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdTraitType).HasColumnName("ID_trait_type");

                entity.Property(e => e.Trait)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.IdTraitTypeNavigation)
                    .WithMany(p => p.MorphologicalTraits)
                    .HasForeignKey(d => d.IdTraitType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Trait_type");
            });

            modelBuilder.Entity<MorphologicalTraitSet>(entity =>
            {
                entity.HasKey(e => e.IdSetLine)
                    .HasName("Morphological_trait_sets_pkey");

                entity.ToTable("Morphological_trait_sets");

                entity.Property(e => e.IdSetLine)
                    .HasColumnName("ID_set_line")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdTrait).HasColumnName("ID_trait");

                entity.Property(e => e.IdTraitType).HasColumnName("ID_trait_type");

                entity.Property(e => e.IdWordForm).HasColumnName("ID_word_form");

                entity.HasOne(d => d.IdTraitNavigation)
                    .WithMany(p => p.MorphologicalTraitSets)
                    .HasForeignKey(d => d.IdTrait)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Trait_type");

                entity.HasOne(d => d.IdTraitTypeNavigation)
                    .WithMany(p => p.MorphologicalTraitSets)
                    .HasForeignKey(d => d.IdTraitType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Trait");

                entity.HasOne(d => d.IdWordFormNavigation)
                    .WithMany(p => p.MorphologicalTraitSets)
                    .HasForeignKey(d => d.IdWordForm)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Word_form");
            });

            modelBuilder.Entity<MorphologicalTraitType>(entity =>
            {
                entity.HasKey(e => e.IdTraitType)
                    .HasName("Morphological_trait_types_pkey");

                entity.ToTable("Morphological_trait_types");

                entity.Property(e => e.IdTraitType)
                    .HasColumnName("ID_trait_type")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.TraitType)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("Trait_type");
            });

            modelBuilder.Entity<ParamsKrepr>(entity =>
            {
                entity.HasKey(e => e.IdParam)
                    .HasName("ParamsKrepr_pkey");

                entity.ToTable("ParamsKrepr");

                entity.Property(e => e.IdParam)
                    .HasColumnName("id_param")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Param)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("param");
            });

            modelBuilder.Entity<ParamsKreprToOntology>(entity =>
            {
                entity.HasKey(e => e.IdMatch)
                    .HasName("ParamsKreprToOntology_pkey");

                entity.ToTable("ParamsKreprToOntology");

                entity.Property(e => e.IdMatch)
                    .HasColumnName("id_match")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdKreprParam).HasColumnName("id_Krepr_param");

                entity.Property(e => e.IdOntologyParam).HasColumnName("id_Ontology_param");
            });

            modelBuilder.Entity<ParamsOntology>(entity =>
            {
                entity.HasKey(e => e.IdParam)
                    .HasName("ParamsOntology_pkey");

                entity.ToTable("ParamsOntology");

                entity.Property(e => e.IdParam)
                    .HasColumnName("id_param")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Param)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("param");
            });

            modelBuilder.Entity<PrepositionFrame>(entity =>
            {
                entity.HasKey(e => e.IdFrame)
                    .HasName("Preposition_frames_pkey");

                entity.ToTable("Preposition_frames");

                entity.Property(e => e.IdFrame)
                    .HasColumnName("ID_frame")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdMeaningAddNoun1).HasColumnName("ID_meaning_add_noun_1");

                entity.Property(e => e.IdMeaningAddNoun2).HasColumnName("ID_meaning_add_noun_2");

                entity.Property(e => e.IdMeaningFrame).HasColumnName("ID_meaning_frame");

                entity.Property(e => e.IdTermPreposition).HasColumnName("ID_term_preposition");

                entity.Property(e => e.IdTraitCase2).HasColumnName("ID_trait_case_2");

                entity.HasOne(d => d.IdMeaningAddNoun1Navigation)
                    .WithMany(p => p.PrepositionFrameIdMeaningAddNoun1Navigations)
                    .HasForeignKey(d => d.IdMeaningAddNoun1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Meaning_add_noun_1");

                entity.HasOne(d => d.IdMeaningAddNoun2Navigation)
                    .WithMany(p => p.PrepositionFrameIdMeaningAddNoun2Navigations)
                    .HasForeignKey(d => d.IdMeaningAddNoun2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Meaning_add_noun_2");

                entity.HasOne(d => d.IdMeaningFrameNavigation)
                    .WithMany(p => p.PrepositionFrameIdMeaningFrameNavigations)
                    .HasForeignKey(d => d.IdMeaningFrame)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Meaning_frame");

                entity.HasOne(d => d.IdTermPrepositionNavigation)
                    .WithMany(p => p.PrepositionFrames)
                    .HasForeignKey(d => d.IdTermPreposition)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Term_preposition");

                entity.HasOne(d => d.IdTraitCase2Navigation)
                    .WithMany(p => p.PrepositionFrames)
                    .HasForeignKey(d => d.IdTraitCase2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Trait_case_2");
            });

            modelBuilder.Entity<QuestionRoleFrame>(entity =>
            {
                entity.HasKey(e => e.IdFrame)
                    .HasName("Question_role_frames_pkey");

                entity.ToTable("Question_role_frames");

                entity.Property(e => e.IdFrame)
                    .HasColumnName("ID_frame")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdMeaningFrame).HasColumnName("ID_meaning_frame");

                entity.Property(e => e.IdTermPreposition).HasColumnName("ID_term_preposition");

                entity.Property(e => e.IdTermPronounInterrogativeRelativeAdverb).HasColumnName("ID_term_pronoun_interrogative_relative_adverb");

                entity.HasOne(d => d.IdMeaningFrameNavigation)
                    .WithMany(p => p.QuestionRoleFrames)
                    .HasForeignKey(d => d.IdMeaningFrame)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Meaning_frame");

                entity.HasOne(d => d.IdTermPrepositionNavigation)
                    .WithMany(p => p.QuestionRoleFrameIdTermPrepositionNavigations)
                    .HasForeignKey(d => d.IdTermPreposition)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Term_preposition");

                entity.HasOne(d => d.IdTermPronounInterrogativeRelativeAdverbNavigation)
                    .WithMany(p => p.QuestionRoleFrameIdTermPronounInterrogativeRelativeAdverbNavigations)
                    .HasForeignKey(d => d.IdTermPronounInterrogativeRelativeAdverb)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Term_pronoun_interrogative_relative_adverb");
            });

            modelBuilder.Entity<Term>(entity =>
            {
                entity.HasKey(e => e.IdTerm)
                    .HasName("Terms_pkey");

                entity.Property(e => e.IdTerm)
                    .HasColumnName("ID_term")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdTraitPartOfSpeech).HasColumnName("ID_trait_part_of_speech");

                entity.Property(e => e.IdTraitSubclass).HasColumnName("ID_trait_subclass");

                entity.HasOne(d => d.IdTraitPartOfSpeechNavigation)
                    .WithMany(p => p.TermIdTraitPartOfSpeechNavigations)
                    .HasForeignKey(d => d.IdTraitPartOfSpeech)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Trait_part_of_speech");

                entity.HasOne(d => d.IdTraitSubclassNavigation)
                    .WithMany(p => p.TermIdTraitSubclassNavigations)
                    .HasForeignKey(d => d.IdTraitSubclass)
                    .HasConstraintName("Trait_subclass");
            });

            modelBuilder.Entity<TermAddMeaning>(entity =>
            {
                entity.HasKey(e => e.IdTermAddMeaning)
                    .HasName("Term_add_meanings_pkey");

                entity.ToTable("Term_add_meanings");

                entity.Property(e => e.IdTermAddMeaning)
                    .HasColumnName("ID_term_add_meaning")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdMeaningAdd).HasColumnName("ID_meaning_add");

                entity.Property(e => e.IdTermMeainMeaning).HasColumnName("ID_term_meain_meaning");

                entity.HasOne(d => d.IdMeaningAddNavigation)
                    .WithMany(p => p.TermAddMeanings)
                    .HasForeignKey(d => d.IdMeaningAdd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Meaning_add");

                entity.HasOne(d => d.IdTermMeainMeaningNavigation)
                    .WithMany(p => p.TermAddMeanings)
                    .HasForeignKey(d => d.IdTermMeainMeaning)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Term_main_meaning");
            });

            modelBuilder.Entity<TermComponent>(entity =>
            {
                entity.HasKey(e => e.IdComponent)
                    .HasName("Term_components_pkey");

                entity.ToTable("Term_components");

                entity.Property(e => e.IdComponent)
                    .HasColumnName("ID_component")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdLexeme).HasColumnName("ID_lexeme");

                entity.Property(e => e.IdTerm).HasColumnName("ID_term");

                entity.Property(e => e.IsMainLexeme)
                    .IsRequired()
                    .HasColumnName("Is_main_lexeme")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.PositionLexeme)
                    .HasColumnName("Position_lexeme")
                    .HasDefaultValueSql("1");

                entity.HasOne(d => d.IdLexemeNavigation)
                    .WithMany(p => p.TermComponents)
                    .HasForeignKey(d => d.IdLexeme)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Lexeme");

                entity.HasOne(d => d.IdTermNavigation)
                    .WithMany(p => p.TermComponents)
                    .HasForeignKey(d => d.IdTerm)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Trem");
            });

            modelBuilder.Entity<TermMainMeaning>(entity =>
            {
                entity.HasKey(e => e.IdTermMainMeaning)
                    .HasName("Term_main_meanings_pkey");

                entity.ToTable("Term_main_meanings");

                entity.Property(e => e.IdTermMainMeaning)
                    .HasColumnName("ID_term_main_meaning")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdMeaningMain).HasColumnName("ID_meaning_main");

                entity.Property(e => e.IdTerm).HasColumnName("ID_term");

                entity.HasOne(d => d.IdMeaningMainNavigation)
                    .WithMany(p => p.TermMainMeanings)
                    .HasForeignKey(d => d.IdMeaningMain)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Meaning_main");

                entity.HasOne(d => d.IdTermNavigation)
                    .WithMany(p => p.TermMainMeanings)
                    .HasForeignKey(d => d.IdTerm)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Term");
            });

            modelBuilder.Entity<TermMeaning>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TermMeaning");

                entity.Property(e => e.IdMeaningMain).HasColumnName("ID_meaning_main");

                entity.Property(e => e.IdTerm).HasColumnName("ID_term");

                entity.Property(e => e.Meaning).HasMaxLength(256);
            });

            modelBuilder.Entity<VerbPrepositionFrame>(entity =>
            {
                entity.HasKey(e => e.IdFrame)
                    .HasName("Verb_preposition_frames_pkey");

                entity.ToTable("Verb_preposition_frames");

                entity.Property(e => e.IdFrame)
                    .HasColumnName("ID_frame")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdMeaningFrame).HasColumnName("ID_meaning_frame");

                entity.Property(e => e.IdMeaningSituation).HasColumnName("ID_meaning_situation");

                entity.Property(e => e.IdTermPreposition).HasColumnName("ID_term_preposition");

                entity.Property(e => e.IdTraitCase).HasColumnName("ID_trait_case");

                entity.Property(e => e.IdTraitVerbForm).HasColumnName("ID_trait_verb_form");

                entity.Property(e => e.IdTraitVerbReflect).HasColumnName("ID_trait_verb_reflect");

                entity.Property(e => e.IdTraitVerbVoice).HasColumnName("ID_trait_verb_voice");

                entity.HasOne(d => d.IdMeaningFrameNavigation)
                    .WithMany(p => p.VerbPrepositionFrameIdMeaningFrameNavigations)
                    .HasForeignKey(d => d.IdMeaningFrame)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Meaning_frame");

                entity.HasOne(d => d.IdMeaningSituationNavigation)
                    .WithMany(p => p.VerbPrepositionFrameIdMeaningSituationNavigations)
                    .HasForeignKey(d => d.IdMeaningSituation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Meaning_situation");

                entity.HasOne(d => d.IdTermPrepositionNavigation)
                    .WithMany(p => p.VerbPrepositionFrames)
                    .HasForeignKey(d => d.IdTermPreposition)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("	 Term_preposition");

                entity.HasOne(d => d.IdTraitCaseNavigation)
                    .WithMany(p => p.VerbPrepositionFrameIdTraitCaseNavigations)
                    .HasForeignKey(d => d.IdTraitCase)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Trait_case");

                entity.HasOne(d => d.IdTraitVerbFormNavigation)
                    .WithMany(p => p.VerbPrepositionFrameIdTraitVerbFormNavigations)
                    .HasForeignKey(d => d.IdTraitVerbForm)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Trait_verb_form");

                entity.HasOne(d => d.IdTraitVerbReflectNavigation)
                    .WithMany(p => p.VerbPrepositionFrameIdTraitVerbReflectNavigations)
                    .HasForeignKey(d => d.IdTraitVerbReflect)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Trait_verb_reflect");

                entity.HasOne(d => d.IdTraitVerbVoiceNavigation)
                    .WithMany(p => p.VerbPrepositionFrameIdTraitVerbVoiceNavigations)
                    .HasForeignKey(d => d.IdTraitVerbVoice)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Trait_verb_voice");
            });

            modelBuilder.Entity<Word>(entity =>
            {
                entity.HasKey(e => e.IdWord)
                    .HasName("Words_pkey");

                entity.HasIndex(e => e.Word1, "Words_Word_key")
                    .IsUnique();

                entity.Property(e => e.IdWord)
                    .HasColumnName("ID_word")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Word1)
                    .IsRequired()
                    .HasColumnName("Word");
            });

            modelBuilder.Entity<WordForm>(entity =>
            {
                entity.HasKey(e => e.IdWordForm)
                    .HasName("Word_forms_pkey");

                entity.ToTable("Word_forms");

                entity.Property(e => e.IdWordForm)
                    .HasColumnName("ID_word_form")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdLexeme).HasColumnName("ID_lexeme");

                entity.Property(e => e.IdWord).HasColumnName("ID_word");

                entity.HasOne(d => d.IdLexemeNavigation)
                    .WithMany(p => p.WordForms)
                    .HasForeignKey(d => d.IdLexeme)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Lexeme");

                entity.HasOne(d => d.IdWordNavigation)
                    .WithMany(p => p.WordForms)
                    .HasForeignKey(d => d.IdWord)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Word");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
