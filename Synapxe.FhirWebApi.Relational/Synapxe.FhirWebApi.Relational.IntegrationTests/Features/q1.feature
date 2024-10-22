@Environment:Integration
Feature: q1

Background:
	Given a new HttpClient as httpClient
		| HeaderName               | Value   |
		| X-Ihis-SourceApplication | testapp |
	And a Resource is created from Samples/1.1_questionnaire.json with data as createdQuestionnaire
		| Path | Value                                  | FhirType |
		| url  | http://synapxe.sg/Questionnaire/{#uri} | uri      |

Scenario: Creating a new QuestionnaireResponse
	Given a Resource is created from Samples/1.2_questionnaireresponse.json with data as createdResponse
		| Path          | Value                                  | FhirType |
		| questionnaire | http://synapxe.sg/Questionnaire/{#uri} | uri      |
	Then createdResponse is a Fhir QuestionnaireResponse with data
		| Path       | Value |
		| statusCode | 201   |
