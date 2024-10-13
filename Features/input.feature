Feature: Input Actions

Performing action with Input Element

Feature: Input Handling  
Scenario: Entering text in input fields
	Given I open edit playground	

	When I check the status of the full Name field
	When I check the status of the Append a text field
	When I check the status of the What is inside field
	When I check the status of the Clear field
	When I check the status of the Confirm edit field
	When I check the status of the Confirm text field

	When I enter Peppa Pig in the full Name field if it's enabled and not readonly
	When I enter No Value in the Append a text field if it's enabled and not readonly
	When I enter Text in the What is inside field if it's enabled and not readonly
	When I enter Cleared Value in the Clear field if it's enabled and not readonly
	When I enter Value 1 in the Confirm edit field if it's enabled and not readonly
	When I enter Value 2 in the Confirm text field if it's enabled and not readonly

	Then I verify that Peppa Pig is entered in the full Name field if it was editable or NOT readonly
	Then I verify that No Value is entered in the Append a text field if it was editable or NOT readonly
	Then I verify that Text is entered in the What is inside field if it was editable or NOT readonly
	Then I verify that Cleared Value is entered in the Clear field if it was editable or NOT readonly

	When I clear the existing value from the full Name field if it's enabled and not readonly
	When I clear the existing value from the Append a text field if it's enabled and not readonly
	When I clear the existing value from the What is inside field if it's enabled and not readonly
	When I clear the existing value from the Clear field if it's enabled and not readonly
	When I clear the existing value from the Confirm edit field if it's enabled and not readonly
	When I clear the existing value from the Confirm text field if it's enabled and not readonly