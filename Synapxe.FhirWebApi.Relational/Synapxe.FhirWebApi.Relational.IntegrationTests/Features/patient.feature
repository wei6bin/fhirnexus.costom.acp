@Environment:Integration
Feature: Patient

Background:
	Given a new HttpClient as httpClient
		| HeaderName               | Value   |
		| X-Ihis-SourceApplication | testapp |
	
Scenario: Create a new Patient successfully
	Given a Resource is created from Samples/Patient.json as createdPatient
	Then createdPatient is a Fhir Patient with data
		| Path       | Value |
		| statusCode | 201   |
