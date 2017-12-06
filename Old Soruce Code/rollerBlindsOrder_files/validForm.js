////////////////////////////////////////////////////////////////////////////////////////////////////////
////
////	JQUERY FORM VALIDATOR
////	
////	AUTHOR: Joel Dawson
////	VERSION: 1.0
////
////	A plug-in script that handles the validation of a form
////
////	USAGE:
////
////	Include this file into the page and make sure that the form(s) has/have the following:
////	- The form(s) to be validated must have the class validForm
////	- A submit button with the class of validSubmitButton that is disabled (ensures that only users with
////	  javascript can use the form).
////	- There is a noscript tag that includes a description of needing javascript
////	- All required fields to have the class validFieldRequired (as well as any others they may need)
////	- If select boxes or radio buttons are to be considered not selected, they must have the value
////	  [NULL]. EG if a select box has an option that is considered not selected, its value must be [NULL]
////	- All fields that must have a valid email address must have the class validFieldEmail
////	- All fields that must only be numbers must have the class validFieldNumber 
////		- Fields with validFieldNumber can also have min[x] and max[x] classes to determine valid values
////	- All fields that must be web addresses (optional http://, subdomain, domain and top-level domain)
////	  must have the class validFieldWeb
////	- The fields have a title with their description (that will be alerted to the user if left empty)
////	- There must be a style called invalidFormItem which styles incorrect items
////
////	The validateForm function calls customValidation(errorStr) to allow users to add any custom validation
////
////////////////////////////////////////////////////////////////////////////////////////////////////////

$(document).ready(function()
{
	$(".validSubmitButton").attr("disabled",false);
	$(".validForm").submit(validateForm);
});


// Enter any additional form validation here.  This function is passed the
// error string after checking base required and types of fields.
// This function should return true if the elements are validated and
// and error string if not valid.
function customValidation(errorStr)
{
	return true;	
}


// This function performs the base form validation and calls customValidation.
// If no errors are found, it allows the form to submit, else it cancels
function validateForm()
{
	// Disable the button until the validator is checked
	$(".validSubmitButton").attr("disabled",true);
	
	// Remove the invalidField class from all elements
	$(".invalidFormItem").removeClass("invalidFormItem");
	
	// Set the error field to empty
	var errorStr = "";
	var invalidItems = [];
	var validForm = true;

	// Make sure all required fields have a value (that is not empty or [NULL])
	$(".validFieldRequired").each(function()
	{
		var value = $(this).attr("value");
		var title = $(this).attr("title");
		
		// If the value is appropriate, return
		if (value != "" && value != "[NULL]") { return true; }
		
		// Add the title to the error string
		errorStr += "\n" + title + " is required";
		validForm = false;
		invalidItems.push($(this));
	});
	
	
	// Make sure all email type fields are properly formed
	$(".validFieldEmail").each(function()
	{
		var value = $(this).attr("value");
		var title = $(this).attr("title");
		
		// Use RegEx to test for the email format and return if it is
		regEx = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
		if(regEx.test(value)) { return true; }
		
		// Add the title to the error string
		errorStr += "\n" + title + " is required and must be a proper email address";
		validForm = false;
		invalidItems.push($(this));
	});
	
	
	// Make sure all number type fields are properly formed
	$(".validFieldNumber").each(function()
	{
		var value = $(this).attr("value");
		var title = $(this).attr("title");
		
		// Use RegEx to test for the number format and return if it is
		regEx = /^(\s*\d+\s*)+$/;
		numCheck = regEx.test(value);
		
		// Check if the fields have min or max values and that the entered values meet the requisites
		fullClass = $(this).attr('class');
		
		// Isolate min and max values
		minVal = fullClass.search("min"); 
		if (minVal != -1) 
		{
			minValEnd = fullClass.search("]")+1; minVal = fullClass.substr(minVal, minValEnd-minVal); 
			fullClass = fullClass.replace(minVal,""); 
			minVal = minVal.substr(4); minVal = minVal.substr(0,minVal.length - 1);
		} else { 
			minVal = -1; 
		}
		
		maxVal = fullClass.search("max"); 
		if (maxVal != -1) 
		{
			maxValEnd = fullClass.search("]")+1; maxVal = fullClass.substr(maxVal, maxValEnd-maxVal);
			maxVal = maxVal.substr(4); maxVal = maxVal.substr(0,maxVal.length - 1);
		} else {
			maxVal = -1;
		}
		
		// Test that the value meets the min and max values
		if(minVal != -1) 
		{
			minCheck = (parseInt(value) >= parseInt(minVal)) ? true : false;
		} else { minCheck = true; }
		
		if(maxVal != -1) 
		{
			maxCheck = (parseInt(value) <= parseInt(maxVal)) ? true : false;
		} else { maxCheck = true; }		
		
		if(numCheck && minCheck && maxCheck) { return true; }
		
		// Add the title to the error string
		if (!minCheck && numCheck) { errorStr += "\n" + title + " is required and is less than the allowed minimum value"; }
		if (!maxCheck && numCheck) { errorStr += "\n" + title + " is required and is greater than the allowed maximum value"; }
		if (!numCheck) { errorStr += "\n" + title + " is required and must be numbers"; }
		
		validForm = false;
		invalidItems.push($(this));
	});
	
	
	// Make sure all web type fields are properly formed
	$(".validFieldWeb").each(function()
	{
		var value = $(this).attr("value");
		var title = $(this).attr("title");
		
		// Use RegEx to test for the web format and return if it is
		regEx = /^\s*(http\:\/\/)?([a-z\d\-]{1,63}]\.)*[a-z\d\-]{1,255}\.[a-z]{2,6}\s*$/;
		if(regEx.test(value)) { return true; }
		
		// Add the title to the error string
		errorStr += "\n" + title + " must be formatted as a web address";
		validForm = false;
		invalidItems.push($(this));
	});
	
	
	// Run a custom validator and add any custom error to the error string
	customError = customValidation(errorStr);
	if (customError != true) { validForm = false; errorStr += customError; }
	
	
	// If the form is valid, enable the submit button and continue
	if (validForm) { return true; }
	
	//// If the form is not valid, this code will run
	
	// Apply an invalid style to incorrect items
	jQuery.each(invalidItems, function()
	{
		this.addClass("invalidFormItem");
	});
	
	alert("There was a problem with submitting the form: \n\r" + errorStr);
	$(".validSubmitButton").attr("disabled",false);
	
	return false;
}