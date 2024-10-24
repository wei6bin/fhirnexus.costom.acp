@Environment:Integration
Feature: q2

Background:
	Given a new HttpClient as httpClient
		| HeaderName               | Value   |
		| X-Ihis-SourceApplication | testapp |
	And a Resource is created from Samples/1.1_q.json with data as createdQuestionnaire
		| Path | Value                                  | FhirType |
		| url  | http://synapxe.sg/Questionnaire/{#uri} | uri      |

Scenario: Creating a new QuestionnaireResponse and a new CarePlan with QuestionnaireResponse id linked at supportingInfo
	Given a Resource is created from Samples/1.2_a.json with data as createdResponse
		| Path          | Value                                  | FhirType |
		| questionnaire | http://synapxe.sg/Questionnaire/{#uri} | uri      |
	And a Resource is created from Samples/CarePlan.json with data as createdCarePlan
		| Path                        | Value                                      | FhirType |
		| supportingInfo[0].reference | QuestionnaireResponse/{createdResponse.Id} | uri      |
	Then createdResponse is a Fhir QuestionnaireResponse with data
		| Path       | Value |
		| statusCode | 201   |
	And createdCarePlan is a Fhir CarePlan with data
		| Path                        | Value                                      | FhirType |
		| statusCode                  | 201                                        |          |
		| supportingInfo[0].reference | QuestionnaireResponse/{createdResponse.Id} | uri      |
