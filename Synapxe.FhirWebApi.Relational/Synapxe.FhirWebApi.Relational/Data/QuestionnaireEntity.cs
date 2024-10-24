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
    }
}
