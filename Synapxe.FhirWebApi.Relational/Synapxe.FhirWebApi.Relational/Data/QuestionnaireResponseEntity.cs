// -------------------------------------------------------------------------------------------------
// Copyright (c) Integrated Health Information Systems Pte Ltd. All rights reserved.
// -------------------------------------------------------------------------------------------------

using Hl7.Fhir.Model;
using Ihis.FhirEngine.Data.Models;

namespace Synapxe.FhirWebApi.Relational.Data;

[GeneratedFhirPoco(typeof(QuestionnaireResponse), "Conformance/AcpQuestionnaireResponse.StructureDefinition.json")]
public partial class QuestionnaireResponseEntity
{
    public partial class ItemComponent
    {
        public string? ParentLinkId { get; set; }
    }
}