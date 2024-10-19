@Environment:Integration
Feature: Questionnaire

Background:
	Given a new HttpClient as httpClient
		| HeaderName               | Value   |
		| X-Ihis-SourceApplication | testapp |

Scenario: Reading a newly created Questionnaire returns the same Questionnaire
	Given a Resource is created from Samples/form-general-simple-version.json as createdResource
	When getting Questionnaire/{createdResource.Id} as readResource
	Then createdResource is a Fhir Questionnaire with data
		| Path       | Value |
		| statusCode | 201   |
	And createdResource and readResource are exactly the same

Scenario: Reading a newly created general form Questionnaire returns the same Questionnaire
	Given a Resource is created from Samples/form-general-question-nested-compare-1.json as createdResource
	When getting Questionnaire/{createdResource.Id} as readResource
	Then createdResource is a Fhir Questionnaire with data
		| Path       | Value |
		| statusCode | 201   |
	And createdResource and readResource are exactly the same

Scenario: Search a newly created general form Questionnaire by url, returns the bundle contains a single Questionnaire
	Given a Resource is created from Samples/form-general-simple-version.json with data as createdResource
		| Path | Value                                  | FhirType |
		| url  | http://synapxe.sg/Questionnaire/{#uri} | uri      |
	When getting Questionnaire?url=http://synapxe.sg/Questionnaire/{#uri}&_total=accurate&_sort=_lastUpdated as searchBundle
	Then searchBundle is a Fhir Bundle which contains createdResource
	Then searchBundle is a Fhir Bundle with data
		| Path       | Value | FhirType |
		| statusCode | 200   |          |
		| total      | 1     | int      |

