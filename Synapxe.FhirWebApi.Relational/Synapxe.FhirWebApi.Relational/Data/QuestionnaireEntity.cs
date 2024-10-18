// -------------------------------------------------------------------------------------------------
// Copyright (c) Integrated Health Information Systems Pte Ltd. All rights reserved.
// -------------------------------------------------------------------------------------------------

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Ihis.FhirEngine.Data.Models;

namespace Synapxe.FhirWebApi.Relational.Data;

[GeneratedFhirPoco(typeof(Questionnaire), "Conformance/AcpQuestionnaire.StructureDefinition.json")]
public partial class QuestionnaireEntity
{
    public partial class ItemComponent
    {
        public string? ParentLinkId { get; set; }
        //    public List<Item2Component> Item { get; set; } = [];

        //    [FhirType("Questionnaire#Item2", IsNestedType = true)]
        //    public class Item2Component : BackboneEntity
        //    {
        //        public string? LinkId { get; set; }
        //        public UriEntity? Definition { get; set; }
        //        public List<CodingEntity> Code { get; set; } = [];
        //        public string? Prefix { get; set; }
        //        public string? Text { get; set; }
        //        public string? Type { get; set; }
        //        public List<EnableWhenComponent> EnableWhen { get; set; } = [];
        //        public string? EnableBehavior { get; set; }
        //        public string? DisabledDisplay { get; set; }
        //        public bool? Required { get; set; }
        //        public bool? Repeats { get; set; }
        //        public bool? ReadOnly { get; set; }
        //        public int? MaxLength { get; set; }
        //        public string? AnswerConstraint { get; set; }
        //        public UriEntity? AnswerValueSet { get; set; }
        //        public List<AnswerOptionComponent> AnswerOption { get; set; } = [];
        //    }
    }
}
